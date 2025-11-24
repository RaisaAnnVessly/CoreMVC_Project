using Core_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core_Project.Controllers
{
    public class InsertController : Controller
    {
        ProjDB obj = new ProjDB();
        public IActionResult Index_Pageload()
        {
            return View();
        }
        public IActionResult Index_click(InsertCls clsobj)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string resp = obj.InsertUsr(clsobj);
                    TempData["msg"] = resp;
                }
            }
            catch(Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Index_Pageload");
        }

    }
}
