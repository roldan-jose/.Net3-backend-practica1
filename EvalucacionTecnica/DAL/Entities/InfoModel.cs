using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EvalucacionTecnica.DAL.Entities
{
    public partial class InfoModel
    {
        public int IdPersona { get; set; }
        [Required(ErrorMessage = "El nombre(s) es obligatorio.")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El número teléfonico es obligatorio.")]
        [Display(Name = "Número Teléfonico")]
        public string Telefono { get; set; }
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "Ciudad y Estado son obligatorios.")]
        [Display(Name = "Ciudad y Estado")]
        public string CiudadEstado { get; set; }
    }
}
