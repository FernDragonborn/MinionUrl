using System.ComponentModel.DataAnnotations;

namespace MinionUrl.Models
{
    public class UrlData
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FullUrl { get; set; }
        [Required]
        public string ShortUrl { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreationDateTime { get; set; }

        public UrlData()
        {
            Id = Guid.NewGuid();
            CreationDateTime = DateTime.Now;
        }
        public UrlData(string Url)
        {
            Id = Guid.NewGuid();
            FullUrl = Url;
            ShortUrl = Shortener.makeShortUrl();
            CreationDateTime = DateTime.Now;
        }
        public UrlData(string Url, int CreatorId)
        {
            Id = Guid.NewGuid();
            FullUrl = Url;
            ShortUrl = Shortener.makeShortUrl();
            this.CreatorId = CreatorId;
            CreationDateTime = DateTime.Now;
        }

    }
    public class UrlBuffer
    {
        public Guid? Id { get; set; }
        public string? FullUrl { get; set; }
        public string? ShortUrl { get; set; }
        public int? CreatorId { get; set; }
        public string? CreationDateTime { get; set; }
    }
}
