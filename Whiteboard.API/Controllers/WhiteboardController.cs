using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whiteboard.API.Controllers
{
    [Route("api/[controller]")]
    public class WhiteboardController : Controller
    {
        WhiteboardColumnsAndRows columnsAndRowsHelper = new WhiteboardColumnsAndRows();
        // GET: api/values
        [HttpGet]
        [Route("/api/clear")]
        public string Clear()
        {
            var s = new ServiceBusSender();
            s.Send("-999");
            return "Cleared the whiteboard";
            
        }

        
        [HttpGet("column/{column}/{colorName?}")]
        public string Column(int column, string colorName = "red")
        {
            Color color = ParseColor(colorName);

            var s = new ServiceBusSender();
            var msg = columnsAndRowsHelper.Column(column, color);
            s.Send(msg);

            return "OK";
        }

        [HttpGet("row/{row}/{colorName?}")]
        public string Row(int row, string colorName = "red")
        {
            Color color = ParseColor(colorName);

            var s = new ServiceBusSender();
            var msg = columnsAndRowsHelper.Row(row, color);
            s.Send(msg);

            return "OK";
        }

        [HttpGet("column/{column}/row/{row}/{colorName?}")]
        public string ColumnAndRow(int column,int row, string colorName = "red")
        {
            Color color = ParseColor(colorName);

            var s = new ServiceBusSender();
            var msg = columnsAndRowsHelper.ColumnAndRow(column,row, color);
            s.Send(msg);

            return "OK";
        }

        private static Color ParseColor(string colorName)
        {
            var color = Color.FromName(colorName);
            if (color == null)
                color = Color.Red;
            return color;
        }
    }
}
