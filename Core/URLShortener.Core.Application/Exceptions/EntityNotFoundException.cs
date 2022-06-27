using System.Net;

namespace Workabroad.Core.Application.Exceptions
{
    public class EntityNotFoundException : EntityValidationException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        /// <summary>
        /// Entity Not Found
        /// </summary>
        public EntityNotFoundException(string message) : base(message) { }
    }
}
