using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InAndOut.Models;
using InAndOut.Data;


namespace InAndOut.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public ExpenseTypeController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(){
            IEnumerable<ExpenseType> objList = _db.ExpenseTypes;
            return View(objList);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreatePost(ExpenseType obj){
            _db.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

         public IActionResult Delete(int? id){
            
            if(id == 0 || id == null){
                return NotFound();
            }

            var obj = _db.ExpenseTypes.Find(id);

            if(obj == null){
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePost(int? id){

            var obj = _db.ExpenseTypes.Find(id);

            if(obj == null){
                return NotFound();
            }

            _db.ExpenseTypes.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

         public IActionResult Update(int? id){
            
            if(id == 0 || id == null){
                return NotFound();
            }

            var obj = _db.ExpenseTypes.Find(id);

            if(obj == null){
                return NotFound();
            }

            return View(obj);
        }

        
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult UpdatePost(ExpenseType obj){

            _db.ExpenseTypes.Update(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}