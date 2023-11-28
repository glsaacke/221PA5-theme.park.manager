

using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using mis221_pa5_glsaacke;

Ride[] rides = new Ride[100];
User[] users = new User[100];
RideUtility rUtility = new RideUtility();
RideReports reports = new RideReports();
UserUtility uUtility = new UserUtility();

uUtility.GetUsersFromFile(users);
int userVal = uUtility.LoginLogic(users);

string menuInput = RunMenu(); 

while (menuInput != "3"){

    MenuLogic(menuInput, rides, users, rUtility, reports);
    Console.Clear();
    menuInput = RunMenu(); 
}

uUtility.UpdateUserFile(users);
rUtility.UpdateRideFile(rides);

//***End Main

//Gathers user menu selection
static string RunMenu(){
    System.Console.WriteLine("Please select an option from the menu below:\n1. Managerial Functions\n2. Customer Functions\n3. Exit");
    string userInput = Console.ReadLine();
    return userInput;
}

//Directs program to respective methods
static void MenuLogic(string menuInput, Ride[] rides, User[] users, RideUtility rUtility, RideReports reports){
    if(menuInput == "1"){
        ManagerialMenu(rides, users, rUtility, reports);
    }
    else if(menuInput == "2"){
        CustomerMenu();
    }
    else{
        rUtility.Error("Please enter a valid input");
    }

}

//Directs program to respective managerial options
static void ManagerialMenu(Ride[] rides, User[] users, RideUtility rUtility, RideReports reports){
    Console.Clear();
    int userInput = -1;
    while(userInput != 5){
        Console.Clear();
        System.Console.WriteLine("You are now in the managerial functions menu. Please select an option below:");
        System.Console.WriteLine("1. Add a new ride to park inventory\n2. Remove a ride from park inventory\n3. Edit information about a ride\n4. Access report menu\n5. Return to home menu");
        int check = 0; //Priming read

        while(check == 0){
            try{
                userInput = int.Parse(Console.ReadLine());
                check = 1; //Update read
            }
            catch{
                rUtility.Error("Please enter a number");
            }
        }

        if(userInput == 1){
            rUtility.AddNewRide(rides);
        }
        else if(userInput == 2){
            rUtility.RemoveRide(rides);
        }
        else if(userInput == 3){
            rUtility.EditRide(rides);
        }
        else if(userInput == 4){
            ReportMenu(reports, rUtility);
        }
        else{
            rUtility.Error("Please enter a valid input");
        }
    }
}   

//Directs program to respective reports
static void ReportMenu(RideReports reports, RideUtility rUtility){
    System.Console.WriteLine("Please choose from the reports below");
    System.Console.WriteLine("1. Most ridden ride\n 2. Active reservations\n 3. Rides Completed\n4. Top five rides\n 5. Exit menu");
    int userInput = -1;

    try{
        userInput = int.Parse(Console.ReadLine());
    }
    catch{
        rUtility.Error("Please enter a number");
    }

    while(userInput != 5){
        if(userInput == 1){
            reports.MostRiddenRide();
        }
        else if(userInput == 2){
            reports.ActiveReservations();
        }
        else if(userInput == 3){
            reports.RidesCompleted();
        }
        else if(userInput == 4){
            reports.TopFiveRides();
        }
        else{
            rUtility.Error("Please enter a valid input");
        }
    }
}

//Directs program to respective customer options
static void CustomerMenu(){
    System.Console.WriteLine("You are now in the customer interface menu. Please select an option below");
    System.Console.WriteLine("1. View all operational rides\n2. Reserve a ride\n3. View ride history\n4. Update user account information\n5. Cancel a reservation\n6. Return to home menu");
}

