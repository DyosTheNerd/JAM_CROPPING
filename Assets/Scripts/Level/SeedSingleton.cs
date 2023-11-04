
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

    }
