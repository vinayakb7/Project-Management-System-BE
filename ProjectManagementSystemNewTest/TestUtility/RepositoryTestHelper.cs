namespace ProjectManagementSystemNewTest.TestUtility
{
    /// <summary>
    /// Helper Class for repository test classes.
    /// </summary>
    public class RepositoryTestHelper
    {
        private const string APP_SETTINGS_JSON = "AppSettings.json";

        /// <summary>
        /// Method to get <see cref="IUnitOfWork"/> for repository test cases.
        /// </summary>
        /// <returns></returns>
        public static UnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(GetConfiguration());
        }

        /// <summary>
        /// Method to build Configuration.
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(APP_SETTINGS_JSON);
            return configurationBuilder.Build();
        }
    }
}
