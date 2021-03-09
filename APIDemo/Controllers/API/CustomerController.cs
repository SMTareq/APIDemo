using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using APIDemo.DatabaseContext;
using APIDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Controllers.API
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        DataContext db = new DataContext();

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return db.Customers.ToList();
        }


        [HttpGet("{id}")]
        public Customer Get(int id)
        {

            return db.Customers.Find(id);

           
        }

        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            if(ModelState.IsValid)
            {
                db.Customers.Add(customer);
                bool IsAdd = db.SaveChanges() > 0;
                if(IsAdd)
                {
                    return Ok(customer);
                }
            }

            return BadRequest(ModelState);
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Customer customer)
        {
            var existingCUstomer = db.Customers.Find(id);

            if(existingCUstomer == null)
            {
                return BadRequest(new { ErrorMessage = "No Product Found!" });
            }


            if(ModelState.IsValid)
            {
                try
                {
                    existingCUstomer.customerName = customer.customerName;
                    existingCUstomer.customerAddress = customer.customerAddress;
                    existingCUstomer.email = customer.email;
                    existingCUstomer.phone = customer.phone;

                    db.Customers.Update(customer);
                    bool IsUpdate = db.SaveChanges() > 0;
                    if (IsUpdate)
                    {
                        return Ok();
                    }
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

            }

            //if (ModelState.IsValid)
            //{

            //    existingCUstomer.customerName = customer.customerName;
            //    existingCUstomer.customerAddress = customer.customerAddress;
            //    existingCUstomer.email = customer.email;
            //    existingCUstomer.phone = customer.phone;

            //    db.Customers.Update(customer);

            //    bool IsUpdate = db.SaveChanges() > 0;
            //    if(IsUpdate)
            //    {
            //        return Ok();
            //    }
            //}
            return BadRequest(ModelState);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Any(e => e.customerId == id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var Customer = db.Customers.Find(id);

            if (Customer == null)
            {
                return BadRequest(new { ErrorMessage = "No Product Found!" });
            }

            db.Customers.Remove(Customer);
            bool IsDelete = db.SaveChanges() > 0;
        
            if(IsDelete)
            {
                return Ok();
            }

            return BadRequest(new { ErrorMessage = "Could no delete product, unknown error!" });

        }



    }
}