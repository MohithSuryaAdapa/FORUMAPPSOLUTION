using System.Collections.Generic;
using System.Linq;
using FORUMAPPLICATION.DATAMODELS;


namespace FORUMAPPLICATION.REPOSITORIES
{
    public interface ICategoryRepository
    {
        void InsertCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(int cId);
        List<Category>GetCategories();
        Category GetCategoryByCategoryID(int categoryId);
    }
    public class CategoryRepository:ICategoryRepository
    {
        private ForumAppDbContext _dbContext;
        public CategoryRepository()

        {
            _dbContext = new ForumAppDbContext();
        }

        public void DeleteCategory(int cid) 
        {
            Category ct=_dbContext.categories.Where(temp=>temp.CategoryID==cid).FirstOrDefault();
            if(ct!=null)
            {
                ct.CategoryName = ct.CategoryName;
                _dbContext.SaveChanges();

            }
        }
        public List<Category>GetCategories() 
        {
            return _dbContext.categories.ToList();
        }
        public Category GetCategoryByCategoryID(int categoryId) 
        {
            return _dbContext.categories.Find(categoryId);
        }

        public void InsertCategory(Category c)
        {
            _dbContext.categories.Add(c);
            _dbContext.SaveChanges();
        }
        public void UpdateCategory (Category c)
        {
            Category ct=_dbContext.categories.Where(temp=>temp.CategoryID==c.CategoryID).FirstOrDefault();
            if(ct!=null)
            {
                ct.CategoryName= c.CategoryName;
                _dbContext.SaveChanges();

            }
        }
    }
}
