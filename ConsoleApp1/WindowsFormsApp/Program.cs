using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class QuizForm : Form
{
    private int lives = 3;
    private int score = 0;
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
        "Profession of Komatsu in the anime Toriko: ---- ",
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
    private Button submitButton;

    public QuizForm()
    {
        // Initialize Form and Controls
        this.Text = "Anime Quiz Game";
        this.Size = new Size(500, 300);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.White;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.KeyPreview = true;
        this.Padding = new Padding(20);

        // Question Label
        questionLabel = new Label()
        {
            Left = 20,
            Top = 20,
            Width = 440,
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            ForeColor = Color.FromArgb(60, 60, 60),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Answer TextBox
        answerTextBox = new TextBox()
        {
            Left = 20,
            Top = 60,
            Width = 300,
            Font = new Font("Segoe UI", 12),
            BackColor = Color.FromArgb(240, 240, 240),
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(10)
        };

        // Submit Button
        submitButton = new Button()
        {
            Text = "Submit",
            Left = 330,
            Top = 60,
            Width = 120,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            BackColor = Color.FromArgb(30, 144, 255),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Height = 30
        };
        submitButton.Click += SubmitAnswer;

        // Lives Label
        livesLabel = new Label()
        {
            Left = 20,
            Top = 100,
            Width = 200,
            Font = new Font("Segoe UI", 12),
            ForeColor = Color.FromArgb(255, 69, 58),
            TextAlign = ContentAlignment.MiddleLeft
        };

        // Score Label
        scoreLabel = new Label()
        {
            Left = 20,
            Top = 130,
            Width = 200,
            Font = new Font("Segoe UI", 12),
            ForeColor = Color.FromArgb(50, 205, 50),
            TextAlign = ContentAlignment.MiddleLeft
        };

        // Add Controls to Form
        this.Controls.Add(questionLabel);
        this.Controls.Add(answerTextBox);
        this.Controls.Add(submitButton);
        this.Controls.Add(livesLabel);
        this.Controls.Add(scoreLabel);

        // Initialize UI
        UpdateUI();
        ShowNextQuestion();

        // Form KeyDown event for Enter key functionality
        this.KeyDown += Form_KeyDown;
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

        if (answers[currentIndex].ToLower() == userAnswer)
        {
            score += 10;
            MessageBox.Show("Correct! You earned 10 points.");
            questions.RemoveAt(currentIndex);
            answers.RemoveAt(currentIndex);
        }
        else
        {
            lives--;
            MessageBox.Show("Wrong! You lost a life.");
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
            questionLabel.Text = questions[randomIndex];
        }
        else
        {
            // Handle the case where there are no more questions
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
        if (lives == 0)
        {
            MessageBox.Show("Game Over! You've run out of lives.");
        }
        else
        {
            MessageBox.Show("Congratulations! You've answered all the questions!");
        }

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
