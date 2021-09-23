using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        public EditRole Command;
        private readonly IRoleApplication _roleApplication;
        public List<SelectListItem> Permissions = new List<SelectListItem>();
        private readonly IEnumerable<IPermissionExpose> _expose;

        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExpose> expose)
        {
            _roleApplication = roleApplication;
            _expose = expose;
        }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);

            var permissions = new List<PermissionDto>();
            foreach (var expose in _expose)
            {
                var exposedPermissions = expose.Expose();
                foreach (var (key,value) in exposedPermissions)
                {
                    var group = new SelectListGroup{Name = key};
                    permissions.AddRange(value);
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };
                        if (Command.MappedPermissions.Any(x=>x.Code == permission.Code))
                            item.Selected=true;

                        Permissions.Add(item);
                    }
                }
            }

        }
 
        public IActionResult OnPost(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            return RedirectToPage("Index");
        }
    }
}