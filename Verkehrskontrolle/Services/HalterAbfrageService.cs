using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Interfaces;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Services
{
    public class HalterAbfrageService : IHalterAbfrageService
    {
        private readonly VerkehrskontrolleDbContext _context;

        public HalterAbfrageService(VerkehrskontrolleDbContext context) { _context = context; }
        /// <summary>
        /// Prüft ob der Halter der FührerscheinId das Auto mit dem angegebenen Kennzeichen fahren darf.
        /// </summary>
        /// <param name="kennzeichen"></param>
        /// <param name="fuehrerscheinId"></param>
        /// <returns></returns>
        public async Task<bool> GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsync(string kennzeichen, int fuehrerscheinId)
        {        
            var fahrzeug = await _context.Fahrzeuge.FirstOrDefaultAsync(f => f.Kennzeichen == kennzeichen);
            var führerschein = _context.Fuehrerscheine.FirstOrDefault(x => x.Id == fuehrerscheinId);

            if(führerschein == null || fahrzeug == null)
            {
                return false;
            }

            if (!führerschein.LKWErlaubnis)
            {
                if (!fahrzeug.Fahrzeugtyp.Contains("LKW"))
                {
                    return false;
                }
            }

            if (!führerschein.PKWErlaublnis)
            {
                if (!fahrzeug.Fahrzeugtyp.Contains("PKW"))
                {
                    return false;
                }
            }
            if (!führerschein.AnhängerErlaubnis)
            {
                if (!fahrzeug.Fahrzeugtyp.Contains("Anhänger"))
                {
                    return false;
                }
            }
            return true;            
        }

        /// <summary>
        /// Erhält eine Liste der erlaubten Fahrzeugtypen für einen Führerschein
        /// </summary>
        /// <param name="fuehrerscheinId"></param>
        /// <returns></returns>
        public async Task<ICollection<string>> GetFahrzeugtypErlaubnisByFuehrerscheinnummerAsync(int fuehrerscheinId)
        {
            var führerschein = _context.Fuehrerscheine.FirstOrDefault(x => x.Id == fuehrerscheinId);
            var erlaubnisListe = new List<string>();

            if (führerschein.AnhängerErlaubnis)
            {
                erlaubnisListe.Add("Anhänger");
            }

            if (führerschein.LKWErlaubnis)
            {
                erlaubnisListe.Add("LKW");
            }

            if (führerschein.PKWErlaublnis)
            {
                erlaubnisListe.Add("PKW");
            }

            return erlaubnisListe;
        }

        public async Task<bool> GetFührerscheinIstGültigByFuehrerscheinnummerAsync(int fuehrerscheinId)
        {
            var getFuehrerscheinById = await _context.Fuehrerscheine.FirstOrDefaultAsync(f => f.Id == fuehrerscheinId);
            
            if (getFuehrerscheinById.Gültigkeit > DateTime.Now) return true;
            
            else return false;
        }

        public async Task<Halter> GetHalterDetailsByKennzeichenAsync(string kennzeichen)
        {
            var fahrzeug = await _context.Fahrzeuge.FirstOrDefaultAsync(f => f.Kennzeichen == kennzeichen);

            if (fahrzeug != null)
            {

                var halter = fahrzeug.Halter;
                return halter;
            }
            else
            {
                return null;
            }



        }

        public async Task<Halter> GetHalterDetailsByNameUndGeburtsdatumAsync(string vorname, string nachname, DateTime geburtsdatum)
        {
            Halter halter = new Halter();
            halter = await _context.Halter.FirstOrDefaultAsync(h => (h.Vorname == vorname) && (h.Nachname == nachname) && (h.Geburtsdatum == geburtsdatum));
            return halter;
        }
    }
}
