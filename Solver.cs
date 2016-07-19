using System;

namespace Sudoko_Solver
{
    static class Solver
    {
        internal static void updateBoard(NumBox obj)
        {
            foreach (NumBox e in MainWindow.numBoxes)
            {
                if (e.valueSet)
                    continue;

                if (e.Column == obj.Column || e.Row == obj.Row || e.Region == obj.Region)
                {
                    int value = Convert.ToInt32(obj.Text);
                    if (value <= 0)
                        throw new IndexOutOfRangeException();

                    e.removeValue(value - 1);
                }
            }
        }

        public static void solve()
        {
            foreach (NumBox e in MainWindow.numBoxes)
            {
                e.IsReadOnly = true;
                if (e.Text != "")
                {
                    e.valueSet = true;

                    updateBoard(e);
                }
            }
        }
    }
}
