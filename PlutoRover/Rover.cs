using Planets;
using PlutoRover.Contracts;
using PlutoRover.Model;
using System;
using System.Drawing;

namespace PlutoRover
{

    public class Rover:IRover
    {
        public Position Position { get; private set; }

        private Planet _grid;     //The grid of the planet

        /// <summary>
        /// Initializes the Rover with it's position
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="facing"></param>
        public Rover(Planet grid,int startX=0,int startY=0,Direction facing=Direction.N)
        {
            _grid = grid;

            //check if Rover is positioned on the planet
            if (!(startX <= _grid.MaxX
                && startX >= _grid.MinX
                && startY <= _grid.MaxY
                && startY >= _grid.MinY))
            {
                throw new ArgumentOutOfRangeException($"Rover is not positioned within the Planet co-ordinats: minimumX:{_grid.MinX},maximumX:{_grid.MaxX},minimumY:{_grid.MinY},maximumY:{_grid.MaxY}");
            }

            Position = new Position
            {
                Location=new Point
                {
                    X=startX,
                    Y=startY
                },
                Direction=facing
            };
        }

        /// <summary>
        /// Receive commands input
        /// And Process One By One
        /// </summary>
        /// <param name="commands"></param>
        /// <returns>The outcome of the operation</returns>
        public bool ProcessCommands(string commands)
        {
            var isSuccess = false;
            foreach(var command in commands)
            {
                switch(command)
                {
                    case 'F':isSuccess = Forward();
                        break;
                    case 'B':
                        isSuccess = Back();
                        break;
                    case 'L':
                        TurnLeft();
                        break;
                    case 'R':
                        TurnRight();
                        break;
                    default:
                        isSuccess = false;
                        break;
                }

                if(!isSuccess)
                {
                    break;
                }
            }

            return isSuccess;
        }

        
        /// <summary>
        /// Moves Rover Forward If there is no obstacle ahead
        /// </summary>
        /// <returns>Returns the outcome of the Forward Operation</returns>
        public bool Forward()
        {
            switch (Position.Direction)
            {
                case Direction.N:
                    incrementY();
                    break;

                case Direction.S:
                    decrementY();
                    break;

                case Direction.E:
                    incrementX();
                    break;

                case Direction.W:
                    decrementX();
                    break;
            }

            //if new current position lies on obstacle
            // move to previous position and report failure
            if (_grid.Obstacles.Contains(Position.Location))
            {
                Back();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Moves Rover Backward If there is no obstacle ahead
        /// </summary>
        /// <returns>Returns the outcome of the Back Operation</returns>
        public bool Back()
        {
            switch (Position.Direction)
            {
                case Direction.N:
                    decrementY();
                    break;

                case Direction.S:
                    incrementY();
                    break;

                case Direction.E:
                    decrementX();
                    break;

                case Direction.W:
                    incrementX();
                    break;
            }

            //if new current position lies on obstacle
            // move to previous position and report failure
            if (_grid.Obstacles.Contains(Position.Location))
            {
                Forward();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Turns Rover to the Left without moving from its current location
        /// </summary>
        public void TurnLeft()
        {
            Position.Direction = (Position.Direction - 1) < Direction.N ? Direction.W : Direction.N - 1;
        }

        /// <summary>
        /// Turns Rover to the Right without moving from its current location
        /// </summary>
        public void TurnRight()
        {
            Position.Direction = (Position.Direction + 1) > Direction.W ? Direction.N : Position.Direction + 1;
        }

        /// <summary>
        /// Increments location along X-axis
        /// with wrap around
        /// </summary>
        private void incrementX()
        {
            int newX = Position.Location.X + 1;

            if (newX > _grid.MaxX)
                newX = _grid.MinX;

            Position.Location = new Point(newX, Position.Location.Y);
        }

        /// <summary>
        /// Decrements location along X-axis
        /// with wrap around
        /// </summary>
        private void decrementX()
        {
            int newX = Position.Location.X - 1;

            if (newX < _grid.MinX)
                newX = _grid.MaxX;

            Position.Location = new Point(newX, Position.Location.Y);
        }

        /// <summary>
        /// Increments location along Y-axis
        /// with wrap around
        /// </summary>
        private void incrementY()
        {
            int newY = Position.Location.Y + 1;

            if (newY > _grid.MaxY)
                newY = _grid.MinY;

            Position.Location = new Point(Position.Location.X, newY);
        }

        /// <summary>
        /// Decrements location along Y-axis
        /// with wrap around
        /// </summary>
        private void decrementY()
        {
            int newY = Position.Location.Y - 1;

            if (newY < _grid.MinY)
                newY = _grid.MaxY;

            Position.Location = new Point(Position.Location.X, newY);
        }

        /// <summary>
        /// Get's current location of Rover along with it's direction of travel
        /// </summary>
        /// <returns>Location and Direction</returns>
        /// <example>2,3,N</example>
        public string GetCurrentPosition()
        {
            return Position.ToString();
        }
    }
}