using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDemo.DatabaseContext;
using APIDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Controllers
{
    public class AddCustomerController : Controller
    {
        // DatabaseContext db= new APIDemo.DatabaseContext

        DataContext db = new DataContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            if(ModelState.IsValid)
            {
                db.Add(customer);
               bool Isadd = db.SaveChanges() > 0;

                if(Isadd)
                {
                  return  RedirectToAction("Index");
                }
            }
            return  View();
        }


        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
           
        }
        [HttpPost]
        public IActionResult Edit(int id, Customer customer)
        {
            //if (id != customer.customerId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(customer);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.customerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Any(e => e.customerId == id);
        }


    }
}