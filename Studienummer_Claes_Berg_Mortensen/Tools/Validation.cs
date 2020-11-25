using System.Text.RegularExpressions;

namespace Studienummer_Claes_Berg_Mortensen.Tools
{
    static class Validation
    {
        static public bool NameValidation(string name)
        {
            if (name.Length > 0)
                return false;
            else
                return true;
        }
        static public bool UsernameVAlidation(string username)
        {
            string usernamePattern = @"^[a-z0-9_]";
            if ((username.Length > 0) && (Regex.IsMatch(username, usernamePattern)))
                return true;
            else
                return false;
        }

        static public bool EmailValidation(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.-]+@\b[a-zA-Z0-9_.-]\.^[a-zA-Z0-9_.-]";
            return Regex.IsMatch(email, emailPattern);
                

        }
        static public bool NullValidation<T>(T valueForValidation)
        {
            if (valueForValidation != null)
                return true;
            else
                return false;
        }
    }
}
