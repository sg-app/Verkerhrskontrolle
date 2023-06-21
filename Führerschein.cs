namespace Verkehrskontrolle
{
    public class Führerschein
    {
        public int Id { get; set; }
       
        public DateTime Gültigkeit { get; set; }

        public string PKWErlaublnis { get; set; }

        public string AnhängerErlaubnis { get; set; }

        public string LKWErlaubnis { get; set; }


    }
}
