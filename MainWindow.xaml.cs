using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoko_Solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public static List<NumBox> numBoxes = new List<NumBox>();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    NumBox numBox = new NumBox();
                    numBox.SetValue(Grid.RowProperty, i);
                    numBox.SetValue(Grid.ColumnProperty, j);

                    numBoxes.Add(numBox);

                    boardGrid.Children.Add(numBox);
                }
            }
        }

        private void NumBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && solveButton.IsEnabled)
            {
                solveButton_Click(sender, e);
                MessageBox.Show(e.OriginalSource.ToString());
            }
        }

        private void solveButton_Click(object sender, RoutedEventArgs e)
        {
            solveButton.IsEnabled = false;
            resetButton.Focus();
            Solver.solve();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (NumBox box in numBoxes)
            {
                box.Foreground = Brushes.Black;
                box.IsReadOnly = false;
                box.Text = "";
            }
            numBoxes[0].Focus();
            solveButton.IsEnabled = true;
        }
    }

    public class NumBox : TextBox
    {
        public NumBox() { }
        
        [Browsable(false)]
        public int Region {
            get
            {
                if (Row >= 0 && this.Row < 3)
                {
                    if (this.Column >= 0 && this.Column < 3)
                        return 0;
                    else if (this.Column >= 3 && this.Column < 6)
                        return 1;
                    else if (this.Column >= 6 && this.Column < 9)
                        return 2;
                }
                else if (this.Row >= 3 && this.Row < 6)
                {
                    if(this.Column >= 0 && this.Column < 3)
                        return 3;
                    else if (this.Column >= 3 && this.Column < 6)
                        return 4;
                    else if (this.Column >= 6 && this.Column < 9)
                        return 5;
                }
                else if (this.Row >= 6 && this.Row < 9)
                {
                    if(this.Column >= 0 && this.Column < 3)
                        return 6;
                    else if (this.Column >= 3 && this.Column < 6)
                        return 7;
                    else if (this.Column >= 6 && this.Column < 9)
                        return 8;
                }
                throw new IndexOutOfRangeException();
            }
        }

        [Browsable(false)]
        public int Row
        {
            get { return Grid.GetRow(this); }
        }

        [Browsable(false)]
        public int Column
        {
            get { return Grid.GetColumn(this); }
        }

        [Browsable(false)]
        public bool[] possibleValues { get; } = Enumerable.Repeat<bool>(true, 9).ToArray<bool>();

        public void removeValue(int i)
        {
            possibleValues[i] = false;

            int sum = 0;
            Array.ForEach(possibleValues, delegate (bool j) { if (j) sum++; });

            if (sum == 1 && !valueSet)
            {
                Foreground = Brushes.Blue;
                Text = Convert.ToString(Array.IndexOf(possibleValues, true) + 1);
                valueSet = true;

                Solver.updateBoard(this);
            }
        }

        [Browsable(false)]
        public bool valueSet { get; set; } = false;
    }
}
