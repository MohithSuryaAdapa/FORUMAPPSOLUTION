using AutoMapper;
using FORUMAPPLICATION.DATAMODELS;
using FORUMAPPLICATION.REPOSITORIES;
using FORUMAPPLICATION.ServiceLayer;
using FORUMAPPLICATION.VIEWMODEL;
using System.Collections.Generic;

namespace FORUMAPPLICATION.SERVICELAYER
{
    public interface ICategoryService
    {

        void InsertCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);
        void DeleteCategory(int cid);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByCategoryID(int categoryId);
    }
    public class CategoryService : ICategoryService
    {
        ICategoryRepository cr;
        public CategoryService()
        {
            cr = new CategoryRepository();
        }
        public void DeleteCategory(int cid)
        {
            cr.DeleteCategory(cid);

        }

        public List<CategoryViewModel> GetCategories()
        {
            List<Category> c = cr.GetCategories();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.IgnorUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> cvm = mapper.Map<List<Category>, List<CategoryViewModel>>(c);
            return cvm;
        }

        public CategoryViewModel GetCategoryByCategoryID(int categoryId)
        {
            Category c = cr.GetCategoryByCategoryID(categoryId).FirstOrDefault();
            CategoryViewModel cvm = null;
            if(c !=null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.IgnorUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Category, CategoryViewModel>(c);
            }
            return cvm;
        }

        public void InsertCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.IgnorUnmapped();
            });

            Category c = Mapper.Map<CategoryViewModel, Category>(cvm);
            cr.InsertCategory(c);
            

        }

        public void UpdateCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.IgnorUnmapped();               
            });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(cvm);
            cr.UpdateCategory(c);
        }
    }
}
