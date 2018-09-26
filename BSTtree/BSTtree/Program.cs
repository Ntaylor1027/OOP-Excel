using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    public class BSTNode<T> where T: IComparable<T>
    {

        public BSTNode(T val)
        {
            this.Value = val;
        }

        public int CompareTo(BSTNode<T> obj)
        {
            if (object.ReferenceEquals(obj, null))
            {
                return -1;
            }
            return Value.CompareTo(obj.Value);
        }

        public static bool operator ==(BSTNode<T> n1, BSTNode<T> n2)
        {
            return n1.Value.Equals(n2.Value);
        }

        public static bool operator !=(BSTNode<T> n1, BSTNode<T> n2)
        {
            return !(n1.Value.Equals(n2.Value));
        }

        public static bool operator <=(BSTNode<T> n1, BSTNode<T> n2)
        {
            return n1.Value.CompareTo(n2.Value) <= 0;
        }

        public static bool operator >=(BSTNode<T> n1, BSTNode<T> n2)
        {
            return n1.Value.CompareTo(n2.Value) >= 0;
        }

        public static bool operator >(BSTNode<T> n1, BSTNode<T> n2)
        {
            return n1.Value.CompareTo(n2.Value) == 1;
        }
        public static bool operator <(BSTNode<T> n1, BSTNode<T> n2)
        {
            return n1.Value.CompareTo(n2.Value) == -1;
        }



        public T Value    { get; private set; }
        public BSTNode<T> LeftNode { get; set; }
        public BSTNode<T> RightNode { get; set; }
    }

    public abstract class BinTree<T> where T: IComparable<T>
    {
        public abstract void Insert(T val); // Insert new item of type T
        public abstract bool Contains(T val); // Returns true if item is in tree
        public abstract void InOrder(); // Print elements in tree inorder traversal
        public abstract void PreOrder();
        public abstract void PostOrder();
    }
    
    public class BSTtree<T> : BinTree<T> where T : IComparable<T>
    {
        public BSTNode<T> HeadNode { get; set; }
        public int NumItems { get; set; }
        public int Depth { get; set; }

        public BSTtree()
        {
            this.Depth = 0;
            this.NumItems = 0;
            this.HeadNode = null;
        }

        public override bool Contains(T val)
        {
            bool found = false;
            BSTNode<T> temp = HeadNode;
            BSTNode<T> checkNode = new BSTNode<T>(val);

            while(temp is null == false)
            {
                if(temp == checkNode)
                {
                    found = true;
                    break;
                }
                else if(checkNode < temp)
                {
                    temp = temp.LeftNode;
                }
                else // bigger than
                {
                    temp = temp.RightNode;
                }

            }
            return found;
        }

        public override void Insert(T nodeVal)
        {
            bool success = false;
            BSTNode<T> newNode = new BSTNode<T>(nodeVal);
            if (HeadNode is null)
            {
                HeadNode = newNode;
                Depth = 1;
                success = true;
            }
            else
            {
                BSTNode<T> current = HeadNode;
                bool keep_going = true;
                int local_depth = 1;
                while (keep_going)
                {
                    local_depth += 1;
                    if (newNode > current)
                    {
                        if (current.RightNode is null)
                        {
                            current.RightNode = newNode;
                            success = true;
                            keep_going = false;
                        }
                        else
                        {
                            current = current.RightNode;
                        }
                    }
                    else if (newNode < current)
                    {
                        if (current.LeftNode is null)
                        {
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
            
        }

        public override void InOrder()
        {
            printInOrder(HeadNode);
        }

        private void printInOrder(BSTNode<T> head)
        {
            if (head is null == false)
            {
                printInOrder(head.LeftNode);
                string value = Convert.ToString(head.Value);
                Console.Write(value + " ");
                printInOrder(head.RightNode);
            }
        }

        public override void PreOrder()
        {
            printPreOrder(HeadNode);
        }

        private void printPreOrder(BSTNode<T> head)
        {
            if (head is null == false)
            {
                string value = Convert.ToString(head.Value);
                Console.Write(value + " ");
                printPreOrder(head.LeftNode);
                printPreOrder(head.RightNode);
            }
        }

        public override void PostOrder()
        {
            printPostOrder(HeadNode);
        }

        private void printPostOrder(BSTNode<T> head)
        {
            if (head is null == false)
            {
                printPostOrder(head.LeftNode);
                printPostOrder(head.RightNode);
                string value = Convert.ToString(head.Value);
                Console.Write(value + " "); 
            }
        }

        public int theoreticalMin()
        {
            return (int)(Math.Log(NumItems, 2)) + 1;
        }

    }

    class Program
    {

        static void Main(string[] args)
        {
                  
                BSTtree<int> tree = new BSTtree<int>();

                Console.WriteLine("Please enter a string of integers from 0-100 seperated by spaces:\n");
                string input_string = Console.ReadLine();
                string[] input_values = input_string.Split(' ');
                foreach(string val in input_values){
                    if (!tree.Contains(Int32.Parse(val)))
                    {
                        tree.Insert(Int32.Parse(val));
                    }
                }
                Console.Write("Tree Contents: ");
                tree.InOrder();
                Console.WriteLine("\nTree Statistics: ");
                Console.WriteLine(" Number of Nodes: "+ tree.NumItems);
                Console.WriteLine(" Number of Levels: "+ tree.Depth);
                Console.WriteLine(" Minimum number of levels that a tree with " + tree.NumItems + "nodes coulde have = "+ tree.theoreticalMin());
                Console.WriteLine("Done");


                Console.ReadKey();
            
            
            
        }
    }
}
