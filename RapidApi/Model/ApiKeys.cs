using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RapidApi.Model
{
    public class ApiKeys
    {
        [Key]
        public string ApiKey { get; set; }

        public string? ipAdress { get; set; }

        public DateTime? RequestTime { get; set; }

        public string? MailAdress { get; set; }
    }

    public class query
    {
        private readonly IConfiguration _configuration;
        public query(IConfiguration configuration)
        {

            _configuration = configuration;

        }

        public void things()
        {
            var getValue = _configuration.GetValue<string>("ConnectionStrings:RapidDatabase");

        }
    }  //Configuration kullandığım taraf


}
