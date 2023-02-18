using FORUMAPPLICATION.SERVICELAYER;
using System.Web.Http;
namespace UIFORUMAPP.ApiControllers
{
    public class QuestionsController : ApiController
    {
        IAnswerService asr;

        public QuestionsController(IAnswerService asr)
        {
            this.asr = asr;
        }

        public void post (int AnswerID , int UserID , int value)
        {
            this.asr.updateAnswerVotesCount (AnswerID , UserID , value);
        }
    }
}