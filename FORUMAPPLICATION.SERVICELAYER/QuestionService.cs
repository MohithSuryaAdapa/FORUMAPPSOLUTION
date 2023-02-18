using AutoMapper;
using FORUMAPPLICATION.DATAMODELS;
using FORUMAPPLICATION.REPOSITORIES;
using FORUMAPPLICATION.ServiceLayer;
using FORUMAPPLICATION.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUMAPPLICATION.SERVICELAYER
{
    public interface IQuestionService
    {

        void InsertQuestion(NewQuestionViewModel qvm);
        void UpdateQuestionDetails(EditQuestionViewModel q);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid , int value);
        void DeleteQuestion(int qid );
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionID(int qid, int UserID);
    }
    public class QuestionService : IQuestionService
    {
        IQuestionsRepository qr;

        public QuestionService()
        { 
            qr = new QuestionsRepository();
        }
        public void DeleteQuestion(int qid)
        {
            qr.DeleteQuestion(qid);
        }

        public QuestionViewModel GetQuestionByQuestionID(int qid, int UserID=0)
        {
            Question q = qr.GetQuestionByQuestionID(qid: qid).FirstOrDefault();
            QuestionViewModel qvm=null;
            if(q!=null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Question,QuestionViewModel>();
                    cfg.IgnorUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                qvm = mapper.Map<Question,QuestionViewModel>(q);

                foreach(var item in qvm.Answers)
                {
                    item.CurrentUserVoteType = 0;
                    VoteViewModel vote = item.Votes.Where(temp=>temp. UserID == UserID).FirstOrDefault(); 
                    if(vote !=null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }
            return qvm;
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<Question>q=qr.GetQuestions();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question,QuestionViewModel>();
                cfg.IgnorUnmapped();
            });
            IMapper mapper=config.CreateMapper();
            List<QuestionViewModel>qvm=mapper.Map<List<Question>, List<QuestionViewModel>>(q);
            return qvm;
        }

        public void InsertQuestion(NewQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewQuestionViewModel, Question>(); cfg.IgnorUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<NewQuestionViewModel, Question>(qvm);
            qr.InsertQuestion(q);
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
           qr.UpdateQuestionAnswersCount(qid, value);
        }

        public void UpdateQuestionDetails(EditQuestionViewModel qvm)
        {
            var config=new MapperConfiguration(cfg => 
            { 
                cfg.CreateMap<NewQuestionViewModel , Question>();
                cfg.IgnorUnmapped(); 
            });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<EditQuestionViewModel, Question>(qvm);
        }

        public void UpdateQuestionViewsCount(int qid , int value)
        {
            qr.UpdateQuestionViewsCount(qid,value);
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            qr.UpdateQuestionVotesCount(qid,value);
        }
    }
}
