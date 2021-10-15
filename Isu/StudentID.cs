namespace Isu
{
    public class StudentID
    {
        private int _count;

        public StudentID(int start)
        {
            _count = start;
        }

        public int MakeID()
        {
            return _count++;
        }
    }
}