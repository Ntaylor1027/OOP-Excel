using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SpreadsheetEngine
{
    public class ExpTree
    {
        Dictionary<string, double> vars = new Dictionary<string, double>();
        public string Expression;
        public Node head;

        public class Node
        {
            public Node()
            {
                this.Value = "";
            }

            public Node(string newValue)
            {
                this.Value = newValue;
            }
            public string Value { get; set; }
        }

        public class OperatorNode : Node
        {
            public OperatorNode(string newValue) : base(newValue)
            {
                leftNode = null;
                rightNode = null;
            }
            public Node leftNode { get; set; }
            public Node rightNode { get; set; }
        }

        public class VariableNode : Node
        {
            public VariableNode(string newValue) : base(newValue)
            {
                doubleValue = 0;
            }
            public double doubleValue { get; set; }
        }

        public class ConstantNode : Node
        {
            public ConstantNode(string newValue) : base(newValue)
            {
                doubleValue = double.Parse(newValue);
            }
            public double doubleValue { get; set; }
        }

        public void SetVar(string varName, double varValue)
        {
            if (vars.ContainsKey(varName) == true)
            {
                vars[varName] = varValue;
            }
            else
            {
                vars.Add(varName, varValue);
            }
        }
        
        public double Eval()
        {
            return Eval(head);
        }

        private double Eval(Node node)
        {
            double sum = 0;
            double sumLeft, sumRight = 0;
            if (node is OperatorNode)
            {
                string op = node.Value;
                sumLeft = Eval((node as OperatorNode).leftNode);
                sumRight = Eval((node as OperatorNode).rightNode);
                switch (op)
                {
                    case "+":
                        sum = sumLeft + sumRight;
                        break;
                    case "-":
                        sum = sumLeft - sumRight;
                        break;
                    case "*":
                        sum = sumLeft * sumRight;
                        break;
                    case "/":
                        sum = sumLeft / sumRight;
                        break;
                }
                return sum;

            }
            else if (node is ConstantNode)
            {
                return (node as ConstantNode).doubleValue;
            }
            else if (node is VariableNode)
            {
                return vars[(node as VariableNode).Value];
            }
            else
            {
                return 0;
            }
        }

        
        
        public void printTree()
        {
            printTree(head);
        }

        private void printTree(Node node)
        {
            if(node is null == false)
            {
                if (node is OperatorNode)
                {
                    printTree((node as OperatorNode).leftNode);
                }
                Console.WriteLine(node.Value);
                if (node is OperatorNode)
                {
                    printTree((node as OperatorNode).rightNode);
                }
            }
        }
        private void makeTree()
        {
            String[] arguments = Regex.Split(this.Expression, @"([-+/*])");
            List<string> variables = new List<string>(); //List of variables
            List<string> operators = new List<string>(); //List of operators
            foreach (string operand in arguments) //put arguments in the correct list
            {
                if ("+-/*".Contains(operand))
                {
                    operators.Add(operand);
                }
                else
                {
                    variables.Add(operand);
                }
            }

            if (variables.Count == 1) //if only one value and no operators
            {
                if (double.TryParse(variables[0], out double val))
                {
                    this.head = new ConstantNode(variables[0]);
                }
                else
                {
                    if (vars.ContainsKey(variables[0]) is false) //Have we seen the variable before 
                    {
                        vars.Add(variables[0], 0); //If not add it to the dictionary of variables and there values
                    }
                    this.head = new VariableNode(variables[0]);
                }
            }
            else //atleast one operator
            {
                int currentIndexVariables = 0;
                Node firstNode;
                Node secondNode;
                if (double.TryParse(variables[currentIndexVariables], out double val)) //is number
                {
                    firstNode = new ConstantNode(variables[currentIndexVariables]);
                }
                else //is variable
                {
                    if (vars.ContainsKey(variables[currentIndexVariables]) is false)
                    {
                        vars.Add(variables[currentIndexVariables], 0);
                    }
                    firstNode = new VariableNode(variables[currentIndexVariables]);
                }
                currentIndexVariables += 1;
                if (double.TryParse(variables[currentIndexVariables], out double val2))// is number
                {
                    secondNode = new ConstantNode(variables[currentIndexVariables]);
                }
                else //is variable
                {
                    if (vars.ContainsKey(variables[currentIndexVariables]) is false)
                    {
                        vars.Add(variables[currentIndexVariables], 0);
                    }
                    secondNode = new VariableNode(variables[currentIndexVariables]);
                }
                string operand = operators[0];
                operators.RemoveAt(0);
                OperatorNode operatorNode = new OperatorNode(operand);
                operatorNode.leftNode = firstNode;
                operatorNode.rightNode = secondNode;

                this.head = operatorNode; //set head to operator node

                //Loop through remaining operators and add variables to trees
                while (operators.Count != 0)
                {
                    currentIndexVariables += 1;
                    operand = operators[0];
                    operators.RemoveAt(0);
                    OperatorNode newOperator = new OperatorNode(operand);
                    newOperator.leftNode = this.head;

                    //Create right node for new operator
                    Node nodeToAdd;
                    double valOfNode = 0;
                    if (double.TryParse(variables[currentIndexVariables], out valOfNode))
                    {
                        nodeToAdd = new ConstantNode(variables[currentIndexVariables]);
                    }
                    else //is variable
                    {
                        if (vars.ContainsKey(variables[0]) is false)
                        {
                            vars.Add(variables[0], 0);
                        }
                        nodeToAdd = new VariableNode(variables[currentIndexVariables]);
                    }

                    newOperator.leftNode = this.head;
                    newOperator.rightNode = nodeToAdd;
                    this.head = newOperator;

                }
            }
        }
        
        public ExpTree(string exprssion)
        {
            Expression = exprssion;
            makeTree();
            
           
        }
    }
}
