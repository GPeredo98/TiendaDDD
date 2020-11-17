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
    public class ResumenCitaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICitaRepository _cita_repository;
        private readonly IResumenCitaRepository _resumenCitaRepository;

        public ResumenCitaController(ICitaRepository citaRepository, IResumenCitaRepository resumenCitaRepository,
            IUnitOfWork unitOfWork)
        {
            _cita_repository = citaRepository;
            _resumenCitaRepository = resumenCitaRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> InsertResumenCita([FromBody] ResumenCitaViewModel resumenCita)
        {
            try
            {
                Cita obj_cita = await _cita_repository.GetCita(resumenCita.Cita);
                ResumenCita obj = new ResumenCita(
                    cita: obj_cita,
                    detalleTrabajo: resumenCita.DetalleTrabajo,
                    fechaFormulario: resumenCita.FechaFormulario
                    );

                await _resumenCitaRepository.Insert(obj);
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
