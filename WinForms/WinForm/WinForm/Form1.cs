using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinForm
{
    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<int> myList = new List<int>();
            Random rdm = new Random();
         
            for(int i = 0; i < 10000; i++)
            {
                int toAppend = rdm.Next(0, 20000);
                myList.Add(toAppend);
            }

            int uniqueCount1 = UniqueCount1(ref myList);
            int uniqueCount2 = UniqueCount2(ref myList);
            int uniqueCount3 = UniqueCount3(ref myList);



            textBoxDisplay.AppendText("1. HashTable method: " + uniqueCount1.ToString() + " unique numbers");
            textBoxDisplay.AppendText("\r\n");
            textBoxDisplay.AppendText("The Time complexity for the HashTable method is O(n). I determined this by the fact that we have to traverse n items in the list and in constant time determine if they will be added (Hash table insertion on average is O(1) with worst case being O(n)) to the list.");
            textBoxDisplay.AppendText("\r\n");
            textBoxDisplay.AppendText("2. O(1) storage method: " + uniqueCount2.ToString() + " unique numbers");
            textBoxDisplay.AppendText("\r\n");
            textBoxDisplay.AppendText("3. Sorted method: " + uniqueCount3.ToString() + " unique numbers");
            

        }
        public int UniqueCount1(ref List <int> rList)
        {
            Dictionary<int,char> d = new Dictionary<int, char>();
            for(int i = 0; i < rList.Count; i++)
            {
                if (d.ContainsKey(rList[i]) == false)
                {
                    d.Add(rList[i], '\0');
                }
            }
            return d.Count;
        }

        public int UniqueCount2(ref List<int> rList)
        {
            int uniques = 0;
            bool isUnique;
            for (int i = 0; i < rList.Count; i++)
            {
                isUnique = true;
                if (i != rList.Count - 1)//The last index is always distinct as it has no comparisons to make it not unique
                {
                    for (int j = i; j < rList.Count; j++)
                    {
                        if (rList[i] == rList[j] && j != i)
                        {
                            isUnique = false;
                            break;
                        }
                    }
                    if(isUnique == true)
                    {
                        uniques += 1;
                    }
                }
                else 
                {
                    uniques += 1;
                }
    
            }
            return uniques;
        }

        public int UniqueCount3(ref List<int> rList)
        {
            rList.Sort();
            int uniques = 0;
            for (int i = 0, j = 1; j < rList.Count; i++, j++) //Count every pair from index 0 to the final index
            {
                if(rList[i] != rList[j]) // if they are different add a unique
                {
                    uniques += 1;
                }
            }
            if(rList[rList.Count-1] != rList[rList.Count - 2]) //if the last pair is different then we add an additional unique
            {
                uniques += 1;
            }
            return uniques;
        }

       
    }
}
