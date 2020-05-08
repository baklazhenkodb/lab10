using System;
using System.Windows.Forms;
using SomeProject.Library.Client;
using SomeProject.Library;

namespace SomeProject.TcpClient
{
    public partial class ClientMainWindow : Form
    {
        public ClientMainWindow()
        {
            InitializeComponent();
        }
      

        /// <summary>
        ///  Процедура обработки отправки файла на сервер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Client client = new Client();

                var fileName = fileDialog.FileName;
                var fileSplit = fileName.Split('.');
                var extention = fileSplit[fileSplit.Length - 1];

                var res = client.SendFileToServer(fileDialog.FileName, extention);

                if (res.Result == Result.OK)
                    LogBox.Text += "File was sent succefully!\r\n";

                else
                    LogBox.Text += "Cannot send the file to the server.\r\n";

                LogBox.Text += "Server: " + res.Message + "\r\n";

                timer.Interval = 2000;
                timer.Start();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки отправки сообщения на сервер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MsgButton_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            OperationResult res = client.SendMessageToServer(textBox.Text);

            if (res.Result == Result.OK)
            {
                textBox.Text = "";

                LogBox.Text += "Text send to server\r\n";

                LogBox.Text += "Server: " + res.Message + "\r\n";
            }

            else
            {
                LogBox.Text += "Cannot send text to server. Some kind of error\r\n";
            }

            timer.Interval = 2000;
            timer.Start();
        }

        /// <summary>
        /// Таймер 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            labelRes.Text = "";
            timer.Stop();
        }
    

        private void ClientMainWindow_Load(object sender, EventArgs e)
        {

        }

        
    }
}
