using Infrastructure.EF.Domain;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iteachart.Controllers
{
    [Authorize]
    public class QuizController : BaseSecureController
    {
        private readonly IQuestionService questionService;
        private readonly IAttemptService attemptService;

        public QuizController(IQuestionService questionService, IAttemptService attemptService)
        {
            this.questionService = questionService;
            this.attemptService = attemptService;
        }
        //
        // GET: /Quiz/
        [Authorize]
        public ActionResult Test(int categoryId)
        {
            List<Question> questions = questionService.GetQuestionsByCategory(categoryId, 10).ToList();
            ViewBag.jsonQuestions = Newtonsoft.Json.JsonConvert.SerializeObject(
                questions.Select(q => new { id = q.Id, text = q.QuestionString, answer = string.Empty }).ToList());

            Attempt attempt = new Attempt(){
                CategoryId = categoryId,
                UserId = CurrentUser.Id,
            };
            ViewBag.attemptId = attemptService.CreateAttempt(attempt);
            return View();
        }

        [Authorize]
        public JsonResult Check(int id, string answer, int attemptId)
        {
            bool result = questionService.CheckAnswer(id, ref answer);
            if (result) attemptService.IncreaseScore(attemptId);
            return Json(new { status = result, answer = answer }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult GetResults(int attemptId)
        {
            Attempt attempt = attemptService.GetAttemptById(attemptId);
            return Json(new { score = attempt.Score }, JsonRequestBehavior.AllowGet);
        }
	}
}