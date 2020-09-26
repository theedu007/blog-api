using Edu.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.Models
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string PostContent { get; set; }
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public IList<PostsTags> Tags { get; set; }
    }
}
