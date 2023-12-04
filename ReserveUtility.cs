using System.Security.Cryptography.X509Certificates;

namespace mis221_pa5_glsaacke
{
    public class ReserveUtility
    {
        private const int MAX_RESERVATIONS = 100;
        public ReserveUtility(){}
        
        public void GetAllReservations(Reservation[] reservations){
            StreamReader inFile = new StreamReader("reseravtions.txt");
            int reservationCount = 0;

            string line; //Priming read
            while ((line = inFile.ReadLine()) != null && reservationCount < MAX_RESERVATIONS){
                
                string[] temp = line.Split('#');

                if (temp.Length >= 5)
                {
                    int interactionID = int.Parse(temp[0]);
                    string custEmail = temp[1];
                    int rideID = int.Parse(temp[2]);
                    string rideName = temp[3];
                    string rideType = temp[4];
                    string reservationDate = temp[5];
                    bool active = false;
                    if(temp[6] == "0"){
                        active = true;
                    }

                    Reservation myReservation = new Reservation(interactionID, custEmail, rideID, rideName, rideType, reservationDate, active);
                    reservations[reservationCount] = myReservation;
                    reservationCount++;
                }
                Reservation.reservationCount++;
                //Update read in while condition
            }
        inFile.Close();
        }

        //TODO add array export method

        public void ReserveRide(Ride[] rides, Reservation[] reservations, User currentUser){
            System.Console.WriteLine("Please enter the name of the ride you would like to reserve");
            string rideName = Console.ReadLine();
            System.Console.WriteLine("Please enter the date you would like to reserve\nEx: March 3");
            string date = Console.ReadLine();
            System.Console.WriteLine("Please enter the time you would like to reserve");
            string time = Console.ReadLine();

            Reservation temp = new Reservation();
            RideUtility rUtility = new RideUtility();

            int rideVal = RideUtility.FindRide(rideName, rides);

            Reservation myReservation = new Reservation(temp.GetMaxInteractionID(), currentUser.GetUserEmail(), rides[rideVal].GetRideID(), rideName, rides[rideVal].GetRideType(), date + time, false);

        }

        public void RideHistory(Reservation[] reservations, User currentUser){
            string custEmail = currentUser.GetUserEmail();

            System.Console.WriteLine("Here are the past reservations under your email: " + custEmail);

            foreach(Reservation r in reservations){
                if(custEmail == r.GetCustEmail()){
                    System.Console.WriteLine($"{r.GetRideName()}{r.GetReservationDate}");
                }
            }

            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public void CancelReservation(Reservation[] reservations, User currentUser){
            System.Console.WriteLine("Here are your active reservations");
            int check = 0;

            foreach(Reservation r in reservations){
                if(r.GetCustEmail() == currentUser.GetUserEmail()){
                    System.Console.WriteLine($"{r.GetRideName()} {r.GetReservationDate()}");
                }
            }
            while(check == 0){
                System.Console.WriteLine("Enter the ride you would like to cancel");
                string rideInput = Console.ReadLine();

                System.Console.WriteLine("Enter the date you would like to cancel on");
                string dateInput = Console.ReadLine();

                foreach(Reservation r in reservations){
                    if(rideInput == r.GetRideName() && dateInput == r.GetReservationDate()){
                        r.ToggleCancelled();
                        check = 1;
                    }
                }

                if(check == 0){
                    RideUtility.Error("Reservation does not exist. Check input and try again");
                }
            }

            System.Console.WriteLine("Reservation sucessfully cancelled!\nPress any key to continue");
            Console.ReadKey();
        }

        static public void SortReservationArray(Reservation[] reservations){
            string rideName = reservations[0].GetRideName();

            for(int i = 0; i < Reservation.reservationCount; i++){
                int index = i;

                for(int j = i + 1; j < Reservation.reservationCount; j++){
                    if(string.Compare(reservations[j].GetRideName(), reservations[i].GetRideName()) < 0){
                        index = j;
                    }
                }

                if(index != i){
                    Reservation temp = reservations[i];
                    reservations[i] = reservations[index];
                    reservations[index] = temp;
                }
            }
        }
    }
}