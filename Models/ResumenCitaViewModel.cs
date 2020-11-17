using Dominio.Model.Soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerParcial.Models
{
    public class ResumenCitaViewModel
    {
        public Guid Cita { get; set; }
        public string DetalleTrabajo { get; set; }
        public DateTime? FechaFormulario { get; set; }
    }
}
