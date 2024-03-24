using System;
using System.Threading;

public class ListingActivity : MindfulnessActivity
{
    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        string[] prompts = {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        Console.WriteLine("Let's begin listing...");

        string prompt = prompts[new Random().Next(prompts.Length)];
        Console.WriteLine(prompt);
        Console.WriteLine("Get ready to list...");

        Thread.Sleep(5000); // Pause for 5 seconds

        Console.WriteLine("Start listing...");

       
        Thread.Sleep(duration * 1000); // Pause for the specified duration
        Console.WriteLine("Listed items.");
    }
}
