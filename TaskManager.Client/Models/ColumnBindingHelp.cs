using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Client.Models
{
    internal class ColumnBindingHelp
    {
        public string Value { get; set; }
        public ColumnBindingHelp(string v)
        {
            Value = v;
        }
    }
}
