using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;

namespace Tour_FP.Controllers
{
//Admin can only access these functions
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly DatabaseContext _Customercontext;
        private readonly IAdminService _adminService;
        private readonly IFileService _fileService;

        public AdminController(DatabaseContext Customercontext, IAdminService AdminService, IFileService fileService)
        {
            _Customercontext = Customercontext;
            _adminService = AdminService;
            _fileService = fileService;

        }
        public IActionResult Index()
        {
            var data = this._adminService.List();
            return View(data);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Admin_Dashboard model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.Images = imageName;
            }
            var result = _adminService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var model = _adminService.GetById(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Admin_Dashboard model, int id)
        {
            model.DestinationId = id;

            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.Images = imageName;
            }
            var result = _adminService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

       

        public IActionResult Delete(int id)
        {
            var existingData = _adminService.GetById(id);
            if (existingData == null)
            {
                return NotFound();
            }
            _fileService.DeleteImage(existingData.Images);
            var result = _adminService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Booking_Details()
        {

            IEnumerable<CustomerDetail> data = _Customercontext.Customer;

            return View(data);
        }
                public IActionResult Booking_Details_testing()
        {

            IEnumerable<CustomerDetail> data = _Customercontext.Customer;

            return View(data);
        }

    }
}
