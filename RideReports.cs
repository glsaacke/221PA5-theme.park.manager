namespace mis221_pa5_glsaacke
{
    public class RideReports
    {
        private string rideName;
        private string rideType;

        static public void MostRiddenRide(Reservation[] reservations){ //Returns the most ridden ride
            Console.Clear();
            System.Console.WriteLine("Calculating the most ridden ride");

            ReserveUtility.SortReservationArray(reservations); //Build out method

            string rideName = reservations[0].GetRideName();
            for(int i = 0; i < reservations.Length; i++){
                
            }
        }

        static public void ActiveReservations(){ //Determines number of active reservations
            
        }

        static public void RidesCompleted(){ //Determines expired reservations

        }

        static public void TopFiveRides(){ //Determines the top 5 rides based on reservation
            
        }
    }
}