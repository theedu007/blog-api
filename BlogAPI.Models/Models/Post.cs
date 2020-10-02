using Edu.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.Models
{
    public class Post : IEntity
    {
        private readonly ILazyLoader _lazyLoader;
        private IList<PostsTags> _tags;
        private User _user;
        private Category _category;

        public Post()
        {

        }
        public Post(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string PostContent { get; set; }
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public User User 
        { 
            get => _lazyLoader.Load(this, ref _user); 
            set => _user = value; 
        }
        public Category Category { 
            get => _lazyLoader.Load(this, ref _category); 
            set => _category = value; 
        }
        public IList<PostsTags> Tags 
        { 
            get => _lazyLoader.Load(this, ref _tags); 
            set => _tags = value; 
        }
    }
}
