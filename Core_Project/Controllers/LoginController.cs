using Core_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Core_Project.Controllers
{
    public class LoginController : Controller
    {
        ProjDB obj = new ProjDB();
        public IActionResult Login_Pageload()
        {
            return View();
        }
        public IActionResult Login_click(LoginCls clsobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int res = obj.Login(clsobj);
                    if (res == 1)
                    {
                        int id = obj.GetId(clsobj);
                        TempData["uid"] = id;
                        string ltype = obj.GetLogType(clsobj);
                        if(ltype=="user")
                        {
                            return RedirectToAction("UserProfile_Pageload","UserProfile");
                        }
                        else
                        {
                            return RedirectToAction("AdminProfile_Pageload", "AdminProfile");
                        }
                    }
                    else
                    {
                        TempData["msg"]= "Invalid Login";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Login_Pageload"); 
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear the TempData value for user_id
            TempData.Remove("uid");

            // Optional: Clear all TempData if you want
            // TempData.Clear();

            // Redirect to homepage
            return RedirectToAction("Index", "Home");
        }



    }
}
