using System.Collections.Generic;

namespace Sdk.Api.Dtos
{
    public class ApiResponseDto<T>
    {
        public T Data { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class ApiResponseDto : ApiResponseDto<object>
    {

    }

    public class Error
    {
        public Error(ExceptionType exceptionType, string name, params string[] messages)
        {
            Type = exceptionType.ToString();
            Name = name;
            Messages = messages;
        }

        public string Type { get; }
        public string Name { get; set; }
        public string[] Messages { get; }
    }

    public enum ExceptionType
    {
        Validation,
        Custom,
        System
    }
}
