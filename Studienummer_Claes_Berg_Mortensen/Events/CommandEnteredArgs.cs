namespace Studienummer_Claes_Berg_Mortensen.Events
{
    public class CommandEnteredArgs
    {
        public CommandEnteredArgs(string command)
        {
            this.command = command;
        }

        string command { get; }
    }
}
