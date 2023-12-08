

using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using mis221_pa5_glsaacke;

Ride[] rides = new Ride[100];
User[] users = new User[100];
Reservation[] reservations = new Reservation[100];

UserUtility.GetAllUsers(users);
int userVal = UserUtility.LoginLogic(users);
User currentUser = users[userVal];

RideUtility.GetAllRides(rides);
ReserveUtility.GetAllReservations(reservations);

string menuInput = RunMenu(); 

while (menuInput != "3"){

    MenuLogic(menuInput, rides, users, reservations, currentUser, userVal);
    Console.Clear();
    menuInput = RunMenu(); 
}

UserUtility.UpdateUserFile(users);
RideUtility.UpdateRideFile(rides);
ReserveUtility.UpdateReservationFile(reservations);

//***End Main
//TODO label all methods
//Gathers user menu selection
static string RunMenu(){ //TODO create word art for waterpark 
    UserUtility.WriteLogo();
    System.Console.WriteLine("Please select an option from the menu below:\n1. Managerial Functions\n2. Customer Functions\n3. Exit");
    string userInput = Console.ReadLine();
    return userInput;
}

//Directs program to respective methods
static void MenuLogic(string menuInput, Ride[] rides, User[] users, Reservation[] reservations, User currentUser, int userVal){
    if(menuInput == "1"){
        ManagerialMenu(rides, users, reservations);
    }
    else if(menuInput == "2"){
        CustomerMenu(users, rides, reservations, currentUser, userVal);
    }
    else{
        RideUtility.Error("Please enter a valid input");
    }

}

//Directs program to respective managerial options
static void ManagerialMenu(Ride[] rides, User[] users, Reservation[] reservations){ //TODO **EXTRA add login/password system
    Console.Clear();
    int userInput = -1;
    while(userInput != 5){
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        System.Console.WriteLine("You are now in the managerial functions menu");
        Console.ResetColor();
        System.Console.WriteLine("\nPlease select an option below:\n");
        System.Console.WriteLine("1. Add a new ride to park inventory\n2. Remove a ride from park inventory\n3. Edit information about a ride\n4. Access report menu\n5. Return to home menu");
        int check = 0; //Priming read

        while(check == 0){
            try{
                userInput = int.Parse(Console.ReadLine());
                check = 1; //Update read
            }
            catch{
                RideUtility.Error("Please enter a number");
            }
        }

        if(userInput == 1){
            RideUtility.AddNewRide(rides);
        }
        else if(userInput == 2){
            RideUtility.RemoveRide(rides);
        }
        else if(userInput == 3){
            RideUtility.EditRide(rides);
        }
        else if(userInput == 4){
            ReportMenu(reservations);
        }
        else{
            RideUtility.Error("Please enter a valid input");
        }
    }
}   

//Directs program to respective reports
static void ReportMenu(Reservation[] reservations){ 
    int userInput = -1;

    while(userInput != 5){
        Console.Clear();
        System.Console.WriteLine("You are now in the REPORTS menu");
        System.Console.WriteLine("\nPlease choose from the reports below");
        System.Console.WriteLine("1. Most ridden ride\n2. Active reservations\n3. Rides Completed\n4. Top five rides\n5. Exit menu");

        try{
            userInput = int.Parse(Console.ReadLine());
        }
        catch{
            RideUtility.Error("Please enter a number");
        }


        if(userInput == 1){
            RideReports.MostRiddenRide(reservations);
        }
        else if(userInput == 2){
            RideReports.ActiveReservations(reservations);
        }
        else if(userInput == 3){
            RideReports.RidesCompleted(reservations);
        }
        else if(userInput == 4){
            RideReports.TopFiveRides(reservations);
        }
        else{
            RideUtility.Error("Please enter a valid input");
        }
    }
}

//Directs program to respective customer options
static void CustomerMenu(User[] users, Ride[] rides, Reservation[] reservations, User currentUser, int userVal){
    int userInput = -1;

    while(userInput != 6){
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        System.Console.WriteLine("You are now in the customer interface menu");
        Console.ResetColor();
        System.Console.WriteLine("\nPlease select an option from below:");
        System.Console.WriteLine("1. View all operational rides\n2. Reserve a ride\n3. View ride history\n4. Update user account information\n5. Cancel a reservation\n6. Return to home menu");
        int check = 0; //Priming read

        while(check == 0){
            try{
                userInput = int.Parse(Console.ReadLine());
                check = 1; //Update read
            }
            catch{
                RideUtility.Error("Please enter a number");
            }
        }

        if(userInput == 1){
            RideUtility.ViewAllRides(rides);
        }
        else if(userInput == 2){
            ReserveUtility.ReserveRide(rides, reservations, currentUser);
        }
        else if(userInput == 3){
            ReserveUtility.RideHistory(reservations, currentUser);
        }
        else if(userInput == 4){
            UserUtility.EditAccountInfo(users, currentUser, userVal); //Write method
        }
        else if(userInput == 5){
            ReserveUtility.CancelReservation(reservations, currentUser); //Edit date config
        }
        else{
            if(userInput != 6){
                RideUtility.Error("Please enter a valid input");
            }
        }
    }
    
}

