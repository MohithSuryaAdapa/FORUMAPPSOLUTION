using AutoMapper;
using FORUMAPPLICATION.DATAMODELS;
using FORUMAPPLICATION.REPOSITORIES;
using FORUMAPPLICATION.ServiceLayer;
using FORUMAPPLICATION.VIEWMODEL;
using System.Collections.Generic;
using System.Linq;

namespace FORUMAPPLICATION.SERVICELAYER
{
    public interface IAnswerService
    {
        void InsertAnswer(NewAnswerViewModel avm);
        void UpdateAnswer(EditAnswerViewModel avm);
        void DeleteAnswer(int aid);
        void updateAnswerVotesCount(int aid, int uid, int value);
        List<AnswerViewModel> GetAnswersByQuestionID(int qid);
        AnswerViewModel GetAnswersByAnswerID(int Answerid);
    }
    public class AnswerService: IAnswerService
    {
        IAnswersRepository _ar;
        public AnswerService()
        {
            _ar=new AnswersRepository();
        }
        public void DeleteAnswer(int aid)
        {
            _ar.DeleteAnswer(aid);
        }
        public AnswerViewModel  GetAnswersByAnswerID(int Answerid)
        {
            Answer a = _ar.GetAnswersByAnswerID(Answerid).FirstOrDefault();
            AnswerViewModel avm = null;
            if(a!=null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.IgnorUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<Answer, AnswerViewModel>(a);
            }
            return avm;
        }

        public List<AnswerViewModel> GetAnswerByQuestionID(int qid)
        {
            List<Answer> a = _ar.GetAnswersByQuestionID(qid);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.IgnorUnmapped();
            });
            IMapper mappermapper = config.CreateMapper();
            List<AnswerViewModel> avm = Mapper.Map<List<Answer>, List<AnswerViewModel>>(a);
            return avm;
        }

        public void InsertAnswer(NewAnswerViewModel avm)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewAnswerViewModel, Answer>();
                cfg.IgnorUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<NewAnswerViewModel, Answer>(avm);
            _ar.InsertAnswer(a);

        }

        public void UpdateAnswer(EditAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<EditAnswerViewModel, Answer>();
                cfg.IgnorUnmapped();
            });
            IMapper mapper= config.CreateMapper();
            Answer a = mapper.Map<EditAnswerViewModel, Answer>(avm);
            _ar.UpdateAnswer(a);
        }

        public void updateAnswerVotesCount(int aid, int uid, int value)
        {
            _ar.UpdateAnswerVotesCount(aid, uid, value);
        }

        public List<AnswerViewModel> GetAnswersByQuestionID(int qid)
        {
            throw new System.NotImplementedException();
        }
    }

    }

