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
    public partial class student_management : Form
    {
        public student_management()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MasterChief\documents\visual studio 2015\Projects\University Management System\University Management System\management.mdf;Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                if (textBox2.Text != string.Empty)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
                        cmd.CommandText = "insert into Students(Id,name,phone,email,address,age,gender) values (@id,@Name,@Phone,@Email,@Address,@Age,@Gender)";
                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Phone", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Address", textBox5.Text);
                        cmd.Parameters.AddWithValue("@Age", textBox6.Text);
                        if (radioButton1.Checked)
                            cmd.Parameters.AddWithValue("@Gender", "Male");
                        else
                            cmd.Parameters.AddWithValue("@Gender", "Female");
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        con.Close();
                        // dataGridView1.Update();
                        //  dataGridView1.Refresh();
                        disp_data();
                        textBox1.ResetText();
                        textBox2.ResetText();
                        textBox3.ResetText();
                        textBox4.ResetText();
                        textBox5.ResetText();
                        textBox6.ResetText();
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        MessageBox.Show("Added");
                    }
                    catch (Exception) {
                        MessageBox.Show("Error");
                        new student_management().Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Please fill out the Id", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please fill out the Name", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new university_management().Show();
            this.Hide();
        }

        public void disp_data() {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "INSERT INTO Table1 (username,password,gender) VALUES (@username,@password,@gender)";
            cmd.CommandText = "select * from Students";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void student_management_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSet2.Course' table. You can move, or remove it, as needed.
            this.courseTableAdapter.Fill(this.managementDataSet2.Course);
            // TODO: This line of code loads data into the 'managementDataSet.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter.Fill(this.managementDataSet.Table1);
            disp_data();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            String name = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            //MessageBox.Show(name);
            if (name == "Male")
                radioButton1.Checked = true;
            if (name == "Female")
                radioButton2.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != string.Empty)
            {
                    var result = MessageBox.Show("Are you sure you want to delete this ?", "Deleting a Vendor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "delete from Students where [Id] =@id";
                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        con.Close();
                        
                        disp_data();

                    textBox1.ResetText();
                    textBox2.ResetText();
                    textBox3.ResetText();
                    textBox4.ResetText();
                    textBox5.ResetText();
                    textBox6.ResetText();
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
            }
            else
            {
                MessageBox.Show("Please fill out the Id", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ///
           // var result = MessageBox.Show("Are you sure you want to delete this ?", "Deleting a Vendor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Students set name = @name, phone=@phone, email=@email, address=@address, age=@age, gender=@gender where Id=@id";
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@name", textBox2.Text);
            cmd.Parameters.AddWithValue("@phone", textBox3.Text);
            cmd.Parameters.AddWithValue("@email", textBox4.Text);
            cmd.Parameters.AddWithValue("@address", textBox5.Text);
            cmd.Parameters.AddWithValue("@age", textBox6.Text);
            if (radioButton1.Checked)
                cmd.Parameters.AddWithValue("@gender", "Male");
            else
                cmd.Parameters.AddWithValue("@gender", "Female");
            cmd.ExecuteNonQuery();
            con.Close();
            disp_data();
            MessageBox.Show("updated");
            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
            textBox6.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
            textBox6.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
    }
}
