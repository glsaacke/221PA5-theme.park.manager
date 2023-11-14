using System.Dynamic;

namespace mis221_pa5_glsaacke
{
    public class Ride
    {
        private int rideID; 
        private string rideName;
        private string rideType;
        private bool operational;

        public Ride(int rideID, string rideName, string rideType, bool operational){
            this.rideID = rideID;
            this.rideName = rideName;
            this.rideType = rideType;
            this.operational = operational;
        }

        public int GetRideID(){
            return rideID;
        }

        public void SetRideID(int rideID){
            this.rideID = rideID;
        }

        public string GetRideName(){
            return rideName;
        }

        public void SetRideName(string rideName){
            this.rideName = rideName;
        }

        public string GetRideType(){
            return rideType;
        }

        public void SetRideType(string rideType){
            this.rideType = rideType;
        }

        public bool ToggleOperational(bool operational){
            return !operational;
        }
    }
}