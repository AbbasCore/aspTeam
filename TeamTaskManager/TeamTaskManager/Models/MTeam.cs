using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamTaskManager.Models
{
    [Table("team")]
    public class MTeam
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public string name { get; set; }

        public ICollection <MUser> user { get; set; }
        public MProject project { get; set; }
    }
}
