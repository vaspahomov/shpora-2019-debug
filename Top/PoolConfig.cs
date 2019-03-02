using System;

namespace Top
{
    public class PoolConfig<T>
    {
        public Func<T> CreateInstance { get; }
        public Action<T> Cleanup { get; }
        public PoolConfig(
            Func<T> createInstance=null,
            Action<T> cleanup=null)
        {   
            Cleanup = cleanup ?? delegate { };
            CreateInstance = createInstance ?? Activator.CreateInstance<T>;
        }
    }
}