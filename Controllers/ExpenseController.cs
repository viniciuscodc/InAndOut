using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InAndOut.Models;
using InAndOut.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using InAndOut.Models.ViewModels;


namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDBContext _db;

        public ExpenseController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Expense> objList = _db.Expenses;

            foreach (var obj in objList)
            {
                obj.ExpenseType = _db.ExpenseTypes.FirstOrDefault(u => u.Id == obj.ExpenseTypeId); 
            }

            return View(objList);
        }


        public IActionResult Create()
        {
            /*
            IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
            {
                Text = i.ExpenseTypeName,
                Value = i.Id.ToString()
            });

            ViewBag.TypeDropDown = TypeDropDown;
            */

            ExpenseVm expenseVm = new ExpenseVm()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                Text = i.ExpenseTypeName,
                Value = i.Id.ToString()
                })
            };

            return View(expenseVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
          public IActionResult Create(ExpenseVm obj)
        {
            if(ModelState.IsValid){
                //obj.ExpenseTypeId = 1;
                _db.Expenses.Add(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0)
            {
                return NotFound();
            }

            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
    
                _db.Expenses.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id == null | id == 0)
            {
                return NotFound();
            }

                ExpenseVm expenseVm = new ExpenseVm()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                Text = i.ExpenseTypeName,
                Value = i.Id.ToString()
                })
            };

            expenseVm.Expense = _db.Expenses.Find(id);
            if (expenseVm.Expense == null)
            { 
                return NotFound();
            }
            return View(expenseVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
          public IActionResult UpdatePost(ExpenseVm obj)
        {
            if(ModelState.IsValid){
                _db.Expenses.Update(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}