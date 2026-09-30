namespace CountriesManagementSystem.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public List<string> Messages { get; set; }
        public T? Data { get; set; }

        public ApiResponse() { }

        // Success Constructor
        public ApiResponse(T? data, List<string> messages)
        {
            Succeeded = true;
            Messages = messages;
            Data = data;
        }

        // Failure Constructor
        public ApiResponse(List<string> errorMessages)
        {
            Succeeded = false;
            Messages = errorMessages;
            Data = default;
        }
    }

    public class ApiResponse
    {
        public bool Succeeded { get; set; }
        public List<string> Messages { get; set; }

        public ApiResponse() { }

        // Success Constructor
        public ApiResponse(List<string> messages)
        {
            Succeeded = true;
            Messages = messages;
        }

        // Failure Constructor
        public static ApiResponse Success(List<string> messages) => new ApiResponse { Succeeded = true, Messages = messages };
        public static ApiResponse Failure(List<string> errorMessages) => new ApiResponse { Succeeded = false, Messages = errorMessages };
    }
}
