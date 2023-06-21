using System.ComponentModel.DataAnnotations;

namespace Verkehrskontrolle.Models
{
    public class Fahrzeug
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Antrieb { get; set; }
        [RegularExpression("[1-9][0-9]")]
        public int Sitze { get; set; }
        [RegularExpression("[0-4][0-9][0-9]")]
        public int Leistung { get; set; }
        public DateTime ZulassungDatum { get; set; }
        public DateTime TüvDatum { get; set; }
        [MaxLength(10)]
        public string Kennzeichen { get; set; }
        public int HalterId { get; set; }

    }
}
