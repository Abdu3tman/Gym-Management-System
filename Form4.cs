using System;
using System.Windows.Forms;

namespace GYM2
{
    public partial class Form4 : Form
    {
        public enum Mode { Update, Delete }

        private Mode _mode;
        private bool _navigatingBack = false;

        public Form4(Mode mode = Mode.Update)
        {
            InitializeComponent();
            _mode = mode;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            RefreshGrid();

            // Show/hide buttons based on mode
            if (_mode == Mode.Delete)
            {
                btnUpdate.Visible = false;
                btnDelete.Visible = true;
                this.Text = "Delete Member";
            }
            else
            {
                btnUpdate.Visible = true;
                btnDelete.Visible = false;
                this.Text = "Update Member";
            }
        }

        private void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = GymData.Members;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadSelectedRow(e.RowIndex);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
                LoadSelectedRow(dataGridView1.CurrentRow.Index);
        }

        private void LoadSelectedRow(int i)
        {
            if (i < 0 || i >= GymData.Members.Count) return;

            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value?.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value?.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value?.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[4].Value?.ToString();
            comboBox1.Text = dataGridView1.Rows[i].Cells[3].Value?.ToString();
            comboBox2.Text = dataGridView1.Rows[i].Cells[5].Value?.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a member to update.");
                return;
            }

            int i = dataGridView1.CurrentRow.Index;
            if (i < 0 || i >= GymData.Members.Count) return;

            try
            {
                GymData.Members[i].Name = textBox1.Text.Trim();
                GymData.Members[i].Phone = textBox2.Text.Trim();
                GymData.Members[i].Age = int.Parse(textBox3.Text);
                GymData.Members[i].Amount = double.Parse(textBox4.Text);
                GymData.Members[i].Gender = comboBox1.Text;
                GymData.Members[i].Timing = comboBox2.Text;

                GymData.Save(); // Persist
                RefreshGrid();
                MessageBox.Show("Updated Successfully");
            }
            catch
            {
                MessageBox.Show("Please enter valid data.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a member to delete.");
                return;
            }

            int i = dataGridView1.CurrentRow.Index;
            if (i < 0 || i >= GymData.Members.Count) return;

            string name = GymData.Members[i].Name;
            var confirm = MessageBox.Show(
                $"Are you sure you want to delete '{name}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                GymData.Members.RemoveAt(i);
                GymData.Save(); // Persist

                // Clear fields
                textBox1.Clear(); textBox2.Clear();
                textBox3.Clear(); textBox4.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;

                RefreshGrid();
                MessageBox.Show("Deleted Successfully");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Add button on Form4
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                Member m = new Member();
                m.Name = textBox1.Text.Trim();
                m.Phone = textBox2.Text.Trim();
                m.Age = int.Parse(textBox3.Text);
                m.Amount = double.Parse(textBox4.Text);
                m.Gender = comboBox1.Text;
                m.Timing = comboBox2.Text;

                GymData.Members.Add(m);
                GymData.Save(); // Persist

                MessageBox.Show("Member Added Successfully");
                RefreshGrid();
            }
            catch
            {
                MessageBox.Show("Please enter valid data.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Reset fields
            textBox1.Clear(); textBox2.Clear();
            textBox3.Clear(); textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Back button
            _navigatingBack = true;
            new Form2().Show();
            this.Close();
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
