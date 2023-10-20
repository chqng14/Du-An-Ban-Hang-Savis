using App_Data.IRepositories;
using App_Data.Models;
using App_Data.ViewModel;
using App_Data.ViewModels.ProductDetail;
using App_Data.ViewModels.Voucher;
using AutoMapper;

namespace App_Api.Helpers.Mapings
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<App_Data.ViewModels.ProductDetail.ProductUpdateDTO, ProductDetails>().ReverseMap();

            CreateMap<App_Data.ViewModel.ProductDetailDTO, ProductDetails>().ReverseMap();

            CreateMap<SaleDetail, SaleDTViewModel>()
               .ForMember(
                   dest => dest.Sale,
                   opt => opt.MapFrom(src => src.Sale.Ten)
               )
               .ForMember(
                   dest => dest.Product,
                   opt => opt.MapFrom(src => src.ProductDetail.Products.Ten)
               ).ReverseMap();
            CreateMap<VoucherDTO, Voucher>().ReverseMap();
        }
    }
}
