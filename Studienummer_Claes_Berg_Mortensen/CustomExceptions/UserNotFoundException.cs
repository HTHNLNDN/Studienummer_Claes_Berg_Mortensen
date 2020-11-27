using Studienummer_Claes_Berg_Mortensen.Core;
using System;
using System.Collections.Generic;

namespace Studienummer_Claes_Berg_Mortensen.CustomExceptions
{
    public class UserNotFoundException : Exception
    {
        public string Username { get; set; }

        public UserNotFoundException(string username, string message) : base(message)
        {
            Username = username;
        }

    }
}
