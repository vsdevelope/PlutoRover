using FluentAssertions;
using NUnit.Framework;
using PlutoRover.UnitTests.Helpers;
using System.Collections.Generic;
using System.Drawing;

namespace PlutoRover.UnitTests
{
    [TestFixture]
    public class PlanetTests
    {
        private Pluto _sut;
        private IList<Point> _obstacles;

        [Test(Description ="SCENARIO: Get Obstacles")]
        public void GivenPlanetHasObstacles_WhenRequestedToGetObstacles_ShouldReturnObstacles()
        {
            //Arrange 
            _sut = new Pluto();
            _obstacles = new List<Point>
            {
                new Point(0,0),
                new Point(1,1),
                new Point (2,2)
            };
            _sut.SetObstacles(_obstacles);

            //Act
            var obstaclesStr = _sut.GetObstacles();

            //Assert
            obstaclesStr.Should().ContainAll("(0,0)", "(1,1)", "(2,2)");
        }
    }
}
