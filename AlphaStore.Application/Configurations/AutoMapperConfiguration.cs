using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaStore.Application.Models;
using AlphaStore.Application.Services.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AlphaStore.Application.Models;

namespace AlphaStore.Application.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddMappings(
            this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            return services;
        }
    }

    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetailsDTO>(MemberList.None)
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => $"{src.Price.Amount} {src.Price.Currency}"))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Quanitity, opt => opt.MapFrom(src => src.Quanitity));

            CreateMap<Product, ProductShortDTO>(MemberList.None)
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => $"{src.Price.Amount} {src.Price.Currency}"))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<CreateProductDTO, Product>(MemberList.None)
                .ForMember(dest => dest.Quanitity, opt => opt.MapFrom(src => src.Quanitity))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Price
                (
                    src.PriceAmount,
                    src.PriceCurrency
                )));

            CreateMap<UpdateProductDTO, Product>(MemberList.None)
               .ForMember(dest => dest.Quanitity, opt => opt.MapFrom(src => src.Quanitity))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }

    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryShortDTO>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }

    internal class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDTO>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items
                    .GroupBy(e => e.ProductId)
                    .Select(e => new ShoppingCartItemDTO
                    {
                        Name = e.First().Product.Name,
                        Quantity = e.Count(),
                        Price = $"{e.First().Product.Price.Amount} {e.First().Product.Price.Currency}",
                        Total = $"{e.Sum(e => e.Product.Price.Amount)} {e.First().Product.Price.Currency}"
                    })));

            CreateMap<ShoppingCart, ShoppingCartShortDTO>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Items.Count));
        }
    }
}
