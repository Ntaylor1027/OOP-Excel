//Noah Taylor 011511292 Cpts 321
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SpreadsheetEngine
{
    public class Spreadsheet //: INotifyPropertyChanged
    {
        internal class SpreadSheetCell : Cell
        {
            public SpreadSheetCell(int newRowIndex, int newColumnIndex, string newText) : base(newRowIndex, newColumnIndex, newText)
            {
                /*
                text = newText;
                RowIndex = newRowIndex;
                ColumnIndex = newColumnIndex;
                */
            }

            public void setValue(string newValue)
            {
                this.val = newValue;
            }
        }

        private SpreadSheetCell[,] sheetCells;
        private int columnCount;
        private int rowCount;

        public int ColumnCount { get { return columnCount; } }
        public int RowCount { get { return rowCount; } }

        public event PropertyChangedEventHandler CellPropertyChanged;



        public Spreadsheet(int rows, int columns)
        {
            sheetCells = new SpreadSheetCell[rows, columns];
            rowCount = rows;
            columnCount = columns;
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    sheetCells[j,i] = new SpreadSheetCell( j, i, "");
                    sheetCells[j,i].PropertyChanged += this.Spreedsheet_PropertyChanged;
                }
            }

        }


        private void Spreedsheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell;
            if (e.PropertyName == "Text")
            {
                if(cell.Text == null || cell.Text == "")
                {
                    cell.setValue("");
                }
                else if (cell.Text[0] != '=')
                {
                    cell.setValue(cell.Text);
                }
                else
                {
                    cell.setValue(calcValue(cell.Text));
                }

                if (CellPropertyChanged != null)
                {
                    CellPropertyChanged(sender, e);
                }
            }

        }

        private string calcValue(string text)
        {
            char[] operators = { '+', '/', '*', '-' };
            if (text.IndexOfAny(operators) != -1)
            {
                ExpTree expression = new ExpTree(text.Substring(1), sheetCells);
                /*if (expression.vars.Count != 0)
                {
                    foreach (KeyValuePair<string, double> cellName in expression.vars)
                    {
                        int row = Convert.ToInt32(cellName.Key.Substring(1)) - 1;
                        int column = Convert.ToInt32(cellName.Key[0]) - 65;
                        if (row < 0 || row > 50 || column < 0 || column > 25) { return "#REF"; }
                        SpreadSheetCell copyCell = (SpreadSheetCell)getCell(row, column);
                        expression.SetVar(cellName.Key, Convert.ToDouble(copyCell.Value));
                    }
                }*/
                return expression.EvalString();
            }
            else
            {
                if (text.Length > 3)
                {
                    return "#REF";
                }
                if(Int32.TryParse(text.Substring(2), out int row) == false)
                {
                    return "#REF";
                }
                 row -= 1;
                int column = Convert.ToInt32(text[1]) - 65; //Convert character to ascii then subtract 65 to get int of column
                if (row < 0 || row > rowCount || column < 0 || column > columnCount)
                {
                    return "#REF";
                }
                else
                {
                    SpreadSheetCell copyCell = (SpreadSheetCell)getCell(row, column);
                    return copyCell.Value;
                }
            }
        }

        public Cell getCell(int row, int column)
        {
            return sheetCells[row,column];
        }

        // public event PropertyChangedEventHandler CellPropertyChanged;

        /*
        protected void OnPropertyChanged(string text)
        {
            PropertyChangedEventHandler handler = CellPropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(text));
            }
        }
        */
        
    }
}
