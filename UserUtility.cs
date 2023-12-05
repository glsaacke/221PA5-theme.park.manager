using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualBasic;

namespace mis221_pa5_glsaacke
{
    public class UserUtility
    {
        static int userCount = 0;
        private const int MAX_USERS = 99;

        public UserUtility(){}

        static public int LoginLogic(User[] users){ //Determines user position or adds new user to system

            System.Console.WriteLine("Login: Please enter your first name");
            string userFirstName = Console.ReadLine().ToUpper();

            System.Console.WriteLine("Please enter your last name");
            string userLastName = Console.ReadLine().ToUpper();

            int count = GetAllUsers(users);
            int check = 0;
            int userVal = 0;
            userCount += count;

            int check2 = SearchUser(users, userFirstName, userLastName, userCount);
            if(check2 != 0){
                for(int i = 0; i < userCount; i++){
                    if(CompareNames(users[i].GetFirstName(), userFirstName)){
                        if(CompareNames(users[i].GetLastName(), userLastName)){
                            Console.ForegroundColor = ConsoleColor.Blue;
                            System.Console.WriteLine("Welcome back " + userFirstName);
                            Console.ResetColor();
                            userVal = i;
                            check++;
                        }
                    }
                }
            }
            else{
                AddUser(users, userFirstName, userLastName);
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine("New user: " + userFirstName + userLastName + "\nUser added to system!");
                Console.ResetColor();
            }

            return userVal;
        }

        static public int GetAllUsers(User[] users){ //Imports user data from file to array
            int userCount = 0;

            StreamReader inFile = new StreamReader("users.txt");
                
                string line; //Priming read
                while ((line = inFile.ReadLine()) != null && userCount < MAX_USERS){
                    
                    string[] temp = line.Split('#');

                    if (temp.Length >= 5)
                    {
                        int userID = int.Parse(temp[0]);
                        string userEmail = temp[1];
                        string userFirst = temp[2];
                        string userLast = temp[3];
                        int userAge = int.Parse(temp[4]);

                        User user = new User(userID, userEmail, userFirst, userLast, userAge);
                        users[userCount] = user;
                        userCount++;
                    }
                    //Update read in while condition
                }
            inFile.Close();

            return userCount;
        }


        static private bool CompareNames(string name1, string name2){ //Compares user data 

            if(name1 == name2){
                    return true;
                }
                else{
                    return false;
                }
        }

        static private void AddUser(User[] users, string userFirstName, string userLastName){ //Adds a new user to the array
            User temp = new User();
            System.Console.WriteLine("Welcome new user!");
            System.Console.WriteLine("Please enter your email");
            string userEmail = Console.ReadLine();
            System.Console.WriteLine("Please enter your age");
            int userAge = int.Parse(Console.ReadLine());

            User user = new User(temp.GetMaxID(), userEmail, userFirstName, userLastName, userAge);
            users[user.GetMaxID()] = user;
            user.IncrementMaxID();
        }

        static public int SearchUser(User[] users, string userFirstName, string userLastName, int userCount){ //Determines if a user exists
            int check = 0;

            for (int i = 0; i < userCount; i++){
                User user = users[i];
                if (user != null && user.GetFirstName() == userFirstName){
                    if(user.GetLastName() == userLastName){
                        check ++;
                    }
                }
            }
            return check; 
        }

        static public void EditAccountInfo(User[] users, User currentUser, int userVal){
            Console.Clear();
            System.Console.WriteLine("Welcome to the account edit interface. Select what you would like to change\n1. Email\n2. First name\n3. Last name\n4. Age\n 5. Return");
            int check = 0;
            int userInput = -1;

            while(check == 0){
                try{
                    userInput = int.Parse(Console.ReadLine());
                    check = 1;
                }
                catch{
                    RideUtility.Error("Error: Please enter a number from the above list");
                }
            }

            if(userInput != 5){
                if(userInput == 1){
                    System.Console.WriteLine("Enter your updated email");
                    users[userVal].SetUserEmail(Console.ReadLine());
                }
                else if(userInput == 2){
                    System.Console.WriteLine("Enter your updated first name");
                    users[userVal].SetFirstName(Console.ReadLine());
                }
                else if(userInput == 3){
                    System.Console.WriteLine("Enter your updated last name");
                    users[userVal].SetLastName(Console.ReadLine());
                }
                else if(userInput == 4){
                    System.Console.WriteLine("Enter your updated age");
                    int check2 = 0;
                    while(check2 == 0){
                        try{
                            users[userVal].SetUserAge(int.Parse(Console.ReadLine()));
                            check2 = 1;
                        }
                        catch{
                            RideUtility.Error("Error: Please enter a number");
                        }
                    }
                }
                else{
                    if(userInput != 5){
                        RideUtility.Error("Please enter a number from the above list");
                    }
                }
                System.Console.WriteLine("User information updated!");
            }
            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static public void UpdateUserFile(User[] users){ //Overwrites text file with updated array
            StreamWriter outFile = new StreamWriter("users.txt", false);

            for(int i = 0; i < users.Length; i ++){
                User user = users[i];
                if(user != null){
                    outFile.WriteLine($"{user.GetUserID()}#{user.GetUserEmail()}#{user.GetFirstName()}#{user.GetLastName()}#{user.GetUserAge()}");
                }
            }
            outFile.Close();
        }
    }
}