using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace University_Management_System
{
    public partial class university_management : Form
    {
        public university_management()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new loginForm().Show();
            this.Hide();
        }

        private void manage_student_Click(object sender, EventArgs e)
        {
            new student_management().Show();
            this.Hide();
        }

        private void manage_teacher_Click(object sender, EventArgs e)
        {
            new teacher_management().Show();
            this.Hide();
        }

        private void manage_course_Click(object sender, EventArgs e)
        {
            new manage_course().Show();
            this.Hide();
        }

        private void view_course_info_Click(object sender, EventArgs e)
        {
            new assign_student_course().Show();
            this.Hide();
        }

        private void grade_to_course_Click(object sender, EventArgs e)
        {
            new gpa().Show();
            this.Hide();
        }
    }
}
