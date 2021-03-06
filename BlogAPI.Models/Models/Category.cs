﻿using Edu.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.Models
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public IList<Post> Posts { get; set; }
    }
} 
