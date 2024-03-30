using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

abstract class Goal
{
    protected string type;
    protected string name;
    protected string description;
    protected int points;

    public Goal(string type, string name, string description, int points)
    {
        this.type = type;
        this.name = name;
        this.description = description;
        this.points = points;
    }

    public abstract void Complete();

    public void DisplayGoal()
    {
        Console.WriteLine($"{type} - Name: {name}, Description: {description}, Points: {points}");
    }

    public int Points => points;

    public string Name => name;

    // Method to save goal details to a file
    public virtual string SaveGoal()
    {
        return $"{type},{name},{description},{points}";
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string type, string name, string description, int points) : base(type, name, description, points)
    {
    }

    public override void Complete()
    {
        Console.WriteLine($"Simple Goal '{Name}' completed!");
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string type, string name, string description, int points) : base(type, name, description, points)
    {
    }

    public override void Complete()
    {
        Console.WriteLine($"Eternal Goal '{Name}' recorded!");
    }
}

class ChecklistGoal : Goal
{
    private int numberTimes;
    private int bonusPoints;
    private int timesCompleted;

    public ChecklistGoal(string type, string name, string description, int points, int numberTimes, int bonusPoints) : base(type, name, description, points)
    {
        this.numberTimes = numberTimes;
        this.bonusPoints = bonusPoints;
        this.timesCompleted = 0;
    }

    public override void Complete()
    {
        timesCompleted++;
        if (timesCompleted >= numberTimes)
        {
            Console.WriteLine($"Checklist Goal '{Name}' completed with bonus {bonusPoints} points!");
        }
        else
        {
            Console.WriteLine($"Checklist Goal '{Name}' completed!");
        }
    }
}

class NegativeGoal : Goal
{
    public NegativeGoal(string type, string name, string description, int points) : base(type, name, description, points)
    {
    }

    public override void Complete()
    {
        Console.WriteLine($"Negative Goal '{Name}' recorded!");
    }
}

class GoalManagement
{
    private List<Goal> goals;

    public GoalManagement()
    {
        goals = new List<Goal>();
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void ListGoals()
    {
        foreach (var goal in goals)
        {
            goal.DisplayGoal();
        }
    }

    public int GetTotalPoints()
    {
        int totalPoints = 0;
        foreach (var goal in goals)
        {
            totalPoints += goal.Points;
        }
        return totalPoints;
    }

    // Method to record an event for a specific goal
    public void RecordGoalEvent(string goalName)
    {
        Goal goal = goals.Find(g => g.Name.Equals(goalName, StringComparison.OrdinalIgnoreCase));
        if (goal != null)
        {
            goal.Complete();
        }
        else
        {
            Console.WriteLine($"Goal '{goalName}' not found!");
        }
    }

    // Method to save goals to a file
    public void SaveGoals(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var goal in goals)
            {
                writer.WriteLine(goal.SaveGoal());
            }
        }
        Console.WriteLine("Goals saved successfully!");
    }

    // Method to load goals from a file
    public void LoadGoals(string fileName)
    {
        goals.Clear();
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 4)
                {
                    string type = parts[0];
                    string name = parts[1];
                    string description = parts[2];
                    int points = int.Parse(parts[3]);
                    Goal goal = null;
                    switch (type)
                    {
                        case "Simple Goal:":
                            goal = new SimpleGoal(type, name, description, points);
                            break;
                        case "Eternal Goal:":
                            goal = new EternalGoal(type, name, description, points);
                            break;
                        case "Check List Goal:":
                            int numberTimes = 5; // Default value
                            int bonusPoints = 0; // Default value
                            goal = new ChecklistGoal(type, name, description, points, numberTimes, bonusPoints);
                            break;
                        case "Negative Goal:":
                            goal = new NegativeGoal(type, name, description, points);
                            break;
                        default:
                            Console.WriteLine($"Invalid goal type: {type}");
                            break;
                    }
                    if (goal != null)
                    {
                        goals.Add(goal);
                    }
                }
            }
        }
        Console.WriteLine("Goals loaded successfully!");
    }
}

class MainMenu
{
    public int UserChoice()
    {
        Console.WriteLine("Main Menu:");
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. List Goals");
        Console.WriteLine("3. Record Event");
        Console.WriteLine("4. Save Goals");
        Console.WriteLine("5. Load Goals");
        Console.WriteLine("6. Quit");
        Console.Write("Enter your choice: ");
        return int.Parse(Console.ReadLine());
    }
}

class GoalMenu
{
    public int GoalChoice()
    {
        Console.WriteLine("Goal Menu:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Negative Goal");
        Console.WriteLine("5. Exit");
        Console.Write("Enter your choice: ");
        return int.Parse(Console.ReadLine());
    }
}

class Program
{
    static void Main(string[] args)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        GoalManagement goals = new GoalManagement();

