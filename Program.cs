

using System.Reflection.Metadata.Ecma335;
using mis221_pa5_glsaacke;

string menuInput = RunMenu(); 

while (menuInput != "4"){
    MenuLogic(menuInput);
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
static void MenuLogic(string menuInput){
    if(menuInput == "1"){
        ManagerialMenu();
    }
    else if(menuInput == "2"){
        CustomerMenu();
    }
    else{
        Error();
    }

}

static void ManagerialMenu(){
    System.Console.WriteLine("You are now in the managerial functions menu. Please select an option below:");
    System.Console.WriteLine("");
}

static void CustomerMenu(){
    System.Console.WriteLine("You are now in the customer interface menu. Please select an option below");
    System.Console.WriteLine("");
}

static void Error(){
    Console.ForegroundColor = ConsoleColor.Red;
    System.Console.WriteLine("Error: Please enter a valid input");
    Console.ResetColor();
}

Ride one = new Ride();
Ride.GetRideID(); 