using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FerchuBot
{
    public class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;

        public int Next(int dicetype)
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next();
                        _local = new Random(seed);
                    }
                }
            }

            int result = _local.Next(dicetype) + 1;
            return result;
        }
    }
}
