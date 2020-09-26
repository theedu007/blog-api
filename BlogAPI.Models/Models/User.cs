using Edu.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.Models
{
    public class User : IEntity
    {
        private readonly ILazyLoader _lazyLoader;

        public User()
        {

        }
        public User(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        private IList<Post> _posts;

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public IList<Post> Posts { 
            get => _lazyLoader.Load(this, ref _posts); 
            set => _posts = value; 
        }
    }
}
