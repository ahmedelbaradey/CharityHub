namespace CharityHub.Application.Services.Accounts.Queries.Responses
{
    public class GetAccountPaginatedListResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Country { get; set; }
        public string? Address { get; set; }

    }
}
