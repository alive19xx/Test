﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}