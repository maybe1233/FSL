namespace FSL.Services
{
    public class ServiceScontrini : IServiceScontrini
    {
        private readonly ZeusContext _context;

        public ServiceScontrini(ZeusContext context)
        {
            _context = context;
        }

        // Implementa i metodi del servizio qui
    }
}
