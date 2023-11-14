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
       private bool cancelled;
    
        public Reservation(int interactionID, string customerEmail, int rideID, string rideName, string rideType, string reservationDate, bool cancelled){
            this.interactionID = interactionID;
            this.customerEmail = customerEmail;
            this.rideID = rideID;
            this.rideName = rideName;
            this.rideType = rideType;
            this.reservationDate = reservationDate;
            this.cancelled = cancelled;
        }
    }
}