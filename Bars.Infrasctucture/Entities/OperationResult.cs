using Bars.Infrasctucture.Enums;

namespace Bars.Infrasctucture.Entities
{
    public class OperationResult<T> : OperationResult
    {
        public OperationResult(ResultCode resultCode, T context)
            : base(resultCode, string.Empty)
        {
            Context = context;
        }

        public OperationResult(ResultCode resultCode, string message, T context)
            : base(resultCode, message)
        {
            Context = context;
        }

        public T Context { get; }
    }

    public class OperationResult
    {
        public OperationResult(ResultCode resultCode, string message)
        {
            Code = resultCode;
            Message = message;
        }

        public bool IsSuccess => Code == ResultCode.Success;

        public ResultCode Code { get; }

        public string Message { get; }
    }
}
