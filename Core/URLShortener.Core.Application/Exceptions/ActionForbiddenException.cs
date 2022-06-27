using System.Net;
using Workabroad.Core.Application.Exceptions;

namespace URLShortener.Core.Application.Exceptions
{
    public class ActionForbiddenException : EntityValidationException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

        /// <summary>
        /// Action Prohibited
        /// </summary>
        public ActionForbiddenException(string message) : base(message) { }
    }

}
