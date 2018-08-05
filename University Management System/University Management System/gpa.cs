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
    public partial class gpa : Form
    {
        public gpa()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MasterChief\documents\visual studio 2015\Projects\University Management System\University Management System\management.mdf;Integrated Security=True;Connect Timeout=30");
        private void gpa_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSet5.CS' table. You can move, or remove it, as needed.
            this.cSTableAdapter.Fill(this.managementDataSet5.CS);
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
            // TODO: This line of code loads data into the 'managementDataSet2.Course' table. You can move, or remove it, as needed.
            this.courseTableAdapter.Fill(this.managementDataSet2.Course);
            comboBox1.SelectedIndex = -1;
            ///comboBox2.SelectedIndex = -1;
            dataGridView2.DataSource = null;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != string.Empty)
            {
                if (textBox6.Text != string.Empty)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
                   // cmd.CommandText = "update Students set name = @name, phone=@phone, email=@email, address=@address, age=@age, gender=@gender where Id=@id";
                    cmd.CommandText = "update [CS] set gpa = @gpa where [course_id]=@id and [student_id]=@id1";
                    cmd.Parameters.AddWithValue("@id", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@id1", textBox4.Text);
                    cmd.Parameters.AddWithValue("@gpa", textBox6.Text);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    // dataGridView1.Update();
                    //  dataGridView1.Refresh();
                    disp_data();
                    textBox4.ResetText();
                    textBox6.ResetText();

                    MessageBox.Show("Added");
                }
                else
                {
                    MessageBox.Show("Please fill out the GPA", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please fill out the Id", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void disp_data()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
            cmd.CommandText = "select student_id,gpa from CS where [course_id]=@course_id";
            cmd.Parameters.AddWithValue("@course_id", comboBox1.Text);
            cmd.ExecuteNonQuery();
            DataTable d = new DataTable();
            SqlDataAdapter dx = new SqlDataAdapter(cmd);
            dx.Fill(d);
            dataGridView1.DataSource = d;
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            disp_data();
        }

        

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cgpa_label.Text = "---";
            //disp_data1();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != string.Empty)
            {
                if (textBox6.Text != string.Empty)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
                    // cmd.CommandText = "update Students set name = @name, phone=@phone, email=@email, address=@address, age=@age, gender=@gender where Id=@id";
                    cmd.CommandText = "update [CS] set gpa = @gpa where [course_id]=@id and [student_id]=@id1";
                    cmd.Parameters.AddWithValue("@id", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@id1", textBox4.Text);
                    cmd.Parameters.AddWithValue("@gpa", textBox6.Text);
                    
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    // dataGridView1.Update();
                    //  dataGridView1.Refresh();
                    disp_data();
                    textBox4.ResetText();
                    textBox6.ResetText();

                    MessageBox.Show("Added");
                }
                else
                {
                    MessageBox.Show("Please fill out the GPA", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please fill out the Id", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public string calculate_cgpa(string x) {
            return x;
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from CS Where [student_id]=@y";
            cmd.Parameters.AddWithValue("@y", textBox1.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            float total_gpa = 0;
            float sub = 0;
            while (rd.Read())
            {
                total_gpa = total_gpa + float.Parse(rd[2].ToString());
                sub++;

                // richTextBox1.Text = richTextBox1.Text + "" + rd[3].ToString() + "\n";
                //textBox1.Text = (rd["Name"].ToString());
                //textBox2.Text = (rd["Phone"].ToString());
                // textBox4.Text = (rd["Address"].ToString());
            }
            float cgpa = total_gpa / sub;
            cgpa_label.Text = cgpa.ToString();
           // MessageBox.Show(cgpa.ToString());
            con.Close();
            //cgpa.Text = calculate_cgpa();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
          

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
            cmd.CommandText = "select course_id,gpa from CS where [student_id]=@student_id";
            cmd.Parameters.AddWithValue("@student_id", textBox1.Text);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
     }

        private void button1_Click(object sender, EventArgs e)
        {
            new university_management().Show();
            this.Hide();
        }
    }
}
