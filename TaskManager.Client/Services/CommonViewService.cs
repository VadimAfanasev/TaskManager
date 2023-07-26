using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManager.Client.Services
{
    public class CommonViewService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
