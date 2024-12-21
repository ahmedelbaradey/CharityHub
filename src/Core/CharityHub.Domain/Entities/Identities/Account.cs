using CharityHub.Domain.Helpers;

namespace CharityHub.Domain.Entities.Identities
{
    public class Account
    {
        public int Id {  get; set; }
        public string MobileNumber { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime JoinedDate { get; set; }
        public bool Blocked { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }

    }
}
