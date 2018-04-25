namespace CMSCore.Shared.Abstractions
{
    public interface IOperationResult
    {
        bool Succeeded { get; set; }
        string Message { get; set; }
    }
}
