using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GYM2
{
    public partial class Form5 : Form
    {
        private bool _navigatingBack = false;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = GymData.Members;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Member> searchResult = new List<Member>();

            foreach (Member m in GymData.Members)
            {
                if (m.Name.ToLower().Contains(txtSearch.Text.ToLower()))
                    searchResult.Add(m);
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = searchResult;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _navigatingBack = true;
            new Form2().Show();
            this.Close();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            GymData.Save();
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!_navigatingBack && e.CloseReason == CloseReason.UserClosing)
            {
                new Form2().Show();
            }
        }
    }
}
