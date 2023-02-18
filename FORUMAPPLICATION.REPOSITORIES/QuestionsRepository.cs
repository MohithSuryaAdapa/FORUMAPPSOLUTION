using FORUMAPPLICATION.DATAMODELS;
using System.Collections.Generic;
using System.Linq;


namespace FORUMAPPLICATION.REPOSITORIES
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question q);
        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int qid,int value);
        void UpdateQuestionAnswersCount(int qid,int value);
        void UpdateQuestionViewsCount(int qid);
        void DeleteQuestion(int qid);
        List<Question>GetQuestions();
        Question GetQuestionByQuestionID(int qid);
        void UpdateQuestionViewsCount(int qid, object value);
    }
    public class QuestionsRepository : IQuestionsRepository
    {
        private ForumAppDbContext _dbContext;
        public QuestionsRepository()

        {
            _dbContext = new ForumAppDbContext();
        }
        public void DeleteQuestion(int qid)
        {
            var qus = _dbContext.Questions.Where(X => X.QuestionID == qid).FirstOrDefault();
            _dbContext.Questions.Remove(qus);
            _dbContext.SaveChanges();
         }

        public Question GetQuestionByQuestionID(int qid)
        {
            return _dbContext.Questions.Find(qid);
        }
        public List<Question> GetQuestions()
        {
            return _dbContext.Questions.ToList();
        }
        public void InsertQuestion(Question q)
        {
            _dbContext.Questions.Add(q);
            _dbContext.SaveChanges();
        }
        public void UpadateQuestionAnswerCount(int qid,int value)
        {
            var qus = _dbContext.Questions.Where(X=>X.QuestionID == qid).FirstOrDefault();
            if(qus!=null)
            {
                qus.AnswersCount += value;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateQuestionDetails(Question q)
        {
            var qus = _dbContext.Questions.Where(X=>X.QuestionID==q.QuestionID).FirstOrDefault();
            if(qus!= null) 
            {
                qus.QuestionName=q.QuestionName;
                qus.QuestionDateAndTime=q.QuestionDateAndTime;
                qus.CategoryID=q.CategoryID;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int qid)
        {
           var qus= _dbContext.Questions.Where(X => X.QuestionID == qid).FirstOrDefault();
            if(qus !=null)
            {
                qus.ViewsCount += 1;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int qid, object value)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateQuestionVotesCount(int qid , int value)
        {
            var qus = _dbContext.Questions.Where(X=>X.QuestionID==qid).FirstOrDefault();
            if(qus!=null) 
            {
                qus.VotesCount += value;
                _dbContext.SaveChanges();
            }
        }
    }

}
