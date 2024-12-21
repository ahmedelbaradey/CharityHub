using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Logger;
using CharityHub.DomainService.Abstractions.Repository;
using CharityHub.DomainService.Abstractions.Services;
using Microsoft.EntityFrameworkCore;


namespace CharityHub.DomainService.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fileds
        private readonly ILoggerManager _logger;
        private readonly IGenericRepository<Role> _roleRepository;
        #endregion

        #region Constructors
        public AuthorizationService(ILoggerManager logger, IGenericRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
            _logger = logger;
      
        }
        #endregion

        #region Functions

        public async Task<bool> AddRoleAsync(string roleName)
        {
            try
            {
                var role = new Role();
                role.Name = roleName.ToLower();

                var result = await _roleRepository.AddAsync(role);

                if (result==null)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogDebug( "Error in AddRoleAsync");
                throw;
            }
        }
        public async Task<Role> GetRoleByID(int Id)
        {
            try
            {
                var role = await _roleRepository.GetByIdAsync(Id);
                return role;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in GetRoleByID");
                throw;
            }
        }
        public async Task<List<Role>> GetRoleListAsync()
        {
            try
            {
                var roles = await _roleRepository.FindAll(trackChanges:false).ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in GetRoleListAsync");
                throw;
            }
        }
 
        #endregion
    }
}
