namespace mis221_pa5_glsaacke
{
    public class RideReports
    {
        static public void MostRiddenRide(Reservation[] reservations){ //Returns the most ridden ride
            Console.Clear();
            System.Console.Write("Calculating the most ridden ride");
            DisplayDots();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");

            ReserveUtility.SortReservationArray(reservations);

            string currentRide = reservations[0].GetRideName(); 
            string mostRiddenRide = currentRide;
            int currentCount = 0; // Priming read
            int greatestCount = 0;

            for(int i = 0; i < Reservation.reservationCount; i++){
            
                if(reservations[i].GetRideName() == currentRide){
                    currentCount++;
                }
                else{
                    if(currentCount > greatestCount){ // Handle break
                        greatestCount = currentCount;
                        mostRiddenRide = currentRide;
                    }
                    currentRide = reservations[i].GetRideName();
                    currentCount = 1; // Update read
                }
            }

            System.Console.WriteLine($"The most ridden ride is {mostRiddenRide} with {greatestCount} rides");
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");
            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        static public void ActiveReservations(Reservation[] reservations){ //Determines number of active reservations by ride

            Console.Clear();
            System.Console.Write("Calculating active reservations by ride");
            DisplayDots();
            System.Console.WriteLine("");
            System.Console.WriteLine();
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");

            string currentRide = reservations[0].GetRideName();
            int currentCount = 0; // Priming read

            for(int i = 0; i < Reservation.reservationCount; i++){

                if(reservations[i].GetRideName() == currentRide){
                    if(reservations[i].GetCancelled() == false && reservations[i].GetReservationDate() > DateTime.Now){
                        currentCount ++;
                    }
                }
                else{
                    System.Console.WriteLine($"{currentRide} has {currentCount} active reservations"); // Handle bread
                    currentRide = reservations[i].GetRideName();
                    currentCount = 1;
                }
            }
            System.Console.WriteLine($"{currentRide} has {currentCount} active reservations");
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");
            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }
        

        static public void RidesCompleted(Reservation[] reservations){ //Displays expired reservations by ride
            Console.Clear();
            System.Console.Write("Calculating completed reservations by ride");
            DisplayDots();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");

            ReserveUtility.SortReservationArray(reservations);
            DateTime currentDateTime = DateTime.Now;
            int oldReservationCount = 0;

            Reservation[] oldReservations = new Reservation[99];

            for(int i = 0; i < Reservation.reservationCount; i++){
                if(reservations[i].GetReservationDate() < currentDateTime){
                    oldReservations[oldReservationCount] = reservations[i];
                    oldReservationCount++;
                }
            }

            string currentRide = oldReservations[0].GetRideName();
            int currentCount = 0;

            for(int i = 0; i < oldReservationCount; i++){
                if(oldReservations[i].GetRideName() == currentRide){
                    currentCount++;
                }
                else{
                    System.Console.WriteLine($"{currentCount} completed reservations for {currentRide}"); // Handle break
                    currentRide = oldReservations[i].GetRideName();
                    currentCount = 1;
                }
            }
            System.Console.WriteLine($"{currentCount} completed reservations for {currentRide}");
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");
            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        static public void TopFiveRides(Reservation[] reservations){ //Determines the top 5 rides based on active reservations
            Console.Clear();
            System.Console.Write("Calculating top 5 rides based on active reservations");
            DisplayDots();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");
            
            ReserveUtility.SortReservationArray(reservations);

            string currentRide = reservations[0].GetRideName();
            int currentCount = 0;
            string rideConcat = "";

            for(int i = 0; i < Reservation.reservationCount; i++){

                if(reservations[i].GetRideName() == currentRide){
                    if(reservations[i].GetCancelled() == false){
                        currentCount ++;
                    }
                }
                else{
                    rideConcat += $"{currentRide},{currentCount}#"; // Handle break
                    currentRide = reservations[i].GetRideName();
                    currentCount = 1;
                }
            }

            rideConcat += $"{currentRide},{currentCount}";

            string[] separateRides = rideConcat.Split('#');

            for (int i = 0; i < separateRides.Length - 1; i++){ // Selection sort
            
                int maxIndex = i;
                for (int j = i + 1; j < separateRides.Length - 1; j++){
                
                    string[] rideInfo1 = separateRides[j].Split(',');
                    string[] rideInfoMax = separateRides[maxIndex].Split(',');

                    int count1 = int.Parse(rideInfo1[1]);
                    int countMax = int.Parse(rideInfoMax[1]);

                    if (count1 > countMax){
                    
                        maxIndex = j;
                    }
                }

                string temp = separateRides[i];
                separateRides[i] = separateRides[maxIndex];
                separateRides[maxIndex] = temp;
            }
            for (int i = 0; i < Math.Min(5, separateRides.Length); i++){
            
                string[] rideInfo = separateRides[i].Split(',');
                string rideName = rideInfo[0];
                int count = int.Parse(rideInfo[1]); //ERROR occurs here after rocketship is listed
                System.Console.WriteLine($"{rideName} has {count} active reservations.");
            }
            System.Console.WriteLine("─────────────────────────────────────────────────────────────");
            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        static private void DisplayDots(){ // Displays "..." but slower
            Thread.Sleep(600);
            Console.Write(".");
            Thread.Sleep(600);
            Console.Write(".");
            Thread.Sleep(600);
            Console.Write(".");
            Thread.Sleep(600);
        }
    }
}