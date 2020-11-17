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
using Tienda.Soporte.SharedKernel.Core;
using PrimerParcial.ViewModel;

namespace PrimerParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecnicoController : ControllerBase
    {
        private readonly ITecnicoRepository _tecnicoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TecnicoController(ITecnicoRepository tecnicoRepository,
            IUnitOfWork unitOfWork)
        {
            _tecnicoRepository = tecnicoRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> InsertTecnico([FromBody] TecnicoViewModel tecnico)
        {
            try
            {
                Tecnico obj = new Tecnico(
                    nombres: tecnico.Nombres,
                    apellidos: tecnico.Apellidos,
                    telefono: tecnico.Telefono,
                    correo: tecnico.Correo,
                    oficios: tecnico.Oficio
                    );
                await _tecnicoRepository.Insert(obj);
                await _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
                throw;
            }
        }
    }
}
