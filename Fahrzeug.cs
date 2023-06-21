namespace Verkehrskontrolle
{
    public class Fahrzeug
    {
        public int Id { get; set; }
        public string Antrieb { get; set; }  
        public int Sitze { get; set; }
        public int Leistung { get; set; }
        public DateTime ZulassungDatum { get; set; }
        public DateTime TüvDatum { get; set; }
        public string Kennzeichen { get; set;}

        

    }
}
