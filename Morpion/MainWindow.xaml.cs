using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Morpion
{
    public partial class MainWindow : Window
    {

        string[,] array = new string[3,3];
        // [0,0] [0,1] [0,2]
        // [1,0] [1,1] [1,2]
        // [2,0] [2,1] [2,2]

        Button[,] buttons= new Button[3,3];

        int round = 0;
        string symbol = "";
        bool playComputer = false;
        bool gameFinished = false;

        int langIndex = 0; // FR = 0, DE = 1

        public MainWindow()
        {
            InitializeComponent();

            buttons[0, 0] = bt0;
            buttons[0, 1] = bt1;
            buttons[0, 2] = bt2;
            buttons[1, 0] = bt3;
            buttons[1, 1] = bt4;
            buttons[1, 2] = bt5;
            buttons[2, 0] = bt6;
            buttons[2, 1] = bt7;
            buttons[2, 2] = bt8;
        }

        string SetSymbol(int dim0, int dim1)
        {
            if (round % 2 == 0) // Player 1
            {
                symbol = "X";
                SetPlayer(2);
            }

            if (round % 2 == 1 && !playComputer) // Player 2
            { 
                symbol = "O";
                SetPlayer(1);
            }
            array[dim0, dim1] = symbol;

            return symbol;
        }

        void SetPlayer(int player)
        {
            switch (player)
            {
                case 1:
                    lblPlayer1.Visibility = Visibility.Visible;
                    lblPlayer2.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    lblPlayer1.Visibility = Visibility.Hidden;
                    lblPlayer2.Visibility = Visibility.Visible;
                    break;
            }
        }

        int GameStatus() // 0 to be continued, 1 draw, 2 won
        {
            // [0,0] [0,1] [0,2]
            // [1,0] [1,1] [1,2]
            // [2,0] [2,1] [2,2]
            
            // Check if won
            string symbolToFind = string.Format("{0}{1}{2}", symbol, symbol, symbol);

            string row1 = string.Format("{0}{1}{2}", array[0, 0], array[0, 1], array[0, 2]);
            string row2 = string.Format("{0}{1}{2}", array[1, 0], array[1, 1], array[1, 2]);
            string row3 = string.Format("{0}{1}{2}", array[2, 0], array[2, 1], array[2, 2]);

            string col1 = string.Format("{0}{1}{2}", array[0, 0], array[1, 0], array[2, 0]);
            string col2 = string.Format("{0}{1}{2}", array[0, 1], array[1, 1], array[2, 1]);
            string col3 = string.Format("{0}{1}{2}", array[0, 2], array[1, 2], array[2, 2]);

            string diag1 = string.Format("{0}{1}{2}", array[0, 0], array[1, 1], array[2, 2]);
            string diag2 = string.Format("{0}{1}{2}", array[2, 0], array[1, 1], array[0, 2]);

            if (row1 == symbolToFind 
                || row2 == symbolToFind 
                || row3 == symbolToFind 
                || col1 == symbolToFind 
                || col2 == symbolToFind 
                || col3 == symbolToFind 
                || diag1 == symbolToFind 
                || diag2 == symbolToFind)
            {
                return 2;
            }

            // Check if draw
            foreach (var v in array)
                if (v == null)
                    return 0;
            return 1;
        }

        void CheckGame1()
        {
            CheckGame2();

            if (gameFinished) return;

            round++;

            if (playComputer && round % 2 == 1)
                PlayComputer();
        }

        void CheckGame2()
        {
            string playerWon = round % 2 == 0 ? (Translations.Get["msgPlayer1Won"])[langIndex] : (Translations.Get["msgPlayer2Won"])[langIndex];

            if (GameStatus() == 2)
            {
                gameFinished = true;
                MessageBox.Show(playerWon);
            }

            if (GameStatus() == 1)
            {
                gameFinished = true;
                if (MessageBox.Show((Translations.Get["msgDraw"])[langIndex], (Translations.Get["msgDrawInfo"])[langIndex], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Restart();
                }
            }
        }

        void PlayComputer()
        {
            symbol = "O";
            string symbolsToFind = "XX";

            // [0,0] [0,1] [0,2]
            // [1,0] [1,1] [1,2]
            // [2,0] [2,1] [2,2]

            string row1 = string.Format("{0}{1}{2}", array[0, 0], array[0, 1], array[0, 2]);
            string row2 = string.Format("{0}{1}{2}", array[1, 0], array[1, 1], array[1, 2]);
            string row3 = string.Format("{0}{1}{2}", array[2, 0], array[2, 1], array[2, 2]);

            string col1 = string.Format("{0}{1}{2}", array[0, 0], array[1, 0], array[2, 0]);
            string col2 = string.Format("{0}{1}{2}", array[0, 1], array[1, 1], array[2, 1]);
            string col3 = string.Format("{0}{1}{2}", array[0, 2], array[1, 2], array[2, 2]);

            string diag1 = string.Format("{0}{1}{2}", array[0, 0], array[1, 1], array[2, 2]);
            string diag2 = string.Format("{0}{1}{2}", array[2, 0], array[1, 1], array[0, 2]);

            if (row1 == symbolsToFind)
            {
                BlockBox("row1");
            }
            else if (row2 == symbolsToFind)
            {
                BlockBox("row2");
            }
            else if (row3 == symbolsToFind)
            {
                BlockBox("row3");
            }
            else if (col1 == symbolsToFind)
            {
                BlockBox("col1");
            }
            else if (col2 == symbolsToFind)
            {
                BlockBox("col2");
            }
            else if (col3 == symbolsToFind)
            {
                BlockBox("col3");
            }
            else if (diag1 == symbolsToFind)
            {
                BlockBox("diag1");
            }
            else if (diag2 == symbolsToFind)
            {
                BlockBox("diag2");
            }
            else
            {
                SetRandomBox();
            }

            CheckGame2();

            SetPlayer(1);
            round++;
        }

        void BlockBox(string box)
        {
            // [0,0] [0,1] [0,2]
            // [1,0] [1,1] [1,2]
            // [2,0] [2,1] [2,2]

            int[,] row1 = new int[,] { { 0, 0 }, { 0, 1 }, { 0, 2 } };
            int[,] row2 = new int[,] { { 1, 0 }, { 1, 1 }, { 1, 2 } };
            int[,] row3 = new int[,] { { 2, 0 }, { 2, 1 }, { 2, 2 } };

            int[,] col1 = new int[,] { { 0, 0 }, { 1, 0 }, { 2, 0 } };
            int[,] col2 = new int[,] { { 0, 1 }, { 1, 1 }, { 2, 1 } };
            int[,] col3 = new int[,] { { 0, 2 }, { 1, 2 }, { 2, 2 } };

            int[,] diag1 = new int[,] { { 0, 0 }, { 1, 1 }, { 2, 2 } };
            int[,] diag2 = new int[,] { { 2, 0 }, { 1, 1 }, { 0, 2 } };


            if (box == "row1")
            {
                for (int dim1 = 0; dim1 < 3; dim1++)
                {
                    int dim2 = 0;

                    if (array[row1[dim1, dim2], row1[dim1, dim2 + 1]] == null)
                    {
                        array[row1[dim1, dim2], row1[dim1, dim2 + 1]] = "O";
                        buttons[row1[dim1, dim2], row1[dim1, dim2 + 1]].Content = "O";
                    }
                }
            }
            else if (box == "row2")
            {
                for (int dim1 = 0; dim1 < 3; dim1++)
                {
                    int dim2 = 0;

                    if (array[row2[dim1, dim2], row2[dim1, dim2 + 1]] == null)
                    {
                        array[row2[dim1, dim2], row2[dim1, dim2 + 1]] = "O";
                        buttons[row2[dim1, dim2], row2[dim1, dim2 + 1]].Content = "O";
                    }
                }
            }
            else if (box == "row3")
            {
                for (int dim1 = 0; dim1 < 3; dim1++)
                {
                    int dim2 = 0;

                    if (array[row3[dim1, dim2], row3[dim1, dim2 + 1]] == null)
                    {
                        array[row3[dim1, dim2], row3[dim1, dim2 + 1]] = "O";
                        buttons[row3[dim1, dim2], row3[dim1, dim2 + 1]].Content = "O";
                    }
                }
            }
            else if (box == "col1")
            {
                for (int dim1 = 0; dim1 < 3; dim1++)
                {
                    int dim2 = 0;

                    if (array[col1[dim1, dim2], col1[dim1, dim2 + 1]] == null)
                    {
                        array[col1[dim1, dim2], col1[dim1, dim2 + 1]] = "O";
                        buttons[col1[dim1, dim2], col1[dim1, dim2 + 1]].Content = "O";
                    }
                }
            }
            else if (box == "col2")
            {
                for (int dim1 = 0; dim1 < 3; dim1++)
                {
                    int dim2 = 0;

                    if (array[col2[dim1, dim2], col2[dim1, dim2 + 1]] == null)
                    {
                        array[col2[dim1, dim2], col2[dim1, dim2 + 1]] = "O";
                        buttons[col2[dim1, dim2], col2[dim1, dim2 + 1]].Content = "O";
                    }
                }
            }
            else if (box == "col3")
            {
                for (int dim1 = 0; dim1 < 3; dim1++)
                {
                    int dim2 = 0;

                    if (array[col3[dim1, dim2], col3[dim1, dim2 + 1]] == null)
                    {
                        array[col3[dim1, dim2], col3[dim1, dim2 + 1]] = "O";
                        buttons[col3[dim1, dim2], col3[dim1, dim2 + 1]].Content = "O";
                    }
                }
            }
            else if (box == "diag1")
            {
                for (int dim1 = 0; dim1 < 3; dim1++)
                {
                    int dim2 = 0;

                    if (array[diag1[dim1, dim2], diag1[dim1, dim2 + 1]] == null)
                    {
                        array[diag1[dim1, dim2], diag1[dim1, dim2 + 1]] = "O";
                        buttons[diag1[dim1, dim2], diag1[dim1, dim2 + 1]].Content = "O";
                    }
                }
            }
            else if (box == "diag2")
            {
                for (int dim1 = 0; dim1 < 3; dim1++)
                {
                    int dim2 = 0;

                    if (array[diag2[dim1, dim2], diag2[dim1, dim2 + 1]] == null)
                    {
                        array[diag2[dim1, dim2], diag2[dim1, dim2 + 1]] = "O";
                        buttons[diag2[dim1, dim2], diag2[dim1, dim2 + 1]].Content = "O";
                    }
                }
            }
        }

        void SetRandomBox()
        {
            bool isEmpty = false;

            while (!isEmpty)
            {
                Random rd1 = new Random();
                int dim1 = rd1.Next(0, 3);

                System.Threading.Thread.Sleep(1);

                Random rd2 = new Random();
                int dim2 = rd2.Next(0, 3);

                dim1 = dim1 == 3 ? 2 : dim1;
                dim2 = dim2 == 3 ? 2 : dim2;

                Console.WriteLine("Computer: {0} {1}", dim1, dim2);

                if (array[dim1, dim2] == null)
                {
                    isEmpty = true;
                    array[dim1, dim2] = symbol;
                    buttons[dim1, dim2].Content = symbol;
                }
            }
        }

        void Restart()
        {
            gameFinished = false;
            symbol = "";
            array = new string[3,3];

            bt0.Content = "";
            bt1.Content = "";
            bt2.Content = "";
            bt3.Content = "";
            bt4.Content = "";
            bt5.Content = "";
            bt6.Content = "";
            bt7.Content = "";
            bt8.Content = "";

            SetPlayer(1);
        }

        private void HandleButtonClick(Button bt, int dim1, int dim2)
        {
            if (gameFinished) return;

            if (bt.Content == "X" || bt.Content == "O")
                return;

            bt.Content = SetSymbol(dim1, dim2);
            CheckGame1();
        }

        private void bt0_Click(object sender, RoutedEventArgs e)
        {
            HandleButtonClick((Button)sender, 0, 0);
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            HandleButtonClick((Button)sender, 0, 1);
        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            HandleButtonClick((Button)sender, 0, 2);
        }

        private void bt3_Click(object sender, RoutedEventArgs e)
        {
            HandleButtonClick((Button)sender, 1, 0);
        }

        private void bt4_Click(object sender, RoutedEventArgs e)
        {
            HandleButtonClick((Button)sender, 1, 1);
        }

        private void bt5_Click(object sender, RoutedEventArgs e)
        {
            HandleButtonClick((Button)sender, 1, 2);
        }

        private void bt6_Click(object sender, RoutedEventArgs e)
        {
            HandleButtonClick((Button)sender, 2, 0);
        }

        private void bt7_Click(object sender, RoutedEventArgs e)
        {
            HandleButtonClick((Button)sender, 2, 1);
        }

        private void bt8_Click(object sender, RoutedEventArgs e)
        {
            HandleButtonClick((Button)sender, 2, 2);
        }

        private void btRestart_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }

        private void btComputer_Click(object sender, RoutedEventArgs e)
        {
            if (playComputer)
            {
                playComputer = false;
                ((Button)sender).Background = Brushes.White;
            }
            else
            {
                playComputer = true;
                ((Button)sender).Background = Brushes.Aquamarine;
            }
        }

        private void btLang_Click(object sender, RoutedEventArgs e)
        {
            langIndex++;
            langIndex = langIndex % 2;

            this.Title = (Translations.Get["mWTitle"])[langIndex];
            this.lblPlayer1.Content = (Translations.Get["lblPlayer1"])[langIndex];
            this.lblPlayer2.Content = (Translations.Get["lblPlayer2"])[langIndex]; 
            this.btComputer.Content = (Translations.Get["btComputer"])[langIndex];
            this.btRestart.Content = (Translations.Get["btRestart"])[langIndex];
            this.btLang.Content = (Translations.Get["btLang"])[langIndex];
        }
    }
}
