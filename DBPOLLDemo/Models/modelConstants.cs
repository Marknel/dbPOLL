//Define all namespace level "constant" classes here

namespace DBPOLLDemo.Models
{
    /// <summary>
    /// Static class containing constants for all user types to ENHANCE readability
    /// </summary>
    public static class User_Type
    {

        /// <summary>
        /// User level 0 in database
        /// </summary>
        public const int SYSTEM_ADMINISTRATOR = 0;
        /// <summary>
        /// User level 1 in database
        /// </summary>
        public const int POLL_USER = 1;
        /// <summary>
        /// User level 2 in database
        /// </summary>
        public const int POLL_MASTER = 2;
        /// <summary>
        /// User level 3 in database
        /// </summary>
        public const int POLL_CREATOR = 3;
        /// <summary>
        /// User level 4 in database
        /// </summary>
        public const int POLL_ADMINISTRATOR = 4;
    }



}