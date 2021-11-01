using System;

namespace Shops
{
    public class IdGenerator
    {
        public static Guid MakeID()
        {
            return Guid.NewGuid();
        }
    }
}