using FORUMAPPLICATION.SERVICELAYER;
using System.Web.Http;

namespace UIFORUMAPP.ApiControllers
{
    public class AccountController : ApiController
    { 
        IUserService us;

        public AccountController(IUserService us)
        {
            this.us = us;
        }
        public string Get(string Email)
        {
            if(this.us.GetUserByEmail(Email)!=null)
            {
                return "Found";
                
            }
            else
            {
                return "Not Found";
            }
        }
    }
}