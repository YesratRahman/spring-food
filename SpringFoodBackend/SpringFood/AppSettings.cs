using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringFoodBackend
{
    public static class AppSettings
    {
        static Random _random = new Random();
        public static string Secret { get; }
        const int SECRET_SIZE = 128;
        static AppSettings()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < SECRET_SIZE; i++)
            {
                sb.Append((char)_random.Next(32, 128));
            }
            Secret = sb.ToString();
        }

    }
} 