        Console.Clear();
        Console.Write("\n*** Welcome to the Eternal Quest Program ****\n");
        Console.Write($"\n*** You currently have {goals.GetTotalPoints()} points! ***\n");

        MainMenu choice = new MainMenu();
        GoalMenu goalChoice = new GoalMenu();

        int action = 0;
        while (action != 6)
        {
            action = choice.UserChoice();
            switch (action)
            {
                case 1:
                    Console.Clear();
                    int goalInput = 0;
                    while (goalInput != 5)
                    {
                        goalInput = goalChoice.GoalChoice();
                        switch (goalInput)
                        {
                            case 1:
                                Console.WriteLine("What is the name of your goal?  ");
                                string name = Console.ReadLine();
                                name = textInfo.ToTitleCase(name);
                                Console.WriteLine("What is a short description of your goal?  ");
                                string description = Console.ReadLine();
                                description = textInfo.ToTitleCase(description);
                                Console.Write("What is the amount of points associated with this goal?  ");
                                int points = int.Parse(Console.ReadLine());
                                SimpleGoal sGoal = new SimpleGoal("Simple Goal:", name, description, points);
                                goals.AddGoal(sGoal);
                                goalInput = 5;
                                break;
                            case 2:
                                Console.WriteLine("What is the name of your goal?  ");
                                name = Console.ReadLine();
                                name = textInfo.ToTitleCase(name);
                                Console.WriteLine("What is a short description of your goal?  ");
                                description = Console.ReadLine();
                                description = textInfo.ToTitleCase(description);
                                Console.Write("What is the amount of points associated with this goal?  ");
                                points = int.Parse(Console.ReadLine());
                                EternalGoal eGoal = new EternalGoal("Eternal Goal:", name, description, points);
                                goals.AddGoal(eGoal);
                                goalInput = 5;
                                break;
                            case 3:
                                Console.WriteLine("What is the name of your goal?  ");
                                name = Console.ReadLine();
                                name = textInfo.ToTitleCase(name);
                                Console.WriteLine("What is a short description of your goal?  ");
                                description = Console.ReadLine();
                                description = textInfo.ToTitleCase(description);
                                Console.Write("What is the amount of points associated with this goal?  ");
                                points = int.Parse(Console.ReadLine());
                                Console.Write("How many times does this goal need to be accomplished for a bonus?  ");
                                int numberTimes = int.Parse(Console.ReadLine());
                                Console.Write("What is the bonus for accomplishing it that many times?  ");
                                int bonusPoints = int.Parse(Console.ReadLine());
                                ChecklistGoal clGoal = new ChecklistGoal("Check List Goal:", name, description, points, numberTimes, bonusPoints);
                                goals.AddGoal(clGoal);
                                goalInput = 5;
                                break;
                            case 4:
                                Console.WriteLine("What is the name of your goal?  ");
                                name = Console.ReadLine();
                                name = textInfo.ToTitleCase(name);
                                Console.WriteLine("What is a short description of your goal?  ");
                                description = Console.ReadLine();
                                description = textInfo.ToTitleCase(description);
                                Console.Write("How many points should be subtracted for not meeting this goal?  ");
                                points = int.Parse(Console.ReadLine());
                                NegativeGoal nGoal = new NegativeGoal("Negative Goal:", name, description, points);
                                goals.AddGoal(nGoal);
                                goalInput = 5;
                                break;
                            case 5:
                                break;
                            default:
                                Console.WriteLine($"\nSorry the option you entered is not valid.");
                                break;
                        }
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.Write($"\n*** You currently have {goals.GetTotalPoints()} points! ***\n");
                    goals.ListGoals();
                    break;
                case 3:
                    Console.Clear();
                    Console.Write($"\n*** You currently have {goals.GetTotalPoints()} points! ***\n");
                    Console.Write("Enter the name of the goal: ");
                    string eventName = Console.ReadLine();
                    goals.RecordGoalEvent(eventName);
                    break;
                case 4:
                    Console.Clear();
                    Console.Write($"\n*** You currently have {goals.GetTotalPoints()} points! ***\n");
                    Console.Write("Enter the file name to save goals: ");
                    string saveFileName = Console.ReadLine();
                    goals.SaveGoals(saveFileName);
                    break;
                case 5:
                    Console.Clear();
                    Console.Write($"\n*** You currently have {goals.GetTotalPoints()} points! ***\n");
                    Console.Write("Enter the file name to load goals: ");
                    string loadFileName = Console.ReadLine();
                    goals.LoadGoals(loadFileName);
                    break;
                case 6:
                    Console.WriteLine("\nThank you for using the Eternal Quest Program!\n");
                    break;
                default:
                    Console.WriteLine($"\nSorry the option you entered is not valid.");
                    break;
            }
        }
    }
}
