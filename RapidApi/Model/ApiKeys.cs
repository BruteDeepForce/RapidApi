using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RapidApi.Model
{
    public class ApiKeys
    {
        [Key]
        public string ApiKey { get; set; }
    }

}
