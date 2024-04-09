using System;

class Lion : Mammal
{
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} the lion roars");
    }
}
