using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to the Mindfulness Activities Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");
            int option;

            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.Write("Enter duration (in seconds) for breathing activity: ");
                    int breathingDuration = int.Parse(Console.ReadLine());
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.StartActivity(breathingDuration);
                    break;
                case 2:
                    Console.Write("Enter duration (in seconds) for reflection activity: ");
                    int reflectionDuration = int.Parse(Console.ReadLine());
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.StartActivity(reflectionDuration);
                    break;
                case 3:
                    Console.Write("Enter duration (in seconds) for listing activity: ");
                    int listingDuration = int.Parse(Console.ReadLine());
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.StartActivity(listingDuration);
                    break;
                case 4:
                    Console.WriteLine("Exiting program...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select again.");
                    break;
            }
        }
    }
}
