using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetApp.Core.DomainService;
using PetApp.Core.Entity;

namespace PetApp.Core.ApplicationService.Services
{
    public class PetService : IPetService
    {
        readonly IPetRepository _petRepo;
        public PetService(IPetRepository petRepository)
        {
            _petRepo = petRepository;
        }
        public Pet NewPet(string Name, string Type, DateTime BirthDate, DateTime SoldDate, string Color, string PreviousOwner, double Price)
        {
            var pet = new Pet()
            {
                Name = Name,
                Type = Type,
                BirthDate = BirthDate,
                SoldDate = SoldDate,
                Color = Color,
                PreviousOwner = PreviousOwner,
                Price = Price
            };

            return pet;
        }

        public Pet CreatePet(Pet pet)
        {
            return _petRepo.Create(pet);
        }

        public Pet FindPetById(int id)
        {
            return _petRepo.ReadyById(id);
        }

        public List<Pet> GetAllPets()
        {
            return _petRepo.ReadAll().ToList();
        }

        public List<Pet> GetAllByType(string type)
        {
            var list = _petRepo.ReadAll();
            var queryContinued = list.Where(pet => pet.Type.Equals(type));
            queryContinued.OrderBy(pet => pet.Type);
            return queryContinued.ToList();
        }

        public List<Pet> GetAllByPrice()
        {
            var list = _petRepo.ReadAll();
            var queryContinued = list.OrderBy(pet => pet.Price);
            return queryContinued.ToList();
        }

        public List<Pet> ShowFiveCheapest()
        {
            var list = _petRepo.ReadAll();
            var queryOrdered = list.OrderBy(pet => pet.Price);
            var firstFiveItems = queryOrdered.Take(5);
            return firstFiveItems.ToList();
        }



        public Pet UpdatePet(Pet petUpdate)
        {
            var pet = FindPetById(petUpdate.Id);
            pet.Name = petUpdate.Name;
            pet.Type = petUpdate.Type;
            pet.BirthDate = petUpdate.BirthDate;
            pet.SoldDate = petUpdate.SoldDate;
            pet.Color = petUpdate.Color;
            pet.PreviousOwner = petUpdate.PreviousOwner;
            pet.Price = petUpdate.Price;

            return pet;
        }

        public Pet DeletePet(int id)
        {
            return _petRepo.Delete(id);
        }
    }
}