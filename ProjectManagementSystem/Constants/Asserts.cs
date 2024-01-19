using System;
namespace ProjectManagementSystem.Constants
{
    /// <summary>
    /// Class to handle asserts.
    /// </summary>
    public class Asserts
    {
        /// <summary>
        /// Method to check whether given object is not null.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Boolean IsNotNull(object obj, string message)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(message);
            }
            return true;
        }

        /// <summary>
        /// Method to check whether condition is true or not.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Boolean IsTrue(bool condition, string message)
        {
            if (!condition)
            {
                throw new ArgumentNullException(message);
            }
            return true;
        }

        /// <summary>
        /// Method to check whether string is empty or not.
        /// </summary>
        /// <param name="stringValue"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Boolean IsNotEmpty(string stringValue, string message)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                throw new ArgumentNullException(message);
            }
            return true;
        }
    }
}
