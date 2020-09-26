using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.Models
{
    public class PostsTags
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
        public Post Post { get; set; }
        public Tags Tag { get; set; }
    }
}
