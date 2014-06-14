﻿using Infrastructure.EF.Domain;
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

        IEnumerable<Category> GetCategoriesByParent(int? parentId);

        Category GetCategoryById(int id);

        void AddCategory(Category cat);

        void EditCategory(Category cat);

        void DeleteCategory(int id);

        void FillDB();
    }

    public class CategoryService: ICategoryService
    {
        public void FillDB(){
            //remove all cats
            foreach(Category c in catRepo.Query().ToList()){
                catRepo.Remove(c);
            }

            catRepo.Add(new Category
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
                        Name = "ASP.NET",
                    }
                }
            });
            catRepo.Add(new Category
            {
                Name = "Java",
                ChildCategories = new List<Category>
                {
                    new Category
                    {
                        Name = "Struts 2",
                    },
                    new Category
                    {
                        Name = "Spring",
                    }
                }
            });
            
            catRepo.Add(new Category
            {
                Name = "JavaScript",
                ChildCategories = new List<Category>
                {
                    new Category
                    {
                        Name = "Knockout JS",
                    },
                    new Category
                    {
                        Name = "Angular JS",
                    },
                    new Category
                    {
                        Name = "Backbone JS",
                    }
                }
            });

        }

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

        public IEnumerable<Category> GetCategoriesByParent(int? parentId)
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


        public void DeleteCategory(int id)
        {
            Category cat = catRepo.Get(id);
            catRepo.Remove(cat);
        }
    }
}
