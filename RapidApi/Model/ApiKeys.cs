using System.ComponentModel.DataAnnotations;

namespace RapidApi.Model
{
    public class ApiKeys
    {
        [Key]
        public string ApiKey { get; set; }
    }
}
