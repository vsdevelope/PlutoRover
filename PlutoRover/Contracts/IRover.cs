namespace PlutoRover.Contracts
{
 
    /// <summary>
    /// Defines capabilties of Rover
    /// </summary>
    public interface IRover
    {
        public bool Forward();
        public bool Back();
        public void TurnLeft();
        public void TurnRight();

        public bool ProcessCommands(string commands);

        public string GetCurrentPosition();
    }
}
