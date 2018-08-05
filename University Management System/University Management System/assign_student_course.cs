using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace University_Management_System
{
    public partial class assign_student_course : Form
    {
        public assign_student_course()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MasterChief\documents\visual studio 2015\Projects\University Management System\University Management System\management.mdf;Integrated Security=True;Connect Timeout=30");
        private void assign_student_course_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSet4.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter.Fill(this.managementDataSet4.Students);
            disp_data();
            //////////
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Id from Course";
            SqlDataReader rd = cmd.ExecuteReader();
            //AutoCompleteStringCollection autocomp = new AutoCompleteStringCollection();
            while (rd.Read())
            {
                comboBox1.Items.Add(rd["Id"]);
            }
            con.Close();
            /////////
            // TODO: This line of code loads data into the 'managementDataSet3.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter1.Fill(this.managementDataSet3.Table1);
            // TODO: This line of code loads data into the 'managementDataSet2.Course' table. You can move, or remove it, as needed.
            this.courseTableAdapter.Fill(this.managementDataSet2.Course);
            // TODO: This line of code loads data into the 'managementDataSet.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter.Fill(this.managementDataSet.Table1);
            disp_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new university_management().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != string.Empty)
            {
                if (textBox4.Text != string.Empty)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
                        cmd.CommandText = "insert into CS(course_id,student_id,gpa) values (@course_id,@student_id,@gpa)";
                        cmd.Parameters.AddWithValue("@course_id", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@student_id", textBox4.Text);
                        cmd.Parameters.AddWithValue("@gpa", "0.0");
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        con.Close();
                        disp_data();
                        // dataGridView1.Update();
                        //  dataGridView1.Refresh();
                        comboBox1.ResetText();
                        textBox4.ResetText();
                        textBox5.ResetText();
                        textBox6.ResetText();
                        textBox7.ResetText();
                        MessageBox.Show("Added");
                    }
                    catch (Exception) {
                        MessageBox.Show("Error");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill out the Course Id", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please fill out the Student Id", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void disp_data()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
            cmd.CommandText = "select * from Students";
            cmd.ExecuteNonQuery();
            DataTable d = new DataTable();
            SqlDataAdapter dx = new SqlDataAdapter(cmd);
            dx.Fill(d);
            dataGridView1.DataSource = d;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != string.Empty)
            {
                var result = MessageBox.Show("Are you sure you want to delete this ?", "Deleting a Vendor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from CS where [course_id] =@id and [student_id]=@s_id";
                    cmd.Parameters.AddWithValue("@id", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@s_id", textBox4.Text);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();

                    disp_data();
                    comboBox1.ResetText();
                    textBox4.ResetText();
                    textBox5.ResetText();
                    textBox6.ResetText();
                    textBox7.ResetText();
                    
                }
            }
            else
            {
                MessageBox.Show("Please fill out the Id", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
            textBox6.ResetText();
            textBox7.ResetText();
        }
    }
}
