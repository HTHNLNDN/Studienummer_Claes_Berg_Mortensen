using Studienummer_Claes_Berg_Mortensen.Core;
using Studienummer_Claes_Berg_Mortensen.Interfaces;
using System;

namespace Studienummer_Claes_Berg_Mortensen
{
    class Program
    {
        static void Main(string[] args)
        {
            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            StregsystemController sc = new StregsystemController(ui, stregsystem);

            ui.Start();
        }
    }
}
