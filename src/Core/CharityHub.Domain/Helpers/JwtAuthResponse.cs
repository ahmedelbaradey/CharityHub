namespace CharityHub.Domain.Helpers
{
    public class JwtAuthResponse
    {
        public string? AccessToken { get; set; }
        public RefreshToken refreshToken { get; set; } = null!;
    }

    public class RefreshToken
    {
        public string MobileNumber { get; set; } = null!;
        public DateTime ExpireAt { get; set; }
        public string TokenString { get; set; } = null!;
    }
}
