﻿using AutoMapper;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;

namespace StokTakipOtomasyon.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, AddProductRequestDto>().ReverseMap();
            CreateMap<Product, UpdateProductRequestDto>().ReverseMap();
        }
    }
}
