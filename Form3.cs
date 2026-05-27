using System;
using System.Windows.Forms;

namespace GYM2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text) ||
                    string.IsNullOrWhiteSpace(txtAge.Text) ||
                    string.IsNullOrWhiteSpace(txtAmount.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                Member m = new Member();
                m.Name = txtName.Text.Trim();
                m.Phone = txtPhone.Text.Trim();
                m.Age = int.Parse(txtAge.Text);
                m.Amount = double.Parse(txtAmount.Text);
                m.Gender = cmbGender.Text;
                m.Timing = cmbTiming.Text;

                GymData.Members.Add(m);
                GymData.Save(); // Persist immediately

                MessageBox.Show("Added Successfully");
                btnReset_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Enter valid data");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPhone.Clear();
            txtAge.Clear();
            txtAmount.Clear();
            cmbGender.SelectedIndex = -1;
            cmbTiming.SelectedIndex = -1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }

        // If user closes with X, go back to dashboard
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                new Form2().Show();
            }
        }
    }
}
