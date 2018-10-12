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
                if (cell.Text[0] != '=')
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
            int row = Convert.ToInt32(text.Substring(2)) -1;
            int column = Convert.ToInt32(text[1]) - 65; //Convert character to ascii then subtract 65 to get int of column
            SpreadSheetCell copyCell = (SpreadSheetCell)getCell(row, column); 
            return copyCell.Value;

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
