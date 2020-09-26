using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.DTOS
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
