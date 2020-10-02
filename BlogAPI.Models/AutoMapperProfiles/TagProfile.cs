using AutoMapper;
using BlogAPI.Models.DTOS;
using BlogAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.AutoMapperProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tags, TagDto>()
                .ForMember(tag => tag.Description, cfg => cfg.MapFrom(source => source.Description))
                .ForMember(tag => tag.Id, cfg => cfg.MapFrom(source => source.Id))
                .ForMember(tag => tag.Name, cfg => cfg.MapFrom(source => source.Name))
                .ForMember(tag => tag.UrlSlug, cfg => cfg.MapFrom(source => source.UrlSlug));
        }
    }
}
