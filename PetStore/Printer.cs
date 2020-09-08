using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using PetApp.Core.ApplicationService;
using PetApp.Core.Entity;

namespace PetStore
{
    public class Printer: IPrinter
    {
        #region Service area

        readonly IPetService _petService;
        #endregion

        public Printer(IPetService petService)
        {
            _petService = petService;
            InitData();

        }

        #region UI

        public void StartUI()
        {
            string[] menuItems = {
                "List All Pets",
                "Add Pet",
                "Delete Pet",
                "Edit Pet",
                "Search By Type",
                "Search By Price",
                "Show Five Cheapest",
                "Clear Console",
                "Exit"
            };

            var selection = ShowMenu(menuItems);

            while (selection != 9)
            {
                switch (selection)
                {
                    case 1:
                        var pets = _petService.GetAllPets();
                        ListPets(pets);
                        break;
                    case 2:
                        var Name = AskQuestion("Pet name: ");
                        var Type = AskQuestion("Pet Type: ");
                        var BirthDate = AskQuestion("Pet BirthDate: ");
                        var SoldDate = AskQuestion("Pet SoldDate: ");
                        var Color = AskQuestion("Pet Color: ");
                        var PreviousOwner = AskQuestion("Pet PreviousOwner: ");
                        var Price = AskQuestion("Pet Price: ");
                        var pet = _petService.NewPet(Name, Type, DateTime.Parse(BirthDate), DateTime.Parse(SoldDate), Color,  PreviousOwner, double.Parse(Price));
                        _petService.CreatePet(pet);
                        break;
                    case 3:
                        var idForDelete = PrintFindPetId();
                        _petService.DeletePet(idForDelete);
                        break;
                    case 4:
                        var idForEdit = PrintFindPetId();
                        var petToEdit = _petService.FindPetById(idForEdit);
                        Console.WriteLine("Updating " + petToEdit.Name + " " + petToEdit.Type + " " + petToEdit.BirthDate + " " + petToEdit.SoldDate + " " + petToEdit.Color + " " + petToEdit.PreviousOwner + " " + petToEdit.Price);
                        var newName = AskQuestion("Pet name: ");
                        var newType = AskQuestion("Pet Type: ");
                        var newBirthDate = AskQuestion("Pet BirthDate: ");
                        var newSoldDate = AskQuestion("Pet SoldDate: ");
                        var newColor = AskQuestion("Pet Color: ");
                        var newPreviousOwner = AskQuestion("Pet PreviousOwner: ");
                        var newPrice = AskQuestion("Pet Price: ");
                        _petService.UpdatePet(new Pet()
                        {
                            Id = idForEdit,
                            Name = newName,
                            Type = newType,
                            BirthDate = DateTime.Parse(newBirthDate),
                            SoldDate = DateTime.Parse(newSoldDate),
                            Color = newColor,
                            PreviousOwner = newPreviousOwner,
                            Price = double.Parse(newPrice)
                        });
                        break;
                    case 5:
                        var typeToSearch = AskQuestion("Insert pet Type ");
                        var petSearch = _petService.GetAllByType(typeToSearch);
                        ListPets(petSearch);
                        break;
                    case 6:
                        var priceSearch = _petService.GetAllByPrice();
                        ListPets(priceSearch);
                        break;
                    case 7:
                        var priceFiveSearch = _petService.ShowFiveCheapest();
                        ListPets(priceFiveSearch);
                        break;
                    case 8:
                        Console.Clear();
                        break;
                    default:
                        break;
                }
                selection = ShowMenu(menuItems);
            }
            Console.WriteLine("Bye bye!");

            Console.ReadLine();
        }


        int PrintFindPetId()
        {
            Console.WriteLine("Insert Pet Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please insert a number");
            }
            return id;
        }

        string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        void ListPets(List<Pet> pets)
        {
            Console.WriteLine("\nList of Pets");
            foreach (var pet in pets)
            {
                Console.WriteLine($"Id: {pet.Id} | Name: {pet.Name} " + $"| Type: {pet.Type} " + $"| BirthDate: {pet.BirthDate}" + $"| SoldDate: {pet.SoldDate} " + $"| Color: {pet.Color} " + $"| PreviousOwner: {pet.PreviousOwner} " + $"| Price: {pet.Price}");
            }
            Console.WriteLine("\n");

        }

        int ShowMenu(string[] menuItems)
        {
            Console.WriteLine("Select What you want to do:\n");

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                || selection < 1
                || selection > 9)
            {
                Console.WriteLine("Please select a number between 1-9");
            }

            return selection;
        }
        #endregion

        #region Infrastructure layer / Initialization Layer

        void InitData()

        {
            var pet1 = new Pet()
            {
                Name = "AName",
                Type = "Dog",
                BirthDate = new DateTime(2017, 04, 04) ,
                SoldDate = new DateTime(2020, 04, 04),
                Color = "Green",
                PreviousOwner = "Random Dude" ,
                Price = 99.99
                
            };
            _petService.CreatePet(pet1);

            var pet2 = new Pet()
            {
                Name = "Another Name",
                Type = "Cat",
                BirthDate = new DateTime(2018, 05, 05),
                SoldDate = new DateTime(2020, 05, 05),
                Color = "Pink",
                PreviousOwner = "Another Random Dude",
                Price = 99.99
            };
            _petService.CreatePet(pet2);
        }

        #endregion

    }
}
