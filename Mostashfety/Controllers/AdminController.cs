using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mostashfety.BLL.Enum;
using Mostashfety.BLL.Managers.Abstract;
using Mostashfety.BLL.ViewModels.AdminVM;
using Mostashfety.BLL.ViewModels.DoctorVM;

namespace Mostashfety.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminManager _adminManager;
        public AdminController(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }
       
        public IActionResult Index()
        {
            var admins = _adminManager.GetAll();
            return View(admins);
        }
        public IActionResult Details(int id)
        {
            var admins = _adminManager.GetById(id);
            if (admins == null)
            {
                return NotFound();
            }
            return View(admins);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateAdminVM adminVM)
        {
            if (ModelState.IsValid)
            {
                _adminManager.Add(adminVM);
                return RedirectToAction(nameof(Index));
            }
            return View(adminVM);
        }
        public IActionResult Edit(int id)
        {
            var admin = _adminManager.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }

            var adminVM = new EditAdminVM
            {
                Id = admin.Id,
                FullName=admin.FullName,
                UserName = admin.UserName,
                Email   = admin.Email,
                Password = admin.Password,
                PhoneNumber = admin.PhoneNumber,

            };
            return View(adminVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditAdminVM adminVM)
        {
            if (id != adminVM.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _adminManager.Update(id, adminVM);
                return RedirectToAction(nameof(Index));
            }
            return View(adminVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var admin = _adminManager.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }
            _adminManager.Delete(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
