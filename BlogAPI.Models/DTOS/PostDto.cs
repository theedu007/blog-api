using BlogAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.DTOS
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string PostContent { get; set; }
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public string PostedOn { get; set; }
        public string UpdatedAt { get; set; }
        public CategoryDto Category { get; set; }
        public IList<TagDto> Tags { get; set; }
    }
}
