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
                    if(reservations[i].GetActive()){
                        currentCount ++;
                    }
                }
                else{
                    System.Console.WriteLine($"{currentRide} has {currentCount} active reservations");
                    currentRide = reservations[i].GetRideName();
                    currentCount = 1;
                }
            }
            System.Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        static public void RidesCompleted(){ //Determines expired reservations
            //TODO Build Rides Completed method
        }

        static public void TopFiveRides(){ //Determines the top 5 rides based on reservation
            //TODO Build Top Five Rides method
        }
    }
}