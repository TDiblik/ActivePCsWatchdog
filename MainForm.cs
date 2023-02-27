using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace ActivePCsWatchdog
{
    public partial class MainForm : Form
    {
        private static readonly System.Timers.Timer ping_timer = new System.Timers.Timer();
        private static readonly System.Timers.Timer scheduler_timer = new System.Timers.Timer();

        public MainForm()
        {
            InitializeComponent();

            FromTimePicker.Format = DateTimePickerFormat.Custom;
            FromTimePicker.CustomFormat = "HH:mm";
            FromTimePicker.ShowUpDown = true;

            ToTimePicker.Format = DateTimePickerFormat.Custom;
            ToTimePicker.CustomFormat = "HH:mm";
            ToTimePicker.ShowUpDown = true;

            if (!Directory.Exists(FileUtils.settings_folder_path)) {
                Directory.CreateDirectory(FileUtils.settings_folder_path);
            }
            if (!Directory.Exists(FileUtils.reports_folder_path)) {
                Directory.CreateDirectory(FileUtils.reports_folder_path);
            }
            if (!File.Exists(FileUtils.SettingsFilePath)) {
                FileStream new_file = File.Create(FileUtils.SettingsFilePath);
                new_file.Close();
            }
            if (!File.Exists(FileUtils.PCsFilePath)) {
                FileStream new_file = File.Create(FileUtils.PCsFilePath);
                new_file.Close();
            }
            if (!File.Exists(FileUtils.ActivePCsFilePath)) {
                FileStream new_file = File.Create(FileUtils.ActivePCsFilePath);
                new_file.Close();
            }

            SettingsObj initial_config = FileUtils.GetConfig();
            if (string.IsNullOrWhiteSpace(initial_config.SMTP_IP)) {
                FileUtils.SaveEnvValue(nameof(SettingsObj.SMTP_IP), "192.168.0.0");
            }
            if (string.IsNullOrWhiteSpace(initial_config.SMTP_PORT)) {
                FileUtils.SaveEnvValue(nameof(SettingsObj.SMTP_PORT), "25");
            }
            if (string.IsNullOrWhiteSpace(initial_config.FROM_EMAIL_ADDRESS)) {
                FileUtils.SaveEnvValue(nameof(SettingsObj.FROM_EMAIL_ADDRESS), "example@exammple.com");
            }
            if (string.IsNullOrWhiteSpace(initial_config.REPORT_RECEIVERS_EMAIL_ADDRESSES)) {
                FileUtils.SaveEnvValue(nameof(SettingsObj.REPORT_RECEIVERS_EMAIL_ADDRESSES), "example@exammple.com ; example_2@exammple.com");
            }
            if (string.IsNullOrWhiteSpace(initial_config.PING_FROM_TIME)) {
                FileUtils.SaveEnvValue(nameof(SettingsObj.PING_FROM_TIME), "20:00");
            }
            if (string.IsNullOrWhiteSpace(initial_config.PING_TO_TIME)) {
                FileUtils.SaveEnvValue(nameof(SettingsObj.PING_TO_TIME), "5:00");
            }
            if (string.IsNullOrWhiteSpace(initial_config.PING_FREQUENCY_TIME)) {
                FileUtils.SaveEnvValue(nameof(SettingsObj.PING_FREQUENCY_TIME), "60");
            }
            if (string.IsNullOrWhiteSpace(initial_config.PING_TIMEOUT)) {
                FileUtils.SaveEnvValue(nameof(SettingsObj.PING_TIMEOUT), "5");
            }

            SettingsObj corrected_config = FileUtils.GetConfig();
            SMTPIPBox.Text = corrected_config.SMTP_IP;
            SMTPPortBox.Text = corrected_config.SMTP_PORT;
            FromEmailAddressBox.Text = corrected_config.FROM_EMAIL_ADDRESS;
            ReceiverMailsBox.Text = corrected_config.REPORT_RECEIVERS_EMAIL_ADDRESSES;
            FromTimePicker.Text = corrected_config.PING_FROM_TIME;
            ToTimePicker.Text = corrected_config.PING_TO_TIME;
            FrequencyTimePicker.Text = corrected_config.PING_FREQUENCY_TIME;
            PingTimeoutPicker.Text = corrected_config.PING_TIMEOUT;

            List<string> pcs = FileUtils.GetPcs();
            foreach (string pc in pcs) {
                PcsBox.Items.Add(pc);
            }

#if DEBUG
            scheduler_timer.Interval = 5 * 1000;
#else
            // * 60 to make it minutes
            scheduler_timer.Interval = 5 * 1000 * 60;
#endif

            scheduler_timer.Elapsed += Scheduler_timer_Elapsed;
            scheduler_timer.Start();

            ping_timer.Elapsed += Ping_timer_Elapsed;
            ping_timer.Stop();
        }

        private void Scheduler_timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            SettingsObj config = FileUtils.GetConfig();

            string[] from_time_splitted = config.PING_FROM_TIME.Split(":");
            string[] to_time_splitted = config.PING_TO_TIME.Split(":");

            int from_hour = int.Parse(from_time_splitted[0]);
            int from_minute = int.Parse(from_time_splitted[1]);

            int to_hour = int.Parse(to_time_splitted[0]);
            int to_minute = int.Parse(to_time_splitted[1]);

            DateTime from_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, from_hour, from_minute, 0);
            DateTime to_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, to_hour, to_minute, 59);
            if (from_hour > to_hour || (from_hour == to_hour && from_minute > to_minute)) {
                to_date = to_date.AddDays(1);
            }

            if ((to_date - DateTime.Now).TotalHours >= 23) {
                from_date = from_date.AddDays(-1);
                to_date = to_date.AddDays(-1);
            }

            if (from_date < DateTime.Now && to_date > DateTime.Now && !ping_timer.Enabled) {
                LogInfo("-- Enabling pc scan timer --");
                ping_timer.Start();
            }

            if (to_date < DateTime.Now && ping_timer.Enabled) {
                LogInfo("-- Disabling pc scan timer --");
                ping_timer.Stop();
                
                LogInfo("-- Sending results --");
                (bool sucess, string err_message) = FileUtils.SaveAndSendActivePcsResults(config.FROM_EMAIL_ADDRESS, MailUtils.ParseMailsFromConfig(config.REPORT_RECEIVERS_EMAIL_ADDRESSES), config.SMTP_IP, int.Parse(config.SMTP_PORT));
                if (!sucess) {
                    LogInfo("\tUnable to send results for following reasons:");
                    LogInfo($"\t\t{err_message}");
                    return;
                }

                LogWindow.Invoke((MethodInvoker)delegate {
                    LogWindow.Clear();
                });
                LogInfo("-- Sucesfully sent an email, cleared log, and cleared active_pcs file --");
            }

