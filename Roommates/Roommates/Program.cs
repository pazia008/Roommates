using System;
using System.Collections.Generic;
using System.Linq;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository mateRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                     case ("Show all rooms"):
             List<Room> rooms = roomRepo.GetAll();
             foreach (Room r in rooms)
             {
                 Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
             }
             Console.Write("Press any key to continue");
             Console.ReadKey();
             break;

         case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                        

         case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                       
         case ("Exit"):
             runProgram = false;
             break;


                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine($"Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;


                    case ("Delete a room"):
                        List<Room> roomChoices = roomRepo.GetAll();
                        foreach (Room r in roomChoices)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to delete? ");
                        int chosenRoomId = int.Parse(Console.ReadLine());
                        Room chosenRoom = roomChoices.FirstOrDefault(r => r.Id == chosenRoomId);

                        roomRepo.Delete(chosenRoomId);

                        Console.WriteLine($"Room has been successfully been deleted");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    //CHORES

                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id} ");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for a chore"):
                        Console.Write("Chore Id: ");
                        int Id = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(Id);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;


                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string choreName = Console.ReadLine();

                        

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Update a chore"):
                        List<Chore> choreOptions = choreRepo.GetAll();
                        foreach (Chore c in choreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name} ");
                        }

                        Console.Write("Which chore would you like to update? ");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        Chore selectedChore = choreOptions.FirstOrDefault(c => c.Id == selectedChoreId);

                        Console.Write("New Name: ");
                        selectedChore.Name = Console.ReadLine();

                        choreRepo.Update(selectedChore);

                        Console.WriteLine($"Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;





                    //Roomies

                    case ("Show all roommates"):
                        List<Roommate> mates = mateRepo.GetAll();
                        foreach (Roommate rm in mates)
                        {
                            Console.WriteLine($"{rm.FirstName} has an Id of {rm.Id}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                        


                }



            }


        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
        {
            "Show all rooms",
            "Search for room",
            "Add a room",
            "Update a room",
            "Delete a room",
            "Show all chores",
            "Search for a chore",
            "Add a chore",
            "Update a chore",
            "Show all roommates",
            "Exit"
        };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }

        }
    }

}