using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mostashfety.BLL.Enum;
using Mostashfety.BLL.Managers.Abstract;
using Mostashfety.BLL.ViewModels.DoctorVM;
using Mostashfety.BLL.ViewModels.MedicalVM;

namespace Mostashfety.Controllers
{
    public class MedicalController : Controller
    {
        private readonly IMedicalManager _medicalManager;
        public MedicalController(IMedicalManager medicalManager)
        {
            _medicalManager = medicalManager;
        }
        public IActionResult Index()
        {
            var records = _medicalManager.GetAll();
            return View(records);
        }
        public IActionResult Details(int id)
        {
            var records = _medicalManager.GetById(id);
            if (records == null)
            {
                return NotFound();
            }
            return View(records);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMedicalVM medicalVM)
        {
            if (ModelState.IsValid)
            {
                _medicalManager.Add(medicalVM);
                return RedirectToAction(nameof(Index));
            }
            return View(medicalVM);
        }
        public IActionResult Edit(int id)
        {
            var records = _medicalManager.GetById(id);
            if (records == null)
            {
                return NotFound();
            }

            var medicalVM = new EditMedicalVM
            {
                Id = records.Id,
                DoctorName=records.DoctorName,
                PatientName=records.PatientName,
                Diagnosis=records.Diagnosis,
                Treatment=records.Treatment,
                Date=records.Date,
            };
            return View(medicalVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditMedicalVM medicalVM)
        {
            if (id != medicalVM.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _medicalManager.Update(id, medicalVM);
                return RedirectToAction(nameof(Index));
            }
            return View(medicalVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var records = _medicalManager.GetById(id);
            if (records == null)
            {
                return NotFound();
            }
            _medicalManager.Delete(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
