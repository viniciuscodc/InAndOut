using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InAndOut.Models;

namespace InAndOut.Controllers
{
    public class AppointmentController : Controller
    {
       public IActionResult Index(){

           return View();
       } 

    }
}