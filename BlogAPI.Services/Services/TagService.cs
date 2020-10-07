using AutoMapper;
using BlogAPI.Models.DTOS;
using BlogAPI.Models.Models;
using Edu.Repository;
using Edu.UnitOfWork;
using Edu.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogAPI.Services.Services
{
    public class TagService
    {
        private readonly IRepository<Tags, DbContext> _tagRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContext> _unitOfWork;

        public TagService(IRepository<Tags,DbContext> tagRepository, IMapper mapper, IUnitOfWork<DbContext> unitOfWork)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ServiceResponse<List<TagDto>> FetchAll()
        {
            try
            {
                var tags = _tagRepository.FetchAll();
                var tagList = tags.Select(tag => _mapper.Map<TagDto>(tag)).ToList();
                return new ServiceResponse<List<TagDto>> { Data = tagList, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<TagDto>> { Success = false, Message = ex.Message };
            }
        }

        public ServiceResponse<string> CreateTag(TagDto tagDto)
        {
            try
            {
                var serviceResponse = new ServiceResponse<string>();
                
                var newTag = new Tags {
                    Name = tagDto.Name,
                    Description = tagDto.Description,
                    UrlSlug = tagDto.UrlSlug
                };
                _tagRepository.Add(newTag);
                _unitOfWork.Save();

                serviceResponse.Message = "Tag creada con exito";
                serviceResponse.Success = true;
                return serviceResponse;


            }
            catch (Exception ex)
            {
                return new ServiceResponse<string> { Message = ex.Message, Success = false };
            }
        }

        public ServiceResponse<string> UpdateTag(TagDto tagDto)
        {
            try
            {
                var tagToUpdate = _tagRepository.Find(tagDto.Id);
                var serviceResponse = new ServiceResponse<string>();
                if (tagDto == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Tag no encontrada";
                    return serviceResponse;
                }

                tagToUpdate.Name = tagDto.Name;
                tagToUpdate.Description = tagDto.Description;
                tagToUpdate.UrlSlug = tagDto.UrlSlug;
                _unitOfWork.Save();

                serviceResponse.Success = true;
                serviceResponse.Message = "Entidad editada con exito";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string> { Message = ex.Message, Success = false };
            }
        }

        public ServiceResponse<string> DeleteTag(TagDto tagDto)
        {
            try
            {
                var tagToDelete = _tagRepository.Find(tagDto.Id);
                var serviceRespone = new ServiceResponse<string>();

                if (tagToDelete == null)
                {
                    serviceRespone.Success = false;
                    serviceRespone.Message = "Tag no encontrada";
                    return serviceRespone;
                }

                _tagRepository.Delete(tagToDelete);
                _unitOfWork.Save();

                serviceRespone.Success = true;
                serviceRespone.Message = "Tag eliminada con exito!";
                return serviceRespone;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string> { Success = false, Message = ex.Message };
            }
        }
    }
}
