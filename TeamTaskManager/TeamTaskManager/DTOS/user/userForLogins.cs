using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamTaskManager.DTOS.user
{
    public class userForLogins
    {
        public string username { get; set; }
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
