using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public class Error(string code, string descriptoin, ErrorType errorType = ErrorType.Failure )
    {
        public string Code { get; } = code;
        public string Descriptoin { get; } = descriptoin;
        public ErrorType ErrorType { get; } = errorType;

        public static Error Failure(string code = "General.Failure", string discription = "General Failure Error",
            ErrorType type = ErrorType.Failure)
             => new Error(code, discription, type);

        public static Error Validation(string code = "General.Validation", string discription = "General Validation Error",
            ErrorType type = ErrorType.Validation)
             => new Error(code, discription, type);


        public static Error NotFound(string code = "General.NotFound", string discription = "Entity Not Found",
           ErrorType type = ErrorType.NotFound)
            => new Error(code, discription, type);

        public static Error Conflict(string code = "General.Conflict", string discription = "General Conflict Error",
          ErrorType type = ErrorType.Conflict)
           => new Error(code, discription, type);

        public static Error Unauthorized(string code = "General.Unauthorized", string discription = "You Are Anonmous, Go Login",
            ErrorType type = ErrorType.Unauthorized)
             => new Error(code, discription, type);

        public static Error Forbidden(string code = "General.Forbidden", string discription = "You Don't Have The Permission",
            ErrorType type = ErrorType.Forbidden)
             => new Error(code, discription, type);

    }
    
    
        [JsonConverter(typeof(JsonStringEnumConverter))]
 
        
    public enum ErrorType
    {
        Failure,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden
    }
}
