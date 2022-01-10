using Common;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public NetworkFile SendData<T>(NetworkFile<T> file)
        {
            NetworkFile<string[]> response = null;

            try
            {
                int port = 13000;
                TcpClient client = new TcpClient("127.0.0.1", port);
                using IMemoryOwner<byte> memory = MemoryPool<byte>.Shared.Rent(1024 * 4);

                byte[] data = Json.SerializeToBytes(file);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                int length = stream.Read(memory.Memory.Span);
                ReadOnlyMemory<byte> request = memory.Memory.Slice(0, length);
                response = Json.DeserializeFromMemory<NetworkFile<string[]>>(request);

                if (response.Info.Length > 0)
                {
                    ErrorBox.Text = "";
                    foreach (var item in response.Info)
                    {
                        ErrorBox.Text += " - " + item;
                    }
                }
                else
                {
                    ErrorBox.Text = "Invalid";
                }

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                ErrorBox.Text = ex.Message;
            }
            return response;
        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            NetworkFile<string[]> message = new NetworkFile<string[]>
            {
                Service = Services.Login,
                Info = new string[] { User.Text, Password.Text }
            };

            SendData(message);
        }

        public void Create_Click(object sender, RoutedEventArgs e)
        {
            NetworkFile<string[]> message = new NetworkFile<string[]>
            {
                Service = Services.Create,
                Info = new string[] { CreateUsername.Text, CreatePassword.Text, CreateNickname.Text }
            };

            SendData(message);
        }

        private void Start_Game_Click(object sender, RoutedEventArgs e)
        {
            Chess.Program.Main();
        }

    }
}
