﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Interfaces;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Services
{
    public class HalterAbfrageService : IHalterAbfrageService
    {

        private readonly VerkehrskontrolleDbContext _context;

        public HalterAbfrageService(VerkehrskontrolleDbContext context)
        {
            _context = context;
        }

        public Task<bool> GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsync(string kennzeichen, int fuehrerscheinId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<string>> GetFahrzeugtypErlaubnisByFuehrerscheinnummerAsync(int fuehrerscheinId)
        {
            var fuehrerschein = await _context.Fuehrerscheine.FirstOrDefaultAsync(f => f.Id == fuehrerscheinId);
         
            ICollection<string> result = new List<string>();

            if (fuehrerschein.PKWErlaublnis == true) result.Add("PKW");
            if (fuehrerschein.AnhängerErlaubnis == true) result.Add("Anhänger");
            if (fuehrerschein.LKWErlaubnis == true) result.Add("LKW");
            if (fuehrerschein.PKWErlaublnis == false && fuehrerschein.AnhängerErlaubnis == false && fuehrerschein.LKWErlaubnis == false) result.Add("Keine Fahrzeuge erlaubt");

            return result;

        }

        public async Task<bool> GetFührerscheinIstGültigByFuehrerscheinnummerAsync(int fuehrerscheinId)
        {
            var getFuehrerscheinById = await _context.Fuehrerscheine.FirstOrDefaultAsync(f => f.Id == fuehrerscheinId);
            
            if (getFuehrerscheinById.Gültigkeit > DateTime.Now) return true;
            
            else return false;
        }

        public async Task<Halter> GetHalterDetailsByKennzeichenAsync(string kennzeichen)
        {
            var fahrzeug = await _context.Fahrzeuge.FindAsync(kennzeichen);

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

        public Task<Halter> GetHalterDetailsByNameUndGeburtsdatumAsync(string vorname, string nachname, DateTime geburtsdatum)
        {
            throw new NotImplementedException();
        }
    }
}