//Noah Taylor 011511292 Cpts 321
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SpreadsheetEngine
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

        public int RowIndex { get; protected set; }
        public int ColumnIndex { get; protected set; }
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

  

}
