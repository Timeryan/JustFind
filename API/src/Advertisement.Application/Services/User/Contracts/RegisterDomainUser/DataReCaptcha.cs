using System.Text.Json.Serialization;

namespace Advertisement.Application.Services.User.Contracts.RegisterDomainUser
{
    public class DataReCaptcha
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}