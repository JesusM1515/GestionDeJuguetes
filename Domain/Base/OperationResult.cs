using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base
{
    public class OperationResult<T>
    {
        public string Message { get; } = string.Empty;
        public bool IsSuccessful { get; }
        public T? Data { get; }

        private OperationResult(bool isSuccessful, T? data, string message)
        {
            this.IsSuccessful = isSuccessful;
            this.Data = data;
            this.Message = message;
        }

        public static OperationResult<T> Success(string message, T? data)
        {
            return new OperationResult<T>(true, data, message);
        }

        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>(false, default, message);
        }
    }
}
