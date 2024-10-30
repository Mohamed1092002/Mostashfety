using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mostashfety.BLL.Enum;
using Mostashfety.BLL.Managers.Abstract;
using Mostashfety.BLL.ViewModels.DoctorVM;
using Mostashfety.BLL.ViewModels.PatientVM;

namespace Mostashfety.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorManager _doctorManager;
        public DoctorController(IDoctorManager doctorManager)
        {
           _doctorManager = doctorManager;
        }
        [Authorize(Roles = nameof(AppRoles.Admin))]
        public IActionResult Index()
        {
            var doctors = _doctorManager.GetAll();
            return View(doctors);
        }
        [Authorize(Roles = nameof(AppRoles.Admin))]
        public IActionResult Details(int id)
        {
            var doctors = _doctorManager.GetById(id);
            if (doctors == null)
            {
                return NotFound();
            }
            return View(doctors);
        }
        [Authorize(Roles = nameof(AppRoles.Admin))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateDoctorVM doctorVM)
        {
            if (ModelState.IsValid)
            {
                _doctorManager.Add(doctorVM);
                return RedirectToAction(nameof(Index));
            }
            return View(doctorVM);
        }
        [Authorize(Roles = nameof(AppRoles.Admin))]
        public IActionResult Edit(int id)
        {
            var doctors = _doctorManager.GetById(id);
            if (doctors == null)
            {
                return NotFound();
            }

            var doctorVM = new EditDoctorVM
            {
                Id = doctors.Id,
                FullName=doctors.FullName,
                specialization =doctors.specialization,
                Department=doctors.Department,
            };
            return View(doctorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditDoctorVM doctorVM)
        {
            if (id != doctorVM.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _doctorManager.Update(id, doctorVM);
                return RedirectToAction(nameof(Index));
            }
            return View(doctorVM);
        }
        [HttpGet]
        [Authorize(Roles = nameof(AppRoles.Admin))]
        public IActionResult Delete(int id)
        {
            var doctor = _doctorManager.GetById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            _doctorManager.Delete(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
