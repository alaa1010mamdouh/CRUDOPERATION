using AutoMapper;
using CRUD.BLL.Interfaces;
using CRUD.BLL.Repositories;
using CRUD.DAL.Models;
using CRUD.PL.Helpers;
using CRUD.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task <IActionResult> Index(string searchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(searchInput))   
             employees =await _unitOfWork.EmployeeRepository.GetAll();
            else
           employees = _unitOfWork.EmployeeRepository.GetEmployeesByName(searchInput);      
            var mapped = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mapped);
        }
        [HttpGet]
        public async Task <IActionResult> Create()
        {
          ViewBag.Departments= await _unitOfWork.EmployeeRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(EmployeeViewModel employeeVm)
        {
            if (ModelState.IsValid)
            {
            employeeVm.ImageName=  DocumentSetting.UploadFile(employeeVm.Image, "Images");


             var mapped=   _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
              await  _unitOfWork.EmployeeRepository.Add(mapped);
               await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVm);
        }
        public async Task <IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var employee =await _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();
            var mapped=_mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, mapped);
        }
        [HttpGet]
        public async Task <IActionResult> Edit(int? id)
        {
            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.GetById(id.Value);
            //if (department is null)
            //    return NotFound();
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(EmployeeViewModel employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var mapped=_mapper.Map<EmployeeViewModel,Employee>(employeeVm);
                try
                {
                    if(employeeVm.Image is not null)
                    {
                        employeeVm.ImageName = DocumentSetting.UploadFile(employeeVm.Image, "Images");

                    }
                    _unitOfWork.EmployeeRepository.Update(mapped);
                   await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
               
            }
            return View(employeeVm);
        }
        [HttpGet]
        public async Task <IActionResult> Delete(int? id)
        {

            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete(EmployeeViewModel employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
                return BadRequest();
            try
            {
                var mapped = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.Delete(mapped);
              var Result=await  _unitOfWork.Complete();
                if (Result > 0&& employeeVm.ImageName is not null)
                {
                    DocumentSetting.DeleteFile(employeeVm.ImageName, "Images");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVm);
            }


        }
    }
}
