using System;
using System.Windows.Forms;

namespace GYM2
{
    public partial class Form6 : Form
    {
        private bool _navigatingBack = false;

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = GymData.Payments;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtAmount.Text))
                {
                    MessageBox.Show("Missing Data");
                    return;
                }

                Payment p = new Payment();
                p.MemberName = txtName.Text.Trim();
                p.Month = dateTimePicker1.Text;
                p.Amount = double.Parse(txtAmount.Text);

                GymData.Payments.Add(p);
                GymData.Save(); // Persist

                MessageBox.Show("Paid Successfully");

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = GymData.Payments;
            }
            catch
            {
                MessageBox.Show("Enter valid data");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtAmount.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _navigatingBack = true;
            new Form2().Show();
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
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
