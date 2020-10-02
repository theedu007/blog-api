using Edu.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.Models
{
    public class Tags : IEntity
    {
        private readonly ILazyLoader _lazyLoader;
        private IList<PostsTags> _posts;

        public Tags()
        {

        }

        public Tags(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public IList<PostsTags> Posts 
        { 
            get => _lazyLoader.Load(this, ref _posts); 
            set => _posts = value; 
        }

    }
}
