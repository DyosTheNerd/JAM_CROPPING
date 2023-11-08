    using System;
    using Random = Unity.Mathematics.Random;
    public class SeedSingleton
    {
        private static SeedSingleton instance;

        public uint seed;
        
        public static SeedSingleton getInstance()
        {
            if (instance == null)
            {
                instance = new SeedSingleton();
                instance.seed = 12345879;
            }

            return instance;
        }

        public void randomizeSeed()
        {
            Random r = new Random(seed);


            this.seed = r.NextUInt();
        }

        public uint getInitialRandomSeed()
        {
            DateTime currentTime = DateTime.UtcNow;

            // Calculate the epoch time (in seconds)
            uint epochTime = (uint)(currentTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

            Random r = new Random(epochTime);
            return r.NextUInt();
        }
        
    }
