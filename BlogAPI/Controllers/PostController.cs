using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Models.DTOS;
using BlogAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IActionResult GetAllPost()
        {
            var response = _postService.FetchAll();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{urlSlug}")]
        public IActionResult GetPost(string urlSlug)
        {
            var response = _postService.GetPost(urlSlug);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("Create")]
        public IActionResult CreatePost(PostDto request)
        {
            var response = _postService.CreatePost(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Created("1",response);
        }
    }
}
