using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WhiteBoardAPI
{
    internal class WhiteboardColumnsAndRows
    {


        private Dictionary<int, int> _columns = new Dictionary<int, int>();
        private Dictionary<int, int> _rows = new Dictionary<int, int>();
        internal WhiteboardColumnsAndRows()
        {
            _columns.Add(0, 1);
            _columns.Add(1, 1);
            _columns.Add(2, 1);
            _columns.Add(3, 2);
            _columns.Add(4, 2);
            _columns.Add(5, 2);
            _columns.Add(6, 3);
            _columns.Add(7, 3);
            _columns.Add(8, 3);

            _rows.Add(0, 1);
            _rows.Add(1, 2);
            _rows.Add(2, 3);
            _rows.Add(3, 3);
            _rows.Add(4, 2);
            _rows.Add(5, 1);
            _rows.Add(6, 1);
            _rows.Add(7, 2);
            _rows.Add(8, 3);

        }

        internal string Row(int rowValue)
        {
            var whiteboardCode = string.Empty;

            foreach(var row in _rows)
            {
                if (row.Value == rowValue)
                {
                    whiteboardCode += $"{row.Key}:255&";
                }

            }

            return whiteboardCode + "\n";
        }
        
        internal string ColumnAndRow(int columnValue, int rowValue, Color color = new Color())
        {
            if (color.IsEmpty)
                color = Color.Red;

            var whiteboardCode = string.Empty;
            
            foreach (var column in _columns)
            {
                if (column.Value == columnValue)
                {
                    foreach(var row in _rows)
                    {
                        if(row.Value == rowValue && row.Key == column.Key)
                        {
                            if (whiteboardCode.Length > 0)
                                whiteboardCode += "&";

                            whiteboardCode += $"{column.Key}:{color.R}:{color.G}:{color.B}";
                        }   
                    }
                }

            }

            return whiteboardCode + "\n";
        }

        internal string Column(int columnValue, Color color = new Color())
        {
            if (color.IsEmpty)
                color = Color.Red;
            
            var whiteboardCode = string.Empty;

            foreach (var column in _columns)
            {                
                if (column.Value == columnValue)
                {
                    if (whiteboardCode.Length > 0)
                        whiteboardCode += "&";
                    whiteboardCode += $"{column.Key}:{color.R}:{color.G}:{color.B}";
                }

            }

            return whiteboardCode + "\n";
        }
        
    }
}
