using FORUMAPPLICATION.SERVICELAYER;
using FORUMAPPLICATION.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIFORUMAPP.Controllers
{
        public class HomeController : Controller
        {
            IQuestionService qs;
            ICategoryService cs;

            public HomeController(IQuestionService qs, ICategoryService cs)
            {
                this.qs = qs;
                this.cs = cs;
            }
            public ActionResult Index()
            {
                List<QuestionViewModel> questions = this.qs.GetQuestions().Take(10).ToList();
                return View(questions);
            }
            public ActionResult About()
            {
                return View();
            }
            public ActionResult Contact()
            {
                return View();
            }
            public ActionResult Categories()
            {
                List<CategoryViewModel> categories = this.cs.GetCategories();
                return View(categories);
            }
            [Route("allquestions")]
            public ActionResult Questions()
            {
                List<QuestionViewModel> questions = this.qs.GetQuestions();
                return View(questions);
            }
            public ActionResult Search(string str)
            {
                List<QuestionViewModel> questions = this.qs.GetQuestions().Where(temp => temp.QuestionName.ToLower().Contains(str.ToLower()) || temp.Category.CategoryName.ToLower().Contains(str.ToLower())).ToList();
                ViewBag.str = str;
                return View(questions);
            }
        }
}