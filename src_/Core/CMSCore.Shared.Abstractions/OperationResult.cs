using System.Linq;

namespace CMSCore.Shared.Abstractions
{
    public class OperationResult : IOperationResult
    {
        public OperationResult()
        {
            Succeeded = true;
        }

        public OperationResult(string errorMessage)
        {
            Succeeded = false;
            Message = errorMessage;
        }

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public static OperationResult Success => new OperationResult();

        public static OperationResult Failed(params string[] errors) =>
            new OperationResult(errors != null && errors.Any()
                ? string.Join(". ", errors)
                : "Failed");
    }
}