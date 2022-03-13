namespace Common.General
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class SuccessResponse<T> : BaseResponse
    {
        public SuccessResponse()
        {
            Success = true;
        }
        public T Data { get; set; }
    }
    public class ErrorResponse<T> : BaseResponse
    {
        public ErrorResponse()
        {
            Success = false;
        }
        public T Error { get; set; }
    }
}
