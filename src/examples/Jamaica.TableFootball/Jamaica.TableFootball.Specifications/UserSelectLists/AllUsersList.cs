using System;
using System.Linq;
using Jamaica.Security;
using Jamaica.TableFootball.Core;
using Jamaica.TableFootball.Core.Xhtml;
using Jamaica.Test;
using NUnit.Framework;
using Rhino.Mocks;

namespace Jamaica.TableFootball.Specifications.UserSelectLists
{
    public class AllUsersList : IntegrationSpecification
    {
        SelectList selectList;

        protected override void Given()
        {
            session.Save(new User("Zebedee"));
            session.Save(new User("Arnold"));
            session.Save(new User("Homer"));

            session.Flush();
        }

        protected override void When()
        {
            selectList = Subject<UserSelectListService>().AllUsers();
        }

        [Then]
        public void ThreePeopleInTheSelectList()
        {
            Verify(selectList.Items.Count(), Is.EqualTo(3));
        }

        [Then]
        public void DisplayedInAlphabeticalOrder()
        {
            var lastDisplay = "";

            foreach (var item in selectList.Items)
            {
                Verify(item.Display, Is.GreaterThanOrEqualTo(lastDisplay));
                lastDisplay = item.Display;
            }
        }

        [Then]
        public void DisplayIsTheSameAsTheSelectValue()
        {
            foreach (var item in selectList.Items)
            {
                Verify(item.Display, Is.EqualTo(item.Value));
            }
        }
    }
}