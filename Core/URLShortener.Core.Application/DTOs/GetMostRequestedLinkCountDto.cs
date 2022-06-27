namespace Project.Core.Application.DTOs
{
    public class GetMostRequestedLinkCountDto
    {
        public string Note { get; set; }
        public string FwToken { get; set; }
        public int RequestCount { get; set; }
    }
}
