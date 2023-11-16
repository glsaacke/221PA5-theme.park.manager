

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
    System.Console.WriteLine("1. Add a new ride to park inventory\n2. Remove a ride from park inventory\n3. Edit information about a ride\n4. Access report menu\n5. Return to home menu");
    int check = 0;
    while(check == 0){
        try{
            int userInput = int.Parse(Console.ReadLine());
            check = 1;
        }
        catch{
            Error();
        }
    }

    if(userInput == 1){

    }
    
}   

static void CustomerMenu(){
    System.Console.WriteLine("You are now in the customer interface menu. Please select an option below");
    System.Console.WriteLine("1. View all operational rides\n2. Reserve a ride\n3. View ride history\n4. Update user account information\n5. Cancel a reservation\n6. Return to home menu");
}

static void Error(){
    Console.ForegroundColor = ConsoleColor.Red;
    System.Console.WriteLine("Error: Please enter a valid input");
    Console.ResetColor();
}

