namespace URLShortener.Core.Application.DTOs
{
    public class GetLinkDto
    {
        public int Id { get; set; }
        public string OriginUrl { get; set; }
        public string FwToken { get; set; }
        public string Note { get; set; }
        public bool IsEnabled { get; set; }
    }
}
