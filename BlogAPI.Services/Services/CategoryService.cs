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
    public class CategoryService
    {
        private readonly IRepository<Category, DbContext> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContext> _unitOfWork;

        public CategoryService(IRepository<Category, DbContext> categoryRepository, IMapper mapper, IUnitOfWork<DbContext> unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ServiceResponse<List<CategoryDto>> FetchAll()
        {
            try
            {
                var categories = _categoryRepository.FetchAll();
                var categoryList = categories.Select(category => _mapper.Map<Category, CategoryDto>(category)).ToList();
                return new ServiceResponse<List<CategoryDto>> { Data = categoryList, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<CategoryDto>> { Success = false, Message = ex.Message };
            }
        }
    }
}
