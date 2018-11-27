//Noah Taylor 011511292 Cpts 321
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadsheetEngine;
using System.IO;

namespace Spreedsheet
{
    public partial class Form1 : Form
    {
        Cell cellSelected;
        private Spreadsheet sheet;
        //public event PropertyChangedEventHandler Sheet_CellPropertyChanged;

        public Form1()
        {
            InitializeComponent();
            //dataGridView1.ColumnCount = 26;
            //dataGridView1.ColumnHeadersVisible = true;
            //dataGridView1.Columns[0].Name = ("A");
            //Spreedsheet spreedsheet = new Spreedsheet(26, 50);
            initSheet();
            initDataGrid();
            
        }

        private void initDataGrid()
        {
            dataGridView1.Columns.Add("A", "A");
            dataGridView1.Columns.Add("B", "B");
            dataGridView1.Columns.Add("C", "C");
            dataGridView1.Columns.Add("D", "D");
            dataGridView1.Columns.Add("E", "E");
            dataGridView1.Columns.Add("F", "F");
            dataGridView1.Columns.Add("G", "G");
            dataGridView1.Columns.Add("H", "H");
            dataGridView1.Columns.Add("I", "I");
            dataGridView1.Columns.Add("J", "J");
            dataGridView1.Columns.Add("K", "K");
            dataGridView1.Columns.Add("L", "L");
            dataGridView1.Columns.Add("M", "M");
            dataGridView1.Columns.Add("N", "N");
            dataGridView1.Columns.Add("O", "O");
            dataGridView1.Columns.Add("P", "P");
            dataGridView1.Columns.Add("Q", "Q");
            dataGridView1.Columns.Add("R", "R");
            dataGridView1.Columns.Add("S", "S");
            dataGridView1.Columns.Add("T", "T");
            dataGridView1.Columns.Add("U", "U");
            dataGridView1.Columns.Add("V", "V");
            dataGridView1.Columns.Add("W", "W");
            dataGridView1.Columns.Add("X", "X");
            dataGridView1.Columns.Add("Y", "Y");
            dataGridView1.Columns.Add("Z", "Z");

            for (int i = 1; i <= 50; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

            dataGridView1.RowHeadersWidth = 50;
        }
        

        private void initSheet()
        {
             sheet = new Spreadsheet(50,26);
            sheet.CellPropertyChanged += Sheet_CellPropertyChanged;
        }

        private void Sheet_CellPropertyChanged(object sender, EventArgs e)
        {
            Cell current = (Cell)sender;

            dataGridView1.Rows[current.RowIndex].Cells[current.ColumnIndex].Value = current.Value;
        }

        private void demo() 
        {
            Random r = new Random();
            for (int i = 0; i < 50; i++)
            {
                int row = r.Next(50);
                int column = r.Next(26);
                sheet.getCell(row, column).Text = "Hello World!";
            }

            for (int i = 0; i < sheet.RowCount; i++)
            {
                sheet.getCell(i, 1).Text = "This is cell B" + (i + 1).ToString();
            }

            for (int i = 0; i < sheet.RowCount; i++)
            {
                sheet.getCell(i, 0).Text = "=B" + (i + 1).ToString();
            }

            int a = 1;
            int b = 1;
            int c = a + b;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            demo();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(textBox1.Text.ToString());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var item = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            cellSelected = sheet.getCell(e.RowIndex, e.ColumnIndex);
            if (sheet.getCell(e.RowIndex, e.ColumnIndex).Value != null)
            {
                textBox1.Text = sheet.getCell(e.RowIndex, e.ColumnIndex).Text.ToString();
            }
            else
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                cellSelected.Text = textBox1.Text;
                evaluateAllCells();
                //MessageBox.Show(sheet.getCell(dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex).Text);
                //sheet.getCell(dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex).Text =
            }
        }

        private void evaluateAllCells()
        {
            string temp = "";
            Cell cellTouched;
            for (int i = 0; i < sheet.RowCount;i++)
            {
                for(int j = 0; j < sheet.ColumnCount; j++)
                {
                    cellTouched = sheet.getCell(i, j);
                    temp = cellTouched.Text;
                    cellTouched.Text = "";
                    cellTouched.Text = temp;
                    
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLparser parser = new XMLparser();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Select a file";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (Stream sw = sfd.OpenFile())
                {
                    parser.Save(sw, sheet, sfd.FileName);
                    
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLparser parser = new XMLparser();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (Stream sw = ofd.OpenFile())
                {
                    parser.Load(sw,ref sheet);
                    sw.Close();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
