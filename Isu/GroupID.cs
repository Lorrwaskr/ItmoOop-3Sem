namespace Isu
{
    public class GroupID
    {
        private int _count;

        public GroupID(int start)
        {
            _count = start;
        }

        public int MakeID()
        {
            return _count++;
        }
    }
}