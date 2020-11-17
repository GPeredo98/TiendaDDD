using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Model.Soporte;
using Dominio.Persistence;
using Dominio.Persistence.Repository;
using PrimerParcial.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using PrimerParcial.ViewModel;

namespace PrimerParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteController(IClienteRepository clienteRepository,
            IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> InsertClient([FromBody] ClienteViewModel cliente)
        {
            try
            {
                Cliente obj = new Cliente(
                    id: cliente.Id,
                    nombres: cliente.Nombres,
                    apellidos: cliente.Apellidos,
                    telefono: cliente.Telefono,
                    correo: cliente.Correo,
                    direccion: cliente.Direccion
                    );

                await _clienteRepository.Insert(obj);
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
