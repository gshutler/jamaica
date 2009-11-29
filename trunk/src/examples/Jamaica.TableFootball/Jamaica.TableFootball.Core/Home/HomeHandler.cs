using System;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.Home
{
    public class HomeHandler : Handler
    {
        public OperationResult Get()
        {
            return OK<HomeResource>();
        }
    }
}