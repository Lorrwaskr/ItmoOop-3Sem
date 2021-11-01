using System;

namespace Isu
{
    public class IdGenerator
    {
        public static Guid MakeID()
        {
            return Guid.NewGuid();
        }
    }
}