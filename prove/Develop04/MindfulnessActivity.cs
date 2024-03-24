using System;

public abstract class MindfulnessActivity
{
    protected string name;
    protected string description;

    public MindfulnessActivity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void StartActivity(int duration)
    {
        ShowWelcomeMessage();
        PerformActivity(duration);
        ShowEndingMessage(duration);
    }

    protected virtual void ShowWelcomeMessage()
    {
        Console.WriteLine($"Starting {name} activity...");
        Console.WriteLine(description);
    }

    protected abstract void PerformActivity(int duration);

    protected virtual void ShowEndingMessage(int duration)
    {
        Console.WriteLine($"Well done! You have completed the {name} activity for {duration} seconds.");
    }
}
