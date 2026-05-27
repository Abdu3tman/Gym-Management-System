using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace GYM2
{
    public static class GymData
    {
        public static List<Member> Members = new List<Member>();
        public static List<Payment> Payments = new List<Payment>();

        private static readonly string DataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GYM2");

        private static string MembersFile => Path.Combine(DataFolder, "members.json");
        private static string PaymentsFile => Path.Combine(DataFolder, "payments.json");

        public static void Load()
        {
            try
            {
                Directory.CreateDirectory(DataFolder);
                var js = new JavaScriptSerializer();

                if (File.Exists(MembersFile))
                    Members = js.Deserialize<List<Member>>(File.ReadAllText(MembersFile)) ?? new List<Member>();

                if (File.Exists(PaymentsFile))
                    Payments = js.Deserialize<List<Payment>>(File.ReadAllText(PaymentsFile)) ?? new List<Payment>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message);
            }
        }

        public static void Save()
        {
            try
            {
                Directory.CreateDirectory(DataFolder);
                var js = new JavaScriptSerializer();
                File.WriteAllText(MembersFile, js.Serialize(Members));
                File.WriteAllText(PaymentsFile, js.Serialize(Payments));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save data: " + ex.Message);
            }
        }
    }
}
