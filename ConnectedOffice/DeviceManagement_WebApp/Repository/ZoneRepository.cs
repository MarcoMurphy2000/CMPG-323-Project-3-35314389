using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public class ZoneRepository : GenericRepository<Zone>, IZoneRepository
    {
        public ZoneRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        // Get Most Recent Zone: Retrieve the zone that was created last.
        public Zone GetMostRecentZone()
        {
            return _context.Zone.OrderByDescending(zone => zone.DateCreated).FirstOrDefault();
        }

        //GET ALL Zones
        public List<Zone> GetAll()
        {
            return _context.Zone.ToList();
        }

        //Get Zone by ID
        public async Task<Zone> Details(Guid? id)
        {
            var zone = await  _context.Zone.FirstOrDefaultAsync(zone => zone.ZoneId == id);
            return (zone);
        }

        // GET: Create Zones
        public IActionResult Create()
        {
            return (IActionResult)_context.Zone.ToList();
        }

        // POST: Create Zones
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone, Index index)
        {
            zone.ZoneId = Guid.NewGuid();
            _context.Add(zone);
            await _context.SaveChangesAsync();
            return (IActionResult)zone;
        }


        // GET: Edit Zones
        public async Task<IActionResult> Edit(Guid? id)
        {
            var zone = await _context.Zone.FindAsync(id);

            return (IActionResult)_context.Zone.ToList();
        }

        // POST: Edit Zones
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            _context.Update(zone);
            await _context.SaveChangesAsync();
            
            return (IActionResult)_context.Zone.ToList();
        }


        // GET: Delete Zones
        public async Task<IActionResult> Delete(Guid? id)
        {
            var zone = await _context.Zone
                .FirstOrDefaultAsync(m => m.ZoneId == id);

            return (IActionResult)zone;
        }

        // POST: Delete Zones
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = await _context.Zone.FindAsync(id);
            _context.Zone.Remove(zone);
            await _context.SaveChangesAsync();
            return (IActionResult)_context.Zone.ToList();
        }

        //Exist
        private bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
        }

    }

}
