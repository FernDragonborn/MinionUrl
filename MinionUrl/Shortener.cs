using System.Text;

namespace MinionUrl
{
    public class Shortener
    {

        public static string makeShortUrl()
        {
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                sb.Append((char)rand.Next(0x0410, 0x44F));
            }
            Console.Write(sb.ToString());

            return sb.ToString();
        }
    }
}
