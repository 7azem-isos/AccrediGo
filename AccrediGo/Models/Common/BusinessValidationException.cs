namespace AccrediGo.Models.Common
{
    public class BusinessValidationException : Exception
    {
        public string MessageCode { get; }

        public BusinessValidationException(string messageCode, string message) : base(message)
        {
            MessageCode = messageCode;
        }

        public BusinessValidationException(string messageCode, string message, Exception innerException) : base(message, innerException)
        {
            MessageCode = messageCode;
        }
    }
} 