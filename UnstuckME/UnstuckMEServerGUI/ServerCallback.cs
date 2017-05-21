using System;
using System.ServiceModel;
using System.Windows;
using UnstuckMEInterfaces;
using UnstuckME_Classes;

namespace UnstuckMEServerGUI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ServerCallback : IServer
    {
		/// <summary>
		/// Not implemented.
		/// </summary>
		public void GetMessage()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds or removes a user to the ServerRunning GUI list of online users.
		/// </summary>
		/// <param name="value">Adding a user - pass 0, Removing a user - pass 1.</param>
		/// <param name="user">The user being added or removed from the list of online users.</param>
        public void GetUpdate(int value, UserInfo user)
        {
            switch (value)
            {
                case 0:
                    {
                        ((ServerRunning)Application.Current.MainWindow).AddUser(user.EmailAddress, user.Privileges);
                        break;
                    }
                case 1:
                    {
                        ((ServerRunning)Application.Current.MainWindow).RemoveUser(user.EmailAddress);
                        break;
                    }
            }
        }
    }
}