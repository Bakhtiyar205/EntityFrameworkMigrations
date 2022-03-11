using EntityFramework.Data;
using EntityFramework.Models;
using EntityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderDetail details = await _context.SliderDetails.FirstOrDefaultAsync();
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Product> products = await _context.Products
                .Include(m => m.Category)
                .Include(m => m.Images)
                .OrderByDescending(m => m.Id)
                .Skip(1)
                .Take(8)
                .ToListAsync();


            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Detail = details,
                Categories = categories,
                Products = products
            };
            return View(homeVM);
        }
    }
}
