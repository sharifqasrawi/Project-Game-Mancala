using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Timers;


namespace Client
{
    public partial class ClientForm : Form
    {
        private TcpClient tcp_client;
        private NetworkStream socket_stream;
        private BinaryReader binary_reader;
        private BinaryWriter binary_writer;
        string player_name = "";
        string other_name = "";
        string current_player = "";
        Button[] board_bt=new Button[14];
        string[] boardgame = new string[14];
        private static System.Timers.Timer aTimer;
 
        public ClientForm()
        {
            InitializeComponent();
            toolStripStatusLabel3.Text = System.Environment.MachineName + " | " + DateTime.Now.ToLongDateString() + " - " + DateTime.Now.ToShortTimeString();
        }
        private void init_form() 
        {
            board_bt[0] = bt1;
            board_bt[1] = bt2;
            board_bt[2] = bt3;
            board_bt[3] = bt4;
            board_bt[4] = bt5;
            board_bt[5] = bt6;
            board_bt[6] = bt7;
            board_bt[7] = bt8;
            board_bt[8] = bt9;
            board_bt[9] = bt10;
            board_bt[10] = bt11;
            board_bt[11] = bt12;
            board_bt[12] = bt13;
            board_bt[13] = bt14;
            if (player_name == "player1")
            {
                for (int i = 0; i < 7; i++)
                {
                    board_bt[i].Enabled =false;
                }
            }
            if (player_name == "player2")
            {
                for (int i = 7; i < 14; i++)
                {
                    board_bt[i].Enabled = false;
                }
            }
 
        }
        private void restatus_board()
        {
            for (int i = 0; i < boardgame.Length; i++)
            {
                board_bt[i].Text = boardgame[i];    
            }
           
        }
        public void InitTimer()
        {

            aTimer = new System.Timers.Timer(10000); aTimer.Elapsed += new ElapsedEventHandler(update_board);
            aTimer.Interval = 1000; aTimer.Enabled = true;
        }
        private void connect_bt_Click(object sender, EventArgs e)
        {
            try
            {             
                connect_bt.Enabled = false;
                disconnect_bt.Enabled = true;
                tcp_client = new TcpClient();
                tcp_client.Connect("localhost", int.Parse("50001"));
                socket_stream = tcp_client.GetStream();
                binary_writer = new BinaryWriter(socket_stream);
                binary_reader = new BinaryReader(socket_stream);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                    String msg = "player";
                    binary_writer.Write(msg);
                    string first_message = binary_reader.ReadString();
                    string[] board_status = first_message.Split('.');
                    player_name = board_status[0];
                    if (player_name == "player1")
                    { other_name = "player2"; }
                    else
                    { other_name = "player1"; }
                    res1_lb.Text = player_name;
                    res2_lb.Text = other_name;
                    lblPlayer.Text = "You are: " + player_name;
                    current_player = board_status[1];
                    boardgame = board_status[2].Split(',');
                
                socket_stream.Flush();
                init_form();
                restatus_board();
                InitTimer();
                DisplayMessage(current_player+" Turn..\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void disconnect_bt_Click(object sender, EventArgs e)
        {
            try
            {
                aTimer.Enabled = false;
                connect_bt.Enabled = true;
                disconnect_bt.Enabled = false;
                binary_writer.Close();
                binary_reader.Close();
                socket_stream.Close();
                tcp_client.Close();
                Application.ExitThread();
                
            }
            catch (Exception ex)
            {
             //   MessageBox.Show(ex.Message);
            }
           

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

        private void play(int ind)
        {
            if (boardgame[ind] != "0")
            {
                try
                {
                    String play_message = player_name + "," + ind;
                    binary_writer.Write(play_message);
                    string played_message = binary_reader.ReadString();
                    string[] res_board = played_message.Split('.');

                    if (res_board[0] != "pleasewait")
                    {
                        if (player_name == "player1")
                        {
                            res1_lb.Text = res_board[0];
                            res2_lb.Text = res_board[1];
                            DisplayMessage("You Played, Player 2's Turn..\r\n");
                        }
                        if (player_name == "player2")
                        {
                            res1_lb.Text = res_board[1];
                            res2_lb.Text = res_board[0];
                            DisplayMessage("You Played, Player 1's Turn..\r\n");
                        }
                        current_player = res_board[2];
                        boardgame = res_board[3].Split(',');
                        
                        restatus_board();
                    }
                    else
                    {
                        MessageBox.Show("Please wait, it's not your turn", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("No Rocks", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
 
            }
            
         
        }

        private  void update_board(object sender, EventArgs e)
        {
            try
            {
                    String update_message = "update";
                    binary_writer.Write(update_message);
                    string updated_message = binary_reader.ReadString();
                    string[] res_board = updated_message.Split('.');
                    if (player_name == "player1")
                    {
                        res1_lb.Text = res_board[0];
                        res2_lb.Text = res_board[1];
                    }
                    if (player_name == "player2")
                    {
                        res1_lb.Text = res_board[1];
                        res2_lb.Text = res_board[0];
                    }
                    current_player = res_board[2];
                    boardgame = res_board[3].Split(',');
                    restatus_board();

            }
            catch (Exception ex)
            {
                aTimer.Enabled = false;
                MessageBox.Show(ex.Message);
            }
 
        }     
        private void bt1_Click(object sender, EventArgs e)
        {
            play(0);
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            play(1);
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            play(2);
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            play(3);
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            play(4);
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            play(5);
        }

        private void bt7_Click(object sender, EventArgs e)
        {
            play(6);
        }

        private void bt8_Click(object sender, EventArgs e)
        {
            play(7);
        }

        private void bt9_Click(object sender, EventArgs e)
        {
            play(8);
        }

        private void bt10_Click(object sender, EventArgs e)
        {
            play(9);
        }

        private void bt11_Click(object sender, EventArgs e)
        {
            play(10);
        }

        private void bt12_Click(object sender, EventArgs e)
        {
            play(11);
        }

        private void bt13_Click(object sender, EventArgs e)
        {
            play(12);
        }

        private void bt14_Click(object sender, EventArgs e)
        {
            play(13);
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //System.Environment.Exit(System.Environment.ExitCode);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About frm = new About();
            frm.ShowDialog();
        }


    }
}
