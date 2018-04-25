using System.Linq;

namespace CMSCore.Shared.Abstractions.Types.Results
{
    public interface IOperationResult
    {
        bool Succeeded { get; set; }
        string Message { get; set; }
    }

    public class OperationResult : IOperationResult
    {
        public OperationResult() => Succeeded = true;

        public OperationResult(string errorMessage)
        {
            Succeeded = false;
            Message = errorMessage;
        }

        public static OperationResult Success => new OperationResult();

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public static OperationResult Failed(params string[] errors) =>
            new OperationResult(errors != null && errors.Any()
                ? string.Join(". ", errors)
                : "Failed");
    }
}