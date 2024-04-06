using System.ComponentModel.DataAnnotations;

namespace RapidApi.Model
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        public string url { get; set; }

        public string title { get; set; }

        public string description { get; set; }
    }
}
