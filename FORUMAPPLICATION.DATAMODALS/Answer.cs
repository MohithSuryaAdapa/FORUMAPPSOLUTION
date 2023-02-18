using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;

namespace FORUMAPPLICATION.DATAMODELS
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnswerDateAndTime{ get; set; }
        public int UserID { get; set; } 
        public int QuestionID { get; set; } 
        public int VotesCount { get; set; }


        [ForeignKey("UserID")]
        public virtual users Users { get; set; }
       

        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }

        public virtual List<Vote> Votes { get; set; }

        public class users
        {
        }
    }
}
