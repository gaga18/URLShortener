using System.Net;

namespace Workabroad.Core.Application.Exceptions
{
    public class ActionProhibitedException : EntityValidationException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotAcceptable;

        /// <summary>
        /// Action Prohibited
        /// </summary>
        public ActionProhibitedException(string message) : base(message) { }
    }
}
