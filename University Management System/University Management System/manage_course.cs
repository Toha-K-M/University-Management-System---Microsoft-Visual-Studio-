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
    public partial class manage_course : Form
    {
        public manage_course()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MasterChief\documents\visual studio 2015\Projects\University Management System\University Management System\management.mdf;Integrated Security=True;Connect Timeout=30");
        private void button3_Click(object sender, EventArgs e)
        {
            new university_management().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Entered");
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
                cmd.CommandText = "insert into Course(Id,name,department,room,teacher_id) values (@id,@Name,@Department,@Room,@teacher_id)";
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                comboBox3.Items.Add(textBox1.Text);
                cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                cmd.Parameters.AddWithValue("@Department", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Room", textBox4.Text);
                cmd.Parameters.AddWithValue("@teacher_id", comboBox2.Text);
                MessageBox.Show("hmm");
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                // dataGridView1.Update();
                //  dataGridView1.Refresh();
                disp_data();
                textBox1.ResetText();
                textBox2.ResetText();
                comboBox1.ResetText();
                textBox4.ResetText();
                //comboBox2.SelectedIndex = -1;

                MessageBox.Show("Added");
            }
            catch (Exception) {
                MessageBox.Show("Error");
                new manage_course().Show();
                this.Hide();
            }
               
        }
        public void disp_data()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
            cmd.CommandText = "select * from Course";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }
        private void manage_course_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSet2.Course' table. You can move, or remove it, as needed.
            
            this.courseTableAdapter.Fill(this.managementDataSet2.Course);
            
            // TODO: This line of code loads data into the 'managementDataSet1.Teacher' table. You can move, or remove it, as needed.
            //this.teacherTableAdapter.Fill(this.managementDataSet1.Teacher);
            //comboBox2.DataSource = null;
            //teacherTableAdapter.Fill(managementDataSet1.Teacher);
            //comboBox2.DataSource = this.teacherBindingSource;
            //comboBox2.Refresh();
            disp_data();
            ////////////////
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select name from Teacher";
            SqlDataReader rd = cmd.ExecuteReader();
            //AutoCompleteStringCollection autocomp = new AutoCompleteStringCollection();
            while (rd.Read())
            {
                comboBox2.Items.Add(rd["name"]);
            }
            con.Close();
            /////////////
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Id from Course";
            rd = cmd.ExecuteReader();
            //AutoCompleteStringCollection autocomp = new AutoCompleteStringCollection();
            while (rd.Read())
            {
                comboBox3.Items.Add(rd["Id"]);
            }
            con.Close();
            //////////////
            comboBox3.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            richTextBox1.Text = null; 
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                var result = MessageBox.Show("Are you sure you want to remove this ?", "Removing a course", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from Course where [Id] =@id";
                    cmd.Parameters.AddWithValue("@id", textBox1.Text);
                    comboBox3.Items.Remove(textBox1.Text);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();

                    disp_data();

                    textBox1.ResetText();
                    textBox2.ResetText();
                    comboBox1.ResetText();
                    textBox4.ResetText();
                    comboBox2.ResetText();
                }
            }
            else
            {
                MessageBox.Show("Please fill out the Id", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            comboBox2.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

       
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            string teacher_id = "";
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Course Where [Id]=@Id";
            cmd.Parameters.AddWithValue("@Id", comboBox3.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            richTextBox1.Text = richTextBox1.Text + "Course Details :  \n";
            //AutoCompleteStringCollection autocomp = new AutoCompleteStringCollection();
            while (rd.Read())
            {
                richTextBox1.Text = richTextBox1.Text + "Course ID : " + rd[0].ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Course Name : " + rd[1].ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Department : " + rd[2].ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Room No. : " + rd[3].ToString() + "\n";
                teacher_id = rd[4].ToString();
                //textBox1.Text = (rd["Name"].ToString());
                //textBox2.Text = (rd["Phone"].ToString());
               // textBox4.Text = (rd["Address"].ToString());
            }
            con.Close();
            richTextBox1.Text = richTextBox1.Text + "\n";
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Teacher Where [name]=@x";
            cmd.Parameters.AddWithValue("@x", teacher_id);
            rd = cmd.ExecuteReader();
            richTextBox1.Text = richTextBox1.Text + "Assigned Teacher :  \n";
            //AutoCompleteStringCollection autocomp = new AutoCompleteStringCollection();
            while (rd.Read())
           {
                  richTextBox1.Text = richTextBox1.Text + "Teacher ID : " + rd[0].ToString() + "\n";
                  richTextBox1.Text = richTextBox1.Text + "Teacher Name : " + rd[1].ToString() + "\n";
                  richTextBox1.Text = richTextBox1.Text + "Email : " + rd[3].ToString() + "\n";
                //textBox1.Text = (rd["Name"].ToString());
                //textBox2.Text = (rd["Phone"].ToString());
                // textBox4.Text = (rd["Address"].ToString());
            }
            con.Close();
            richTextBox1.Text = richTextBox1.Text + "\n";
            int student = 0;
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from CS Where [course_id]=@y";
            cmd.Parameters.AddWithValue("@y", comboBox3.Text);
            rd = cmd.ExecuteReader();
            richTextBox1.Text = richTextBox1.Text + "Student ID: \n";
            while (rd.Read())
            {
                richTextBox1.Text = richTextBox1.Text + "   " + rd[1].ToString() + "\n";
                student++;
                
               // richTextBox1.Text = richTextBox1.Text + "" + rd[3].ToString() + "\n";
                //textBox1.Text = (rd["Name"].ToString());
                //textBox2.Text = (rd["Phone"].ToString());
                // textBox4.Text = (rd["Address"].ToString());
            }
            con.Close();
            richTextBox1.Text = richTextBox1.Text + "Total Student : " + student + "\n";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            comboBox1.ResetText();
            textBox4.ResetText();
            comboBox2.SelectedIndex = -1;
        }
    }
}
