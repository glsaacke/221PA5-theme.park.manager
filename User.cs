namespace mis221_pa5_glsaacke
{
    public class User
    {
        private int userID;
        private string userEmail;
        private string firstName;
        private string lastName;
        private int userAge;
        private int admin;
        static public int maxID = 0;

        public User(int userID, string userEmail, string firstName, string lastName, int userAge, int admin){ // Constructor
            this.userID = userID;
            this.userEmail = userEmail;
            this.firstName = firstName;
            this.lastName = lastName;
            this.userAge = userAge;
            this.admin = admin;
        }

        public User(){} //No args User constructor

        public int GetUserID(){
            return userID;
        }

        public void SetUserID(int userID){
            this.userID = userID;
        }



        public string GetUserEmail(){
            return userEmail;
        }

        public void SetUserEmail(string userEmail){
            this.userEmail = userEmail;
        }



        public string GetFirstName(){
            return firstName;
        }

        public void SetFirstName(string firstName){
            this.firstName = firstName;
        }


        public string GetLastName(){
            return lastName;
        }

        public void SetLastName(string lastName){
            this.lastName = lastName;
        }



        public int GetUserAge(){
            return userAge;
        }

        public void SetUserAge(int userAge){
            this.userAge = userAge;
        }


        public int GetAdmin(){
            return admin;
        }

        public void SetAdmin(int admin){
            this.admin = admin;
        }


        public int GetMaxID(){
            return maxID;
        }
        public void IncrementMaxID(){
            maxID ++;
        }
        
    }
}