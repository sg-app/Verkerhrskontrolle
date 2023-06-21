using Verkehrskontrolle.Interfaces;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Services
{
    public class HalterAbfrageService : IHalterAbfrageService
    {
        public Task<bool> GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsync(string kennzeichen, int fuehrerscheinId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<string>> GetFahrzeugtypErlaubnisByFuehrerscheinnummerAsync(int fuehrerscheinId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetFührerscheinIstGültigByFuehrerscheinnummerAsync(int fuehrerscheinId)
        {
            throw new NotImplementedException();
        }

        public Task<Halter> GetHalterDetailsByKennzeichenAsync(string kennzeichen)
        {
            throw new NotImplementedException();
        }

        public Task<Halter> GetHalterDetailsByNameUndGeburtsdatumAsync(string vorname, string nachname, DateTime geburtsdatum)
        {
            throw new NotImplementedException();
        }
    }
}
