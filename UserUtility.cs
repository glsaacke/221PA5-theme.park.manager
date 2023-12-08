using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualBasic;

namespace mis221_pa5_glsaacke
{
    public class UserUtility
    {
        static public int userCount = 0;
        private const int MAX_USERS = 999;

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
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            if(users[i].GetAdmin() == 1){
                                System.Console.WriteLine("Welcome back " + userFirstName + " (ADMIN)");
                            }
                            else{
                                System.Console.WriteLine("Welcome back " + userFirstName);
                            }
                            Console.ResetColor();
                            userVal = i;
                            check++;
                        }
                    }
                }
            }
            else{
                AddUser(users, userFirstName, userLastName);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine("New user: " + userFirstName + " " + userLastName + "\nUser added to system!");
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

                    if (temp.Length >= 6)
                    {
                        int userID = int.Parse(temp[0]);
                        string userEmail = temp[1];
                        string userFirst = temp[2];
                        string userLast = temp[3];
                        int userAge = int.Parse(temp[4]);
                        int admin = int.Parse(temp[5]);

                        User user = new User(userID, userEmail, userFirst, userLast, userAge, admin);
                        users[userCount] = user;
                        userCount++;
                    }
                    //Update read in while condition

                }
            inFile.Close();
            User.maxID = userCount;

            return userCount;
        }


        static private bool CompareNames(string name1, string name2){ //Compares input data 

            if(name1 == name2){
                    return true;
                }
                else{
                    return false;
                }
        }

        static private void AddUser(User[] users, string userFirstName, string userLastName){ //Adds a new user to the array
            User temp = new User();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Welcome new user!\n");
            Console.ResetColor();
            System.Console.WriteLine("Please enter your email");
            string userEmail = Console.ReadLine();
            System.Console.WriteLine("Please enter your age");
            int userAge = int.Parse(Console.ReadLine());

            User user = new User(temp.GetMaxID(), userEmail, userFirstName, userLastName, userAge, 0);
            user.IncrementMaxID();
            users[user.GetMaxID()] = user;
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

        static public void EditAccountInfo(User[] users, User currentUser, int userVal){ // Changes a specified aspect of a user object
            Console.Clear();
            System.Console.WriteLine("Welcome to the account edit interface");
            System.Console.WriteLine("\nSelect what you would like to change:1\n1. Email\n2. First name\n3. Last name\n4. Age\n5. Return");
            int check = 0;
            int userInput = -1;

            while(check == 0){
                try{
                    userInput = int.Parse(Console.ReadLine());
                    check = 1;
                }
                catch{
                    RideUtility.Error("Please enter a number from the above list");
                }
            }

            if(userInput != 5){
                if(userInput == 1){
                    Console.Clear();
                    System.Console.WriteLine("Enter your updated email");
                    users[userVal].SetUserEmail(Console.ReadLine());
                }
                else if(userInput == 2){
                    Console.Clear();
                    System.Console.WriteLine("Enter your updated first name");
                    users[userVal].SetFirstName(Console.ReadLine());
                }
                else if(userInput == 3){
                    Console.Clear();
                    System.Console.WriteLine("Enter your updated last name");
                    users[userVal].SetLastName(Console.ReadLine());
                }
                else if(userInput == 4){
                    Console.Clear();
                    System.Console.WriteLine("Enter your updated age");
                    int check2 = 0;
                    while(check2 == 0){
                        try{
                            users[userVal].SetUserAge(int.Parse(Console.ReadLine()));
                            check2 = 1;
                        }
                        catch{
                            RideUtility.Error("Please enter a number");
                        }
                    }
                }
                else{
                    if(userInput != 5){
                        RideUtility.Error("Please enter a number from the above list");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("\nUser information updated!\n");
                Console.ResetColor();
            }
            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static public void ChangeAdminStatus(User[] users, int userVal){ // Requests password to change admin variable
            Console.Clear();
            System.Console.WriteLine("Attempting to change admin status\n");

            if(users[userVal].GetAdmin() == 0){
                System.Console.WriteLine("Please enter the password\n");
                string userPass = Console.ReadLine();

                if(userPass == "mis221"){
                    users[userVal].SetAdmin(1);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine("\nYou are now an admin!\n");
                    Console.ResetColor();
                }
                else{
                    RideUtility.Error("Incorrect password\n");
                }
            }
            else{
                RideUtility.Error("You are already an admin\n");
            }

            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static public void UpdateUserFile(User[] users){ //Overwrites text file with updated array
            StreamWriter outFile = new StreamWriter("users.txt", false);

            for(int i = 0; i < users.Length; i ++){
                User user = users[i];
                if(user != null){
                    outFile.WriteLine($"{user.GetUserID()}#{user.GetUserEmail()}#{user.GetFirstName()}#{user.GetLastName()}#{user.GetUserAge()}#{user.GetAdmin()}");
                }
            }
            outFile.Close();
        }

        static public void WriteLogo(){ // Displays wordart
            System.Console.WriteLine(@"   _______          ________                        ____             __  ");
            System.Console.WriteLine(@"  / ____( )_____   /_  __/ /_  ___  ____ ___  ___  / __ \____ ______/ /__");
            System.Console.WriteLine(@" / / __ |// ___/    / / / __ \/ _ \/ __ `__ \/ _ \/ /_/ / __ `/ ___/ //_/");
            System.Console.WriteLine(@"/ /_/ /  (__  )    / / / / / /  __/ / / / / /  __/ ____/ /_/ / /  / ,<   ");
            System.Console.WriteLine(@"\____/  /____/    /_/ /_/ /_/\___/_/ /_/ /_/\___/_/    \__,_/_/  /_/|_|  ");
            System.Console.WriteLine(@"                                                                         ");
        }
    }
}