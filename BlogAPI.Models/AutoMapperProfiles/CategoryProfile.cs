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
            CreateMap<Category, CategoryDto>();
        }
    }
}
