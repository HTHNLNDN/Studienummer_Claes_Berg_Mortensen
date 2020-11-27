using Studienummer_Claes_Berg_Mortensen.Core;

namespace Studienummer_Claes_Berg_Mortensen.Events
{
    public class UserBalanceNotificationArgs
    {
        User user { get; }

        public UserBalanceNotificationArgs(User user)
        {
            this.user = user;
        }


    }
}
