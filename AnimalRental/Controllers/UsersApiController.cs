using System.Web.Http;
using Umbraco.Web.WebApi;

namespace AnimalRental.Controllers
{
    /// <summary>
    /// Class for user entity management
    /// </summary>
    public class UsersApiController : UmbracoApiController
    {
        /// <summary>
        /// Gets a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetUserById(int id)
        {
            return new { userName = Members.GetById(id).Name };
        }

        /// <summary>
        /// Gets currently loged-in user's id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetCurrentUser()
        {
            var member = Members.GetCurrentMember();
            if (member != null)
            {
                return new { userId = member.Id };
            }
            return new { userId = -1 };
        }

    }
}
