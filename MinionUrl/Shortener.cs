using Microsoft.Data.SqlClient;
using MinionUrl.Controllers;
using System.Text;

namespace MinionUrl
{
    public class Shortener
    {

        public static string makeShortUrl()
        {
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                sb.Append((char)rand.Next(0x0061, 0x0078));
            }
            //TODO remake identification of short Urls
            sb.Insert(0, 'm');
            Console.Write(sb.ToString());
            return sb.ToString();
        }
    }
}
