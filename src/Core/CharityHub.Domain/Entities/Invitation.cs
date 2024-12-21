using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CharityHub.Domain.Entities
{
    public class Invitation
    {
        public string InvitedMobileNumber { get; set; }
        public int InviterId { get; set; }
    }
}
