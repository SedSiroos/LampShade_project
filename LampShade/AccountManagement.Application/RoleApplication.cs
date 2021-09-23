using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }


        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();
            if (_roleRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var newRole = new Role(command.Name,new List<Permission>());
            _roleRepository.Create(newRole);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.Get(command.Id);

            if (role == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            //if (_roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            //    return operation.Failed(ApplicationMessage.DuplicatedRecord);


            var permissions = new List<Permission>();
            command.Permissions.ForEach(code=> permissions.Add(new Permission(code)));

            role.Edit(command.Name,permissions);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<RoleViewModel> ListRoles()
        {
            return _roleRepository.ListRoles();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }
    }
}
