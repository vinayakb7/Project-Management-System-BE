using Microsoft.Extensions.Configuration;
using ProjectManagementSystem.Business;
using ProjectManagementSystem.Utility;

namespace ProjectManagementSystemNewTest.TestUtility
{
    public class ResultUtility
    {
        public static Result<T> GetSuccessResult<T>(T data)
        {
            Result<T> result = new();
            result.IsSuccessfull = true;
            result.Data = data;
            return result;
        }
    }
}
