using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PilotBrothersSafe
{
    public partial class Form1 : Form
    {
        Game game;
        bool sizeChanged = false;
        public Form1()
        {
            InitializeComponent();
            game = new Game(3); // полей по умолчанию
            StartGame();
        }

        private void StartGame()
        {
            game.Start();
            for (int j = 0; j < 4; j++)
                game.RandomStep();
            Refresh();
        }

        // Кнопка новая игра
        private void newGameButton_Click(object sender, EventArgs e)
        {
            if (sizeChanged)
            {
                int size = (int)countHandler.Value;
                changeSizeSafePanel(size);
                game = new Game(size);
                sizeChanged = false;
            }
            StartGame();
        }

        // Размерность поля сейфа
        private void changeSizeSafePanel(int size)
        {
            panelGame.ColumnCount = size;
            panelGame.RowCount = size;

            int width = 100 / size;
            int height = 100 / size;

            panelGame.ColumnStyles.Clear();
            panelGame.RowStyles.Clear();
            panelGame.Controls.Clear();

            int position = 0;

            for (int row = 0; row < size; row++)
            {
                panelGame.RowStyles.Add(new RowStyle(SizeType.Percent, height));
                panelGame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
                int fontSize = panelGame.Height;
                for (int col = 0; col < size; col++)
                {
                    var button = new Button();
                    button.Tag = position++;
                    button.Dock = DockStyle.Fill;
                    button.Click += ButtonOnClick;
                    panelGame.Controls.Add(button, col, row);
                }
            }
        }

        // Обновление панели
        public void Refresh()
        {
            foreach (Control control in panelGame.Controls)

            {
                Button button = control as Button;
                int position = Convert.ToInt16(button.Tag);
                if (game.GetOrientation(position) == Orientation.Horizontal)
                    //button.Image = Image.FromFile("D:/Изучение C#/Projects/PilotBrothersSafe/Resources/horizontal.jpg");
                    button.Text = "-";
                else
                    //button.Image = Image.FromFile("D:/Изучение C#/Projects/PilotBrothersSafe/Resources/vertical.jpg");
                    button.Text = "|";
                button.Font = new Font("Arial", button.Height, GraphicsUnit.Pixel);

            }
        }

        // Нажатие рукоятей
        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var button = (Button)sender;
            int position = Convert.ToInt16(button.Tag);
            game.TurnHandlers(position);
            Refresh();
            if (game.Check())
            {
                MessageBox.Show("Сейф открыт!");
                StartGame();
            }
        }

        private void countHandler_ValueChanged(object sender, EventArgs e)
        {
            sizeChanged = true;
        }
    }
}
