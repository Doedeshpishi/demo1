using Npgsql;
using System;
using System.Windows.Forms;

namespace demo
{
    public partial class LoginForm : Form
    {
        private string connectionString = "Host=195.46.187.72;Username=postgres;Password=1337;Database=task_management1";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT role FROM users WHERE username = @username AND password = @password";
                        cmd.Parameters.AddWithValue("username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("password", txtPassword.Text);
                        object roleObj = cmd.ExecuteScalar();
                        if (roleObj != null)
                        {
                            string userRole = roleObj.ToString();
                            Form1 mainForm = new Form1(txtUsername.Text, userRole);
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Неверное имя пользователя или пароль.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
