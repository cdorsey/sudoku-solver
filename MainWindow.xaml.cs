using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sudoko_Solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
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

                    Solver.numBoxes.Add(numBox);

                    boardGrid.Children.Add(numBox);
                }
            }
        }

        private void numBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NumBox numBox = (NumBox)sender;

            MessageBox.Show(Convert.ToString(numBox.Row) + ";" + Convert.ToString(numBox.Column) +
                ";" + Convert.ToString(numBox.Region));
        }

        private void solveButton_Click(object sender, RoutedEventArgs e)
        {
            Solver.solve();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (NumBox box in Solver.numBoxes)
            {
                box.Foreground = new SolidColorBrush(Colors.Black);
                box.IsReadOnly = false;
                box.Text = "";
            }
        }
    }

    public class NumBox : TextBox
    {
        public NumBox() { }
        
        [Browsable(false)]
        public int Region {
            get
            {
                if (this.Row >= 0 && this.Row < 3)
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
                this.Foreground = new SolidColorBrush(Colors.Blue);
                this.Text = Convert.ToString(Array.IndexOf(possibleValues, true) + 1);
                this.valueSet = true;

                Solver.updateBoard(this);
            }
        }

        [Browsable(false)]
        public bool valueSet { get; set; } = false;
    }
}
