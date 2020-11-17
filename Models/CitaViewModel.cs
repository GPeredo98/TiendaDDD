using Dominio.Model.Soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerParcial.Models
{
    public class CitaViewModel
    {
        public Guid OrdenServicio { get; set; }
        public DateTime FechaVisita { get; set; }
        public string Direccion { get; set; }
        public string DescripcionCita { get; set; }
        public List<Guid> Tecnicos { get; set; }
    }
}
