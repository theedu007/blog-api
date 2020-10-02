﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.DTOS
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
    }
}
