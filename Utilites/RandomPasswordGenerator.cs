using System;
using System.Text;

namespace WafferAPIs.Utilites
{
    public class RandomPasswordGenerator
    {
        public string Password { get; }
        public RandomPasswordGenerator()
        {
            Password = RandomPassword(10);
        }

        public string RandomPassword(int size)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(new Random().Next(500, 1500));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
