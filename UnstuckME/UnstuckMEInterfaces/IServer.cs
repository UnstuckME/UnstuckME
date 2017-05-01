using System.ServiceModel;

namespace UnstuckMEInterfaces
{
    [ServiceContract]
    public interface IServer
    {
        /// <summary>
        /// Not implemented.
        /// </summary>
        [OperationContract]
		void GetMessage();

        /// <summary>
        /// Adds or removes a user to the ServerRunning GUI list of online users.
        /// </summary>
        /// <param name="value">Adding a user - pass 0, Removing a user - pass 1.</param>
        /// <param name="user">The user being added or removed from the list of online users.</param>
        [OperationContract]
        void GetUpdate(int value, UnstuckME_Classes.UserInfo user);
    }
}
