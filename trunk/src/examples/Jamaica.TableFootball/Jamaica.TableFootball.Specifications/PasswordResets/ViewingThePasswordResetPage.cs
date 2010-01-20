using System;
using Jamaica.TableFootball.Core;
using Jamaica.TableFootball.Core.PasswordReset;
using Jamaica.TableFootball.Core.Xhtml;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.PasswordResets
{
    public class ViewingThePasswordResetPage : Specification
    {
        OperationResult result;
        readonly SelectList users = new SelectList();

        protected override void Given()
        {
            Dependency<IUserSelectListService>()
                .Stub(x => x.AllUsers())
                .Return(users);
        }

        protected override void When()
        {
            result = Subject<PasswordResetHandler>().Get();
        }

        [Then]
        public void ResponseIsOK()
        {
            Verify(result, Is.InstanceOf<OperationResult.OK>());
        }

        [Then]
        public void UserRetrieved()
        {
            Verify(result.Response<PasswordResetResource>().Users, Is.SameAs(users));
        }
    }
}