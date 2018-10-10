using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SpreedsheetEngine
{
    public abstract class Cell : INotifyPropertyChanged
    {
        protected string text;
        protected string val;

        string Text
        {
            get
            {
                return text;
            }

            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        string Value
        {
            get
            {
                return val;
            }

            set
            {
                val = Value;
            }
        }

        protected int RowIndex { get; set; }
        protected int ColumnIndex { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string text)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(text));
            }
        }

        protected Cell(int newRowIndex, int newColumnIndex, string newText)
        {
            this.RowIndex = newRowIndex;
            this.ColumnIndex = newColumnIndex;
            this.text = newText;
        }

    }

    public class Spreadsheet : INotifyPropertyChanged
    {
        private class SpreadSheetCell : Cell
        {
            public SpreadSheetCell(int newRowIndex, int newColumnIndex, string newText) : base(newRowIndex, newColumnIndex, newText)
            {
                /*
                text = newText;
                RowIndex = newRowIndex;
                ColumnIndex = newColumnIndex;
                */
            }
        }

        private SpreadSheetCell[,] sheetCells;
        private int columnCount;
        private int rowCount;

        public int ColumnCount { get { return columnCount; } }
        public int RowCount { get { return rowCount; } }


        public Spreadsheet(int rows, int columns)
        {
            sheetCells = new SpreadSheetCell[rows, columns];
            rowCount = rows;
            columnCount = columns;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    sheetCells[i, j] = new SpreadSheetCell(i, j, "");
                }
            }

        }

        private SpreadSheetCell getCell(int row, int column)
        {
            return sheetCells[row, column];
        }

        public event PropertyChangedEventHandler CellPropertyChanged;


        protected void OnPropertyChanged(string text)
        {
            PropertyChangedEventHandler handler = CellPropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(text));
            }
        }
    }

}
