using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDemoApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _Db;
        public UserController(UserContext Db)
        {
            //this is db context initialization 
            _Db = Db;
        }
        public IActionResult UserList()
        {
            try
            {
                var userList = _Db.Users.Where(x => x.isActive == true).ToList();
                return View(userList);
            }
            catch(Exception)
            {
                throw;
            } 
        }

        public IActionResult AddUser(Users userModel)
        {
            DesignationList(); 
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserOperation(Users userModel)
        {
            try
            {
                ModelState.Remove("UserId");
                if (ModelState.IsValid && userModel != null)
                {
                    if(userModel.UserId == 0) {
                        userModel.CreatedBy = 1;
                        userModel.CreatedDate = DateTime.Now;
                        userModel.isActive = true;
                        _Db.Users.Add(userModel);
                        await _Db.SaveChangesAsync();
                    }
                    else
                    { 
                        userModel.UpdatedBy = 1;
                        userModel.UpdatedDate = DateTime.Now;
                        _Db.Update(userModel);
                        await _Db.SaveChangesAsync();
                    } 
                }
                return RedirectToAction("UserList");
            }
            catch (Exception)
            {
                return RedirectToAction("UserList");
                throw;
            }
        }

        private void DesignationList()
        {
            try
            {  
                var designationList = new List<SelectListItem>()
                {
                    new SelectListItem{ Text="Software Engineer", Value = "Software Engineer", Selected = true },
                    new SelectListItem{ Text="Senior Software Engineer", Value = "Senior Software Engineer" },
                    new SelectListItem{ Text="Analyst Programmer", Value = "Analyst Programmer" },
                    new SelectListItem{ Text="Senior Analyst Programmer", Value = "Senior Analyst Programmer" },
                    new SelectListItem{ Text="Tech Lead", Value = "Tech Lead" },
                    new SelectListItem{ Text="Project Manager", Value = "Project Manager" },
                };
                ViewData["designationList"] = designationList;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<IActionResult> DeleteUser(long UserId)
        {
            try
            {
                var userDetails = await _Db.Users.FindAsync(UserId);
                if(userDetails != null)
                {
                    userDetails.UpdatedBy = 1;
                    userDetails.UpdatedDate = DateTime.Now;
                    userDetails.isActive = false;
                    _Db.Update(userDetails);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("UserList");
            }
            catch (Exception)
            {
                return RedirectToAction("UserList");
                throw;
            }
        }
    }
}
