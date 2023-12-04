using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;

namespace mis221_pa5_glsaacke
{

    public class Reservation
    {
       private int interactionID;
       private string customerEmail;
       private int rideID;
       private string rideName;
       private string rideType;
       private string reservationDate;
       private bool active;
       public int maxInteractionID = 0;
       static public int reservationCount;
    
        public Reservation(){}
        public Reservation(int interactionID, string customerEmail, int rideID, string rideName, string rideType, string reservationDate, bool active){
            this.interactionID = interactionID;
            this.customerEmail = customerEmail;
            this.rideID = rideID;
            this.rideName = rideName;
            this.rideType = rideType;
            this.reservationDate = reservationDate;
            this.active = active;
        }

        public int GetMaxInteractionID(){
            return maxInteractionID;
        }
        public void IncrementInteractionID(){
            maxInteractionID++;
        }

        public int GetInteractionID(){
            return interactionID;
        }

        public string GetCustEmail(){
            return customerEmail;
        }

        public int GetRideID(){
            return rideID;
        }

        public string GetRideName(){
            return rideName;
        }

        public string GetRideType(){
            return rideType;
        }


        public string GetReservationDate(){
            return reservationDate;
        }

        public bool GetActive(){
            return active;
        }
        public void ToggleActive(){
            active = !active;
        }
        

        
    }
}