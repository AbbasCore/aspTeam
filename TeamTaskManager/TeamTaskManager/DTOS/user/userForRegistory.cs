using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamTaskManager.DTOS.user
{
    public class userForRegistory
    {
        [Required]
        public string username { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "this is not email")]
        public string email { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "this is not phone number")]
        public string phoneNumber { get; set; }
        public int teamIdFK { get; set; }
    }
}
