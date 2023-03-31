namespace AnimalShelter.Models
{
    public class Shelter
    {
        public int ShelterId {get; set;}
        public string ShelterName {get; set;}
        public string Location {get;set;}
        public List<int> AnimalId {get;set;}
    }
}
