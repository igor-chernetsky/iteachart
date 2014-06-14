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
                        Name = "C#",
                        Questions =  new List<Question>{
                            new Question{
                                QuestionString = "Value type to Pointer type is",
                                Type = Code.QuestionTypes.text,
                                Answer = "Boxing"
                            },
                            new Question{
                                QuestionString = "Short way to increment",
                                Type = Code.QuestionTypes.text,
                                Answer = "++"
                            },
                            new Question{
                                QuestionString = "Class that is used for building stirng",
                                Type = Code.QuestionTypes.text,
                                Answer = "StringBuilder"
                            },
                            new Question{
                                QuestionString = "just write `dog` to be cool",
                                Type = Code.QuestionTypes.text,
                                Answer = "dog"
                            },
                            new Question{
                                QuestionString = "one more silly question",
                                Type = Code.QuestionTypes.text,
                                Answer = "ok"
                            }
                        }
                    },
                    new Category
                    {
                        Name = "ASP.NET",
                        Questions =  new List<Question>{
                            new Question{
                                QuestionString = "What class does the ASP.NET Web Form class inherit form by default",
                                Type = Code.QuestionTypes.text,
                                Answer = "Syste.Web.Form"
                            },
                            new Question{
                                QuestionString = "Default Session Data is stored in ASP.NET",
                                Type = Code.QuestionTypes.text,
                                Answer = "Session Object"
                            },
                            new Question{
                                QuestionString = "... element in the web.config file to run code using the permissions of a specific user",
                                Type = Code.QuestionTypes.text,
                                Answer = "authorization"
                            },
                            new Question{
                                QuestionString = "An altirnative way to show text on web page",
                                Type = Code.QuestionTypes.text,
                                Answer = "label"
                            },
                            new Question{
                                QuestionString = "Why Global.asax is used",
                                Type = Code.QuestionTypes.text,
                                Answer = "Implement app and session level events"
                            },
                            new Question{
                                QuestionString = "Default Session Data is stored in ASP.NET",
                                Type = Code.QuestionTypes.text,
                                Answer = "Session Object"
                            },
                            new Question{
                                QuestionString = "... element in the web.config file to run code using the permissions of a specific user",
                                Type = Code.QuestionTypes.text,
                                Answer = "authorization"
                            },
                            new Question{
                                QuestionString = "An altirnative way to show text on web page",
                                Type = Code.QuestionTypes.text,
                                Answer = "label"
                            },
                            new Question{
                                QuestionString = "Why Global.asax is used",
                                Type = Code.QuestionTypes.text,
                                Answer = "Implement app and session level events"
                            }
                        }
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
