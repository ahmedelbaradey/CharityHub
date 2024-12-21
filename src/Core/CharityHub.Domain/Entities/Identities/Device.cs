namespace CharityHub.Domain.Entities.Identities
{
    public class Device
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DeviceType { get; set; }
        public DateTime LastAccessTime { get; set; }
        public string RefreshToken { get; set; } = null!;
        public string FCMToken { get; set;  } = null!;
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Account Account { get; set; }
    }
}
