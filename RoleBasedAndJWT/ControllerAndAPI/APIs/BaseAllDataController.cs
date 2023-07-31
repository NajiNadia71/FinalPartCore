using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModelAnd;
using System.Linq;
using Bussiness.Interfaces;
using Model.ModelClass;

namespace RoleBasedAndJWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAllDataController : ControllerBase
    {
        private IRoleCRUDInterface RoleCRUD;
        public BaseAllDataController(IRoleCRUDInterface roleCRUD)
        {
            RoleCRUD = roleCRUD;
        }
        [HttpGet]
        [Route("GetALLGroupName")]
        public IList<GroupNameVM> GetALLGroupName(int? pageRow = 1, int? pageNumber = 100)
        {
            int PageRow = pageRow.HasValue ? pageRow.Value : 1;
            int PageNumber = pageNumber.HasValue ? pageNumber.Value : 100;
            var y = RoleCRUD.GetAllGroupName().Skip(PageRow * PageNumber)
                                              .Take(PageRow)
                ;
            var x = y.Select(i => new GroupNameVM
            { Id = i.Id, Description = i.Description, Title = i.Title }).ToList();
            return x;
        }
        [HttpPost]
        [Route("AddGroupName")]
        public Response AddGroupName(GroupNameVM group)
        {
            var x = new GroupName();
            // x.Id = group.Id;
            x.Description = group.Description;
            x.Title = group.Title;
            return RoleCRUD.CreateGroupName(x);
        }
        [HttpPost]
        public Response UpdateGroupName(GroupNameVM group)
        {
            var x = new GroupName();
            x.Id = group.Id;
            x.Description = group.Description;
            x.Title = group.Title;
            return RoleCRUD.EditGroupName(x);
        }

        [HttpPost]
        public Response DeleteGroupName(GroupNameVM group)
        {
            return RoleCRUD.DeleteGroupName(group.Id);
        }
    }
}
