using System;
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
using System.Threading;
using System.ComponentModel;

namespace DiscordBotUI
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private DiscordBotModel _discordBotModel;
        public MainText mainText;
        public MapTable gridData;

        

        public MainWindow()
        {
            InitializeComponent();
            mainText = new MainText();
            this.DataContext = mainText;
            gridData = new MapTable();
            MessageGrid.ItemsSource = gridData.maptable;
            Task t = new Task(CheckConnect);
            t.Start();

        }

        private void CheckConnect()
        {
            while (true)
            {
                if (_discordBotModel != null && _discordBotModel.IsConnected())
                {
                    mainText.CONNECT = "Connected";
                    break;
                }
                else
                {
                    //mainText.CONNECT = "Connect";
                }
                Thread.Sleep(500);
            }
        }


        





        public void Buttom_Connect(object sender, RoutedEventArgs e)
        {
            if (_discordBotModel != null) 
                return;
            _discordBotModel = new DiscordBotModel(mainText.TOKEN, gridData);
            _discordBotModel.Connect();
            mainText.CONNECT = "Connecting...";

        }
        

        private void MessageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var data in MessageGrid.SelectedItems)
            {
                MessageMap myData = data as MessageMap;
                gridData.maptable.Remove(myData);
            }
            //gridData.RemoveChecked();
            MessageGrid.Items.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            gridData.maptable.Add(new MessageMap("", ""));
            MessageGrid.Items.Refresh();
        }

        
    }

    public class MainText : INotifyPropertyChanged
    {
        public MainText()
        {
            
        }
        private string _token = "";
        private string _connect = "Connect";
        public string TOKEN
        {
            set
            {
                if (value == _token)
                    return;
                _token = value;
                this.OnPropertyChanged("TOKEN");
            }
            get
            {
                return _token;
            }
        }
        public string CONNECT
        {
            set
            {
                if (value == _connect)
                    return;
                _connect = value;
                this.OnPropertyChanged("CONNECT");
            }
            get
            {
                return _connect;
            }
        }

        // Events
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
