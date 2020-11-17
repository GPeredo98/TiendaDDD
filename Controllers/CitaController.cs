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
using Tienda.Soporte.SharedKernel.Core;
using PrimerParcial.ViewModel;

namespace PrimerParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITecnicoRepository _tecnicoRepository;
        private readonly ICitaTecnicoRepository _citaTecnicoRepository;
        private readonly IOrdenServicioRepository _ordenServicioRepository;

        public CitaController(ICitaRepository clienteRepository, ITecnicoRepository tecnicoRepository, ICitaTecnicoRepository citaTecnicoRepository, IOrdenServicioRepository ordenServicioRepository,
            IUnitOfWork unitOfWork)
        {
            _citaRepository = clienteRepository;
            _tecnicoRepository = tecnicoRepository;
            _citaTecnicoRepository = citaTecnicoRepository;
            _ordenServicioRepository = ordenServicioRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> InsertCita([FromBody] CitaViewModel cita)
        {
            try
            {
                OrdenServicio obj_orden = await _ordenServicioRepository.GetOrdenServicio(cita.OrdenServicio);

                Cita obj_cita = new Cita(
                        ordenServicio: obj_orden,
                        fechaVisita: cita.FechaVisita,
                        direccion: cita.Direccion,
                        descripcionCita: cita.DescripcionCita
                    );

                await _citaRepository.Insert(obj_cita);

                foreach (var item in cita.Tecnicos)
                {   
                    Tecnico obj_tecnico = await _tecnicoRepository.GetTecnico(item);
                    CitaTecnico obj_cita_tecnico = new CitaTecnico(cita: obj_cita, tecnico: obj_tecnico);
                    await _citaTecnicoRepository.Insert(obj_cita_tecnico);
                }
                
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
