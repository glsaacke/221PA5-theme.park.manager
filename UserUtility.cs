namespace mis221_pa5_glsaacke
{
    public class UserUtility
    {
        static int userCount = 0;
        private const int MAX_USERS = 99;

        public UserUtility(){}

        public int LoginLogic(User[] users){ //Determines user position or adds new user to system

            System.Console.WriteLine("Login: Please enter your first name");
            string userFirstName = Console.ReadLine().ToUpper();

            System.Console.WriteLine("Please enter your last name");
            string userLastName = Console.ReadLine().ToUpper();

            int count = GetUsersFromFile(users);
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

        public int GetUsersFromFile(User[] users){ //Imports user data from file to array
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


        private bool CompareNames(string name1, string name2){ //Compares user data 

            if(name1 == name2){
                    return true;
                }
                else{
                    return false;
                }
        }

        private void AddUser(User[] users, string userFirstName, string userLastName){ //Adds a new user to the array
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

        public int SearchUser(User[] users, string userFirstName, string userLastName, int userCount){ //Determines if a user exists
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

        public void UpdateUserFile(User[] users){ //Overwrites text file with updated array
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