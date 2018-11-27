using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace SpreadsheetEngine
{
    public class XMLparser
    {
        internal class CellData{

            public string name { get;set;}
            public int row { get; set; }
            public int column { get; set; }
            public string text { get; set; }

            public CellData()
            {
                string name = "";
                int row = -1;
                int column = -1;
                string text = "";
            }

        }

        public void Save(Stream stream, Spreadsheet spreadsheet, string name)
        {
            using(XmlWriter writer = XmlWriter.Create(stream))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Cells");
                for(int i = 0; i < spreadsheet.RowCount; i++)
                {
                    for (int j = 0; j < spreadsheet.ColumnCount; j++)
                    {
                        Cell workingCell = spreadsheet.getCell(i, j);
                        if (workingCell.Text != "" || (workingCell.Value != null && workingCell.Value != ""))
                        {
                            string cellName = convertForName(i, j);
                            int row = i;
                            int column = j;
                            string text = workingCell.Text;
                            writer.WriteStartElement("Cell");
                            writer.WriteElementString("name", cellName);
                            writer.WriteElementString("text", text);
                            writer.WriteElementString("row", row.ToString());
                            writer.WriteElementString("column", column.ToString());
                            writer.WriteEndElement();
                        }
                    }
                  
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void Load(Stream stream, ref Spreadsheet spreadsheet)
        {
            CellData newCellData = null;
            string valueOperator = "";
            using (XmlReader reader = XmlReader.Create(stream))
            {
                spreadsheet.clear();
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Cell":
                                newCellData = new CellData();
                                break;

                            case "text":
                                if (newCellData != null)
                                {
                                    valueOperator = "text";
                                }
                                break;

                            case "row":
                                if (newCellData != null)
                                {
                                    valueOperator = "row";
                                }
                                break;

                            case "column":
                                if (newCellData != null)
                                {
                                    valueOperator = "column";
                                }
                                break;

                            case "name":
                                if (newCellData != null)
                                {
                                    valueOperator = "name";
                                }
                                break;

                        }

                    }
                    else if(reader.NodeType == XmlNodeType.Text)
                    {
                        switch (valueOperator)
                        {
                            case "text":
                                newCellData.text = reader.Value;
                                break;

                            case "name":
                                newCellData.name = reader.Value;
                                break;

                            case "column":
                                newCellData.column = Int32.Parse(reader.Value);
                                break;

                            case "row":
                                newCellData.row = Int32.Parse(reader.Value);
                                break;
                        }
                    }
                    else if (reader.Name == "Cell" && reader.NodeType == XmlNodeType.EndElement)
                    {
                        spreadsheet.getCell(newCellData.row, newCellData.column).Text = newCellData.text;
                        newCellData = null;
                    }
                 

                    
                }
            }
        }

        private string convertForName(int row, int column)
        {
            string name = "";
            name += ((char)(column + 65)).ToString();
            name += row.ToString();
            return name;
        }
    }
}
