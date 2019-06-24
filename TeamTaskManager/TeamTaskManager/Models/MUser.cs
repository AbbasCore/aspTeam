using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamTaskManager.Models
{
    [Table("user")]
    public class MUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string userName { get; set; }
        public string name { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int teamIdFK { get; set; }
        [ForeignKey(nameof(teamIdFK))]
        public  MTeam Team { get; set; }
    }
}
