//using Application.Photos;
//using Microsoft.AspNetCore.Http;

namespace Infrastructure.Interfaces
{
    public interface IPhotoAccessor
    {
         //PhotoUploadResult AddPhoto(IFormFile file);
         string DeletePhoto(string publicId);
    }
}