using AutoMapper;
using BlogAPI.Models.DTOS;
using BlogAPI.Models.Models;
using Edu.Repository;
using Edu.UnitOfWork;
using Edu.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace BlogAPI.Services.Services
{
    public class PostService
    {
        private readonly IRepository<Post, DbContext> _postRepository;
        private readonly IRepository<Category, DbContext> _categoryRepository;
        private readonly IRepository<Tags, DbContext> _tagRepository;
        private readonly IRepository<User, DbContext> _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork<DbContext> _unitOfWork;

        public PostService(
            IRepository<Post, DbContext> postRepository, 
            IRepository<Category, DbContext> categoryRepository, 
            IRepository<Tags, DbContext> tagRepository,
            IRepository<User, DbContext> userRepository,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IUnitOfWork<DbContext> unitOfWork)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _userRepository = userRepository;
            _httpContext = httpContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ServiceResponse<List<PostDto>> FetchAll()
        {
            try
            {
                var posts = _postRepository.FetchAll();
                var postsList = posts.Select(post => _mapper.Map<Post, PostDto>(post));
                return new ServiceResponse<List<PostDto>> { Success = true, Data = postsList.ToList() };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ServiceResponse<PostDto> GetPost(string urlSlug)
        {
            try
            {
                var post = _postRepository.Find(post => post.UrlSlug == urlSlug);
                if (post == null)
                {
                    return new ServiceResponse<PostDto> { Message = "No se encontro el post", Success = false };
                }
                var postDto =_mapper.Map<Post, PostDto>(post);
                return new ServiceResponse<PostDto> { Data = postDto, Success = true };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ServiceResponse<PostDto> { Success = false, Message = ex.Message };
            }
        }

        public ServiceResponse<PostDto> GetPost(int id) 
        {
            try
            {
                var post = _postRepository.Find(id);
                if (post == null)
                {
                    return new ServiceResponse<PostDto> { Message = "No se encontro el post", Success = false };
                }
                var postDto = _mapper.Map<Post, PostDto>(post);
                return new ServiceResponse<PostDto> { Data = postDto, Success = true };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ServiceResponse<PostDto> { Success = false, Message = ex.Message };
            }
        }

        public ServiceResponse<string> CreatePost(PostDto model)
        {
            try
            {
                int userId;
                bool userExists = int.TryParse(_httpContext.HttpContext.User.GetUserId(), out userId);

                if (!userExists)
                {
                    return new ServiceResponse<string> { Success = false, Message = "usuario no encontrado" };
                }

                Category category = _categoryRepository.Find(model.Category.Id);
                User user = _userRepository.Find(userId);

                Post newPost = new Post
                {
                    Category = category,
                    PostContent = model.PostContent,
                    PostedOn = DateTime.Now,
                    Published = model.Published,
                    ShortDescription = model.ShortDescription,
                    Title = model.Title,
                    UrlSlug = model.UrlSlug,
                    User = user,
                };

                List<Tags> tags = _tagRepository.FindMatchesId(model.Tags.Select(tag => tag.Id)).ToList();
                List<PostsTags> postsTagsList = new List<PostsTags>();
                foreach (var tag in tags)
                {
                    PostsTags postTag = new PostsTags { Post = newPost, Tag = tag };
                    postsTagsList.Add(postTag);
                }
                newPost.Tags = postsTagsList;


                _postRepository.Add(newPost);
                _unitOfWork.Save();
                return new ServiceResponse<string> { Success = true, Message = "El post se ha guardado" };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ServiceResponse<string> { Success = false, Message = ex.Message };
            }
        }

        public ServiceResponse<string> UpdatePost(PostDto model)
        {
            try
            {
                ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                var existingPost = _postRepository.Find(model.Id);
                
                if (existingPost == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No se encontro el post";
                    return serviceResponse;
                }

                existingPost.Title = model.Title;
                existingPost.ShortDescription = model.ShortDescription;
                existingPost.PostContent = model.PostContent;
                existingPost.Published = model.Published;
                existingPost.UrlSlug = model.UrlSlug;
                existingPost.UpdatedAt = DateTime.Now;

                var categories = _categoryRepository.Find(model.Id);

                _unitOfWork.Save();
                serviceResponse.Success = true;
                serviceResponse.Message = "Post guardado con exito";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>();
            }
        }

        public ServiceResponse<string> DeletePost(PostDto model)
        {
            try
            {
                ServiceResponse<string> response = new ServiceResponse<string>();
                var existingPost = _postRepository.Find(model.Id);
                if (existingPost == null)
                {
                    response.Success = false;
                    response.Message = "El post no existe";
                }
                _postRepository.Delete(existingPost);
                _unitOfWork.Save();

                response.Success = true;
                response.Message = "El post fue eliminado con exito";
                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string> { Success = false, Message = "Ocurrio un error " };
            }
        }
    }
}
