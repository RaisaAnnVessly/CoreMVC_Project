using Core_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core_Project.Controllers
{
    public class UserProfileController : Controller
    {
        ProjDB obj = new ProjDB();
        public IActionResult UserProfile_Pageload()
        {
            int uid = Convert.ToInt32(TempData["uid"]);
            var data = obj.ListServicesDB(uid);
            TempData.Keep("uid");
            return View(data);
        }

        public IActionResult AddService_click()
        {
            int uid = Convert.ToInt32(TempData["uid"]);
            TempData.Keep("uid");
            ViewBag.uid = uid;
            return View("Insert_Vehicle");
        }
        public IActionResult Insert_Vehicle(ServiceCls clsobj, IFormFile documents)
        {
            if (documents != null && documents.Length > 0)
            {
                // Get the uploaded file name
                var fileName = Path.GetFileName(documents.FileName);

                // Full path to save the file in wwwroot/Phs
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Phs");

                // Ensure the folder exists
                //if (!Directory.Exists(uploadFolder))
                //    Directory.CreateDirectory(uploadFolder);

                var filePath = Path.Combine(uploadFolder, fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    documents.CopyTo(stream);
                }

                // Store relative path in the model (to save in DB)
                clsobj.documents = "/Phs/" + fileName;
            }


            TempData.Keep("uid");
            clsobj.uid = Convert.ToInt32(TempData["uid"]);
            clsobj.bookingdate = DateOnly.FromDateTime(DateTime.Now);
            int i = obj.InsertVehicle(clsobj);
            if (i == 1)
            {
                TempData["msg"] = "Inserted successfully";
            }
            return RedirectToAction("UserProfile_Pageload");
        }
        
    }
}
