﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class QuizForm : Form
{
    private int lives = 5;
    private int score = 0;
    private int highScore = 0;
    private DateTime questionStartTime;
    private TimeSpan questionTimeLimit = TimeSpan.FromSeconds(30); // Set time limit for each question
    private Timer timer = new Timer();

    private Random random = new Random();

    private List<string> questions = new List<string>
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
        "Profession of Komatsu in the anime Toriko ",
        "Goku's first son ",
        "Beerus the _________ ",
        "First name of 11th division in Bleach ",
        "First name of Yuu Otosaka sister ",
        "Main charater in Hero Academia (Full Name)"
    };

    private List<string> answers = new List<string>
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

    private Label questionLabel;
    private TextBox answerTextBox;
    private Label livesLabel;
    private Label scoreLabel;
    private Label timerLabel; // Label to display the timer
    private Button submitButton;

    public QuizForm()
    {
        // Initialize Form and Controls
        this.Text = "Anime Quiz Game";
        this.Size = new Size(500, 350); // Adjust size to accommodate the timer label
        this.StartPosition = FormStartPosition.CenterScreen;

        // Initialize Controls
        questionLabel = new Label { Left = 20, Top = 20, Width = 440 };
        answerTextBox = new TextBox { Left = 20, Top = 60, Width = 300 };
        submitButton = new Button { Text = "Submit", Left = 330, Top = 60, Width = 120 };
        livesLabel = new Label { Left = 20, Top = 100, Width = 200 };
        scoreLabel = new Label { Left = 20, Top = 130, Width = 200 };
        timerLabel = new Label { Left = 20, Top = 160, Width = 200 }; // Position the timer label

        // Apply styles
        StyleManager.ApplyStyles(this);
        StyleManager.ApplyLabelStyles(questionLabel, true);
        StyleManager.ApplyLabelStyles(livesLabel);
        StyleManager.ApplyLabelStyles(scoreLabel);
        StyleManager.ApplyLabelStyles(timerLabel); // Apply styles to the timer label

        // Add Controls to Form
        this.Controls.Add(questionLabel);
        this.Controls.Add(answerTextBox);
        this.Controls.Add(submitButton);
        this.Controls.Add(livesLabel);
        this.Controls.Add(scoreLabel);
        this.Controls.Add(timerLabel); // Add the timer label

        // Initialize Timer
        timer.Interval = 1000; // Set the timer interval to 1 second
        timer.Tick += Timer_Tick;
        timer.Start();

        LoadHighScore();
        // Initialize UI
        UpdateUI();
        ShowNextQuestion();

        // Event handlers
        submitButton.Click += SubmitAnswer;
        this.KeyDown += Form_KeyDown;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        TimeSpan remainingTime = questionTimeLimit - (DateTime.Now - questionStartTime);
        if (remainingTime <= TimeSpan.Zero)
        {
            // Time's up, treat it as an incorrect answer
            lives--;
            MessageBox.Show("Time's up! You lost a life.");
            UpdateUI();
            ShowNextQuestion();

            if (lives <= 0 || !questions.Any())
            {
                EndGame();
            }
        }
        else
        {
            timerLabel.Text = $"Time left: {remainingTime.ToString(@"mm\:ss")}";
        }
    }

    private void LoadHighScore()
    {
        string filePath = "highscore.txt";
        if (File.Exists(filePath))
        {
            string highScoreText = File.ReadAllText(filePath);
            int.TryParse(highScoreText, out highScore);
        }
    }

    private void SaveHighScore()
    {
        string filePath = "highscore.txt";
        File.WriteAllText(filePath, highScore.ToString());
    }

    private void Form_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SubmitAnswer(this, EventArgs.Empty);
        }
    }

    private void SubmitAnswer(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(answerTextBox.Text)) return;

        string userAnswer = answerTextBox.Text.Trim().ToLower();
        int currentIndex = questions.IndexOf(questionLabel.Text);

        if (currentIndex == -1)
        {
            MessageBox.Show("No question found. Please try again.");
            return;
        }

        bool isCorrect = answers[currentIndex].ToLower() == userAnswer;
        TimeSpan answerTime = DateTime.Now - questionStartTime;
        int timePenalty = (int)answerTime.TotalSeconds * 2; // Penalty of 2 points per second

        if (isCorrect)
        {
            int scoreIncrement = Math.Max(10 - timePenalty, 1); // Minimum score increment of 1
            score += scoreIncrement;
            questions.RemoveAt(currentIndex);
            answers.RemoveAt(currentIndex);

            // Random chance to restore a life
            if (random.NextDouble() < 0.2) // 20% chance
            {
                lives++;
                MessageBox.Show($"Correct! You earned {scoreIncrement} points and restored 1 life!");
            }
            else
            {
                MessageBox.Show($"Correct! You earned {scoreIncrement} points.");
            }
        }
        else
        {
            lives--;
            MessageBox.Show($"Wrong! The correct answer was '{answers[currentIndex]}'. You lost a life.");
        }

        UpdateUI();
        ShowNextQuestion();

        if (lives <= 0 || !questions.Any())
        {
            EndGame();
        }

        answerTextBox.Text = string.Empty;
    }

    private void ShowNextQuestion()
    {
        if (lives > 0 && questions.Any())
        {
            int randomIndex = random.Next(0, questions.Count);
            string selectedQuestion = questions[randomIndex];

            // Reset the question start time and timer
            questionStartTime = DateTime.Now;

            // Set the question text
            questionLabel.Text = selectedQuestion;
            timerLabel.Text = $"Time left: {questionTimeLimit.ToString(@"mm\:ss")}";

            // Check if this question has a chance to restore a life
            if (random.NextDouble() < 0.2) // 20% chance
            {
                questionLabel.ForeColor = Color.Green; // Change text color to indicate a special question
                questionLabel.Text += " (This question has a chance to restore a life!)";
                MessageBox.Show("This question has a chance to restore a life!", "Special Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                questionLabel.ForeColor = Color.Black; // Reset text color for regular questions
            }
        }
        else
        {
            EndGame();
        }
    }

    private void UpdateUI()
    {
        livesLabel.Text = $"Lives: {lives}";
        scoreLabel.Text = $"Score: {score}";
    }

    private void EndGame()
    {
        timer.Stop(); // Stop the timer when the game ends

        if (lives == 0)
        {
            MessageBox.Show("Game Over! You've run out of lives.");
        }
        else
        {
            MessageBox.Show("Congratulations! You've answered all the questions!");
        }

        // Check if the current score is higher than the high score
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }

        MessageBox.Show($"Your final score is {score}. High score: {highScore}", "Game Over");

        Application.Exit();
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new QuizForm());
    }
}
