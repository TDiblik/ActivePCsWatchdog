using System.Text;
using System.Globalization;

namespace ActivePCsWatchdog
{
    public static class FileUtils
    {
        public const string settings_folder_path = @"C:\program_active_pcs_watchdog\";
        public const string reports_folder_path = @$"{settings_folder_path}reports\";

        private const string settings_file_name = "settings.txt";
        private const string pcs_file_name = "pcs.txt";
        private const string active_pcs_file_name = "active_pcs.txt";

        public const string SettingsFilePath = $"{settings_folder_path}{settings_file_name}";
        public const string PCsFilePath = $"{settings_folder_path}{pcs_file_name}";
        public const string ActivePCsFilePath = $"{settings_folder_path}{active_pcs_file_name}";


        public static SettingsObj GetConfig()
        {
            Dictionary<string, string> env_variables = retrieve_env_variables();

            return new SettingsObj() {
                SMTP_IP = get_env_value(env_variables, nameof(SettingsObj.SMTP_IP)),
                SMTP_PORT = get_env_value(env_variables, nameof(SettingsObj.SMTP_PORT)),
                FROM_EMAIL_ADDRESS = get_env_value(env_variables, nameof(SettingsObj.FROM_EMAIL_ADDRESS)),
                REPORT_RECEIVERS_EMAIL_ADDRESSES = get_env_value(env_variables, nameof(SettingsObj.REPORT_RECEIVERS_EMAIL_ADDRESSES)),
                PING_FROM_TIME = get_env_value(env_variables, nameof(SettingsObj.PING_FROM_TIME)),
                PING_TO_TIME = get_env_value(env_variables, nameof(SettingsObj.PING_TO_TIME)),
                PING_FREQUENCY_TIME = get_env_value(env_variables, nameof(SettingsObj.PING_FREQUENCY_TIME)),
                PING_TIMEOUT = get_env_value(env_variables, nameof(SettingsObj.PING_TIMEOUT)),
            };
        }

        public static void SaveEnvValue(string key, string value)
        {
            Dictionary<string, string> env_variables = retrieve_env_variables();
            if (!env_variables.ContainsKey(key)) {
                env_variables.Add(key, "");
            }
            env_variables[key] = value;

            StringBuilder new_env = new StringBuilder();
            foreach (KeyValuePair<string, string> item in env_variables) {
                new_env.AppendLine($"{item.Key}={item.Value}");
            }
            File.WriteAllText(SettingsFilePath, new_env.ToString());
        }

        public static List<string> GetPcs() => File.ReadLines(PCsFilePath).Select(s => s.Trim()).ToList();

        public static void SavePcs(List<string> pcs) => File.WriteAllLines(PCsFilePath, pcs);

        private const string active_pcs_date_divider = ";=-=;";
        private const string active_pcs_date_format = "dd.MM.yyyy HH:mm";
        public static void LogActivePcs(List<string> active_pcs)
        {
            string current_date = DateTime.Now.ToString(active_pcs_date_format);
            File.AppendAllLines(ActivePCsFilePath, active_pcs.Select(s => $"{current_date} {active_pcs_date_divider} {s}").ToList());
        }

        public static (bool, string) SaveAndSendActivePcsResults(string sender, string[] receivers, string smtp_ip, int smtp_port)
        {
            StringBuilder report_body = new StringBuilder();

            Dictionary<DateTime, List<string>> sorted_out_records = new Dictionary<DateTime, List<string>>();
            string[] all_records = File.ReadAllLines(ActivePCsFilePath);
            foreach (string record in all_records) {
                string[] line = record.Split(active_pcs_date_divider);
                if (line.Length != 2) {
                    continue;
                }

                DateTime date_key = DateTime.ParseExact(line[0].Trim(), active_pcs_date_format, CultureInfo.InvariantCulture);
                if (!sorted_out_records.ContainsKey(date_key)) {
                    sorted_out_records.Add(date_key, new List<string>());
                }

                string pc_key = line[1].Trim();
                if (sorted_out_records.TryGetValue(date_key, out List<string>? current_list_of_values) && !current_list_of_values.Contains(pc_key)) {
                    current_list_of_values.Add(pc_key);
                }
            }

            List<string> pcs_active_whole_time = sorted_out_records.Count > 0 ? sorted_out_records.OrderBy(s => s.Key).First().Value : new List<string>();
            foreach (KeyValuePair<DateTime, List<string>> active_pcs in sorted_out_records) {
                pcs_active_whole_time.RemoveAll(s => !active_pcs.Value.Contains(s));
            }

            report_body.AppendLine("Aktivní PC po celou dobu: ");
            foreach (string active_pc in pcs_active_whole_time) {
                report_body.AppendLine($"\t{active_pc}");
            }
            report_body.AppendLine("");

            foreach (KeyValuePair<DateTime, List<string>> active_pcs in sorted_out_records) {
                List<string> active_pcs_filtered = active_pcs.Value.Where(s => !pcs_active_whole_time.Contains(s)).ToList();
                if (active_pcs_filtered.Count == 0) {
                    continue;
                }

                report_body.AppendLine($"Aktivní PC v {active_pcs.Key.ToString(active_pcs_date_format)}: ");
                foreach (string active_pc in active_pcs_filtered) {
                    report_body.AppendLine($"\t{active_pc}");
                }
            }

            string report = report_body.ToString();
            File.WriteAllText($"{reports_folder_path}report-{DateTime.Now.ToString("dd_MM_yyyy__HH_mm")}.txt", report);
            File.WriteAllText(ActivePCsFilePath, "");

            return MailUtils.SendEmail($"Report zapnutých PC {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}", report, sender, receivers, smtp_ip, smtp_port);
        }

        private static Dictionary<string, string> retrieve_env_variables()
        {
            Dictionary<string, string> env_variables = new Dictionary<string, string>();
            foreach (string line in File.ReadLines(SettingsFilePath)) {
                string[] env_variable = line.Split("=");
                if (env_variable.Length != 2) {
                    continue;
                }

                env_variables.Add(env_variable[0].Trim(), env_variable[1].Trim());
            }
            return env_variables;
        }

        private static string get_env_value(Dictionary<string, string> env_variables, string key)
            => env_variables.TryGetValue(key, out string? value) ? (value ?? "") : "";

    }

    public class SettingsObj
    {
        public string SMTP_IP { get; set; } = "";
        public string SMTP_PORT { get; set; } = "";
        public string FROM_EMAIL_ADDRESS { get; set; } = "";
        public string REPORT_RECEIVERS_EMAIL_ADDRESSES { get; set; } = "";
        public string PING_FROM_TIME { get; set; } = "";
        public string PING_TO_TIME { get; set; } = "";
        public string PING_FREQUENCY_TIME { get; set; } = "";
        public string PING_TIMEOUT { get; set; } = "";
    }
}
