using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    public class BSTNode
    {

        public BSTNode(int val)
        {
            this.Value = val;
            this.LeftNode = null;
            this.RightNode = null;
        }

        public int Value    { get; private set; }
        public BSTNode LeftNode { get; set; }
        public BSTNode RightNode { get; set; }
    }

    public class BSTtree
    {
        public BSTtree()
        {
            this.Depth = 0;
            this.NumItems = 0;
            this.HeadNode = null;
        }

        public bool insert(int nodeVal)
        {

            bool success = false;

            if (HeadNode == null)
            {
                BSTNode newNode = new BSTNode(nodeVal);
                HeadNode = newNode;
                Depth = 1;
                success = true;
            }
            else 
            {
                BSTNode current = HeadNode;
                bool keep_going = true;
                int local_depth = 1;
                while (keep_going)
                {
                    local_depth += 1;
                    if (nodeVal > current.Value)
                    {
                        if(current.RightNode == null)
                        {
                            BSTNode newNode = new BSTNode(nodeVal);
                            current.RightNode = newNode;
                            success = true;
                            keep_going = false;
                        }
                        else
                        {
                            current = current.RightNode;
                        }
                    }
                    else if(nodeVal < current.Value)
                    {
                        if (current.LeftNode == null)
                        {
                            BSTNode newNode = new BSTNode(nodeVal);
                            current.LeftNode = newNode;
                            success = true;
                            keep_going = false;
                        }
                        else
                        {
                            current = current.LeftNode;
                        }
                    }
                }
                if (local_depth > Depth)
                {
                    Depth = local_depth;
                }
            }

            if (success)
            {
                NumItems += 1;
            }
            return success;
        }

        public bool search(int val)
        {
            bool success = search(this.HeadNode, val);
            return success;
        }

        private bool search(BSTNode root, int val)
        {
            bool success = false;
            if (root == null)
            {
                success = false;
                return success;
            }
            else if (root.Value == val)
            {
                success = true;
                return success;
            }
            else if (val > root.Value)
            {
                success = search(root.RightNode, val);
            }
            else if (val < root.Value)
            {
                success = search(root.LeftNode, val);
            }
            return success;
        }

        public void print()
        {
            print(this.HeadNode);
        }

        private void print(BSTNode head)
        {
            if(head != null)
            {
                print(head.LeftNode);
                string value = Convert.ToString(head.Value);
                Console.Write(value + " ");
                print(head.RightNode);
            }
        }

        public int theoreticalMin()
        {
            return (int)(Math.Log(NumItems,2))+1;
        }

        public BSTNode HeadNode { get; private set; }
        public int NumItems { get; private set; }
        public int Depth { get; private set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            
            BSTtree tree = new BSTtree();
            
            Console.WriteLine("Please enter a string of integers from 0-100 seperated by spaces:\n");
            string input_string = Console.ReadLine();
            string[] input_values = input_string.Split(' ');
            foreach(string val in input_values){
                if (!tree.search(Int32.Parse(val)))
                {
                    tree.insert(Int32.Parse(val));
                }
            }
            Console.Write("Tree Contents: ");
            tree.print();
            Console.WriteLine("\nTree Statistics: ");
            Console.WriteLine(" Number of Nodes: "+ tree.NumItems);
            Console.WriteLine(" Number of Levels: "+ tree.Depth);
            Console.WriteLine(" Minimum number of levels that a tree with " + tree.NumItems + "nodes coulde have = "+ tree.theoreticalMin());
            Console.WriteLine("Done");
            
            
            Console.ReadKey();
        }
    }
}
