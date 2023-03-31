namespace AnimalShelter.Models
{
    public class Animal
    {
        public int AnimalId {get; set;}
        public string AnimalName {get; set;}
        public string Species {get;set;}
        public string Breed {get;set;}
        public DateTime DateAcquired {get;set;}
        public int ShelterId {get;set;}
    }
}
