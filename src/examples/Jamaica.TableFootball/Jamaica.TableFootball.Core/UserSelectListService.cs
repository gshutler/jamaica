using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Xhtml;
using NHibernate;
using NHibernate.Criterion;

namespace Jamaica.TableFootball.Core
{
    public interface IUserSelectListService
    {
        SelectList AllUsers();
    }

    public class UserSelectListService : IUserSelectListService
    {
        readonly ISession session;

        public UserSelectListService(ISession session)
        {
            this.session = session;
        }

        public SelectList AllUsers()
        {
            var opponents = session.CreateCriteria<User>()
                .AddOrder(Order.Asc("Name"))
                .List<User>();

            return new SelectList(opponents.AsSelectListItems());
        }
    }
}