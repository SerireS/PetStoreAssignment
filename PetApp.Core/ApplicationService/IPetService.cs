using System;
using System.Collections.Generic;
using System.Text;
using PetApp.Core.Entity;

namespace PetApp.Core.ApplicationService
{
    public interface IPetService
    {

        Pet NewPet(string Name, string Type, DateTime BirthDate, DateTime SoldDate, string Color, string PreviousOwner, double Price);

        Pet CreatePet(Pet pet);

        Pet FindPetById(int id);

        List<Pet> GetAllPets();

        List<Pet> GetAllByType(string type);

        Pet UpdatePet(Pet petUpdate);

        Pet DeletePet(int id);

        public List<Pet> GetAllByPrice();
        public List<Pet> ShowFiveCheapest();
    }
}
