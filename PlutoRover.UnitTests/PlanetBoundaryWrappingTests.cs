using FluentAssertions;
using NUnit.Framework;
using Planets;
using PlutoRover.Contracts;
using PlutoRover.UnitTests.Helpers;
using System.Collections.Generic;
using System.Drawing;

namespace PlutoRover.UnitTests
{
    [TestFixture]
    public class PlanetBoundaryWrappingTests
    {
        private IRover _sut;
        private Planet _planet;
        private IList<Point> _obstacles;

        [SetUp]
        public void BeforeEachTest()
        {
            _planet = new Pluto();

            _obstacles = new List<Point>
            {
                new Point(8,8)
            };
           
            _planet.SetObstacles(_obstacles);
        }

        [Test(Description = "SCENARIO: Should wrap around while facing North")]
        public void GivenFacingNorth_AndOnTheBottomLeft_WhenMovesBackward_ShouldMoveToTopLeft()
        {
            //Arrange 
            _sut = new Rover(_planet, -10, -10, Direction.N);
            var expcPositionString = "-10, 10, N";

            //Act
            var isSuccessfulMove = _sut.ProcessCommands("B");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccessfulMove.Should().BeTrue("Wrapping On BottomLeft boundary failed");
            expcPositionString.ToString().Should().Be(currentPosition);
        }

        [Test(Description = "SCENARIO: Should wrap around while facing West")]
        public void GivenFacingWest_AndOnTheTopLeft_WhenMovesForward_ShouldMoveToTopRight()
        {
            //Arrange 
            _sut = new Rover(_planet, -10, 10, Direction.W);
            var expcPositionString = "10, 10, W";

            //Act
            var isSuccessfulMove = _sut.ProcessCommands("F");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccessfulMove.Should().BeTrue("Wrapping On TopLeft boundary failed");
            expcPositionString.ToString().Should().Be(currentPosition);
        }

        [Test(Description = "SCENARIO: Should wrap around while facing East")]
        public void GivenFacingEast_AndOnTheBottomRight_WhenMovesForward_ShouldMoveToBottomLeft()
        {
            //Arrange 
            _sut = new Rover(_planet, 10, -10, Direction.E);
            var expcPositionString = "-10, -10, E";

            //Act
            var isSuccessfulMove = _sut.ProcessCommands("F");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccessfulMove.Should().BeTrue("Wrapping On BottomRight boundary failed");
            expcPositionString.ToString().Should().Be(currentPosition);
        }


        [Test(Description = "SCENARIO: Should wrap around while facing South")]
        public void GivenFacingSouth_AndOnTheTopRight_WhenMovesBackward_ShouldMoveToBottomRight()
        {
            //Arrange 
            _sut = new Rover(_planet, 10, 10, Direction.S);
            var expcPositionString = "10, -10, S";

            //Act
            var isSuccessfulMove = _sut.ProcessCommands("B");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccessfulMove.Should().BeTrue("Wrapping On TopRight boundary failed");
            expcPositionString.ToString().Should().Be(currentPosition);
        }
    }
}
