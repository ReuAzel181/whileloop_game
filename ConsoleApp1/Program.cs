using System;
using System.Collections.Generic;
using System.Linq;

int lives = 3;

List<string> questions = new List<string>
{
    "______ , Coco, Zebra, and Sunny ",
    "First name of the main character in Bleach? ",
    "How many dragon balls are needed to call Shenron? ",
    "What is the main ability of Yuu Otosaka? ",
    "Who owns Ryuk deatnote? (Full Name)",
    "Pokemon: Ash _______ ",
    "Magikarp Evolution ",
    "Ash main pokemon ",
    "Toriko's pet ",
    "Profession of Komatsu in the anime Toriko: ---- ",
    "Goku's first son ",
    "Beerus the _________ ",
    "First name of 11th division in Bleach ",
    "First name of Yuu Otosaka sister ",
    "Main charater in Hero Academia (Full Name)"
};

List<string> answers = new List<string>
{
    "Toriko",
    "Kurosaki",
    "7",
    "Plunder",
    "Light Yagami",
    "Ketchum",
    "Gyarados",
    "Pikachu",
    "Terry",
    "Chef",
    "Gohan",
    "Destroyer",
    "Zaraki",
    "Ayumi",
    "Izuku Midoriya"
};

Random random = new Random();

while (lives > 0 && questions.Any())
{
    int randomIndex = random.Next(0, questions.Count);
    string randomValue = questions[randomIndex];

    string userans = "";
    try
    {
        Console.WriteLine(randomValue);
        userans = Console.ReadLine()?.ToLower()!;
        Console.WriteLine();

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }


    if (answers[randomIndex].ToLower() == userans)
    {
        Console.WriteLine("Correct!\n");
        questions.RemoveAt(randomIndex);
        answers.RemoveAt(randomIndex);
    }
    else
    {
        questions.RemoveAt(randomIndex);
        answers.RemoveAt(randomIndex);
        Console.WriteLine("Wrong!\n");
        lives--;
    }
}

if (lives == 0)
{
    Console.WriteLine("Game over! You've run out of lives.");
}
else
{
    Console.WriteLine("Congratulations! You've answered all the questions.");
}
