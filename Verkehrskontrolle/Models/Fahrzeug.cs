﻿using System.ComponentModel.DataAnnotations;

namespace Verkehrskontrolle.Models
{
    public class Fahrzeug
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Antrieb { get; set; }
        [MaxLength(50)]
        public string Fahrzeugtyp { get; set;}
        [RegularExpression(@"^[1-9]?\d{0,2}")]
        public int Sitze { get; set; }
        [RegularExpression(@"^[1-9]?\d{0,3}")]
        public int Leistung { get; set; }
        public DateTime ZulassungDatum { get; set; }
        public DateTime TüvDatum { get; set; }
        [MaxLength(10)]
        public string Kennzeichen { get; set; }
        public int HalterId { get; set; }
        public Halter Halter { get; set; }

    }
}
