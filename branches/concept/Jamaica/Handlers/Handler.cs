using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Web;
using OpenRasta;

namespace Jamaica.Handlers
{
    public abstract class Handler : IHandler
    {
        protected OperationResult NotFound()
        {
            return new OperationResult.NotFound();
        }

        protected OperationResult OK(object resource)
        {
            return new OperationResult.OK(resource);
        }

        protected OperationResult BadRequest(object resource, IList<Error> errors)
        {
            return new OperationResult.BadRequest
                       {
                           ResponseResource = resource,
                           Errors = errors
                       };
        }

        protected OperationResult BadRequest(object resource, params Error[] errors)
        {
            return BadRequest(resource, errors.ToList());
        }

        protected OperationResult BadRequest(object resource, params string[] errorMessages)
        {
            var errors = errorMessages
                .Select(message => new Error {Message = message})
                .ToList();

            return BadRequest(resource, errors);
        }

        protected OperationResult SeeOther(Uri redirectLocation)
        {
            return new OperationResult.SeeOther
                       {
                           RedirectLocation = redirectLocation
                       };
        }
    }
}