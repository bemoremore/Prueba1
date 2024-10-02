using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
namespace Prueba1_Real.src.Models
{
    public class User
    {
        
        [RegularExpression(@"[0-9]{8}-[0-9]{1}", ErrorMessage = "Debe ingresar un rut valido.")]
        [Key]
        public string Rut { get; set; } = string.Empty;
        [StringLength(100,MinimumLength = 3,ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [RegularExpression(@"[A-Za-z0-9]+@[A-Za-z0-9]+\.[A-Za-z0-9]+", ErrorMessage = "Debe ingresar un correo valido.")]
        public string correo { get; set; } = string.Empty;
        [RegularExpression(@"masculino|femenino|otro|prefiero no decirlo", ErrorMessage = "Debe ingresar una opción valida.") ]
        public string genero { get; set; } = string.Empty;

        [RegularExpression(@"[0-9]{2}-[0-9]{2}-[0-9]{4}", ErrorMessage = "Debe ingresar una fecha valida.")]
        public string fechaNacimiento { get; set; } = string.Empty;
    }
}