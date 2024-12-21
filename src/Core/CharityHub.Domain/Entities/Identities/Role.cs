
using CharityHub.Domain.Helpers;

namespace CharityHub.Domain.Entities.Identities
{
    public class Role 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
