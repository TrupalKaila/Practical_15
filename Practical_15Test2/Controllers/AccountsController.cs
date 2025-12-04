using Practical_15Test2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Practical_15Test2.Controllers
{
    public class AccountsController : Controller
    {
        [AllowAnonymous]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public ActionResult Login(UserModel model)
        {
            using (EmployeeDBContext context = new EmployeeDBContext())
            {
                bool isValidUser = context.Users.Any(user => user.UserName == model.UserName && user.UserPassword == model.UserPassword);
                if (isValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}