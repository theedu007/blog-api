using AutoMapper;
using BlogAPI.Models.DTOS;
using BlogAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogAPI.Models.AutoMapperProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(post => post.PostedOn, cfg => cfg.MapFrom(source => source.PostedOn.ToString("O")))
                .ForMember(post => post.UpdatedAt, cfg => cfg.MapFrom(source => source.UpdatedAt != null ? source.UpdatedAt.Value.ToString("O") : null))
                .ForMember(post => post.Category, cfg => cfg.MapFrom(new CategoryResolver(), source => source.Category))
                .ForMember(post => post.Tags, cfg => cfg.MapFrom(new TagResolver(), source => source.Tags));
        }
    }

    class TagResolver : IMemberValueResolver<Post, PostDto, IList<PostsTags>, IList<TagDto>>
    {
        public IList<TagDto> Resolve(Post source, PostDto destination, IList<PostsTags> sourceMember, IList<TagDto> destMember, ResolutionContext context)
        {
            var tagList = new List<TagDto>();
            foreach (var item in sourceMember)
            {
                var tagDto = new TagDto
                {
                    Description = item.Tag.Description,
                    Name = item.Tag.Name,
                    UrlSlug = item.Tag.UrlSlug,
                    Id = item.Tag.Id
                };
                tagList.Add(tagDto);
            }
            return tagList;
        }
    }

    class CategoryResolver : IMemberValueResolver<Post, PostDto, Category, CategoryDto>
    {
        public CategoryDto Resolve(Post source, PostDto destination, Category sourceMember, CategoryDto destMember, ResolutionContext context)
        {
            var categoryDto = new CategoryDto
            {
                Id = sourceMember.Id,
                Description = sourceMember.Description,
                Name = sourceMember.Name,
                UrlSlug = sourceMember.UrlSlug
            };
            return categoryDto;
        }
    }
}
