using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.Models
{
    public class PostsTags
    {
        private readonly ILazyLoader _lazyLoader;
        
        public PostsTags()
        {

        }

        public PostsTags(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        private Post _post;
        private Tags _tag;

        public int PostId { get; set; }
        public int TagId { get; set; }
        public Post Post 
        { 
            get => _lazyLoader.Load(this, ref _post); 
            set => _post = value; 
        }
        public Tags Tag 
        { 
            get => _lazyLoader.Load(this, ref _tag); 
            set => _tag = value; 
        }
    }
}
