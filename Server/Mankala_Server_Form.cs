using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Linq;
using System.Drawing;
namespace Server
{
    public partial class Mankala_Server_Form : Form
    {
        string server_host = "127.0.0.1";
        string server_port = "50001";
        static int playerNumber = 0;
        IPAddress server_ip;
        int server_Iport;
        Socket server_start_socket;
        Thread receiver_thread;
        private Thread listner_thread;
        private List<Thread> list_of_thread = new List<Thread>();
        string message_recived = "";
        string player1name = "player1";
        string player2name = "player2";
        int[] boardgame=new int[14];
        int player1Score = 0;
        int player2Score = 0;
        string curent_player = "player1";
        int num_player = 0;

        public Mankala_Server_Form()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = System.Environment.MachineName + " | " + DateTime.Now.ToLongDateString() + " - " + DateTime.Now.ToShortTimeString();
        }

        private void init_board()
        {
            for (int i = 0; i < boardgame.Length; i++)
                boardgame[i] = 7;
        }

        private string get_board_status()
        {

            string res = "" + curent_player + "." + boardgame[0];
            for (int i = 1; i < boardgame.Length; i++)
            {
                res = res + "," + boardgame[i];
            }
         
            return res;
        
        }

        // delegate that allows method DisplayMessage to be called
        // in the thread that creates and maintains the GUI       
        private delegate void DisplayDelegate(string message);

        // method DisplayMessage sets displayTextBox's Text property
        // in a thread-safe manner
        internal void DisplayMessage(string message)
        {
            // if modifying displayTextBox is not thread safe
            if (displayTextBox.InvokeRequired)
            {
                // use inherited method Invoke to execute DisplayMessage
                // via a delegate                                       
                Invoke(new DisplayDelegate(DisplayMessage),
                   new object[] { message });
            } // end if
            else // OK to modify displayTextBox in current thread
                displayTextBox.Text += message;
        } // end method DisplayMessage

        private void player_play(int index)
        {
            int temp = boardgame[index];
            boardgame[index] = 0;
            for(int i=index;;)
            {
                i++;
                if (temp == 1)
                {
                    int i1 = i % 14;
                    boardgame[i1] += 1;
                    temp--;
                    if (boardgame[i1] == 2 || boardgame[i1] == 4)
                    {
                        if (curent_player == "player1")
                        {
                            player1Score += boardgame[i1];
                            
                        }
                        if (curent_player == "player2")
                        {
                            player2Score += boardgame[i1];
                           
                        }
                        boardgame[i1] = 0;
                    }
                    break;
                }
                else
                {
                    int i1 = i % 14;
                    boardgame[i1] += 1;
                    temp--;
                }
                if (player1Score > 49)
                {
                    DisplayMessage("Player 1 wins !!!\r\n");
                }
                else if (player2Score > 49)
                {
                    DisplayMessage("Player 2 wins !!!\r\n");
                }
            }
        }
        private string get_score()
        {
            string res = "player1:" + player1Score + "." + "player2:" + player2Score;
            return res;
        }
        private bool Finishing()
        {
            bool end = true;
            for (int i = 0; i < boardgame.Length; i++)
            {
                if (boardgame[i] != 0)
                {
                    end = false;
                    break;
                }
            }
            return end;
        }     
        private void switchP()
        {
            stop_server_btn.Enabled = !stop_server_btn.Enabled;
            start_server_btn.Enabled = !start_server_btn.Enabled;
          

        }  
        
        private void start_server_btn_Click(object sender, EventArgs e)
        {
            try
            {
                init_board();
                switchP();               
                server_ip = IPAddress.Parse(server_host);
                server_Iport = int.Parse(server_port);

                IPEndPoint endPoint
                    = new IPEndPoint(server_ip, server_Iport);

                server_start_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server_start_socket.Bind(endPoint);

                // server listen 
                server_start_socket.Listen(500);
                listner_thread = new Thread(new ThreadStart(listenToClients));
                list_of_thread.Add(listner_thread);
                listner_thread.Start();
                DisplayMessage("Waiting for players...\r\n");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Messenger Server", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
          
        }
       
        private void listenToClients()
        {

            while (true)
            {
                try
                {

                    Socket acceptClient = server_start_socket.Accept();
                    receiver_thread = new Thread(receiveFromClient);
                    list_of_thread.Add(receiver_thread);
                    receiver_thread.Start(acceptClient);
                    if (playerNumber == 0)
                        playerNumber = 1;
                    else if (playerNumber == 1)
                        playerNumber = 2;
                    else
                        DisplayMessage("Only 2 players allowed...\r\n");

                    DisplayMessage("Player: "+ playerNumber.ToString() +" connected\r\n");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void receiveFromClient(object ob)
        {
            Socket receivesocket = (Socket)ob;
            try
            {
                while (receivesocket.Connected)
                {
                    NetworkStream network_stream = new NetworkStream(receivesocket);

                    BinaryReader reader = new BinaryReader(network_stream);

                    BinaryWriter writer = new BinaryWriter(network_stream);
                    // ********************************************//
                    message_recived = reader.ReadString();
                   
                    if (message_recived == "player")
                    {
                        if (num_player == 0)
                        {
                            num_player++;
                            string s = "" + player1name + ".";
                            s = s + get_board_status();
                            writer.Write(s);
                        
                        }
                        else  if (num_player == 1)
                        {
                            num_player++;
                            string s = "" + player2name + ".";
                            s = s + get_board_status();
                            writer.Write(s);
                        
                        }
                           

                    }
                    else if (message_recived == "update")
                    {
                        string res = get_score();
                        string s = "" + res + ".";
                        s = s + get_board_status();
                        writer.Write(s);
                    }
                    else
                    {
                             string[] playy = message_recived.Split(',');

                             if (playy[0] == curent_player)
                             {

                                 int ind = Int16.Parse(playy[1]);
                                 player_play(ind);
                                 string res = get_score();
                                 string s = "" + res + ".";
                                 s = s + get_board_status();
                                 writer.Write(s);
                                 if (curent_player=="player1")
                                 { curent_player = "player2"; }
                                 else
                                 { curent_player = "player1"; }

                             }
                             else
                             {
                                 string s = "pleasewait";                              
                                 writer.Write(s);
 
                             }

                    }
                    network_stream.Flush();
                    writer.Close();
                    reader.Close();
                    network_stream.Close();
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }
        private void stop_server_btn_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void Mankala_Server_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About frm = new About();
            frm.ShowDialog();
        }
    }
}
