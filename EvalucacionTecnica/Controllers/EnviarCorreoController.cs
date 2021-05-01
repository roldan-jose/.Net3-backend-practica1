using EvalucacionTecnica.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EvalucacionTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviarCorreoController : ControllerBase
    {
        public EnviarCorreoController()
        {

        }

        [HttpGet]
        public IActionResult get()
        {
            string hola = "Hola Mundo";
            return Ok(hola);
        }

        [HttpPost]
        public IActionResult Index(EmailModel model)
        {

            string para = model.paraTo;
            string asunto = model.asunto;
            string body = model.Body;
            MailMessage mm = new MailMessage();
            mm.To.Add(para);
            mm.Subject = asunto;
            mm.Body = body;
            mm.From = new MailAddress("josesitorolmon95@gmail.com");
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("josesitorolmon95@gmail.com", "6%vtC,%j/8,xR@]-T");
            smtp.Send(mm);
            return Ok();
        }
    }
}
