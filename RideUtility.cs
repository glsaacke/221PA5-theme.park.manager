using System.Runtime.Intrinsics.Arm;

namespace mis221_pa5_glsaacke
{
    public class RideUtility
    {
        private const int MAX_RIDES = 99;

        public RideUtility(){

        }

        public void GetAllRides(Ride[] rides){
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
        }

        public void UpdateRideFile(Ride[] rides){
            
        }

        public void AddNewRide(Ride[] rides){ //Adds a new ride to the rides array
            Console.Clear();

            System.Console.WriteLine("Enter the name of the new ride");
            string inputName = Console.ReadLine();

            System.Console.WriteLine("Enter the ride type of " + inputName);
            string inputType = Console.ReadLine();

            Ride ride = new Ride();

            Ride myRide = new Ride(ride.GetMaxID(), inputName, inputType, true, true);

            rides[ride.GetMaxID()] = myRide;

            ride.IncrementMaxID();
        }

        public void RemoveRide(Ride[] rides){ //Changes ride status to deleted
            Console.Clear();

            System.Console.WriteLine("Enter the name of the ride you would like to remove");
            string inputRide = Console.ReadLine();

            int foundIndex = FindRide(inputRide, rides);

            if(foundIndex == -1){
                Error("Ride does not exist");
            }
            else{
                rides[foundIndex].ToggleDeleted();
            }
        }

        public void EditRide(Ride[] rides){ //Edits information about a ride
            Console.Clear();
            System.Console.WriteLine("Enter the name of the ride you would like to edit");
            string inputName = Console.ReadLine();

            int foundIndex = FindRide(inputName, rides);

            System.Console.WriteLine("Select which aspect of " + inputName + " you would like to change");
            System.Console.WriteLine("1. Ride name\n 2. Ride Type\n 3. Ride operating status");
            int userInput = -1;

            try{
                userInput = int.Parse(Console.ReadLine());
            }
            catch{
                Error("Please enter a number");
            }

            if(userInput == 1){
                System.Console.WriteLine("Enter the new ride name");
                rides[foundIndex].SetRideName(Console.ReadLine());
                System.Console.WriteLine("Ride name has been changed");
            }
            else if( userInput == 2){
                System.Console.WriteLine("Enter the new ride type");
                rides[foundIndex].SetRideType(Console.ReadLine());
                System.Console.WriteLine("Ride type has been changed");
            }
            else if(userInput == 3){
                rides[foundIndex].ToggleOperational();

                if(rides[foundIndex].GetOperational()){
                    System.Console.WriteLine($"{inputName} is now operational");
                }
                else{
                    System.Console.WriteLine(inputName + " is now not operational");
                }
            }
            else{
                Error("Please enter a valid input");
            }
        }

        public int FindRide(string inputRide, Ride[] rides){ //Determines the position of a ride inside the array
            int foundIndex = -1;

            Ride ride = new Ride();

            for(int i = 0; i < ride.GetMaxID(); i ++){

                if(rides[i].GetRideName() == inputRide){
                    foundIndex = i;
                }
            }
            return foundIndex;
        }

        public void Error(string errorMessage){
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Error: " + errorMessage);
            Console.ResetColor();
        }
    }
}