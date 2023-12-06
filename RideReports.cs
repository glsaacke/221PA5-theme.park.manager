namespace mis221_pa5_glsaacke
{
    public class RideReports
    {
        private string rideName;
        private string rideType;

        static public void MostRiddenRide(Reservation[] reservations){ //Returns the most ridden ride
            Console.Clear();
            System.Console.WriteLine("Calculating the most ridden ride...");

            ReserveUtility.SortReservationArray(reservations); //Build out method

            string currentRide = reservations[0].GetRideName(); 
            string mostRiddenRide = currentRide;
            int currentCount = 0;
            int greatestCount = 0;

            for(int i = 0; i < Reservation.reservationCount; i++){
            
                if(reservations[i].GetRideName() == currentRide){
                    currentCount++;
                }
                else{
                    if(currentCount > greatestCount){
                        greatestCount = currentCount;
                        mostRiddenRide = currentRide;
                    }
                    currentRide = reservations[i].GetRideName();
                    currentCount = 1;
                }
            }

            System.Console.WriteLine($"The most ridden ride is {mostRiddenRide} with {greatestCount} rides");
            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static public void ActiveReservations(Reservation[] reservations){ //Determines number of active reservations

            Console.Clear();
            System.Console.WriteLine("Calculating active reservations by ride...");

            string currentRide = reservations[0].GetRideName();
            int currentCount = 0;

            for(int i = 0; i < Reservation.reservationCount; i++){

                if(reservations[i].GetRideName() == currentRide){
                    if(reservations[i].GetCancelled() == false && reservations[i].GetReservationDate() > DateTime.Now){
                        currentCount ++;
                    }
                }
                else{
                    System.Console.WriteLine($"{currentRide} has {currentCount} active reservations");
                    currentRide = reservations[i].GetRideName();
                    currentCount = 1;
                }
            }
            System.Console.WriteLine($"{currentRide} has {currentCount} active reservations");
            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }
        

        static public void RidesCompleted(Reservation[] reservations){ //Determines expired reservations
            Console.Clear();
            System.Console.WriteLine("Calculating completed reservations by ride...");

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
                    System.Console.WriteLine($"{currentCount} completed reservations for {currentRide}");
                    currentRide = oldReservations[i].GetRideName();
                    currentCount = 1;
                }
            }
            System.Console.WriteLine($"{currentCount} completed reservations for {currentRide}");
            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        static public void TopFiveRides(Reservation[] reservations){ //Determines the top 5 rides based on reservation
            Console.Clear();
            System.Console.WriteLine("Calculating top 5 rides based on active reservations");
            
            ReserveUtility.SortReservationArray(reservations);

            string currentRide = reservations[0].GetRideName();
            int currentCount = 0;
            string rideConcat = "";
            int uniqueRideCount = 0;

            for(int i = 0; i < Reservation.reservationCount; i++){

                if(reservations[i].GetRideName() == currentRide){
                    if(reservations[i].GetCancelled() == false){
                        currentCount ++;
                    }
                }
                else{
                    rideConcat += $"{currentRide},{currentCount}#";
                    currentRide = reservations[i].GetRideName();
                    currentCount = 1;
                }
            }

            if(reservations[Reservation.reservationCount - 1].GetRideName() != reservations[Reservation.reservationCount - 2].GetRideName()){
                rideConcat += $"{reservations[Reservation.reservationCount - 1].GetRideName()},1";
            }

            string[] separateRides = rideConcat.Split('#');

            for (int i = 0; i < separateRides.Length - 1; i++){
            
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
            System.Console.WriteLine("\nHere are the top 5 rides:");
            for (int i = 0; i < Math.Min(5, separateRides.Length); i++){
            
                string[] rideInfo = separateRides[i].Split(',');
                string rideName = rideInfo[0];
                int count = int.Parse(rideInfo[1]);
                System.Console.WriteLine($"{rideName} has {count} active reservations.");
            }

            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }
    }
}