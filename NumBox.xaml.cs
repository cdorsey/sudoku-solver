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

namespace Sudoko_Solver
{
    public class NumBox : System.Windows.Controls.TextBox
    {
        private int box = 0;
        private int row = 0;
        private int col = 0;

        public int memberOfBox
        {
            get { return box; }
            set { box = value; }
        }

        public int memberOfRow
        {
            get { return row; }
            set { row = value; }
        }
        public int memberOfColumn
        {
            get { return col; }
            set { col = value; }
        }

        NumBox() { }
    }
}
