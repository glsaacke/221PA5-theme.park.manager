using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;

namespace mis221_pa5_glsaacke
{
    public class RideUtility
    {
        private const int MAX_RIDES = 999;

        public RideUtility(){

        }

        static public void GetAllRides(Ride[] rides){
            StreamReader inFile = new StreamReader("rides.txt");
            int rideCount = 0;

            string line; //Priming read
            while ((line = inFile.ReadLine()) != null && rideCount < MAX_RIDES){
                
                string[] temp = line.Split('#');

                if (temp.Length >= 5)
                {
                    int rideID = int.Parse(temp[0]);
                    string rideName = temp[1];
                    string rideType = temp[2];
                    bool operational = false;
                    if(temp[3] == "0"){
                        operational = true;
                    }
                    bool deleted = false;
                    if(temp[4] == "0"){
                        deleted = true;
                    }

                    Ride ride = new Ride(rideID, rideName, rideType, operational, deleted);
                    rides[rideCount] = ride;
                    rideCount++;
                }
                //Update read in while condition
            }
            inFile.Close();
            Ride.rideCount = rideCount;
            if(rideCount > 0){
                Ride.maxID = rides.Where(r=> r != null).Max(r=> r.GetRideID());
            }
            else{
                Ride.maxID = 0;
            }
        }

        static public void UpdateRideFile(Ride[] rides){ //FIXME index creation error: starts at -1
            StreamWriter outFile = new StreamWriter("rides.txt", false);

            foreach(Ride r in rides){
                string operational = "1";
                string deleted = "1";

                if(r != null){
                    if(r.GetOperational()){
                        operational = "0";
                    }
                    if(r.GetDeleted()){
                        deleted = "0";
                    }
                    outFile.WriteLine($"{r.GetRideID()}#{r.GetRideName().ToUpper()}#{r.GetRideType().ToUpper()}#{operational}#{deleted}");
                }
            }
            outFile.Close();
        }

        static public void AddNewRide(Ride[] rides){ //Adds a new ride to the rides array
            Console.Clear();

            System.Console.WriteLine("Enter the name of the new ride");
            string inputName = Console.ReadLine();

            System.Console.WriteLine("Enter the ride type of " + inputName);
            string inputType = Console.ReadLine();

            Ride myRide = new Ride(Ride.GetMaxID() + 1, inputName.ToUpper(), inputType.ToUpper(), true, false);

            Ride.IncrementMaxID();
            rides[Ride.GetMaxID()] = myRide;


            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\nNew ride added!\n");
            Console.ResetColor();
            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static public void RemoveRide(Ride[] rides){ //FIXME index out of bounds when removing a recently added ride
            Console.Clear();

            System.Console.WriteLine("Enter the name of the ride you would like to remove");
            string inputRide = Console.ReadLine().ToUpper();

            int foundIndex = FindRide(inputRide, rides);

            if(foundIndex == -2){
                Error("Ride does not exist");
            }
            else{
                rides[foundIndex].ToggleDeleted();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("\nRide sucessully removed!");
                Console.ResetColor();
            }
            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        static public void EditRide(Ride[] rides){ //FIXME does not edit a recently added ride
            Console.Clear();
            System.Console.WriteLine("Enter the name of the ride you would like to edit");
            string inputName = Console.ReadLine().ToUpper();

            int foundIndex = FindRide(inputName, rides);

            if(foundIndex != -2){
                System.Console.WriteLine("\nSelect which aspect of " + inputName + " you would like to change");
                System.Console.WriteLine("1. Ride name\n2. Ride Type\n3. Ride operating status");
                int userInput = -1;

                try{
                    userInput = int.Parse(Console.ReadLine());
                }
                catch{
                    Error("Please enter a number");
                }

                if(userInput == 1){
                    System.Console.WriteLine("Enter the new ride name");
                    rides[foundIndex].SetRideName(Console.ReadLine().ToUpper());
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("\nRide name has been changed");
                    Console.ResetColor();
                }
                else if( userInput == 2){
                    System.Console.WriteLine("Enter the new ride type");
                    rides[foundIndex].SetRideType(Console.ReadLine().ToUpper());
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("\nRide type has been changed");
                    Console.ResetColor();
                }
                else if(userInput == 3){
                    rides[foundIndex].ToggleOperational();

                    if(rides[foundIndex].GetOperational()){
                        System.Console.WriteLine($"{inputName} is now operational");
                    }
                    else{
                        System.Console.WriteLine(inputName + " is no longer operational");
                    }
                }
                else{
                    Error("Please enter a valid input");
                }
            }
            else{
                Error("Ride does not exist. Please try again");
            }

            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        static public int FindRide(string inputRide, Ride[] rides){ //Determines the position of a ride inside the array
            int foundIndex = -2;

            Ride ride = new Ride();

            for(int i = 0; i <= Ride.maxID; i ++){

                if(rides[i].GetRideName() == inputRide){
                    foundIndex = i;
                }
            }
            return foundIndex;
        }

        static private void SortRideArray(Ride[] rides){

            for(int i = 0; i <= Ride.maxID; i++){
                int index = i;

                for(int j = i + 1; j <= Ride.maxID; j++){
                    if(string.Compare(rides[j].GetRideName(), rides[i].GetRideName()) < 0){
                        index = j;
                    }
                }

                if(index != i){
                    Ride temp = rides[i];
                    rides[i] = rides[index];
                    rides[index] = temp;
                }
            }
        }

        static public void ViewAllRides(Ride[] rides){
            Console.Clear();
            System.Console.WriteLine("Here are the current operational rides:\n");
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");
            SortRideArray(rides);
            string currentRide = rides[0].GetRideName();

            foreach(Ride r in rides){
                if(r != null){
                    if(r.GetOperational() && r.GetDeleted() == false){
                        System.Console.WriteLine($"{r.GetRideName()}, a {r.GetRideType()} ride");
                    }
                }
            }
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");
            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        static public void Error(string errorMessage){
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Error: " + errorMessage);
            Console.ResetColor();
        }
    }
}