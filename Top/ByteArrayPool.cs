namespace Top
{
    public class ByteArrayPool
    {
        public static readonly ByteArrayPool Instance = new ByteArrayPool();
        
        // pool with number N store arrays of size 2^N bytes
        private readonly Pool<byte[]>[] pools = new Pool<byte[]>[32];

        private ByteArrayPool()
        {
            for (var i = 0; i < pools.Length; i++)
            {
                var size = 1 << i;
                pools[i] = new Pool<byte[]>(new PoolConfig<byte[]>(() => new byte[size]));
            }
        }

        public Disposable<byte[]> Acquire(int size)
        {
            return pools[FindNextPowerOf2(size)].Acquire();
        }

        private static int FindNextPowerOf2(int x)
        {
            var val = 1;
            var power = 0;
            
            while (val < x)
            {
                val <<= 1;
                power++;
            }

            return power;
        }
    }
}