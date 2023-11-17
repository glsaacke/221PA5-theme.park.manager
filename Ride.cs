using System.Dynamic;

namespace mis221_pa5_glsaacke
{
    public class Ride
    {
        private int rideID; 
        private string rideName;
        private string rideType;
        private bool operational;
        private bool deleted;
        static int maxID = 0;

        public Ride(int rideID, string rideName, string rideType, bool operational, bool deleted){
            this.rideID = rideID;
            this.rideName = rideName;
            this.rideType = rideType;
            this.operational = operational;
            this.deleted = deleted;
        }
        public Ride(){

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


        public bool GetOperational(){
            return operational;
        }
        public bool ToggleOperational(){
            return !operational;
        }


        public bool 
        public bool ToggleDeleted(){
            return !deleted;
        }
        

        public int GetMaxID(){
            return maxID;
        }
        public void IncrementMaxID(){
            maxID ++;
        }
    }
}