using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nimap_MachineTest1.DAL;
using Nimap_MachineTest1.Models;

namespace Nimap_MachineTest1.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationContext context;
        public CategoryController(ApplicationContext context)
        {
            this.context = context;

        }

        public IActionResult Index()
        {
            var result = context.categories.ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var cat = new Category()
                {
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                };
                context.categories.Add(cat);
                context.SaveChanges();
                return RedirectToAction("Index");


            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var ct = context.categories.SingleOrDefault(c => c.CategoryId == id);
            var result = new Category()
            {
                CategoryId = ct.CategoryId,
                CategoryName = ct.CategoryName,
            };
            return View(ct);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var ct = new Category()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };
            context.categories.Update(ct);
            context.SaveChanges();
            return RedirectToAction("Index");


        }

        public IActionResult Delete(int id)
        {
            var cat = context.categories.SingleOrDefault(ct => ct.CategoryId == id);
            context.categories.Remove(cat);
            context.SaveChanges();

            return RedirectToAction("Index");


        }


    }
}

