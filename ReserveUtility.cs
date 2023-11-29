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
                    bool cancelled = false;
                    if(temp[6] == "0"){
                        cancelled = true;
                    }

                    Reservation myReservation = new Reservation(interactionID, custEmail, rideID, rideName, rideType, reservationDate, cancelled);
                    reservations[reservationCount] = myReservation;
                    reservationCount++;
                }
                //Update read in while condition
            }
        inFile.Close();
        }

        public void ReserveRide(Ride[] rides, Reservation[] reservations, User[] users, int userVal){
            System.Console.WriteLine("Please enter the name of the ride you would like to reserve");
            string rideName = Console.ReadLine();
            System.Console.WriteLine("Please enter the date you would like to reserve\nEx: March 3");
            string date = Console.ReadLine();
            System.Console.WriteLine("Please enter the time you would like to reserve");
            string time = Console.ReadLine();

            Reservation temp = new Reservation();
            RideUtility rUtility = new RideUtility();

            int rideVal = rUtility.FindRide(rideName, rides);

            Reservation myReservation = new Reservation(temp.GetMaxInteractionID(), users[userVal].GetUserEmail(), rides[rideVal].GetRideID(), rideName, rides[rideVal].GetRideType(), date + time, false);

        }

        public void RideHistory(Reservation[] reservations, User[] users, int userVal){
            string custEmail = users[userVal].GetUserEmail();

            System.Console.WriteLine("Here are the past reservations under your email: " + custEmail);

            foreach(Reservation r in reservations){
                if(custEmail == r.GetCustEmail()){
                    System.Console.WriteLine($"{r.GetRideName()}{r.GetReservationDate}");
                }
            }

            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public void CancelReservation(Reservation[] reservations){
            System.Console.WriteLine("Here are your active reservations");

            foreach(Reservation r in reservations){
                if 
            }
        }
    }
}