using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Web.Helpers;
using System.Windows;
using System.Windows.Controls;
using SuperSimpleTcp;

namespace DSS
{

    public partial class Pilot
    {
        public String name;
        public float[,] scores = new float[2, 12];

        public String Describe()
        {
            String str = name + "\n";
            for(int x=0; x<2; x++)
            {
                for(int y=0; y<12; y++)
                {
                    str += scores[x, y] + ", ";
                }
                str += "\n";
            }
            return str;
        }
    }

    public partial class ScoreUpdate
    {
        public String operation;
        public String name;
        public int sequence;
        public int figure;
        public float score;
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MainWindow mainWindow = null;
        IDictionary<string, Pilot> pilots = new Dictionary<string, Pilot>();

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            ServerMenuStart(null,null);   
        }

        static private void ServerMenuEdit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit server details");
        }

        static private void AcceptCallback(IAsyncResult result)
        {
            MessageBox.Show("AcceptCallback");
        }

        private void ServerMenuStart(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Starting Server");

            SimpleTcpServer server = new SimpleTcpServer("0.0.0.0:8888");
            // set events
            server.Events.ClientConnected += ClientConnected;
            server.Events.ClientDisconnected += ClientDisconnected;
            server.Events.DataReceived += DataReceived;

            // let's go!
            server.Start();

            // once a client has connected...
            server.Send("[ClientIp:Port]", "Hello, world!");
        }

        public void setLabel(string str)
        {
            
        }

        public void receiveData(string str)
        {
            ScoreUpdate json = Json.Decode<ScoreUpdate>(str);
            setLabel(json.operation + " " + json.name + " " + json.score);
            Pilot pilot = GetPilot(json);
            setLabel(pilot.Describe());
        }

        private Pilot GetPilot(ScoreUpdate json)
        {
            Pilot pilot = null;
            if (!pilots.ContainsKey(json.name))
            {
                pilot = new Pilot();
                pilots[json.name] = pilot;
            }
            else
            {
                pilot = pilots[json.name];
            }

            pilot.name = json.name;
            pilot.scores[json.sequence, json.figure] = json.score;

            return pilot;
        }
        static public void OnConnection(IAsyncResult result)
        {
            MessageBox.Show("OnConnection");
        }

        static void ClientConnected(object sender, ConnectionEventArgs e)
        {
            //MessageBox.Show("ClientConnected");
            // add a frame on screen for this client
            mainWindow.Dispatcher.Invoke(() =>
            {
                mainWindow.setLabel("new connection");
            });
        }

        static void ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            //Console.WriteLine($"[{e.IpPort}] client disconnected: {e.Reason}");
            MessageBox.Show("ClientDisconnected");
            // remove frame on screen for the client
        }

        static void DataReceived(object sender, DataReceivedEventArgs e) {
            //var pilot = JsonSerializer.Deserialize<PilotData>(Encoding.UTF8.GetString(e.Data.Array,0,e.Data.Count));
            //MessageBox.Show(pilot.name);
            //Console.WriteLine($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
            //MessageBox.Show($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
            //label1.content = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);
            //mainWindow.label1.Content = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);

            mainWindow.Dispatcher.Invoke(() =>
            {
                //mainWindow.setLabel(Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count));
                mainWindow.receiveData(Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count));
            });
        }
    }
}
