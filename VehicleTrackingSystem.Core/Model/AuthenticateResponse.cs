using System.Text.Json.Serialization;

namespace VehicleTrackingSystem.Core.Model
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiryAt { get; set; }


    }
}
