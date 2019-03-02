using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Diagnostics.Runtime;

namespace ClrMD
{
    public class ClrValueClassFactory
    {
        private static readonly Func<ulong, ClrType, bool, ClrValueClass> factory;
        
        static ClrValueClassFactory()
        {
            var ctor = typeof(ClrValueClass).GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic, 
                null,
                new[]{typeof(ulong), typeof(ClrType), typeof(bool)},
                null);

            var entryAddr = Expression.Parameter(typeof(ulong));
            var type = Expression.Parameter(typeof(ClrType));
            var interior = Expression.Parameter(typeof(bool));

            factory = Expression
                .Lambda<Func<ulong, ClrType, bool, ClrValueClass>>(
                    Expression.New(ctor, entryAddr, type, interior),
                    entryAddr, type, interior)
                .Compile();
        }

        public static ClrValueClass Create(ulong address, ClrType type, bool interior=true) =>
            factory(address, type, interior);
    }
}