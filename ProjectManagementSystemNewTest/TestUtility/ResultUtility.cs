using ProjectManagementSystem.Utility;

namespace ProjectManagementSystemNewTest.TestUtility
{
    public class ResultUtility
    {
        public static Result<T> GetSuccessResult<T>(T data)
        {
            Result<T> result = new()
            {
                IsSuccessfull = true,
                Data = data
            };

            return result;
        }
    }
}
