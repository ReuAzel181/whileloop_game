using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

int lives = 3;
int score = 0;

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

void ShowLives()
{
    Console.WriteLine($"\nLives remaining: {lives}");
}

void ShowScore()
{
    Console.WriteLine($"Your score: {score}");
}

while (lives > 0 && questions.Any())
{
    Console.Clear();
    ShowLives();
    ShowScore();
    
    int randomIndex = random.Next(0, questions.Count);
    string randomQuestion = questions[randomIndex];

    Console.WriteLine("\n" + randomQuestion);
    
    string userAnswer = "";
    try
    {
        userAnswer = Console.ReadLine()?.ToLower()!;
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    if (answers[randomIndex].ToLower() == userAnswer)
    {
        Console.WriteLine("Correct! You earned 10 points.\n");
        score += 10;

        // Simulate some thinking time
        Thread.Sleep(1000);

        questions.RemoveAt(randomIndex);
        answers.RemoveAt(randomIndex);
    }
    else
    {
        Console.WriteLine("Wrong! You lost a life.\n");

        // Simulate some thinking time
        Thread.Sleep(1000);

        lives--;
    }
}

Console.Clear();
if (lives == 0)
{
    Console.WriteLine("Game Over! You've run out of lives.");
    ShowScore();
}
else
{
    Console.WriteLine("Congratulations! You've answered all the questions!");
    ShowScore();
}
