using FluentAssertions;
using NUnit.Framework;
using Planets;
using PlutoRover.Contracts;
using System;

namespace PlutoRover.UnitTests
{
    [TestFixture]
    public class RoverSimpleMovesTests
    {
        private IRover _sut;
        private Planet _planet;

        [SetUp]
        public void BeforeEachTests()
        {
            _planet = new Helpers.Pluto();
        }

        [Test(Description = "SCENARIO: Setting an Rovers poisition out of planet's grid system should throw exception")]
        public void GivenSettingRoversPosition_WhenOutOfPlanetsRange_ShouldThrowException()
        {
            //Arrange 
            
            //Act && Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
             new Rover(_planet, 80, 80, Direction.N));
        }

        [Test(Description ="SCENARIO: Set initial position")]
        public void GivenInitialPosition_WhenProbed_ShouldReturnCurrentPosition()
        {
            //Arrange 
            _sut = new Rover(_planet, 0, 0, Direction.N);

            var expcPositionString = "0, 0, N";

            //Act
            var currentPosition = _sut.GetCurrentPosition();

            //Assert

            expcPositionString.ToString().Should().Be(currentPosition);
        }

        [Test(Description = "SCENARIO: Move Horizontally Forward In The Same Direction")]
        public void GivenForwardCommand_WhenRoverMoves_ShouldMoveHorizontally()
        {
            //Arrange 
            _sut = new Rover(_planet, 2, 3, Direction.E);

            var expcPositionString = "3, 3, E";

            //Act
            var isSuccess = _sut.ProcessCommands("F");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccess.Should().BeTrue("Horizontal Forward operation failed");
            expcPositionString.ToString().Should().Be(currentPosition,"Rover is not at expected position");
        }

        [Test(Description = "SCENARIO: Move Horizontally Backward In The Same Direction")]
        public void GivenBackwardCommand_WhenRoverMoves_ShouldMoveBackHorizontally()
        {
            //Arrange 
            _sut = new Rover(_planet, 2, 3, Direction.E);

            var expcPositionString = "1, 3, E";

            //Act
            var isSuccess = _sut.ProcessCommands("B");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccess.Should().BeTrue("Horizontal Backward operation failed");
            expcPositionString.ToString().Should().Be(currentPosition, "Rover is not at expected position");
        }

        [Test(Description = "SCENARIO: Move Vertically Upwards In The Same Direction")]
        public void GivenForwardCommand_WhenRoverMoves_ShouldMoveUpwardsVertically()
        {
            //Arrange 
            _sut = new Rover(_planet, 2, 3, Direction.N);

            var expcPositionString = "2, 4, N";

            //Act
            var isSuccess = _sut.ProcessCommands("F");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccess.Should().BeTrue("Vertial Upwards operation failed");
            expcPositionString.ToString().Should().Be(currentPosition, "Rover is not at expected position");
        }

        [Test(Description = "SCENARIO: Move Vertically Downwards In The Same Direction")]
        public void GivenBackCommand_WhenRoverMoves_ShouldMoveDownwardsVertically()
        {
            //Arrange 
           _sut = new Rover(_planet, 2, 3, Direction.N);

            var expcPositionString = "2, 2, N";

            //Act
            var isSuccess = _sut.ProcessCommands("B");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccess.Should().BeTrue("Vertial Downwards operation failed");
            expcPositionString.ToString().Should().Be(currentPosition, "Rover is not at expected position");
        }

        [Test(Description = "SCENARIO: Move the Rover from Initial position through a series of commands")]
        public void GivenSeriesOfCommands_WhenRoverMoves_ShouldMoveToExpectionPosition()
        {
            //Arrange 
            _sut = new Rover(_planet, 0, 0, Direction.N);

            var expcPositionString = "2, 2, E";

            //Act
            var isSuccess = _sut.ProcessCommands("FFRFF");
            var currentPosition = _sut.GetCurrentPosition();

            //Assert
            isSuccess.Should().BeTrue("Rover Move operation failed");
            expcPositionString.ToString().Should().Be(currentPosition, "Rover is not at expected position");
        }
    }
}
