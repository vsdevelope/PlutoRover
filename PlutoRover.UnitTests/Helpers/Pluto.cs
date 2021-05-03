using Planets;
using System.Collections.Generic;
using System.Drawing;

namespace PlutoRover.UnitTests.Helpers
{
    /// <summary>
    /// Concrete Implementation of Planet for Pluto
    /// </summary>
    public sealed class Pluto : Planet
    {
        //Grid boundaries
        public override int MaxX { get; protected set; }
        public override int MinX { get; protected set; }
        public override int MaxY { get; protected set; }
        public override int MinY { get; protected set; }

        //initialize Grid with obstacles
        public Pluto(int maxX = 10, int minX = -10, int maxY = 10, int minY = -10)
        {
            MaxX = maxX;
            MinX = minX;
            MaxY = maxY;
            MinY = minY;

            Obstacles = new HashSet<Point>();
        }
    }
}
