namespace ProjectManagementSystem.Models
{
    public class Result
    {
        public bool IsSuccessfull { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }
    }

    public class CustomException : Exception
    {
        public Result Result { get; set; }
        public CustomException(Exception ex) : base(ex.Message, ex)
        {
            Result = new Result() { IsSuccessfull = false, Message = ex.Message , StatusCode = 422};
        }
    }
}
