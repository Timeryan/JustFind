
using Microsoft.AspNetCore.Http;

namespace Advertisement.PublicApi.Models.Photo
{
    public class PhotoCreateDto
    {
        public IFormFile PhotoBytes { get; set; }
    }
}