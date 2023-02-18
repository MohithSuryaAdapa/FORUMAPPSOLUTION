
using System.Collections.Generic;
using System.Linq;
using FORUMAPPLICATION.DATAMODELS;


namespace FORUMAPPLICATION.REPOSITORIES
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer a);
        void UpdateAnswer(Answer a);
        void DeleteAnswer(int aid); 
        void UpdateAnswerVotesCount(int aid, int uid, int value);   
        List<Answer>GetAnswersByQuestionID(int qid);
        List<Answer>GetAnswersByAnswerID(int aid);
    }
    public class AnswersRepository: IAnswersRepository
    {
        private ForumAppDbContext _dbContext;
        private IQuestionsRepository _qr;
        private IVotesRepository _vr;
        public AnswersRepository()
        {
            _dbContext = new ForumAppDbContext();
            _qr = new QuestionsRepository();
            _vr = new VotesRepository();
        }
        public void DeleteAnswer(int aid)
        {
            Answer ans = _dbContext.Answers.Where(temp => temp.AnswerID == aid).First();
            if(ans!=null)
            {
                _dbContext.Answers.Remove(ans);
                _dbContext.SaveChanges();
                _qr.UpdateQuestionAnswersCount(ans.QuestionID, -1);

            }
        }
        public List<Answer> GetAnswersByAnswerID(int aId)
        {
           List<Answer> ans = _dbContext.Answers.Where(temp=>temp.AnswerID== aId).ToList();
            return ans;
        }

        public List<Answer> GetAnswersByQuestionID(int qid)
        {
            List<Answer> ans= _dbContext.Answers.Where(temp=> temp.QuestionID == qid).OrderByDescending(temp=>temp.AnswerDateAndTime).ToList();
            return ans;
        }

        public void InsertAnswer(Answer a)
        {
            _dbContext.Answers.Add(a);
            _dbContext.SaveChanges();
            _qr.UpdateQuestionAnswersCount(a.QuestionID, 1);
        }
        public void UpdateAnswer(Answer a)
        {
            Answer ans = _dbContext.Answers.Where(X => X.AnswerID == a.AnswerID).FirstOrDefault();
            if (ans != null)
            {
                ans.AnswerText = a.AnswerText;
                ans.AnswerDateAndTime = a.AnswerDateAndTime;
                _dbContext.SaveChanges();
            }
        }
        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer ans = _dbContext.Answers.Where(X => X.AnswerID ==aid).FirstOrDefault();
            if(ans != null) 
            {
                ans.VotesCount += value;
                _dbContext.SaveChanges();
                _qr.UpdateQuestionVotesCount(ans.QuestionID, value);
                _vr.UpdateVote(aid, uid,value);
            }
        }
    }
}
