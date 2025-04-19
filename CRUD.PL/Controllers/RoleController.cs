using AutoMapper;
using CRUD.DAL.Models;
using CRUD.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CRUD.PL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _role;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> role,IMapper mapper)
        {
            _role = role;
            _mapper = mapper;
        }
        public async Task <IActionResult> Index(string searchInput)
        {
           
            if (string.IsNullOrEmpty(searchInput))
            {
                var roles=await _role.Roles.ToListAsync();
                var mapped = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(roles);
                return View(mapped);
            }
            else
            {
            var roo=await _role.FindByNameAsync(searchInput);
                var map=_mapper.Map<IdentityRole,RoleViewModel>(roo);
                 return View(new List<RoleViewModel> { map });
            }
        
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mapped = _mapper.Map<RoleViewModel, IdentityRole>(model); 
                await _role.CreateAsync(mapped);
                return RedirectToAction("Index");
            }
            return View(model);
        }





        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();
            var role = await _role.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var mapped = _mapper.Map<IdentityRole, RoleViewModel>(role);
            return View(viewname, mapped);


        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var rol = await _role.FindByIdAsync(id);
                 rol.Name=model.RoleName;

                    await _role.UpdateAsync(rol);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> ConDelete(string id)
        {
            try
            {
                var user = await _role.FindByIdAsync(id);
                await _role.DeleteAsync(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }




    }
}
