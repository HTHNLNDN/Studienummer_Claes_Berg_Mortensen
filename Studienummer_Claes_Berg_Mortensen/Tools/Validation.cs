using System.Text.RegularExpressions;

namespace Studienummer_Claes_Berg_Mortensen.Tools
{
    static class Validation
    {
        /// <summary>
        /// Validates wether or not a name is longer than 0 characters
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static public bool NameValidation(string name)
        {
            if (name.Length > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// validates wether or not a username has the correct characters
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        static public bool UsernameVAlidation(string username)
        {
            string usernamePattern = @"^[a-zA-Z0-9_]+$";
            if ((username.Length > 0) && (Regex.IsMatch(username, usernamePattern)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// validates wether or not an email is valid based on certain characters
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        static public bool EmailValidation(string email)
        {
            string emailPattern = @"^[\w\.\-\,]+@[a-zA-Z0-9][a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]+$";
            return Regex.IsMatch(email, emailPattern);
                

        }

        /// <summary>
        /// validates wether or not a value in null, returns true if it is not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueForValidation"></param>
        /// <returns></returns>
        static public bool NullValidation<T>(T valueForValidation)
        {
            if (valueForValidation != null)
                return true;
            else
                return false;
        }
    }
}
