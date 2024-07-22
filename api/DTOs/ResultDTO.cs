namespace api.DTOs
{
    public class ResultDTO<T>
    {
        public bool IsSucess { get; }
        public string Error { get; }
        public T Value { get; }
        public string Message { get; } 

        private ResultDTO(bool isSucess, T value, string error, string message)
        {
            IsSucess = isSucess;
            Value = value;
            Error = error;
            Message = message;
        }

        public static ResultDTO<T> Success(T value, string message = null)
        {
            return new ResultDTO<T>(true, value, null, message);
        }

        public static ResultDTO<T> Failure(T value, string error = null, string message = null)
        {
            return new ResultDTO<T>(false, value, error, message);
        }
    }
}
