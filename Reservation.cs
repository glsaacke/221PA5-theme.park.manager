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
       private DateTime reservationDate;
       private bool cancelled;
       static public int maxInteractionID = 0;
       static public int reservationCount;
    
        public Reservation(){} // No args consrtuctor
        public Reservation(int interactionID, string customerEmail, int rideID, string rideName, string rideType, DateTime reservationDate, bool cancelled){ // Constructor
            this.interactionID = interactionID;
            this.customerEmail = customerEmail;
            this.rideID = rideID;
            this.rideName = rideName;
            this.rideType = rideType;
            this.reservationDate = reservationDate;
            this.cancelled = cancelled;
        }

        static public int GetMaxInteractionID(){
            return maxInteractionID;
        }
        static public void IncrementInteractionID(){
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


        public DateTime GetReservationDate(){
            return reservationDate;
        }

        public bool GetCancelled(){
            return cancelled;
        }
        public void ToggleCancelled(){
         cancelled =  !cancelled;
        }
        

        
    }
}