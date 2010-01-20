using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Home;
using NHibernate;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.PasswordReset
{
    public class PasswordResetHandler : Handler
    {
        readonly IUserSelectListService userSelectListService;
        readonly ISession session;

        public PasswordResetHandler(IUserSelectListService userSelectListService, ISession session)
        {
            this.userSelectListService = userSelectListService;
            this.session = session;
        }

        public OperationResult Get()
        {
            var resource = new PasswordResetResource {Users = userSelectListService.AllUsers()};
            return OK(resource);
        }

        public OperationResult Post(PasswordResetResource resource)
        {
            var user = session.Get<User>(resource.UserId);
            user.SetPassword(resource.NewPassword);

            return SeeOther<HomeResource>();
        }
    }
}