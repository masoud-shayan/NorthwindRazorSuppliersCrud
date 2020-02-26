using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindContextLib;
using NorthwindEntitiesLib;

namespace NorthwindWeb.Pages
{
    public class SuppliersModel : PageModel
    {
        // Inject dbContext with EFcore
        private Northwind db;

        public SuppliersModel(Northwind injectedContext)
        {
            db = injectedContext;
        }

        // Select from db.suppliers
        public IQueryable Suppliers { get; set; }
        public int SuppliersCount { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind Web Site - Suppliers";
            Suppliers = db.Suppliers.Select(s => s.CompanyName);
            SuppliersCount = db.Suppliers.Select(s => s.CompanyName).Count();
        }

        
        
        
        
        

        // Insert  into db.suppliers or Delete from db.suppliers based on methodName
        
        [BindProperty] 
        public Supplier Supplier { get; set; }
        
        public IActionResult OnPost()
        {
            
            if (ModelState.IsValid)
            {
                
                if (Request.Form["methodName"].Equals("Post"))
                {
                    db.Suppliers.Add(Supplier);
                }
                else if (Request.Form["methodName"].Equals("Delete"))
                {
                    IQueryable<Supplier> Supplier = db.Suppliers.Where(s => s.CompanyName == this.Supplier.CompanyName);
                    db.Suppliers.RemoveRange(Supplier);
                }

                db.SaveChanges();
                return RedirectToPage("/suppliers");
            }

            return Page();
        }
    }
}