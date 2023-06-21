namespace Verkehrskontrolle.Models
{
    public class Führerschein
    {
        public int Id { get; set; }

        public DateTime Gültigkeit { get; set; }

        public bool PKWErlaublnis { get; set; } = false;

        public bool AnhängerErlaubnis { get; set; } = false;

        public bool LKWErlaubnis { get; set; } = false;


    }
}
