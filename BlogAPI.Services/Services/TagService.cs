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
    }
}
