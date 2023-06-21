using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Interfaces
{
    public interface IHalterAbfrageService
    {

        Task<Halter> GetHalterDetailsByKennzeichenAsync(string kennzeichen);
        Task<Halter> GetHalterDetailsByNameUndGeburtsdatumAsync(string vorname, string nachname, DateTime geburtsdatum);

        Task<bool> GetFührerscheinIstGültigByFuehrerscheinnummerAsync(int fuehrerscheinId);
       
        /// <summary>
        /// Abfrage was Halter alles fahren darf.
        /// </summary>
        /// <param name="fuehrerscheinId"></param>
        /// <returns>Liste der erlaubten Farzeugtypen</returns>
        Task<ICollection<string>> GetFahrzeugtypErlaubnisByFuehrerscheinnummerAsync(int fuehrerscheinId);

        Task<bool> GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsync(string kennzeichen, int fuehrerscheinId);

    }
}
