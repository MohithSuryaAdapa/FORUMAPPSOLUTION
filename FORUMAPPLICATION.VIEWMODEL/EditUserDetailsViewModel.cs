using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUMAPPLICATION.VIEWMODEL
{
    public class EditUserDetailsViewModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Name { get; set; }
        [Required]
        public string Mobile { get; set; }
    }
}
