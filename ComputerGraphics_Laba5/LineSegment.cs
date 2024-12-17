using System.Windows;

namespace Laba5
{
    public class LineSegment
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public LineSegment(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }

}
