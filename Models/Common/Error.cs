using Models.Common.Enums;

namespace Models.Common
{
    public class Error
    {
        public Error(string message, ErrorTypes type)
        {
            Message = message;
            Type = type;
            StatusCode = (int)type;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ErrorTypes Type { get; set; }
    }
}
