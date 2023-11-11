namespace mis221_pa5_glsaacke
{
    public class User
    {
        private int customerID;
        private string customerEmail;
        private string firstName;
        private string lastName;
        private int age;

        public User(int customerID, string customerEmail, string firstName, string lastName, int age){
            this.customerID = customerID;
            this.customerEmail = customerEmail;
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }

        public int GetCustomerID(){
            return customerID;
        }

        public void SetCustomerID(int customerID){
            this.customerID = customerID;
        }

        public string GetCustomerEmail(){
            return customerEmail;
        }

        public void SetCustomerID(string customerID){
            this.customerEmail = customerEmail;
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

        public int GetAge(){
            return age;
        }

        public void SetAge(int age){
            this.age = age;
        }
        
    }
}