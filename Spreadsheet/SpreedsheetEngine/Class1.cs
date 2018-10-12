//Noah Taylor 011511292 Cpts 321
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

        public string Text
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

        public string Value
        {
            get
            {
                return val;
            }

        }

        protected internal void setValue(string newValue)
        {
            if(val != newValue)
            {
                val = newValue;
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
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

        public event ProgressChangedEventHandler CellPropertyChanged;



        public Spreadsheet(int rows, int columns)
        {
            sheetCells = new SpreadSheetCell[rows, columns];
            rowCount = rows;
            columnCount = columns;
            //Traverse Columns first then traverse rows
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    sheetCells[i, j] = new SpreadSheetCell(i, j, "");
                    sheetCells[i, j].PropertyChanged += this.Spreedsheet_PropertyChanged;
                }
            }

        }


        private void Spreedsheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell;
            if(e.PropertyName == "Text")
            {
                if (cell.Text[0] != '=')
                {
                    cell.setValue(cell.Text);
                }
                else
                {
                    cell.setValue()
                }
            }
        
        }

        private string calcValue(string text)
        {

        }

        private SpreadSheetCell getCell(int row, int column)
        {
            return sheetCells[row, column];
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
