using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models
{
    public class CarModel
    {

        public string VINnumber { get; set; }
        public string RegistrationNumber { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }

        public int SeatsNumber { get; set; }

        public TrunkType Trunk { get; set; }

    }
    }
