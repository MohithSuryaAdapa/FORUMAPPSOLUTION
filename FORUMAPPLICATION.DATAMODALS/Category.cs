using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace FORUMAPPLICATION.DATAMODELS
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public Category FirstOrDefault()
        {
            throw new NotImplementedException();
        }
    }
}
