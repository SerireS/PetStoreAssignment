using System;
using System.Collections.Generic;
using System.Text;
using PetApp.Core.DomainService;
using PetApp.Core.Entity;

namespace PetApp.Infrastructure.Static.Data.Repositories
{
    public class PetRepository: IPetRepository
    {
        static int id = 1;
        private List<Pet> _pets = new List<Pet>();

        public Pet Create(Pet pet)
        {
            pet.Id = id++;
            _pets.Add(pet);
            return pet;
        }

        public IEnumerable<Pet> ReadAll()
        {
            return _pets;
        }

        public Pet ReadyById(int id)
        {
            foreach (var pet in _pets)
            {
                if (pet.Id == id)
                {
                    return pet;
                }
            }
            return null;
        }

        public Pet Update(Pet petUpdate)
        {
            var petFromDB = this.ReadyById(petUpdate.Id);
            if (petFromDB != null)
            {
                petFromDB.Name = petUpdate.Name;
                petFromDB.Type = petUpdate.Type;
                petFromDB.BirthDate = petUpdate.BirthDate;
                petFromDB.SoldDate = petUpdate.SoldDate;
                petFromDB.Color = petUpdate.Color;
                petFromDB.PreviousOwner = petUpdate.PreviousOwner;
                petFromDB.Price = petUpdate.Price;
                return petFromDB;
            }
            return null;
        }

        public Pet Delete(int id)
        {
            var petFound = this.ReadyById(id);
            if (petFound != null)
            {
                _pets.Remove(petFound);
                return petFound;
            }
            return null;
        }

    }
}
