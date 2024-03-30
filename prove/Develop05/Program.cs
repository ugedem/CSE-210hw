using System;
using System.Collections.Generic;
using System.IO;

// Base class for goals
abstract class Goal
{
    // Shared properties for all goals
    public string Name { get; }
    public int Value { get; }
    public bool Completed { get; protected set; }

    // Constructor
    public Goal(string name, int value)
    {
        Name = name;
        Value = value;
        Completed = false;
    }

    // Method to mark the goal as completed
    public void Complete()
    {
        Completed = true;
    }

    // Abstract methods to be overridden by subclasses
    public abstract void RecordEvent();
    public abstract string DisplayStatus();
}

// Simple goal class
class SimpleGoal : Goal
{
    // Constructor
    public SimpleGoal(string name, int value) : base(name, value) { }

    // Implementation of abstract methods
    public override void RecordEvent() { }

    public override string DisplayStatus()
    {
        return Completed ? "[X]" : "[ ]";
    }
}

// Eternal goal class
class EternalGoal : Goal
{
    // Constructor
    public EternalGoal(string name, int value) : base(name, value) { }

    // Implementation of abstract methods
    public override void RecordEvent() { }

    public override string DisplayStatus()
    {
        return Completed ? "[X]" : "[ ]";
    }
}

// Checklist goal class
class ChecklistGoal : Goal
{
    private int _completedCount;
    private int _targetCount;

    // Constructor
    public ChecklistGoal(string name, int value, int targetCount) : base(name, value)
    {
        _targetCount = targetCount;
        _completedCount = 0;
    }

    // Implementation of abstract methods
    public override void RecordEvent()
    {
        _completedCount++;
        if (_completedCount >= _targetCount)
            Complete(); // Mark the goal as completed when the target count is reached
    }

    public override string DisplayStatus()
    {
        return Completed ? $"Completed {_completedCount}/{_targetCount} times" : $"Incomplete {_completedCount}/{_targetCount} times";
    }
}

// Class for managing eternal quest
class EternalQuest
{
    // List to hold goals
    private List<Goal> _goals;
    private int _score;

    // Constructor
    public EternalQuest()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    // Method to create a goal
    public void CreateGoal(string goalType, string name, int value, int targetCount = 0)
    {
        switch (goalType.ToLower())
        {
            case "simple":
                _goals.Add(new SimpleGoal(name, value));
                break;
            case "eternal":
                _goals.Add(new EternalGoal(name, value));
                break;
            case "checklist":
                _goals.Add(new ChecklistGoal(name, value, targetCount));
                break;
            default:
                Console.WriteLine("Invalid goal type");
                break;
        }
    }

    // Method to record an event for a goal
    public void RecordEvent(string goalName)
    {
        foreach (var goal in _goals)
        {
            if (goal.Name == goalName)
            {
                goal.RecordEvent();
                _score += goal.Value;
                return;
            }
        }
        Console.WriteLine($"Goal '{goalName}' not found");
    }

    // Method to display all goals
    public void DisplayGoals()
    {
        Console.WriteLine("Goals:");
        foreach (var goal in _goals)
        {
            Console.WriteLine($"{goal.Name} {goal.DisplayStatus()}");
        }
    }

    // Method to display score
    public void DisplayScore()
    {
        Console.WriteLine($"Score: {_score}");
    }

    // Method to save goals to a text file
    public void SaveToText(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var goal in _goals)
            {
                writer.WriteLine($"{goal.GetType().Name}#{goal.Name}#{goal.Value}#{(goal is ChecklistGoal ? ((ChecklistGoal)goal).DisplayStatus() : "")}");
            }
        }
    }
}

// Main program class
class Program
{
    // Main method
    static void Main(string[] args)
    {
        EternalQuest quest = new EternalQuest();

        Console.WriteLine("Welcome to Eternal Quest!");
        while (true)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Progress");
            Console.WriteLine("6. Exit");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter goal type (simple, eternal, checklist):");
                    string goalType = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter goal name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter goal value:");
                    int value;
                    if (!int.TryParse(Console.ReadLine(), out value))
                    {
                        Console.WriteLine("Invalid value. Please enter a number.");
                        continue;
                    }
                    if (goalType == "checklist")
                    {
                        Console.WriteLine("Enter target count:");
                        int targetCount;
                        if (!int.TryParse(Console.ReadLine(), out targetCount))
                        {
                            Console.WriteLine("Invalid target count. Please enter a number.");
                            continue;
                        }
                        quest.CreateGoal(goalType, name, value, targetCount);
                    }
                    else
                    {
                        quest.CreateGoal(goalType, name, value);
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter the name of the goal to record event:");
                    string eventName = Console.ReadLine();
                    quest.RecordEvent(eventName);
                    break;
                case 3:
                    quest.DisplayGoals();
                    break;
                case 4:
                    quest.DisplayScore();
                    break;
                case 5:
                    Console.WriteLine("Enter filename to save progress:");
                    string saveFilename = Console.ReadLine();
                    quest.SaveToText(saveFilename);
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    break;
            }
        }
    }
}
