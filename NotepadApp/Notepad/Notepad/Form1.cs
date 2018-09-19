/*
Name: Noah Taylor
ID: 011511292 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Numerics;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFileDialog = new OpenFileDialog();
            loadFileDialog.Title = "Select a File";
            loadFileDialog.ShowDialog();

            using( StreamReader sr = new StreamReader(loadFileDialog.OpenFile()))
            {
                textBox1.Text = sr.ReadToEnd();
            }

        }

        private void LoadText(TextReader sr)
        {
            textBox1.Text = sr.ReadToEnd();
        }

        private void loadFibonacciNumbersfirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(50);
            LoadText(ftr);

        }

        private void loadFibonacciNumbersfirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(100);
            LoadText(ftr);
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Select a file";
            sfd.ShowDialog();
            using(StreamWriter sw = new StreamWriter(sfd.OpenFile()))
            {
                sw.Write(textBox1.Text);
                sw.Close();
            }
        }
    }

    public class FibonacciTextReader : TextReader
    {
        public int maxNumber { get; set; }
        private int count { get; set; }
        private BigInteger? previous { get; set; }
        private BigInteger? current { get; set; }
        

        public FibonacciTextReader(int n)
        {
            this.maxNumber = n;
            this.count = 0;
            this.previous = null;
            this.current = null;
        }



        public override string ReadLine()
        {
            if (this.count < this.maxNumber)
            {
                this.count += 1;
                if (this.previous != null && this.current != null) //If not the first two numbers in the fib sequence
                {
                    BigInteger? newCurrent = this.previous + this.current;
                    this.previous = this.current;
                    this.current = newCurrent;
                    return this.current.ToString();
                }
                else if (this.previous == null && this.current != null) // If the second number in the fib sequence
                {
                    this.previous = this.current;
                    this.current = 1;
                    return this.current.ToString();
                }
                else //First number in the fib sequence
                {
                    this.current = 0;
                    return current.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        public override string ReadToEnd()
        {
            StringBuilder sb = new StringBuilder();
            int index = 1;
            string value = this.ReadLine();
            while (value != null)
            {
                sb.AppendFormat(index.ToString()+": "+ value+"\r\n");
                index += 1;
                value = this.ReadLine();
            }
            return sb.ToString();
        }
    }


}
