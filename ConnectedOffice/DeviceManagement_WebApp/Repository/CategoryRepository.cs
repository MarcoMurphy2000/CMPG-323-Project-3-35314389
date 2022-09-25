using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement_WebApp.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        // Get Most Recent Category: Retrieve the category that was created last.
        public Category GetMostRecentCategory()
        {
            return _context.Category.OrderByDescending(category => category.DateCreated).FirstOrDefault();
        }

        //GET ALL Categories
        public List<Category> GetAll()
        {
            return _context.Category.ToList();
        }

        //Get Category by ID
        public async Task<Category> Details(Guid? id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(category => category.CategoryId == id);
            return (category);
        }



        // GET: Create Categories
        public IActionResult Create()
        {
            return (IActionResult)_context.Category.ToList();
        }

        // POST: Create Categories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();
            _context.Add(category);
            await _context.SaveChangesAsync();
            return (IActionResult)category;
        }

        // GET: Edit Categories
        public async Task<IActionResult> Edit(Guid? id)
        {
            var category = await _context.Category.FindAsync(id);
            return ((IActionResult)category);
        }

        // POST: Edit Categories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();

            return (IActionResult)_context.Category.ToList();
        }

        // GET: Delete Categories
        public async Task<IActionResult> Delete(Guid? id)
        {
            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            return (IActionResult)category;
        }

        // POST: Delete Categories
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return (IActionResult)_context.Category.ToList();
        }

        //Exists
        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }

}
