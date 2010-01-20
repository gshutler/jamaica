using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Home;
using Jamaica.TableFootball.Core.PasswordReset;
using NHibernate;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.PasswordResets
{
    public class SettingAPassword : Specification
    {
        readonly PasswordResetResource resource = new PasswordResetResource();
        OperationResult result;
        User user;

        protected override void Given()
        {
            user = new User("jimbob");

            resource.UserId = "jimbob";
            resource.NewPassword = "shinynew";

            Dependency<ISession>()
                .Stub(x => x.Get<User>("jimbob"))
                .Return(user);

            UriResolver.Add(new UriRegistration("/home", typeof(HomeResource)));
        }

        protected override void When()
        {
            result = Subject<PasswordResetHandler>().Post(resource);
        }
        
        [Then]
        public void RedirectedToHomeResource()
        {
            Verify(result, Is.TypeOf<OperationResult.SeeOther>());
            Verify(result.RedirectLocation, Is.EqualTo(typeof(HomeResource).CreateUri()));
        }

        [Then]
        public void UsersPasswordChanged()
        {
            Verify(user.VerifyPassword("shinynew"), Is.True);
        }
    }
}