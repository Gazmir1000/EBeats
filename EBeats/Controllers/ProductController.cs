﻿using EBeats.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBeats.Controllers
{
    public class ProductController : Controller
    {

        private readonly EBeatsContext _context;

        public ProductController(EBeatsContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            if (products!=null)
            {
                return View(products);
            }
            return View();
           
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return View(product);
        }
    }
}
