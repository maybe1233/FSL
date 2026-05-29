using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FSL.Data;
using FSL.DTOs;
using FSL.Utilities;
using FSL.Entities;
namespace FSL.Services
{
    public class ServiceScontrini : IServiceScontrini
    {
        private readonly ZeusContext _context;
        private readonly IConfiguration _configuration;

        public ServiceScontrini(ZeusContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public TestaScontrinoDTO? GetScontrino(long numeroScontrino)
        {
            var testaScontrino = _context.TestaScontrini
        .Include(t => t.RigheScontrino)
        .FirstOrDefault(t => t.NumeroScontrino == numeroScontrino);

            var cassa = _context.Casse
                .FirstOrDefault(c => c.numeroScontrino == numeroScontrino
                                  && c.tipoOperazione == 'S');

            if (testaScontrino == null)
            {
                return null;
            }

            if (testaScontrino.RigheScontrino == null ||
                !testaScontrino.RigheScontrino.Any() ||
                cassa == null)
            {
                return new TestaScontrinoDTO();
            }

            var scontrinoDTO = Converter.Convert(testaScontrino);

            foreach (var riga in testaScontrino.RigheScontrino)
            {
                scontrinoDTO.Prodotti.Add(Converter.Convert(riga));
            }
            

            scontrinoDTO.NumeroProdotti = scontrinoDTO.Prodotti.Sum(p => p.Quantita);
            Utility.SetAppSettings(_configuration, scontrinoDTO);

            return scontrinoDTO;
        }


        public List<TestaScontrinoDTO> GetScontrini(int anno, int mese, int giorno)
        {
            var data = new DateTime(anno, mese, giorno);

            var scontrini = _context.TestaScontrini
                .Where(s => s.DataScontrino.Date == data.Date)
                .ToList();

            var result = new List<TestaScontrinoDTO>();

            foreach (var scontrino in scontrini)
            {
                var cassa = _context.Casse
                    .FirstOrDefault(c =>
                        c.numeroScontrino == scontrino.NumeroScontrino &&
                        c.tipoOperazione == 'S' &&
                        c.numeroCassa == scontrino.NumeroCassa);

                var righe = _context.RigheScontrini
                    .Where(r => r.NumeroScontrino == scontrino.NumeroScontrino)
                    .ToList();

                if (cassa == null || righe.Count == 0)
                    continue;

                var dto = Converter.Convert(scontrino);

                dto.Cassa = Converter.Convert(cassa);
                dto.Prodotti = new List<RigaScontrinoDTO>();

                foreach (var riga in righe)
                {
                    dto.Prodotti.Add(Converter.Convert(riga));
                }

                dto.NumeroProdotti = dto.Prodotti.Sum(p => p.Quantita);

                Utility.SetAppSettings(_configuration, dto);

                result.Add(dto);
            }

            return result;
        }
    }
}