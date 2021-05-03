using System.Drawing;

namespace PlutoRover.Model
{

    /// <summary>
    /// Position of Rover with it's (x,y) co-ordinates and direction of facing.
    /// </summary>
    public class Position
    {
        public Point Location { get; set; }
        public Direction Direction { get; set; }

        public override string ToString()
        {
            return $"{Location.X}, {Location.Y}, {Direction}";
        }
    }
}
