using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using RapidApi.Model;
using System.Reflection.Metadata.Ecma335;
using RapidApi.Authentication;
using System.Net;
using System.Collections.Immutable;

namespace RapidApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RapidController : ControllerBase
    {
        #region DI
        public readonly Context _context;
        private readonly IApiKeyValidation _apiKeyValidation;
        private readonly GenerateKey generaTe;
        private readonly Actions _actions;
        private readonly IMailSender _mailSender;

        #endregion
        #region Ctor
        public RapidController(Context context, IApiKeyValidation apiKeyValidation, GenerateKey gen, Actions actions, IMailSender mailActions)
        {
             _context = context;
             _apiKeyValidation = apiKeyValidation;
             generaTe = gen;
            _actions = actions;
            _mailSender = mailActions;
        }
        #endregion
        [HttpGet("test")]
        public async Task<string> Getter(int season, int data)
        {
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-nba-v1.p.rapidapi.com/teams/statistics?season={season}&id={data}"),
                Headers =
             {
             { "X-RapidAPI-Key", "" },
              { "X-RapidAPI-Host", "api-nba-v1.p.rapidapi.com" },
             },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);

                var json = JsonConvert.DeserializeObject<TeamStatisticsResponse>(body);
              
                if (json.Response != null && json.Response.Count > 0)
                {
                    foreach (var item in json.Response)
                    {
                        return item.Points.ToString() + item.Assists.ToString();
                    }
                }
            }
            return "No data available";
        }  //NBA apisi üzerinden veri çekme


        [HttpPost("UserApiGenerator")]
        public IActionResult Generator(string mailadress)
        {
            var userApiKey = generaTe.Key();
            _mailSender.SendMail(mailadress, userApiKey);
            return Ok(userApiKey);

        } //api Key Generator


        [HttpGet("GetPicture")]
        public IEnumerable<string> PictureUrls([FromBody] RequestModel model)
        {            
            if (string.IsNullOrWhiteSpace(model.ApiKey)) 
            {

                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return null;
            }
            
            bool isValid = _apiKeyValidation.IsExistApiKey(model.ApiKey);

            if (!isValid) {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return null;
            }
            var apikeyEntity= _context.apiKeys.FirstOrDefault(x => x.ApiKey == model.ApiKey);

            apikeyEntity.ipAdress = HttpContext.Connection.RemoteIpAddress.ToString();

            apikeyEntity.RequestTime = DateTime.UtcNow;

            _context.SaveChanges();

            var urls = new List<string>();

            foreach (var item in _context.Pictures)
            {
                urls.Add(item.url.ToString());
            }

            if(urls.Any())
            {
                return urls;

            }
            else
            {
                return new List<string>() { "no list"};
            }

           //  return urls.Any() ? urls : new List<string>() { "no item" };   kısa yol
        }


        [HttpPost("GetSingleUrl")]
        public string SingleUrl([FromBody] RequestModel model, string title)
        {
            if (string.IsNullOrWhiteSpace(model.ApiKey))
            {

                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return null;
            }

            bool isValid = _apiKeyValidation.IsExistApiKey(model.ApiKey);

            if (!isValid)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return null;
            }
            var apikeyEntity = _context.apiKeys.FirstOrDefault(x => x.ApiKey == model.ApiKey);

            apikeyEntity.ipAdress = HttpContext.Connection.RemoteIpAddress.ToString();

            apikeyEntity.RequestTime = DateTime.UtcNow;

            _context.SaveChanges();

            var single = _actions.GetSinglePicture(title);
            if (single == null) { return "No Item"; }

            return single.url.ToString();

            #region
            //var control = _context.Pictures.Where(x=>x.title == title).FirstOrDefault();

            //return control != null ? control.url.ToString() : "No Item";
            #endregion  //iptal edilen taraf.
        }


        [HttpDelete("DeletePicture")]
        public IActionResult Delete(int id) 
        {
            var control = _actions.DeletePic(id);

            if (control == 0) { return BadRequest(); }
            else { return Ok(); }   
        }


        [HttpPost("AddItem")]
        public IActionResult addItem(Picture model)
        {
            var aResp = _actions.add(model);
            if (aResp == 1) { return Ok(); }
            return BadRequest();
        }


        [HttpPut("UpdateItem")]
        public IActionResult updateItem(Picture model) 
        {
            var aResp = _actions.Put(model);
            if(aResp == 1) { return Ok(); };
            return BadRequest();
        }

        [HttpPost("gettest")]
        public IActionResult gettest([FromBody] string data) 
        {
            return Ok();
        }
    }

    public class TeamStatisticsResponse
    {
        public string Get { get; set; }
        public Parameters Parameters { get; set; }
        public List<object> Errors { get; set; }
        public int Results { get; set; }
        public List<TeamStatistics> Response { get; set; }
    }  //NBA apisi üzerine yapılan req response classları. Json deserialize için....

    public class Parameters
    {
        public string Season { get; set; }
        public string Id { get; set; }
    }

    public class TeamStatistics
    {
        public int Games { get; set; }
        public int FastBreakPoints { get; set; }
        public int PointsInPaint { get; set; }
        public int BiggestLead { get; set; }
        public int SecondChancePoints { get; set; }
        public int PointsOffTurnovers { get; set; }
        public int LongestRun { get; set; }
        public int Points { get; set; }
        public int Fgm { get; set; }
        public int Fga { get; set; }
        public string Fgp { get; set; }
        public int Ftm { get; set; }
        public int Fta { get; set; }
        public string Ftp { get; set; }
        public int Tpm { get; set; }
        public int Tpa { get; set; }
        public string Tpp { get; set; }
        public int OffReb { get; set; }
        public int DefReb { get; set; }
        public int TotReb { get; set; }
        public int Assists { get; set; }
        public int PFouls { get; set; }
        public int Steals { get; set; }
        public int Turnovers { get; set; }
        public int Blocks { get; set; }
        public int PlusMinus { get; set; }
    }

}
