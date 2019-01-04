using System;
using System.Windows.Forms;
using System.Drawing;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Data_Server.Database.MySQL;
using Data_Server.Communication;
using Data_Server.Util;
using Data_Server.Server;

namespace Data_Server {
    public partial class FrmMain : Form  {

        #region Peek Message
        [SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        private struct Message {
            public IntPtr hWnd;
            public IntPtr msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Point p;
        }

        public void OnApplicationIdle(object sender, EventArgs e) {
            while (this.AppStillIdle) {

                if (Server != null) {
                    Server.ServerLoop();

                    Thread.Sleep(1); 

                    if (!Server.ServerRunning) {
                        Server.StopServer();
                        Environment.Exit(0);
                    }
                }
            }
        }

        private bool AppStillIdle {
            get {
                return !PeekMessage(out Message msg, IntPtr.Zero, 0, 0, 0);
            }
        }

        #endregion

        DataServer Server;
      
        const int MaxLogsLines = 250;
        const int PreserveLogsLines = 25;

        enum CloseSteps {
            None,
            Close,
            SaveAndClose
        }

        public FrmMain() {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e) {
            InitServer();
        }

        private void InitServer() {
            InitLogs();

            Configuration.ReadSqlConfig();

            CheckDatabaseConnection();

            Server = new DataServer();
            Server.UpdateUps += UpdateUps;
            Server.InitServer();
        }

        private void InitLogs() {
            Global.SystemLogs = new Log("System") {
                Index = 0
            };

            Global.SystemLogs.LogEvent += WriteLog;

            var result = Global.SystemLogs.OpenFile();

            if (result) {
                Global.SystemLogs.Enabled = true;
            }
            else {
                MessageBox.Show("An error ocurred when trying to open the file log.");
            }

            Global.PlayerLogs = new Log("Player") {
                Index = 1
            };

            Global.PlayerLogs.LogEvent += WriteLog;

            result = Global.PlayerLogs.OpenFile();

            if (result) {
                Global.PlayerLogs.Enabled = true;
            }
            else {
                MessageBox.Show("An error ocurred when trying to open the file log.");
            }
        }

        private void CheckDatabaseConnection() {
            var connection = new DBConnection();
            var dbError = connection.Open();

            if (dbError.Number > 0) {
                Global.WriteLog(LogType.System, $"Database is not connected", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
            }
            else {
                Global.WriteLog(LogType.System, "Database is connected successfully", LogColor.Green);
            }

            connection.Close();
        }

        private void UpdateUps(int ups) {
            Text = $"Data Relay @ {ups} Ups";
        }

        private void WriteLog(object sender, LogEventArgs e) {
            var text = TextSystem;

            switch ((LogType)e.Index) {
                case LogType.System:
                    text = TextSystem;
                    break;
                case LogType.Player:
                    text = TextPlayer;
                    break;
            }

            text.SelectionStart = text.TextLength;
            text.SelectionLength = 0;
            text.SelectionColor = GetColor(e.Color);
            text.AppendText($"{DateTime.Now}: {e.Text}{Environment.NewLine}");
            text.ScrollToCaret();
        }

        private void MenuExit_Click(object sender, EventArgs e) {
            CheckForCloseApplication();
        }

        private void MenuSavePlayer_Click(object sender, EventArgs e) {
            SaveCharactersAsync(false);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            CheckForCloseApplication();
        }

        private Color GetColor(LogColor logColor) {
            Color color = Color.Empty;

            switch (logColor) {
                case LogColor.Black:
                    color = Color.Black;
                    break;
                case LogColor.Blue:
                    color = Color.Black;
                    break;
                case LogColor.Coral:
                    color = Color.Coral;
                    break;
                case LogColor.Green:
                    color = Color.Green;
                    break;
                case LogColor.Red:
                    color = Color.Red;
                    break;
            }

            return color;
        }

        private async void SaveCharactersAsync(bool applicationExit) {
            var result = await Task.Run(() =>
                Server.SaveCharacters()
            );

            ShowSaveResult(result);

            if (applicationExit) {
                if (result >= 0) {
                    Server.ServerRunning = false;
                }
                else {
                    var msg = $"The character list is not saved.\nDo you REALLY want to exit?";
                    var dialogResult = MessageBox.Show(msg, "Error", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes) {
                        Server.ServerRunning = false;
                    }
                }
            }
        }

        private void ShowSaveResult(int result) {
            var msg = "The character list is not saved! Database is not connected!";
            var caption = "Error";

            if (result > 0) {
                msg = $"The character list has been saved, total of {result} characters!";
                caption = "Success";
            }
            else if (result == 0) {
                msg = "The character list is empty!";
                caption = "Normal";
            }

            MessageBox.Show(msg, caption);
        }

        private void CheckForCloseApplication() {
            var steps = GetCloseApplicationStep();

            switch (steps) {
                case CloseSteps.Close:
                    Server.ServerRunning = false;
                    break;
                case CloseSteps.SaveAndClose:
                    // Desativa o servidor para evitar que outros dados atrapalhem a operação do banco.
                    Server.StopServer();
                    SaveCharactersAsync(true);
                    break;
            }
        }

        private CloseSteps GetCloseApplicationStep() {
            var closeSteps = CloseSteps.None;
            var msg = "Do you want to exit?";
            var caption = "Question";

            var result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes) {
                closeSteps = CloseSteps.Close;

                msg = "Save character list?";
                result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes) {
                    closeSteps = CloseSteps.SaveAndClose;
                }
            }

            return closeSteps;
        }

        private void TimerClearText_Tick(object sender, EventArgs e) {
            if (TextPlayer.Lines.Length >= MaxLogsLines) {

                var currentLines = TextPlayer.Lines;
                var newLines = new string[PreserveLogsLines];

                Array.Copy(currentLines, currentLines.Length - PreserveLogsLines, newLines, 0, PreserveLogsLines);

                TextPlayer.Lines = newLines;

                currentLines = null;
            }
        }

        private void MenuClear_Click(object sender, EventArgs e) {
            var msg = "Do you want to clear player list?";
            var caption = "Question";

            var result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);
             
            if (result == DialogResult.Yes) {
                var count = Server.ClearPlayers();

                if (count <= 0) {
                    msg = "The character list is empty!";                   
                } else {
                    msg = $"Cleared {count} characters";
                }

                MessageBox.Show(msg, "Message");
            }
        }

        private async void MenuSerialize_Click(object sender, EventArgs e) {
            var result = await Task.Run(() =>
                Server.SerializeCharacters()
            );

            var msg = string.Empty;

            if (result <= 0) {
                msg = "The character list is empty!";
            }
            else {
                msg = $"Serialized {result} characters.";
            }

            MessageBox.Show(msg, "Message");
        }

        private async void MenuDeserialize_Click(object sender, EventArgs e) {
            var msg = $"Do you want to deserialize player list?\nOperation will REPLACE current list with serialized files.";
            var caption = "Question";

            var result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes) {

                var count = await Task.Run(() =>
                    Server.DeserializeCharacters()
                );

                MessageBox.Show($"Loaded {count} serialized characters.", "Success");
            }
        }
    }
}