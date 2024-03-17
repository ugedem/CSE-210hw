using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] arg)
    {
        // create a library for scriptures
        List<Scripture> library = new List<Scripture>();
        
        string reference; // The reference of the scripture
        ScriptureReference parsedReference = new ScriptureReference("Helaman 5:11-12");
        if (parsedReference.EndVerse == null) 
        {
            reference = $"{parsedReference.Book} {parsedReference.Chapter}:{parsedReference.StartVerse}";
        }
        else
        {
            reference = $"{parsedReference.Book} {parsedReference.Chapter}:{parsedReference.StartVerse}-{parsedReference.EndVerse}";
        }
        // string reference = "Helaman 5:11-12";
        Scripture scripture = new Scripture(reference, "And he hath power given unto him from the Father to redeem them from their sins because of repentance; therefore he hath sent his angels to declare the tidings of the conditions of repentance, which bringeth unto the power of the Redeemer, unto the salvation of their souls.\n And now, my sons, remember, remember that it is upon the rock of our Redeemer, who is Christ, the Son of God, that ye must build your foundation; that when the devil shall send forth his mighty winds, yea, his shafts in the whirlwind, yea, when all his hail and his mighty storm shall beat upon you, it shall have no power over you to drag you down to the gulf of misery and endless wo, because of the rock upon which ye are built, which is a sure foundation, a foundation whereon if men build they cannot fall.");
           DisplayScripture(scripture); // Displays the original scripture

            do // Loops until all words are hidden
            {
                 Console.WriteLine("Press Enter to hide words or type 'quit' to end:");
                 string input = Console.ReadLine() ?? "";

                 
            if (input.ToLower() == "quit") // Ends the program if the user types 'quit'
            {
                break;
            }

            string newString = HideRandomWords(scripture);
            Scripture scriptureWithUnderscores = new Scripture(reference, newString);
            DisplayScripture(scriptureWithUnderscores);
        } while (!scripture.AllWordsHidden);
    }

    static void DisplayScripture(Scripture scripture) // Displays the scripture with underscores
    {

        Console.WriteLine($"{scripture.Reference}  {scripture.Text}"); 
    }

    static string HideRandomWords(Scripture scripture)
    {
        Random random = new Random(); // Creates a new Random object
        List<string> wordsToHide = scripture.Words.Where(word => !word.IsHidden).Select(word => word.Text).ToList();

        string newString = ""; // Stores the new string with underscores

        if (wordsToHide.Count > 1)
        {
            int randomIndex1 = random.Next(wordsToHide.Count);
            int randomIndex2;

            do
            {
                randomIndex2 = random.Next(wordsToHide.Count);
            } while (randomIndex2 == randomIndex1);

            string wordToHide1 = wordsToHide[randomIndex1];
            string wordToHide2 = wordsToHide[randomIndex2];

            foreach (Word word in scripture.Words) // Hides the words
            {
                if (word.Text == wordToHide1 || word.Text == wordToHide2) 
                {
                    string underscores = ReplaceWithUnderscore(word.Text); 
                    word.Text = underscores;
                    word.Hide();
                }

                newString += word.Text + " "; // Concatenates the new string
            }
        }
        else
        {
            scripture.HideAllWords();
        }
        return newString;
    }

    static string ReplaceWithUnderscore(string word)
    {
        // Replaces the word with underscores
        char[] underscores = new char[word.Length];
        for (int i = 0; i < word.Length; i++)
        {
            underscores[i] = '_';
        }

        return new string(underscores);
    }
}

class Scripture
{
    public string Reference { get; private set; }
    public string Text { get; private set; }
    public List<Word> Words { get; private set; }

    public bool AllWordsHidden => Words.All(word => word.IsHidden);

    public Scripture(string reference, string text)
    {
        Reference = reference;
        Text = text;
        Words = InitializeWords();
    }

    private List<Word> InitializeWords()
    {
        // Split the text into words
        string[] wordArray = Text.Split(' ');
        return wordArray.Select(wordText => new Word(wordText)).ToList();
    }

    public void HideAllWords()
    {
        foreach (Word word in Words)
        {
            word.Hide();
        }
    }
}

class ScriptureReference
{
    public string Book { get; private set; } = "";
    public int Chapter { get; private set; }
    public int? StartVerse { get; private set; }
    public int? EndVerse { get; private set; }

    public ScriptureReference(string reference)
    {
        ParseReference(reference);
    }

    private void ParseReference(string reference)
    {
        // Split the reference into parts
        string[] parts = reference.Split(' ');

        // Assuming the reference format is "Book Chapter:StartVerse-EndVerse"
        if (parts.Length >= 2)
        {
            Book = parts[0];

            // Parse Chapter
            if (int.TryParse(parts[1].Split(':')[0], out int chapter))
            {
                Chapter = chapter;
            }

            // Parse StartVerse and EndVerse if available
            if (parts[1].Contains(':'))
            {
                string[] verseParts = parts[1].Split(':')[1].Split('-');

                if (int.TryParse(verseParts[0], out int startVerse))
                {
                    StartVerse = startVerse;
                }

                if (verseParts.Length == 2 && int.TryParse(verseParts[1], out int endVerse))
                {
                    EndVerse = endVerse;
                }
            }
        }
    }

}

class Word
{
    public string Text { get; set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }
}