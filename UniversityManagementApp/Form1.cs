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


namespace UniversityManagementApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Привязка обработчика события Click для кнопки Войти
            btnLogin.Click += btnLogin_Click;
            this.FormClosing += Form1_FormClosing;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Полностью закрываем приложение, если закрывается Form1
            Application.Exit();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Получаем данные из текстовых полей
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            // Проверяем правильность логина и пароля
            if (ValidateLoginAndPassword(login, password))
            {
                // Получаем роль из базы данных
                string userRole = GetUserRoleFromDatabase(login);

                // Проверка, что роль была найдена
                if (!string.IsNullOrEmpty(userRole))
                {
                    // Показываем уведомление с логином и ролью пользователя
                    MessageBox.Show($"Добро пожаловать, {login}!\nВаша роль: {userRole}",
                                     "Успешный вход",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information);

                    // Создаем объект MainForm и передаем параметры
                    MainForm mainForm = new MainForm(login, password, userRole);

                    // Открываем MainForm
                    mainForm.Show();

                    // Скрываем текущую форму
                    this.Hide();
                }
                else
                {
                    // Если роль не найдена
                    MessageBox.Show("Роль пользователя не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Если логин и пароль неверные
                MessageBox.Show("Неверные данные для входа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Получение роли пользователя из базы данных
        private string GetUserRoleFromDatabase(string login)
        {
            // Убираем зависимость от поля для ввода сервера
            string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={txtPassword.Text};";
            string role = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Запрос для получения имени пользователя, соответствующего логину
                    string query = @"
            DECLARE @UserName NVARCHAR(128);
            SET @UserName = (SELECT name FROM sys.database_principals WHERE type = 'S' AND authentication_type = 1 AND sid = SUSER_SID(@login));
            SELECT dr.name AS RoleName
            FROM sys.database_principals dp
            JOIN sys.database_role_members drm
              ON dp.principal_id = drm.member_principal_id
            JOIN sys.database_principals dr
              ON drm.role_principal_id = dr.principal_id
            WHERE dp.name = @UserName;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@login", login);  // Передаем логин, введенный пользователем

                    connection.Open();

                    // Получаем результат (роль пользователя)
                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        role = result.ToString();  // Если роль найдена, присваиваем значение
                    }
                    else
                    {
                        role = "guest";  // Роль по умолчанию
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Ошибка при подключении к базе данных: {sqlEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return role;
        }
        private bool ValidateLoginAndPassword(string login, string password)
        {
            bool isValid = false;

            // Формируем строку подключения для базы данных, используя жестко заданное имя сервера
            string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Если подключение удачно, то это означает, что логин и пароль правильные
                    isValid = true; // Логин и пароль верны
                }
                catch (SqlException ex)
                {
                    // Ошибка при подключении
                    MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return isValid;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Здесь можно добавить логику, выполняемую при загрузке формы
        }
    }
}




