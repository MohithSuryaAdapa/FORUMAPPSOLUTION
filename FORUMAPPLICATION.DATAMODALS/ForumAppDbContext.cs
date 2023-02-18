using System.Data.Entity;

namespace FORUMAPPLICATION.DATAMODELS
{
    public class ForumAppDbContext:DbContext
    {
        public DbSet<User> users { get; set; }  
        public DbSet<Category> categories { get; set; } 
        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
