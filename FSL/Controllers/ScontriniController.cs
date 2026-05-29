using FSL.Data;
using FSL.DTOs;
using FSL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FSL.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ScontriniController : ControllerBase
    {
        private readonly IServiceScontrini _ServiceScontrini;
        private readonly ZeusContext _context;
        private readonly IConfiguration _configuration;


        public ScontriniController(IServiceScontrini serviceScontrini, ZeusContext context,IConfiguration configuration)
        {
            _ServiceScontrini = serviceScontrini;
            _context = context;          
            _configuration = configuration;
        }


        [HttpGet("scontrino/{numeroScontrino}")]
        public ActionResult <TestaScontrinoDTO> GetScontrino(string numeroScontrino)
        {
            if (!long.TryParse(numeroScontrino, out long numeroValido))
            {
                return BadRequest("scontrino non valido");
            }

            TestaScontrinoDTO? scontrino = _ServiceScontrini.GetScontrino(numeroValido);

            if (scontrino == null)
            {
                return NotFound("scontrino non trovato");
            }

            return Ok(scontrino);
        }



        [HttpGet("scontrinigiornata/{anno}/{mese}/{giorno}")]
        public ActionResult<List<TestaScontrinoDTO>> GetScontrini(int anno, int mese, int giorno)
        {
            List<TestaScontrinoDTO> scontrini = _ServiceScontrini.GetScontrini(anno, mese, giorno);

            if (scontrini == null)
            {
                return NotFound("scontrino non trovato");
            }

            return Ok(scontrini);
        }


    }
}
