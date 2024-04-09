using System;

class Goldfish : Fish
{
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} the goldfish makes a gentle bubbling sound");
    }
}
