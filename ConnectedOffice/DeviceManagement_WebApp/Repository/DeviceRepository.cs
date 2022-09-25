using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement_WebApp.Repository
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        // Get Most Recent Device: Retrieve the device that was created last.
        public Device GetMostRecentDevice()
        {
            return _context.Device.OrderByDescending(device => device.DateCreated).FirstOrDefault();
        }

        //GET ALL Devices
        public List<Device> GetAll()
        {
            return _context.Device.ToList();
        }



        // GET Devices by ID
        public async Task<IActionResult> Details(Guid? id)
        {
            var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);

            return (IActionResult)device;
        }

        // GET: Create Devices
        public IActionResult Create()
        {
            return (IActionResult)_context.Device.ToList();
        }

        // POST: Create Devices
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();
            _context.Add(device);
            await _context.SaveChangesAsync();
            return (IActionResult)device;


        }

        // GET: Edit Devices
        public async Task<IActionResult> Edit(Guid? id)
        {
            var device = await _context.Device.FindAsync(id);

            return (IActionResult)_context.Zone.ToList();
        }

        // POST: Edit Devices
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            _context.Update(device);
            await _context.SaveChangesAsync();


            return (IActionResult)_context.Zone.ToList();
        }

        // GET: Delete Devices
        public async Task<IActionResult> Delete(Guid? id)
        {
            var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);

            return (IActionResult)device;
        }

        // POST: Delete Devices
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var device = await _context.Device.FindAsync(id);
            _context.Device.Remove(device);
            await _context.SaveChangesAsync();
            return (IActionResult)_context.Zone.ToList();
        }

        //Exists
        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }

    }

}
