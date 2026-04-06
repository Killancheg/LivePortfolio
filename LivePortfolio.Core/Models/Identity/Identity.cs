namespace LivePortfolio.Core.Models.Identity
{
    public sealed record AppUser(string Name, string Email);

    public sealed record RegisterRequest(string UserName, string Email, string Password);

    public sealed record RegisterResult(bool Succeeded, IReadOnlyList<string> Errors)
    {
        public static RegisterResult Success() => new(true, []);
        public static RegisterResult Failure(IEnumerable<string> errors) => new(false, [.. errors]);
    }
}
