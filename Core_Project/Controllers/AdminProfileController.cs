using Core_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core_Project.Controllers
{
    public class AdminProfileController : Controller
    {
        ProjDB obj = new ProjDB();
        public IActionResult AdminProfile_Pageload()
        {
            return View();
        }
        public IActionResult AddMechanic()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMechanic(MechanicCls clsobj)
        {
            int i = obj.InsertMechanic(clsobj);
            if(i==1)
            {
                TempData["msg"] = "Inserted Successfully";
            }
            return View("AdminProfile_Pageload");
        }
        public IActionResult ViewMechanic(MechanicCls clsobj)
        {
            var data = obj.ListMechanic();
            return View("ViewMechanic",data);
        }
        public IActionResult ListAdminServices(ServiceCls clsobj)
        {
            ViewBag.Mechanics = obj.ListMechanic();
            var getdata = obj.ListAdmin();
            return View(getdata);
        }
        public IActionResult AssignMechanic(int bid, int mid)
        {

            obj.AssignMechanicToService(bid, mid); // this calls your stored procedure
            return RedirectToAction("ListAdminServices");
        }
        public IActionResult UpdateStatus(int bookid)
        {
            obj.UpdateServiceStatus(bookid); // call your DAL method that uses sp_ServiceStatus
            return RedirectToAction("ListAdminServices");
        }

    }
}
