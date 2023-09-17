using App_Data.IRepositories;
using App_Data.Models;
using App_Data.ViewModels.ProductDetail;
using AutoMapper;

namespace App_Api.Helpers.Mapings
{
    public class MappingProfiles: Profile
    {
       
        public MappingProfiles()
        {
            CreateMap<ProductDetailDTO, ProductDetails>();
        }
    }
}
