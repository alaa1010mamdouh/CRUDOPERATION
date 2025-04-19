using AutoMapper;
using CRUD.DAL.Models;
using CRUD.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchInput)
        {
            if (string.IsNullOrEmpty(searchInput))
            {
                var Users = await _userManager.Users.Select(
                    u => new UserViewModel()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        Roles = _userManager.GetRolesAsync(u).Result
                    }).ToListAsync();
                return View(Users);
            }

            else
            {
                var useer = await _userManager.FindByEmailAsync(searchInput);
                var mapped = new UserViewModel()
                {
                    Id = useer.Id,
                    FirstName = useer.FirstName,
                    LastName = useer.LastName,
                    Email = useer.Email,
                    PhoneNumber = useer.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(useer).Result
                };
                return View(new List<UserViewModel> { mapped });
            }

        }

        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();
            var User = await _userManager.FindByIdAsync(id);
            if (User is null)
                return NotFound();
            var mapped = _mapper.Map<ApplicationUser, UserViewModel>(User);
            return View(viewname, mapped);


        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.PhoneNumber = model.PhoneNumber;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public async Task <IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> ConDelete(string id)
        {
            try
            {
                var user=await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
