

using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using mis221_pa5_glsaacke;

Ride[] rides = new Ride[100];
User[] users = new User[100];

string menuInput = RunMenu(); 

while (menuInput != "4"){

    MenuLogic(menuInput, rides, users);
    Console.Clear();
    menuInput = RunMenu(); 
}

//***End Main

//Gathers user menu selection
static string RunMenu(){
    System.Console.WriteLine("Plese select an option from the menu below:\n1. Managerial Functions\n2. Customer Functions\n3. Exit");
    string userInput = Console.ReadLine();
    return userInput;
}

//Directs program to respective methods
static void MenuLogic(string menuInput, Ride[] rides, User[] users){
    if(menuInput == "1"){
        ManagerialMenu(rides, users);
    }
    else if(menuInput == "2"){
        CustomerMenu();
    }
    else{
        Error("Please enter a valid input");
    }

}

static void ManagerialMenu(Ride[] rides, User[] users){
    Console.Clear();
    System.Console.WriteLine("You are now in the managerial functions menu. Please select an option below:");
    System.Console.WriteLine("1. Add a new ride to park inventory\n2. Remove a ride from park inventory\n3. Edit information about a ride\n4. Access report menu\n5. Return to home menu");
    int check = 0; //Priming read
    int userInput;

    while(check == 0){
        try{
            userInput = int.Parse(Console.ReadLine());
            check = 1; //Update read
        }
        catch{
            Error("Please enter a number");
        }
    }

    while(userInput != 5){

        if(userInput == 1){
            AddNewRide(rides);
        }
        else if(userInput == 2){
            RemoveRide(rides);
        }
        else if(userInput == 3){
            EditRide();
        }
        else if(userInput == 4){
            ReportMenu();
        }
        else{
            Error("Please enter a valid input");
        }
    }
    
}   

static void AddNewRide(Ride[] rides){
    Console.Clear();

    System.Console.WriteLine("Enter the name of the new ride");
    string inputName = Console.ReadLine();

    System.Console.WriteLine("Enter the ride type of " + inputName);
    string inputType = Console.ReadLine();

    Ride ride = new Ride(Ride.GetMaxID(), inputName, inputType, true);

    rides[Ride.GetMaxID()] = ride;

}

static void RemoveRide(Ride[] rides){
    Console.Clear();

    System.Console.WriteLine("Enter the name of the ride you would like to remove");
    string inputRide = Console.ReadLine();

    int foundIndex = FindRide(inputRide, rides);

    if(foundIndex == -1){
        Error("Ride does not exist");
    }
    else{
        //Set new variable to rides to allow it to become invisible
    }
}

static void EditRide(){

}

static void ReportMenu(){

}

static int FindRide(string inputRide, Ride[] rides){
    int foundIndex = -1;

    for(int i = 0; i < Ride.GetMaxID(); i ++){

        if(rides[i].GetRideName() == inputRide){
            foundIndex = i;
        }
    }
    return foundIndex;
}

static void CustomerMenu(){
    System.Console.WriteLine("You are now in the customer interface menu. Please select an option below");
    System.Console.WriteLine("1. View all operational rides\n2. Reserve a ride\n3. View ride history\n4. Update user account information\n5. Cancel a reservation\n6. Return to home menu");
}

static void Error(string errorMessage){
    Console.ForegroundColor = ConsoleColor.Red;
    System.Console.WriteLine("Error: " + errorMessage);
    Console.ResetColor();
}

