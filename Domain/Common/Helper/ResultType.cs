namespace Domain.Common.Helper
{
    public class ResultType<T>
    {
        public ResultType(string message, bool result, T data)
        {
            Message = message;
            Result = result;
            Data = data;
        }

        public string Message { get; set; }
        public bool Result { get; set; }
        public T Data { get; set; }
    }
}