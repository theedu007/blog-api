using AutoMapper;
using BlogAPI.Models.DTOS;
using BlogAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(category => category.Description, cfg => cfg.MapFrom(source => source.Description))
                .ForMember(category => category.Id, cfg => cfg.MapFrom(source => source.Id))
                .ForMember(category => category.Name, cfg => cfg.MapFrom(source => source.Name))
                .ForMember(category => category.UrlSlug, cfg => cfg.MapFrom(source => source.UrlSlug));
        }
    }
}
