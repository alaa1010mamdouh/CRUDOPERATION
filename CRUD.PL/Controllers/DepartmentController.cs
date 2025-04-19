using CRUD.BLL.Interfaces;
using CRUD.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CRUD.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
    
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
        public async Task <IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(Department department )
        {
            if (ModelState.IsValid)
            {

           await  _unitOfWork.DepartmentRepository.Add(department);
                int Result =await _unitOfWork.Complete();
                if (Result > 0)
                {
                    TempData["Message"] = "Department is Created";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        public async Task <IActionResult> Details(int? id,string ViewName="Details")
        {
            if (id is null)
                return BadRequest();

            var department =await _unitOfWork.DepartmentRepository.GetById(id.Value);
            if(department is null) 
                return NotFound();
            return View(ViewName,department);
        }
        [HttpGet]
        public async Task <IActionResult >Edit(int? id)
        {
           
            return await Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                   await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
                _unitOfWork.DepartmentRepository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        [HttpGet]
        public async Task <IActionResult> Delete(int? id)
        {
           
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(Department department ,[FromRoute] int id)
        {
            if(id != department.Id)
                return BadRequest();
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
               await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);
            }


        }
    }
}
