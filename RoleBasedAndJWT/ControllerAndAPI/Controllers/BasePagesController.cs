using Microsoft.AspNetCore.Mvc;
using Bussiness.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elmah.Io.Client;
using Microsoft.EntityFrameworkCore;
using Model.ModelClass;
using ViewModelAnd;
using NLog;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using NuGet.Packaging.Signing;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Net;
using NuGet.Common;
using RoleBasedAndJWT.Pages;
using System.IdentityModel.Tokens.Jwt;
using Model.Migrations;
using System.Security.Claims;
using Bussiness;
namespace RoleBasedAndJWT
{
    public class BasePagesController : Controller
    {
        #region GeneralStuff
        private IAuthenticationInterface Authentication1;
        private IRoleCRUDInterface RoleCRUDInterface;
        private readonly ILogger<BasePagesController> _logger;
        public BasePagesController(IAuthenticationInterface authentication, IRoleCRUDInterface roleCRUDInterface, ILogger<BasePagesController> logger)
        {
            Authentication1 = authentication;
            RoleCRUDInterface = roleCRUDInterface;
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region UnAuthorizedActions 
        public IActionResult Register()
        {
            var viewModel = new ViewModelAnd.RegisterModel();
            return View(viewModel);


        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            registerModel.IsActive = true;
            var x =await  Authentication1.Register(registerModel);

            ViewBag.Message = x.Message.ToString();

            // Redirecting to another page
            return RedirectToAction("UserInfo", "BasePages");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Login()
        {
            var x = new LoginModel();
            return View(x);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try {  
            var x = await Authentication1.LoginOnlyJWTtokenAndRefreshToken(loginModel);
       
            if (x.ErrorCode == ErrorCodeEnum.NotAuthorize)
            {
                return Unauthorized();
            }
            if (x.ErrorCode == ErrorCodeEnum.Authorize)
            {
     
                    Response.Cookies.Append("X-ErrorCode", x.ErrorCode.ToString(), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    Response.Cookies.Append("X-Access-Token", x.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    Response.Cookies.Append("X-Expiration", x.Expiration.ToString(), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    Response.Cookies.Append("X-Refresh-Token", x.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

                    return RedirectToAction("GetAllUsers", "BasePages");
             
               
            }
            else
            {
                return Unauthorized();
            }
                
            }
            catch (Exception ex) {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });

            }
        }
        #endregion
        #region AdminLevelRoleAccess
        public IActionResult GetAllUsers(int? pageRow, int? pageNumber)
        {
            try
            {
                int PageRow = pageRow.HasValue ? pageRow.Value : 10;
                int PageNumber = pageNumber.HasValue ? pageNumber.Value : 1;
                int len = RoleCRUDInterface.GetAllUser().Count();
                var res = RoleCRUDInterface.GetAllUser().Skip((PageNumber - 1) * PageRow).Take(PageRow)
                    .Select(i => new User
                    {
                        
                        Name = i.UserName,
                        Email = i.Email,
                        Id = i.Id

                    }).ToList();
                ViewBag.PageRow = PageRow;
                ViewBag.PageNumber = PageNumber;
                ViewBag.TotalPages = Math.Abs(decimal.Divide(len, PageRow));
                return View(res);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        #region RoleForUser
        /// <summary>
        [CustomAuthorizeRole("Admin")]
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult GetAllRolesForUser(string Id)
        {
            try
            {
                var res = RoleCRUDInterface.GetUserWithAllRole(Id);
                ViewBag.RolesthatUserDosentHave = RoleCRUDInterface.GetRoldesThatUserDosenHave(Id).ToList();
                return PartialView("GetAllRolesForUser",res);
               
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        [HttpPost]
        public IActionResult AddRolesForUser([FromBody]ViewModelAnd.User user)
        {
            try
            {
               var res = RoleCRUDInterface.CreateNewRoleForUser(Guid.Parse(user.RoleId), user.Id);
                var str = res.ErrorCode.ToString();
               return Json(str);
                
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
      //  [Authorize(Roles = "DeleteRolesForUser")]
        public IActionResult DeleteRolesForUser([FromBody] ViewModelAnd.User user)
        {
            try
            {
                var res = RoleCRUDInterface.DeleteUserRole(Guid.Parse(user.Id));
                var str=res.ErrorCode.ToString();
                return Json(str);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        #endregion
        #region RoleGroupForUser
     
        public IActionResult GetAllRolesGroupsForUser(string Id)
        {
            try
            {
                var res = RoleCRUDInterface.GetUserWithAllRole(Id);
                ViewBag.GroupRolesthatUserDosentHave = RoleCRUDInterface.GetGroupRolesThatUserDosentHave(Id).ToList();
                return PartialView("GetAllRolesGroupsForUser", res);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        [HttpPost]
        public IActionResult AddRoleGroupForUser([FromBody] ViewModelAnd.User user)
        {
            try
            {
                var res = RoleCRUDInterface.CreateNewGroupNameForUser(user.RoleGroupId, user.Id);
                var str = res.ErrorCode.ToString();
                return Json(str);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        public IActionResult DeleteRoleGroupForUser([FromBody] ViewModelAnd.User user)
        {
            try
            {
                var res = RoleCRUDInterface.DeleteUserGroup(Guid.Parse(user.Id));
                var str = res.ErrorCode.ToString();
                return Json(str);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        #endregion
        #region AddOrEditGroupName
     
        public IActionResult GetAllGroupRoleNames(int? pageRow, int? pageNumber)
        {
            try
            {
                int PageRow = pageRow.HasValue ? pageRow.Value : 10;
                int PageNumber = pageNumber.HasValue ? pageNumber.Value : 1;
                int len = RoleCRUDInterface.GetAllGroupName().Count();
                var res = RoleCRUDInterface.GetAllGroupName().Skip((PageNumber - 1) * PageRow).Take(PageRow)
                    .Select(i => new ViewModelAnd.GroupNameVM
                    {

                        Description = i.Description,
                        Title = i.Title,
                        Id = i.Id

                    }).ToList();
                ViewBag.PageRow = PageRow;
                ViewBag.PageNumber = PageNumber;
                ViewBag.TotalPages = Math.Abs(decimal.Divide(len, PageRow));
                return View(res);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        public IActionResult CreateRoleGroup()
        {
            var x = new GroupNameVM();
            return PartialView("CreateRoleGroup", x);
        }
        [HttpPost]
        public IActionResult CreateRoleGroupd([FromBody] GroupNameVM groupNameVM)
        {
            try
            {
                var x = new GroupName();
                x.Title= groupNameVM.Title;
                x.Description= groupNameVM.Description;
                var res = RoleCRUDInterface.CreateGroupName(x);
                var str = res.ErrorCode.ToString();
                return Json(str);


            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        [HttpPost]
        public IActionResult EditRoleGroup(int id)
        {
            var res = RoleCRUDInterface.GetAllGroupName().Where(i=>i.Id==id).FirstOrDefault();
            var x = new GroupNameVM();
            x.Id= res.Id;
            x.Title = res.Title;
            x.Description = res.Description;

            return PartialView("EditRoleGroup", x);
        }
        [HttpPost]
        public IActionResult EditRoleGroupd([FromBody] GroupNameVM groupNameVM)
        {
            try
            {
                var x = new GroupName();
                x.Id = groupNameVM.Id;
                x.Title = groupNameVM.Title;
                x.Description = groupNameVM.Description.ToString();
                var res = RoleCRUDInterface.EditGroupName(x);
                var str = res.ErrorCode.ToString();
                return Json(str);


            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        public IActionResult DeleteRoleGroup([FromBody] GroupNameVM groupNameVM)
        {
            try
            {
                var res = RoleCRUDInterface.DeleteGroupName(groupNameVM.Id);
                var str = res.ErrorCode.ToString();
                return Json(str);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        #endregion

        #region MangeRolesOfRoleGroup
        [HttpPost]
        public IActionResult GetAllRolesOfGroupRole(int id)
        {
            try
            {
                var RoleGroupVMs = new List<ViewModelAnd.RoleGroupVM>();

                var res = RoleCRUDInterface.GetAllRoleGroupForGroupRole(id);
                ViewBag.AllRolesThatGroupRoleDosentHave = RoleCRUDInterface.GetAllRolesThatGroupRoleDosentHave(id).ToList();
                return PartialView("GetAllRolesOfGroupRole", res);


                
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        //[HttpPost]
        //public IActionResult AddRoleToRoleGroup(RoleGroup roleGroup)
        //{
        //    var res = RoleCRUDInterface.CreateRoleGroup(roleGroup);
        //    return View(res);
        //}
        [HttpPost]
        public IActionResult DeleteRoleFromRoleGroup([FromBody] GroupNameVM roleGroup)
        {
            try
            { 
                var res = RoleCRUDInterface.DeleteRoleGroup(Guid.Parse(roleGroup.Title));
                var str = res.ErrorCode.ToString();
                return Json(str);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        [HttpPost]
        public IActionResult AddRoleToRoleGroup([FromBody] GroupNameVM groupNameVM)
        {
            try
            {
                var res = RoleCRUDInterface.CreateNewRoleToRoleGroup(Guid.Parse(groupNameVM.RoleId), groupNameVM.Id);
                var str = res.ErrorCode.ToString();
                return Json(str);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Is:", ex.Message.ToString());
                return Json(new { status = "error", message = "Exception Is:" + " " + ex.Message.ToString() });
            }
        }
        
        #endregion
        #endregion
        #region BasicLevelRoleAccess
        public IActionResult GetUserInfo()
        {
             //string? UserName = Request.Cookies["X-Access-Token"];
            //int? UserId = Convert.ToInt32(Request.Cookies["X-Refresh-Token"]);
            return View();
        }
        #endregion
       

        public IActionResult paginationWithBootstrap(int? pageRow, int? pageNumber)
        {
            int PageRow = pageRow.HasValue ? pageRow.Value : 10;
            int PageNumber = pageNumber.HasValue ? pageNumber.Value : 1;

            int len = 178;
            var resList = new List<Response>();
            for (int i = 0; i <= len; i++)
            {
                var obj = new Response();
                obj.Message = i.ToString() + "NameA";
                obj.IdOfStatus = i;
                resList.Add(obj);
            }
            var res = resList.Skip((PageNumber - 1) * PageRow).Take(PageRow);
            ViewBag.PageRow = PageRow;
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = 18;
            return View(res);
        }
        public IActionResult ShowModal()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PartialModal() {
            /// return PartialModal();
            ///
            var s = 765;
           return PartialView("_PartialModal");
        }
    }
}
