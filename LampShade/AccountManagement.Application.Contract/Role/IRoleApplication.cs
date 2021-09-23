using System.Collections.Generic;
using _0_Framework.Application;

namespace AccountManagement.Application.Contract.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        List<RoleViewModel> ListRoles();
        EditRole GetDetails(long id);
    }
}
