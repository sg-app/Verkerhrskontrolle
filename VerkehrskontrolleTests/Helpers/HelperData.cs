using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verkehrskontrolle.Models;

namespace VerkehrskontrolleTests.Helpers
{
    public static class HelperData
    {
        public static List<Halter> HalterList()
        {
            List<Halter> list = new();
            var fsCount = FuehrerscheinList().Count;
            for (int i = 0; i < 10; i++)
            {
                var idx = new Random().Next(fsCount);
                list.Add(new Halter()
                {
                    Id = i,
                    Vorname = $"Vorname {i}",
                    Nachname = $"Nachname {i}",
                    Geburtsdatum = DateTime.Today.AddDays(-new Random().Next(8000)),
                    Straße = $"Straße {i}",
                    Hausnummer = new Random().Next(100),
                    Postleitzahl = new Random().Next(10000,99999),
                    Ort = $"Ort {i}",
                    FührerscheinId = FuehrerscheinList()[idx].Id,
                });
            }
            return list;
        }

        public static List<Führerschein> FuehrerscheinList()
        {
            List<Führerschein> list = new();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Führerschein()
                {
                    Id = i,
                    Gültigkeit = DateTime.Today.AddDays(new Random().Next(8000)),
                    PKWErlaublnis = new Random().Next(2) == 0,
                    AnhängerErlaubnis = new Random().Next(2) == 0,
                    LKWErlaubnis = new Random().Next(2) == 0,
                });
            }
            return list;
        }
    }
}
