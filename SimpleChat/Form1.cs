using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;

namespace SimpleChat
{
    public partial class Form1 : Form
    {
        private IHubProxy chat;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var hubConnection = new HubConnection("http://localhost:1806/");
            chat = hubConnection.CreateHubProxy("chatHub");
            chat.On<string>("newMessage",
                msg => lbMessages.Invoke(
                    new Action(() => lbMessages.Items.Add(msg))));

            hubConnection.Start().Wait();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chat.Invoke<string>("sendMessage", tbMessage.Text);
        }
    }
}
