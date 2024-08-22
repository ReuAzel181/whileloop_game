using System.Drawing;
using System.Windows.Forms;

public static class StyleManager
{
    public static void ApplyStyles(Form form)
    {
        form.BackColor = Color.White;
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.MaximizeBox = false;
        form.KeyPreview = true;
        form.Padding = new Padding(20);

        foreach (Control control in form.Controls)
        {
            if (control is Label label)
            {
                label.Font = new Font("Segoe UI", 12);
                label.ForeColor = Color.FromArgb(60, 60, 60);
                label.TextAlign = ContentAlignment.MiddleLeft;
            }
            else if (control is TextBox textBox)
            {
                textBox.Font = new Font("Segoe UI", 12);
                textBox.BackColor = Color.FromArgb(240, 240, 240);
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Padding = new Padding(10);
            }
            else if (control is Button button)
            {
                button.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                button.BackColor = Color.FromArgb(30, 144, 255);
                button.ForeColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Height = 30;
            }
        }
    }

    public static void ApplyLabelStyles(Label label, bool isQuestion = false)
    {
        if (isQuestion)
        {
            label.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(60, 60, 60);
            label.TextAlign = ContentAlignment.MiddleCenter;
        }
        else
        {
            label.Font = new Font("Segoe UI", 12);
            label.ForeColor = Color.FromArgb(255, 69, 58);
            label.TextAlign = ContentAlignment.MiddleLeft;
        }
    }
}
