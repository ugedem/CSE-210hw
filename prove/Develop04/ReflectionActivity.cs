using System;
using System.Threading;

public class ReflectionActivity : MindfulnessActivity
{
    public ReflectionActivity() : base("Reflection", "Reflect on times of strength and perseverance.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        string[] prompts = {
            "What motivates you in life to persevere despite your difficulties.",
            "Think of a time when you almost broke down but you managed to overcome it.",
            "Reflect on a time that you were strengthened of the Lord.",
            "Reflect on a time that you were physically and mentally strong."
        };

        Console.WriteLine("Let's begin reflection...");

        for (int i = 0; i < duration; i++)
        {
            string prompt = prompts[new Random().Next(prompts.Length)];
            Console.WriteLine(prompt); // Display the prompt
            Thread.Sleep(3000); // Pause for 3 seconds
        }
    }
}
