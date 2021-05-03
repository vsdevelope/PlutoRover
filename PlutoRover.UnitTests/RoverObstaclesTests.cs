using FluentAssertions;
using NUnit.Framework;
using Planets;
using PlutoRover.Contracts;
using PlutoRover.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PlutoRover.UnitTests
{
    [TestFixture]
    public class RoverObstaclesTests
    {
        private IRover _sut;
        private Planet _planet;
        private IList<Point> _obstacles;
               
        [SetUp]
        public void BeforeEachTests()
        {
            _planet = new Pluto();
        }

        [Test(Description = "SCENARIO: Setting an obstacle out of planet's grid system should throw exception")]
        public void GivenSettingUpObstacle_WhenOutOfPlanetsRange_ShouldThrowException()
        {
            //Arrange 
            _planet = new Pluto(100,0,100,0);
            _obstacles = new List<Point>
            {
                new Point(-1,-11)
            };

            //Act && Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
             _planet.SetObstacles(_obstacles));
        }

        [Test(Description = "SCENARIO: Should Detect Obstacle On First Vertial Upward Move")]
        public void GivenObstacleIsPresentImmediately_WhenMovesUpward_ShouldNotChangeInPosition()
        {
            //Arrange 
            _obstacles = new List<Point>
            {
                new Point(0,1)
            };
            _planet.SetObstacles(_obstacles);

            _sut = new Rover(_planet, 0, 0, Direction.N);

            var expcPositionString = "0, 0, N";

            //Act
            var isSuccessfulMove = _sut.ProcessCommands("F");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccessfulMove.Should().BeFalse("Rover moved past obstacle");
            expcPositionString.ToString().Should().Be(currentPosition);
        }

        [Test(Description = "SCENARIO: Should Detect Obstacle On First Vertial Backward Move")]
        public void GivenObstacleIsPresentImmediately_WhenMovesDownward_ShouldNotChangeInPosition()
        {
            //Arrange 
            _obstacles = new List<Point>
            {
                new Point(0,-1)
            };
            _planet.SetObstacles(_obstacles);

            _sut = new Rover(_planet, 0, 0, Direction.N);

            var expcPositionString = "0, 0, N";

            //Act
            var isSuccessfulMove = _sut.ProcessCommands("B");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccessfulMove.Should().BeFalse("Rover moved past obstacle");
            expcPositionString.ToString().Should().Be(currentPosition);
        }

        [Test(Description = "SCENARIO: Should Detect Obstacle On First Horizontal Forward Move")]
        public void GivenObstacleIsPresentImmediately_WhenMovesForward_ShouldNotChangeInPosition()
        {
            //Arrange 
            _obstacles = new List<Point>
            {
                new Point(1,0)
            };
            _planet.SetObstacles(_obstacles);

            _sut = new Rover(_planet, 0, 0, Direction.E);

            var expcPositionString = "0, 0, E";

            //Act
            var isSuccessfulMove = _sut.ProcessCommands("F");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccessfulMove.Should().BeFalse("Rover moved past obstacle");
            expcPositionString.ToString().Should().Be(currentPosition);
        }

        [Test(Description = "SCENARIO: Should Detect Obstacle On First Horizontal Backward Move")]
        public void GivenObstacleIsPresentImmediately_WhenMovesBackward_ShouldNotChangeInPosition()
        {
            //Arrange 
            _obstacles = new List<Point>
            {
                new Point(-1,0)
            };
            _planet.SetObstacles(_obstacles);

            _sut = new Rover(_planet, 0, 0, Direction.E);

            var expcPositionString = "0, 0, E";

            //Act
            var isSuccessfulMove = _sut.ProcessCommands("B");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccessfulMove.Should().BeFalse("Rover moved past obstacle");
            expcPositionString.ToString().Should().Be(currentPosition);
        }


        [Test(Description = "SCENARIO: Should Detect Obstacle In the Middle of Moves")]
        public void GivenCommandsToMove_WhenObstacleIsEncounteredInTheMiddle_ShouldReturnLastSuccessfulPoint()
        {
            //Arrange 
            _obstacles = new List<Point>
            {
                new Point(80,80)
            };
            _planet = new Pluto(100, 0, 100, 0);
            _planet.SetObstacles(_obstacles);

            _sut = new Rover(_planet, 70, 70, Direction.N);

            var expcPositionString = "79, 80, E";

            //Act
            var isSuccessfulMove = _sut.ProcessCommands("FFFFFFFFFFRFFFFFFFFFF");
            var currentPosition = _sut.GetCurrentPosition();
            //Assert
            isSuccessfulMove.Should().BeFalse("Rover moved past obstacle");
            expcPositionString.ToString().Should().Be(currentPosition);
        }
    }
}
