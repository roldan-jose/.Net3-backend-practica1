using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_CURSO.CodeHelp
{
    public class ErrorsHelp
    {
        public static ResponseHttp RespuestaHttp(int StatusCode, string Message)
        {
            return new ResponseHttp()
            {
                StatusCode = StatusCode,
                Message = Message,
                Type = "Error Personalizado"
            };
        }


        public static List<ModelErrors> ModelStateErrors(ModelStateDictionary Modls)
        {
            return Modls.Select(z => new ModelErrors() { Type = "Error de Validación", Key = z.Key, Messages = z.Value.Errors.Select(x => x.ErrorMessage).ToList() }).ToList();
        }



    }

    public class ModelErrors
    {
        public string Type { get; set; }
        public string Key { get; set; }
        public List<string> Messages { get; set; }
    }

    public class ResponseHttp
    {
        public string Type { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
