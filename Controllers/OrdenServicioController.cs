using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dominio.Model.Soporte;
using Dominio.Persistence;
using Dominio.Persistence.Repository;
using PrimerParcial.Models;
using PrimerParcial.ViewModel;

namespace PrimerParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenServicioController : ControllerBase
    {
        private readonly IOrdenServicioRepository _ordenServicioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClienteRepository _cliente_repository;

        public OrdenServicioController(IOrdenServicioRepository ordenServicioRepository,
            IUnitOfWork unitOfWork, IClienteRepository clienteRepository)
        {
            _ordenServicioRepository = ordenServicioRepository;
            _unitOfWork = unitOfWork;
            _cliente_repository = clienteRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrderService([FromBody] OrdenServicioViewModel orden)
        {
            try
            {
                Cliente obj_cliente = await _cliente_repository.GetCliente(orden.Cliente);
                OrdenServicio obj = new OrdenServicio(
                    cliente: obj_cliente,
                    tipo: orden.TipoServicio,
                    precio: orden.Precio,
                    producto: orden.Producto,
                    descripcion_servicio: orden.Descripcion_Servicio
                    );
                await _ordenServicioRepository.Insert(obj);
                await _unitOfWork.Commit();
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
                throw;
            }
        }
    }
}
