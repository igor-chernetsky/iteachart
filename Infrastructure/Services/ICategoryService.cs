using Infrastructure.EF.Domain;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    interface ICategoryService
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<Category> GetCategoriesByParent(int parentId);

        Category GetCategoryById(int id);

        void AddCategory(Category cat);

        void EditCategory(Category cat);
    }

    public class CategoryService: ICategoryService
    {
        private IRepository<Category> catRepo;

        public CategoryService(IRepository<Category> catRepo)
        {
            this.catRepo = catRepo;
        }

        public IEnumerable<Category> GetCategories()
        {
            List<Category> result = catRepo.Query().ToList();
            return result;
        }

        public IEnumerable<Category> GetCategoriesByParent(int parentId)
        {
            List<Category> result = catRepo.Query().Where(c=>c.ParentId == parentId).ToList();
            return result;
        }

        public Category GetCategoryById(int id)
        {
            Category result = catRepo.Get(id);
            return result;
        }

        public void AddCategory(Category cat)
        {
            catRepo.Add(cat);
        }

        public void EditCategory(Category cat)
        {
            catRepo.Update(cat);
        }
    }
}
