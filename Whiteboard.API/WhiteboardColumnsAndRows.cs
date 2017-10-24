using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Whiteboard.API
{
    internal class WhiteboardColumnsAndRows
    {


        private Dictionary<int, int> _columns = new Dictionary<int, int>();
        private Dictionary<int, int> _rows = new Dictionary<int, int>();
        internal WhiteboardColumnsAndRows()
        {
            var columns = 9;
            var rows = 31;

            var leds = columns * rows -1;
            
            for(int i = leds; i >= 0;i--)
            {
                if((columns -1) * rows <= i)
                {
                    _columns.Add(i, 10 - columns);
                }
                else
                {
                    if(i == 0)
                    {
                        _columns.Add(i, 10 -columns);
                    }
                    else
                    {
                        columns--;
                        _columns.Add(i, 10 - columns);
                    }
                    
                }
            }

            columns = 9;
            rows = 0;
            var change = 1;
            bool goingUp = true;
            for (int i = leds; i >= 0; i--)
            {
                _rows.Add(i, rows);
                
                if(rows == 0  && !goingUp)
                {
                    change = 1;
                    rows -= change; //Because of the curve in the board design the "switch" row occurs twise   
                    goingUp = true;
                }
                else if (rows == 30 && goingUp)
                {
                    change = -1;
                    rows -= change; //Because of the curve in the board design the "switch" row occurs twise
                    goingUp = false;
                }
                
                rows += change;
                
                

            }
            
        }

        internal string Row(int rowValue, Color color = new Color())
        {
            var whiteboardCode = string.Empty;

            if (color.IsEmpty)
                color = Color.Red;

            foreach (var row in _rows)
            {
                if (row.Value == rowValue)
                {
                    whiteboardCode += $"{row.Key}:{color.R}:{color.G}:{color.B}&";
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
