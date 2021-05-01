using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvalucacionTecnica.DAL.Entities
{
    public class EmailModel
    {
        public string paraTo { get; set; }
        public string asunto { get; set; }
        public string Body { get; set; }
    }
}
