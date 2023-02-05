using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace DSS.MVVM.View
{
    /// <summary>
    /// Interaction logic for PilotManagerView.xaml
    /// </summary>
    public partial class PilotManagerView : UserControl
    {
        public PilotManagerView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // open file explorer window
            // select file
            // load xml
            // display list of pilots

            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = fileDialog.ShowDialog();
            string message = "";

            if(response == true)
            {
                string filepath = fileDialog.FileName;
                ArrayList list = new ArrayList();

                XElement root = XElement.Load(filepath);
                IEnumerable<XElement> pilots = from el in root.Descendants("pilot") select el;
                foreach (XElement pilot in pilots)
                {
                    XElement classes = pilot.Element("classes");
                    string name = pilot.Element("name").Value;
                    string _class = classes.Element("class").Value;
                    message += (name + " " + _class + "\n");
                }

                MessageBox.Show(message);
            }
        }
    }
}
