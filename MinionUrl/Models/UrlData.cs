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
            ShortUrl = Url;
            CreationDateTime = DateTime.Now;
        }
        public UrlData(string Url, int CreatroId)
        {
            Id = Guid.NewGuid();
            FullUrl = Url;
            ShortUrl = Url;
            this.CreatorId = CreatroId;
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
