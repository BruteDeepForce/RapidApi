using System.Security.Cryptography;
using System.Text;

namespace RapidApi.Model
{
    public class GenerateKey
    {
        private readonly Context _context;

        public GenerateKey(Context context)
        {
             _context = context;
        }
        public string Key()
        {
            char[] characters = new char[10];

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                int rndType = rnd.Next(0,3);
                int charCode = 0;

                switch (rndType) 
                
                {
                    case 0:
                        charCode = rnd.Next(65,90); 
                        break;
                    case 1: 
                        charCode = rnd.Next(48 , 58);
                        break;
                    case 2:
                        charCode = rnd.Next(97, 122);
                        break;
                
                }
                characters[i] = Convert.ToChar(charCode);
            } 

            var generated = new string(characters);
            var encode = Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(characters)));

            var api = new ApiKeys { ApiKey = encode };  //önemli burası

            _context.apiKeys.Add(api);
            _context.SaveChanges();


            return new string(encode);

        }
    }
}
