using Microsoft.AspNetCore.Mvc;

namespace App_View.IServices
{
    public interface IImageService
    {
        Task CreatImage(Guid idProductDetail, List<IFormFile> lstIFormFile);
        Task DeleteImage(Guid idProductDetail, List<string> lstImageRemove);

    }
}
