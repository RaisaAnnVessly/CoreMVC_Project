using Core_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core_Project.Controllers
{
    public class AdminInsController : Controller
    {
        ProjDB obj = new ProjDB();
        public IActionResult Admin_Pageload()
        {
            return View();
        }
        public IActionResult Admin_click(AdminCls clsobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = obj.InsertAdmin(clsobj);
                    TempData["msg"] = resp;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Admin_Pageload");
        }
    }
}
