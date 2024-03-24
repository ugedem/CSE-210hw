using System;

class PromptGenerator
{
    public List<string> prompts;

    public PromptGenerator()
    {
        // Initialize with some example prompts
        prompts = new List<string>
        {

            "Why is it important to excercise faith in the Lord?.",
            "What is your favorite food?.",
            "Why is it imortant to study?.",
            "When do you intend settling down?.",
            "What does the Book of Mormon means to you?.",
        };
    }

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}