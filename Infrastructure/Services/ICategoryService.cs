using Infrastructure.EF.Domain;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<Category> GetCategoriesByParent(int parentId);

        Category GetCategoryById(int id);

        void AddCategory(Category cat);

        void EditCategory(Category cat);

        void FillDB();
    }

    public class CategoryService : ICategoryService
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
            List<Category> result = catRepo.Query().Where(c => c.ParentId == parentId).ToList();
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

        public void FillDB()
        {
            var categories = new List<Category>();
            categories.Add(new Category
            {
                Name = ".NET",
                ChildCategories = new List<Category>
                {
                    new Category
                    {
                        Name = "WinForms",
                    },
                    new Category
                    {
                        Name = "ASP.NET Web Forms"
                    },
                    new Category
                    {
                        Name = "ASP.NET MVC"
                    }
                }
            });
            
            categories.Add(new Category
            {
                Name = "Java",
                ChildCategories = new List<Category>
                {
                    new Category
                    {
                        Name = "Java Servlets",
                    },
                    new Category
                    {
                        Name = "Struts 2 framework"
                    },
                    new Category
                    {
                        Name = "Spring framework"
                    }
                }
            });
            
            categories.Add(new Category
            {
                Name = "CSS",
                ChildCategories = new List<Category>
                {
                    new Category
                    {
                        Name = "CSS3",
                    },
                    new Category
                    {
                        Name = "CSS Best practice"
                    }
                }
            });
            foreach (var category in categories)
            {
                catRepo.Add(category);
            }
        }
    }
}
