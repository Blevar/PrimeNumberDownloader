using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberDownloader
{
    static class Constants
    {
        public const string SOURCE_URL = "http://compoasso.free.fr/primelistweb/page/prime/liste_online_en.php";
        public const string DEFAULT_FILE_PATH = "primes.txt";
        public const string FIRST_PRIME = "2";

        public const long LAST_PRIME_TOO_LOOK_FOR = 1000000000039;
        public const int DEFAULT_SLEEP = 15; // ms
    }
}
