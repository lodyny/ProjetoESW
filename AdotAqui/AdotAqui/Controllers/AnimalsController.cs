﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdotAqui.Controllers
{
    public class AnimalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}