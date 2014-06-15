using Infrastructure.EF.Domain;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iteachart.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IQuestionService questionService;

        public CategoryController(ICategoryService categoryService, IQuestionService questionService)
        {
            this.categoryService = categoryService;
            this.questionService = questionService;
        }
        //
        // GET: /Category/
        public ActionResult Index()
        {
            IEnumerable<Category> categories = categoryService.GetCategories();
            return View(categories);
        }

        //
        // GET: /Category/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        //
        // GET: /Category/Create
        public ActionResult Create()
        {
            ViewBag.parentCats = GetParentCategory(true, -1);
            return View();
        }

        //
        // POST: /Category/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Category newCat = new Category(){
                    Name=collection["Name"]
                };
                if (!string.IsNullOrEmpty(collection["parentCat"]))
                {
                    int parentId;
                    if (int.TryParse(collection["parentCat"], out parentId))
                    {
                        newCat.ParentId = parentId;
                    }
                }
                categoryService.AddCategory(newCat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Edit/5
        public ActionResult Edit(int id)
        {
            Category cat = categoryService.GetCategoryById(id);

            ViewBag.parentCats = GetParentCategory(true, cat.ParentId.HasValue ? cat.ParentId.Value : -1);

            return View(cat);
        }

        //
        // POST: /Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Category cat = categoryService.GetCategoryById(id);
                cat.Name = collection["Name"];
                if (!string.IsNullOrEmpty(collection["parentCat"]))
                {
                    int parentId;
                    if (int.TryParse(collection["parentCat"], out parentId))
                    {
                        cat.ParentId = parentId;
                    }
                }
                categoryService.EditCategory(cat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Delete/5
        public ActionResult Delete(int id)
        {
            categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        //
        // POST: /Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Delete/5
        public ActionResult DeleteQuestion(int categoryId, int id)
        {
            questionService.DeleteQuestion(id);
            return RedirectToAction("Edit", new { Id = categoryId });
        }


        //
        // GET: /Category/Create
        public ActionResult CreateQuestion(int categoryId)
        {
            Question q = new Question { CategoryId = categoryId };
            return View(q);
        }

        //
        // POST: /Category/Create
        [HttpPost]
        public ActionResult CreateQuestion(FormCollection collection)
        {
            try
            {
                Question question = new Question
                {
                    QuestionString = collection["QuestionString"],
                    Answer = collection["Answer"],
                    CategoryId = int.Parse(collection["CategoryId"])
                };

                
                questionService.CreateQuestion(question);
                return RedirectToAction("Edit", new { Id = question.CategoryId });
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /Category/Edit/5
        public ActionResult EditQuestion(int id)
        {
            Question q = questionService.GetQuestion(id);
            return View(q);
        }

        //
        // POST: /Category/Edit/5
        [HttpPost]
        public ActionResult EditQuestion(int id, FormCollection collection)
        {
            try
            {
                Question q = questionService.GetQuestion(id);
                q.QuestionString = collection["QuestionString"];
                q.Answer = collection["Answer"];
                questionService.EditQuestion(q);
                return RedirectToAction("Edit", new { id = q.CategoryId });
            }
            catch
            {
                return View();
            }
        }

        #region "Private Methods"

        private IEnumerable<SelectListItem> GetParentCategory(bool withEmpty, int selectedId)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            if (withEmpty)
            {
                result.Add(new SelectListItem()
                {
                    Value = null,
                    Text = "---"
                });
            }
            result.AddRange(categoryService.GetCategoriesByParent(null).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == selectedId
            }));
            return result;
        }

        #endregion
    }
}
