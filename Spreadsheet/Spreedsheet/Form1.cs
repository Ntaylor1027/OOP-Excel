using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreedsheet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //dataGridView1.ColumnCount = 26;
            //dataGridView1.ColumnHeadersVisible = true;
            //dataGridView1.Columns[0].Name = ("A");
            dataGridView1.Columns.Add("columnA", "A");
            dataGridView1.Columns.Add("columnB", "B");
            dataGridView1.Columns.Add("columnC", "C");
            dataGridView1.Columns.Add("columnD", "D");
            dataGridView1.Columns.Add("columnE", "E");
            dataGridView1.Columns.Add("columnF", "F");
            dataGridView1.Columns.Add("columnG", "G");
            dataGridView1.Columns.Add("columnH", "H");
            dataGridView1.Columns.Add("columnI", "I");
            dataGridView1.Columns.Add("columnJ", "J");
            dataGridView1.Columns.Add("columnK", "K");
            dataGridView1.Columns.Add("columnL", "L");
            dataGridView1.Columns.Add("columnM", "M");
            dataGridView1.Columns.Add("columnN", "N");
            dataGridView1.Columns.Add("columnO", "O");
            dataGridView1.Columns.Add("columnP", "P");
            dataGridView1.Columns.Add("columnQ", "Q");
            dataGridView1.Columns.Add("columnR", "R");
            dataGridView1.Columns.Add("columnS", "S");
            dataGridView1.Columns.Add("columnT", "T");
            dataGridView1.Columns.Add("columnU", "U");
            dataGridView1.Columns.Add("columnV", "V");
            dataGridView1.Columns.Add("columnW", "W");
            dataGridView1.Columns.Add("columnX", "X");
            dataGridView1.Columns.Add("columnY", "Y");
            dataGridView1.Columns.Add("columnZ", "Z");

            for (int i = 1; i <= 50; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i-1].HeaderCell.Value = i.ToString();
            }
        }

        
    }
}
