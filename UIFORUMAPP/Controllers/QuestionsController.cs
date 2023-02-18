using FORUMAPPLICATION.SERVICELAYER;
using FORUMAPPLICATION.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace UIFORUMAPP.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionService qs;
        IAnswerService asr;
        ICategoryService cs;

        public QuestionsController(IQuestionService qs, IAnswerService asr, ICategoryService cs)
        {
            this.qs = qs;
            this.asr = asr;
            this.cs = cs;
        }

        public ActionResult view(int id)
        {
            this.qs.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm =this.qs.GetQuestionByQuestionID(id,uid);
            return View(qvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthoriZationFilterAttribute]
        private ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;
            if(ModelState.IsValid) 
            {
                this.asr.InsertAnswer(navm);
                return RedirectToAction("View", "Questions", new { id = navm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                QuestionViewModel qvm=this.qs.GetQuestionByQuestionID(navm.QuestionID,navm.UserID);
                return View("View", qvm);
            }
        }
        [HttpPost]
        public ActionResult EditAnswer(EditAnswerViewModel avm)
        {
            if(ModelState.IsValid) 
            {
                avm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.asr.UpdateAnswer(avm);
                return RedirectToAction("View", new { id = avm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return RedirectToAction("view", new { id =avm.QuestionID });
            }

        }
        public ActionResult Create()
        {
            List<CategoryViewModel>categories=this.cs.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]

        public ActionResult Create(NewQuestionViewModel qvm)
        {
            if (ModelState.IsValid)
            {
                qvm.AnswersCount= 0;
                qvm.ViewsCount= 0;
                qvm.VotesCount= 0;
                qvm.QuestionDateAndTime= DateTime.Now;
                qvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.qs.InsertQuestion(qvm);
                return RedirectToAction ("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
    }
}