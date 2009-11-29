using System;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core
{
    public abstract class Handler
    {
        protected OperationResult.OK OK(object responseResource)
        {
            return new OperationResult.OK(responseResource);
        }

        protected OperationResult.OK OK<T>() where T : new()
        {
            return new OperationResult.OK(new T());
        }

        protected OperationResult.BadRequest BadRequest(object responseResource)
        {
            return new OperationResult.BadRequest {ResponseResource = responseResource};
        }

        protected OperationResult.SeeOther SeeOther(Uri redirectLocation)
        {
            return new OperationResult.SeeOther {RedirectLocation = redirectLocation};
        }

        protected OperationResult.SeeOther SeeOther<T>()
        {
            return SeeOther(typeof(T).CreateUri());
        }

        protected OperationResult.SeeOther SeeOther(object resource)
        {
            return SeeOther(resource.CreateUri());
        }
    }
}