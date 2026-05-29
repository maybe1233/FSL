using FSL.DTOs;
namespace FSL.Services
{
    public interface IServiceScontrini
    {
        TestaScontrinoDTO? GetScontrino(long numeroScontrino);
        List<TestaScontrinoDTO> GetScontrini(int anno, int mese, int giorno);
    }
}
