using Dominio.Model.Soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerParcial.ViewModel
{
    public class DetalleServicioViewModel
    {
        public Guid Id { get; set; }
        public int TipoServicio { get; set; }
        public double Precio { get; set; }
        public OrdenServicio OrdenServicio { get; set; }
        public string Producto { get; set; }
        public string Descripcion_Servicio { get; set; }
    }
}
