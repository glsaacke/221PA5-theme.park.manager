using System.Security.Cryptography.X509Certificates;

namespace mis221_pa5_glsaacke
{
    public class ReserveUtility
    {
        private const int MAX_RESERVATIONS = 100;
        public ReserveUtility(){}
        
        static public void GetAllReservations(Reservation[] reservations){
            StreamReader inFile = new StreamReader("reservations.txt");
            int reservationCount = 0;

            string line; //Priming read
            while ((line = inFile.ReadLine()) != null && reservationCount < MAX_RESERVATIONS){
                
                string[] temp = line.Split('#');

                if (temp.Length >= 7)
                {
                    int interactionID = int.Parse(temp[0]);
                    string custEmail = temp[1];
                    int rideID = int.Parse(temp[2]);
                    string rideName = temp[3];
                    string rideType = temp[4];
                    DateTime reservationDate = DateTime.ParseExact(temp[5], "MM/dd/yyyy HH:mm:ss", null);
                    bool active = false;
                    if(temp[6] == "0"){
                        active = true;
                    }

                    Reservation myReservation = new Reservation(interactionID, custEmail, rideID, rideName, rideType, reservationDate, active);
                    reservations[reservationCount] = myReservation;
                    reservationCount++;
                }
                Reservation.reservationCount++;
                Reservation.maxInteractionID = Reservation.reservationCount - 1;
                //Update read in while condition
            }
        inFile.Close();
        }

        static public void UpdateReservationFile(Reservation[] reservations){
            StreamWriter outFile = new StreamWriter("reservations.txt", false);

            for(int i = 0; i < Reservation.reservationCount; i ++){
                string cancelled = "1";

                Reservation reservation = reservations[i];

                if(reservation != null){
                    if(reservation.GetCancelled()){
                        cancelled = "0";
                    }
                    outFile.WriteLine($"{reservation.GetInteractionID()}#{reservation.GetCustEmail()}#{reservation.GetRideID()}#{reservation.GetRideName()}#{reservation.GetRideType()}#{reservation.GetReservationDate().ToString("MM/dd/yyyy HH:mm:ss")}#{cancelled}");
                }
            }
            outFile.Close();
        }

        static public void ReserveRide(Ride[] rides, Reservation[] reservations, User currentUser){ //FIXME Error: invalid input on creating a reservation, possibly issue with date input
            int check = 0;
            Console.Clear();

            while(check == 0){
                try{
                    System.Console.WriteLine("Please enter the name of the ride you would like to reserve");
                    string rideName = Console.ReadLine().ToUpper();

                    System.Console.WriteLine("Please enter the date you would like to reserve in mm/dd/yyyy format");
                    string userDate = Console.ReadLine();
                    string[] dates = userDate.Split('/');

                    System.Console.WriteLine("Please enter the time you would like to reserve in hh:mm format");
                    string userTime = Console.ReadLine();
                    string[] times = userTime.Split(':');

                    DateTime dateTime = new DateTime(int.Parse(dates[2]), int.Parse(dates[0]), int.Parse(dates[1]), int.Parse(times[0]), int.Parse(times[1]), 0);
                    System.Console.WriteLine(dateTime);

                    Reservation temp = new Reservation();

                    int rideVal = RideUtility.FindRide(rideName, rides);

                    Reservation.IncrementInteractionID();

                    Reservation myReservation = new Reservation(Reservation.maxInteractionID, currentUser.GetUserEmail(), rides[rideVal].GetRideID(), rideName, rides[rideVal].GetRideType(), dateTime, false);

                    reservations[Reservation.reservationCount] = myReservation;

                    check = 1;
                }
                catch{
                    RideUtility.Error("Error: invalid input. Please try again");
                }
            }
            Reservation.reservationCount++;
            System.Console.WriteLine("Ride reserved!");
            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();


        }

        static public void RideHistory(Reservation[] reservations, User currentUser){ //FIXME object reference not set to an instance //TODO add message for no reservations
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

        static public void CancelReservation(Reservation[] reservations, User currentUser){
            Console.Clear();
            int check = 0;

            while(check == 0){
                System.Console.WriteLine("Enter the ride you would like to cancel a reservation for");
                string rideInput = Console.ReadLine().ToUpper();
                string indexConcat = "";
                int check2 = 0;
                int check3 = 0;
                string reservationIndex = "";

                for(int i = 0; i < Reservation.reservationCount; i++){
                    if(rideInput == reservations[i].GetRideName() && reservations[i].GetCustEmail() == currentUser.GetUserEmail()){
                        System.Console.WriteLine($"{i}. {rideInput} reservation on {reservations[i].GetReservationDate().ToString("MM/dd/yyyy HH:mm")}");
                        indexConcat += $"{i},";                    
                    }
                }

                if(indexConcat == ""){
                    check2 = 1;
                    System.Console.WriteLine("You have no current reservations for " + rideInput);
                }

                string[] options = indexConcat.Split(',');

                while(check2 == 0){
                    System.Console.WriteLine("\nPlease enter the number of the reservation you would like to cancel");

                    while(check3 == 0){
                        string indexInput = Console.ReadLine();

                        for(int i = 0; i < options.Length; i++){
                            if(options[i] == indexInput){
                                reservationIndex = indexInput;
                                check3 = 1;
                            }
                        }

                        if(check3 == 0){
                            RideUtility.Error("Error: Please enter a number from the above results");
                        }
                    }
                
                    reservations[int.Parse(reservationIndex)].ToggleCancelled();

                    System.Console.WriteLine("Reservation sucessfully cancelled!");
                }
            }

            System.Console.WriteLine("Press any key to continue");
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