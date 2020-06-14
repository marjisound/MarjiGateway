namespace MarjiGateway.Application.Models
{
    public class ErrorModel
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string ParameterName { get; set; }

        public ErrorLevelModel Level { get; set; }
    }

    public enum ErrorLevelModel
    {
        Error,
        Information,
        Warning
    }
}