using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Application.Contract.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<long,Role>
    {
        List<RoleViewModel> ListRoles();
        EditRole GetDetails(long id);
    }
}
