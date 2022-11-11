using ShopDev.APIModels;

namespace ShopDev.Client.Models
{
    public class UserState
    {
        public string? Username { get; set; }
        public Guid UserId { get; set; }
        public Token? Token { get; set; }
        public List<string>? Permissions { get; set; }
        public Dictionary<string, int>? Limits { get; set; }
    }
}
