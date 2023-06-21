using System.ComponentModel.DataAnnotations;

namespace Verkehrskontrolle.Models
{
    public class Halter
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Vorname { get; set; }
        [MaxLength(100)]
        public string Nachname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        [MaxLength(100)]
        public string Straße { get; set; }
        public int Hausnummer { get; set; }
        [RegularExpression(@"\d{5}")]
        public int Postleitzahl { get; set; }
        [MaxLength(100)]
        public string Ort { get; set; }
        public int FührerscheinId { get; set; }
        public Führerschein? Führerschein { get; set; }






    }
}
