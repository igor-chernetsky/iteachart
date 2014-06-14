using Infrastructure.EF.Domain;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IQuestionService
    {
        void CreateVariant(Variant v);
        void DeleteVariant(int id);
        IEnumerable<Variant> GetVariantsForQuestion(int questionId);

        void CreateQuestion(Question q);
        void EditQuestion(Question q);
        void DeleteQuestion(int id);
        Question GetQuestion(int id);
        IEnumerable<Question> GetQuestionsByCategory(int categoryId, int? limit = null);
    }

    public class QuestionService: IQuestionService
    {
        private IRepository<Question> questionRepo;
        private IRepository<Variant> variantRepo;

        public QuestionService(IRepository<Question> questionRepo, IRepository<Variant> variantRepo)
        {
            this.questionRepo = questionRepo;
            this.variantRepo = variantRepo;
        }

        public void CreateVariant(Variant v)
        {
            variantRepo.Add(v);
        }

        public void DeleteVariant(int id)
        {
            Variant varToDel = variantRepo.Get(id);
            variantRepo.Remove(varToDel);
        }

        public IEnumerable<Variant> GetVariantsForQuestion(int questionId)
        {
            IEnumerable<Variant> result = variantRepo.Query().Where(v => v.QuestionId == questionId).ToList();
            return result;
        }

        public void CreateQuestion(Question q)
        {
            questionRepo.Add(q);
        }

        public void EditQuestion(Question q)
        {
            questionRepo.Update(q);
        }

        public void DeleteQuestion(int id)
        {
            Question questionToDelete = questionRepo.Get(id);
            questionRepo.Remove(questionToDelete);
        }

        public IEnumerable<Question> GetQuestionsByCategory(int categoryId, int? limit = null)
        {
            IEnumerable<Question> result = questionRepo.Query()
                .Where(q => q.CategoryId == categoryId);
            if (limit != null)
            {
                result =
                result.OrderBy(q => Guid.NewGuid())
                .Take(limit.Value);
            }
            return result;
        }
        public Question GetQuestion(int id)
        {
            Question result = questionRepo.Get(id);
            return result;
        }
    }
}
