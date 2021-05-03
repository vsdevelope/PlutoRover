using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Planets
{
    /// <summary>
    /// Defines abstract Planet with abstracted Co-ordinates
    /// </summary>
    public abstract class Planet
    {
        public abstract int MaxX { get; protected set; }
        public abstract int MinX { get; protected set; }
        public abstract int MaxY { get; protected set; }
        public abstract int MinY { get; protected set; }
        public HashSet<Point> Obstacles { get; protected set; }

        /// <summary>
        /// Defines the locations of obstacles
        /// </summary>
        /// <param name="points"></param>
        public void SetObstacles(IEnumerable<Point> points)
        {
            foreach (var point in points)
            {
                if (!IsValidPoint(point))
                {
                    throw new ArgumentOutOfRangeException($"Obstacle is positioned out of planet boundaries: minimumX:{MinX},maximumX:{MaxX},minimumY:{MinY},maximumY:{MaxY}");
                }

                if (!Obstacles.Contains(point))
                {
                    Obstacles.Add(point);
                }
            }
        }

        public string GetObstacles()
        {
            if(!Obstacles.Any())
            {
                return "There are no obstacles defined for the planet";
            }

            StringBuilder obstaclesBuilder = new StringBuilder($"Obstacles:{Environment.NewLine}");
            foreach (Point p in Obstacles)
            {
                obstaclesBuilder.Append($"({p.X},{p.Y}){Environment.NewLine}");
            }

            return obstaclesBuilder.ToString();
        }

        private bool IsValidPoint(Point point)
        {
            return (point.X <= MaxX
                    && point.X >= MinX
                    && point.Y <= MaxY
                    && point.Y >= MinY);
        }
    }
}