#if DEBUG
            int timer_frequency = int.Parse(config.PING_FREQUENCY_TIME) * 1000;
#else
            // * 60 to make it minutes
            int timer_frequency = int.Parse(config.PING_FREQUENCY_TIME) * 1000 * 60;
#endif

            if (ping_timer.Enabled && ping_timer.Interval != timer_frequency ) {
                LogInfo("-- Updating scan timer frequency --");
                ping_timer.Interval = timer_frequency;
            }
        }

        private void Ping_timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e) 
        {
            LogInfo("-- Starting pc scan --");

            SettingsObj config = FileUtils.GetConfig();
            List<string> active_pcs = new List<string>();

            int ping_timeout = int.Parse(config.PING_TIMEOUT) * 1000; // From seconds to miliseconds

            List<string> pcs_to_ping = FileUtils.GetPcs();
            foreach (string pc in pcs_to_ping) {
                Ping pinger_intance = new Ping();
                try {
                    PingReply reply = pinger_intance.Send(pc, ping_timeout);
                    if (reply.Status == IPStatus.Success) {
                        active_pcs.Add(pc);
                    }
                } catch {}
                finally {
                    pinger_intance?.Dispose();
                }
            }

            FileUtils.LogActivePcs(active_pcs);

            LogInfo("\tActive pcs:");
            foreach (string active_pc in active_pcs) {
                LogInfo($"\t\t{active_pc}");
            }
            LogInfo("-- Ended pc scan --");
        }

        private void AddNewPcButton_Click(object sender, EventArgs e)
        {
            string new_pc = NewPcInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(new_pc)) {
                MessageBox.Show("Název pc nesmí být prázdný!", "Nový název není validní.", MessageBoxButtons.OK);
                return;
            }

            List<string> pcs = FileUtils.GetPcs();
            if (pcs.Contains(new_pc)) {
                MessageBox.Show("Název pc již v souboru s počítači existuje!", "Nový název není unikátní.", MessageBoxButtons.OK);
                return;
            }

            pcs.Add(new_pc);
            FileUtils.SavePcs(pcs);

            PcsBox.Items.Add(new_pc);

            NewPcInput.Text = "";
        }

        private void NewPcInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) {
                AddNewPcButton_Click(AddNewPcButton, new EventArgs());
            }
        }

        private void PcsBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (PcsBox.SelectedItems.Count == 0) {
                return;
            }

            string selected_pc = (string)PcsBox.SelectedItems[0];
            if (MessageBox.Show($"Opravdu chcete odebrat PC s názvem \"{selected_pc}\"", "Upozornìní pøed odebráním.", MessageBoxButtons.YesNo) != DialogResult.Yes) {
                return;
            }

            FileUtils.SavePcs(FileUtils.GetPcs().Where(s => s != selected_pc).ToList());
            PcsBox.Items.Remove(selected_pc);
        }

        private void ImportFromCSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog file_dialog = new OpenFileDialog {
                InitialDirectory = "c:\\",
                Filter = "CSV Soubor (*.csv, *.txt)|*.csv;*.txt",
                FilterIndex = 0,
                RestoreDirectory = true,
                Multiselect = false,
            };

            if (file_dialog.ShowDialog() != DialogResult.OK) {
                return;
            }

            string selected_file_path = file_dialog.FileName;
            List<string> current_pcs = FileUtils.GetPcs();
            List<string> new_pcs_filtered = File.ReadAllLines(selected_file_path)
                .Select(s => s.Split(";")[0].Split(",")[0].Trim())
                .Where(s => !current_pcs.Contains(s) && !string.IsNullOrWhiteSpace(s))
                .Distinct()
                .ToList();

            current_pcs.AddRange(new_pcs_filtered);
            FileUtils.SavePcs(current_pcs);

            foreach (string new_pc in new_pcs_filtered) {
                PcsBox.Items.Add(new_pc);
            }

            MessageBox.Show($"Nahrál jsem nové počítače: {string.Join(", ", new_pcs_filtered)}", "Přidané počítače.");
        }

        private void SendTestMailButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(SMTPPortBox.Text, out int port)) {
                return;
            }

            LogInfo($"-- Sending test email to: {SMTPIPBox.Text.Trim()}:{port} --");
            (bool result, string err_text) = MailUtils.SendEmail("Testovací mail.", "test", FromEmailAddressBox.Text.Trim(), MailUtils.ParseMailsFromConfig(ReceiverMailsBox.Text), SMTPIPBox.Text.Trim(), port);
            if (!result) {
                LogInfo($"-- Test email error: {err_text} --");
            } else {
                LogInfo("-- Test email successfully sent. --");
            }
        }

        private static readonly Regex ip_regex = new Regex("^((25[0-5]|(2[0-4]|1[0-9]|[1-9]|)[0-9])(\\.(?!$)|$)){4}$");
        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            string new_ip = SMTPIPBox.Text.Trim();
            if (!ip_regex.IsMatch(new_ip)) {
                MessageBox.Show("Nepovedlo se validovat SMTP IP.", "IP není validní.", MessageBoxButtons.OK);
                return;
            }

            string new_port_text = SMTPPortBox.Text.Trim();
            if (!int.TryParse(new_port_text, out int new_port) || new_port < 0 || new_port > 65536) {
                MessageBox.Show("Nepovedlo se validovat SMTP PORT. PORT musí být èíslo v rozmezí 0 - 65536", "PORT není validní.", MessageBoxButtons.OK);
                return;
            }

            DateTime from_time = FromTimePicker.Value;
            DateTime to_time = ToTimePicker.Value;

            FileUtils.SaveEnvValue(nameof(SettingsObj.SMTP_IP), new_ip);
            FileUtils.SaveEnvValue(nameof(SettingsObj.SMTP_PORT), new_port.ToString());
            FileUtils.SaveEnvValue(nameof(SettingsObj.FROM_EMAIL_ADDRESS), FromEmailAddressBox.Text.Trim());
            FileUtils.SaveEnvValue(nameof(SettingsObj.REPORT_RECEIVERS_EMAIL_ADDRESSES), ReceiverMailsBox.Text.Trim());
            FileUtils.SaveEnvValue(nameof(SettingsObj.PING_FROM_TIME), $"{from_time.Hour}:{from_time.Minute}");
            FileUtils.SaveEnvValue(nameof(SettingsObj.PING_TO_TIME), $"{to_time.Hour}:{to_time.Minute}");
            FileUtils.SaveEnvValue(nameof(SettingsObj.PING_FREQUENCY_TIME), FrequencyTimePicker.Value.ToString());

            MessageBox.Show("Úspěšně uloženo nové nastavení", "Úspěch");
        }

        private void LogInfo(string info) =>
            LogWindow.Invoke((MethodInvoker)delegate {
                LogWindow.AppendText($"{info}{Environment.NewLine}");
                LogWindow.ScrollToCaret();
            });
    }
}