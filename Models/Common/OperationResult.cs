﻿namespace Models.Common
{
    public class OperationResult
    {
        public OperationResult()
        {
            Errors = new List<Error>();
        }

        public ICollection<Error> Errors { get; set; }

        public void AppendError(Error error)
        {
            Errors.Add(error);
        }

        public void AppendErrors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Errors.Add(error);
            }
        }

        public void AppendErrors(OperationResult operationResult)
        {
            foreach (var error in operationResult.Errors)
            {
                Errors.Add(error);
            }
        }
    }

    public class OperationResult<T> : OperationResult where T : class
    {
        public T Data { get; init; }

        public OperationResult(T data) : base()
        {
            Data = data;
        }
    }
}
