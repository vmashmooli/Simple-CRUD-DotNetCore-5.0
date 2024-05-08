using Domain.Common.Enum;

namespace Domain.Common.Helper
{
    public class ApiResponse<T> 
    {
        public int Status { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }

        public ApiResponse(ResponseStatusEnum status, T result, string message)
        {
            this.Result = result;
            this.Status = (int)status;
            this.Message = message;
        }
    }
}