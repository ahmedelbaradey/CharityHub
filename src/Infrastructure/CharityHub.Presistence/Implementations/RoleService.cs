using CharityHub.DomainService.Abstractions.Services;
using CharityHub.DomainService.Abstractions.Repository;
using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Logger;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CharityHub.DomainService.Implementations
{
    public class RoleService : IRoleService
    {

        #region Fileds
        private readonly ILoggerManager _logger;
        private readonly IGenericRepository<Role> _roleRepository;
 
        #endregion

        #region Constructors
        public RoleService(IGenericRepository<Role> roleRepository, ILoggerManager logger)
        {
            _logger = logger;
            _roleRepository = roleRepository;
        }




        #endregion

        #region Handle Functions

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            if(!await CheckIfItExists(roleName))
                throw new ArgumentNullException(nameof(roleName));
            return await _roleRepository.FindByCondition(c=>c.Name.Equals(roleName),trackChanges:false).FirstOrDefaultAsync();  
        }

        public async Task<bool> CheckIfItExists(string roleName)
        {
            var role = await _roleRepository.FindByCondition(c => c.Name.Equals(roleName), trackChanges: false).FirstOrDefaultAsync();
          if(role is null)
                return false;
          return true;
        }
        #endregion
    }
}
