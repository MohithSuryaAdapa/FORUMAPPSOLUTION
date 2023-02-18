using FORUMAPPLICATION.SERVICELAYER;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.WebApi;

namespace UIFORUMAPP
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IQuestionService, QuestionService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IAnswerService, AnswerService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}