using System;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for first name
        Console.Write("Enter your first name: ");
        string firstName = Console.ReadLine();

        // Prompt for last name
        Console.Write("Enter your last name: ");
        string lastName = Console.ReadLine();

        // Display the formatted output
        Console.WriteLine("Your name is {0}, {1}, {0}", lastName, firstName);

        // Keep the console window open until a key is pressed
        Console.ReadKey();
    }
}
