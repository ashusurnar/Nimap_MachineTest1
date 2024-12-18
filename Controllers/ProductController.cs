using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nimap_MachineTest1.DAL;
using Nimap_MachineTest1.Models;

namespace Nimap_MachineTest1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationContext context;

        public ProductController(ApplicationContext context)
        {
            this.context=context;
        }

        //public ActionResult Index(int page = 1, int pageSize = 10)
        //{
        //    var products = context.products.Include(p => p.Category)
        //                               .OrderBy(p => p.ProductId)
        //                               .Skip((page - 1) * pageSize)
        //                               .Take(pageSize)
        //                               .ToList();

        //    var totalRecords = context.products.Count();
        //    ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        //    ViewBag.CurrentPage = page;

        //    return View(products);
        //}

        public IActionResult Index()
        {


            var data = (from p in context.products
                        join c in context.categories
                        on p.CategoryId equals c.CategoryId
                        select new ProductCategoryModel
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            CategoryId = c.CategoryId,
                            CategoryName = c.CategoryName,
                        }).ToList();

            return View(data);



        }

        [HttpGet]
         
        public ActionResult Create()
        {
             

            return View( );
        }

        [HttpPost]
        public ActionResult Create(ProductCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                // Map ViewModel to Entity
                var product = new Product
                {
                    ProductName = model.ProductName,
                    CategoryName=model.CategoryName,
                    CategoryId = model.CategoryId // Foreign Key
                };

                // Add and Save to Database
                context.products.Add(product);
                context.SaveChanges();

                return RedirectToAction("Index"); // Redirect to Product List
            }
 
           

            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var ct = context.products.SingleOrDefault(c => c.ProductId == id);
            var result = new Product()
            {
                ProductId = ct.ProductId,
                ProductName = ct.ProductName,
                CategoryId = ct.CategoryId,
                CategoryName= ct.CategoryName,
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var pt = new Product()
            {
                ProductId= product.ProductId,
                ProductName = product.ProductName,
                CategoryName = product.CategoryName,
                CategoryId = product.CategoryId,

               
            };
            context.products.Update(pt);
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

