using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace UniversityManagementApp
{
    public partial class MainForm : Form
    {
        private string login;
        private string password;
        private string userRole;

        public MainForm(string login, string password, string userRole)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
            this.userRole = userRole;
            UpdateWelcomeMessage();
            this.FormClosing += MainForm_FormClosing;
            btnExit.Click += BtnExit_Click;
            InitializeRoleBasedControls(); // Проверка роли пользователя
            cmbTables.Items.AddRange(new string[] { "student", "subject", "professor", "audience", "group", "lesson", "attendance", "grade" });
            cmbTables.SelectedIndexChanged += cmbTables_SelectedIndexChanged;
            if (userRole == "admins" || userRole == "metodists" || userRole == "professors")
            {
                LoadStudentsData();
            }
        }
        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = cmbTables.SelectedItem.ToString();
            LoadTableData(selectedTable);
        }
        private void LoadTableData(string tableName)
        {
            string query = $"SELECT * FROM [{tableName}]"; // Добавляем квадратные скобки
            string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewStudents.DataSource = dataTable;
                    dataGridViewStudents.AllowUserToAddRows = true;  // Разрешаем добавление строк
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных из таблицы {tableName}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            // Скрываем MainForm, но не закрываем приложение
            this.Hide();

            // Показываем Form1
            Form1 form1 = new Form1();
            form1.Show();
        }

        // Метод для обновления приветственного сообщения
        private void UpdateWelcomeMessage()
        {
            // Получаем имя пользователя, привязанное к логину
            string fullName = GetUserFullName(login, password);

            // Обновляем текст приветствия
            lblWelcomeMessage.Text = $"Добро пожаловать, {fullName}!\nВаша роль в системе: {userRole}";
        }
        private string GetUserFullName(string login, string password)
        {
            string fullName = "Неизвестный пользователь";  // Значение по умолчанию

            // Строка подключения с использованием логина и пароля
            string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Запрос для получения имени пользователя, связанного с логином
                    string query = @"
                SELECT dp.name 
                FROM sys.database_principals dp
                WHERE dp.type = 'S' AND dp.sid = SUSER_SID(@login)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@login", login);

                    connection.Open();
                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        fullName = result.ToString();  // Если имя найдено
                    }
                    else
                    {
                        MessageBox.Show("Имя пользователя не найдено в базе данных.", "Диагностика", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            return fullName;
        }

        // Метод получения полного имени пользователя
        private void DisplayStudents(DataTable dt, string userRole)
        {
            dataGridView.DataSource = dt;

            // Устанавливаем доступность редактирования на основе роли
            if (userRole == "students")
            {
                dataGridView.ReadOnly = true; // Только просмотр
            }
            else
            {
                dataGridView.ReadOnly = false; // Остальные роли могут редактировать
            }

            // Исключаем ненужные столбцы
            if (dataGridView.Columns.Contains("ImageColumn"))
            {
                dataGridView.Columns["ImageColumn"].Visible = false;
            }
        }
        private void LoadStudentsData()
        {
            string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[student]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewStudents.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных студентов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Загрузка данных, когда форма загружается
            LoadData();
            btnAddUser.Visible = userRole == "admins";
            btnDeleteUser.Visible = userRole == "admins";
            btnViewAllUsers.Visible = userRole == "admins";
            btnSetBadGrades.Visible = userRole == "professors" || userRole == "admins";
            btnAddAttendanceAndGrade.Visible = userRole == "professors" || userRole == "admins" || userRole == "metodists";
            btnDeleteGrade.Visible = userRole == "professors" || userRole == "admins" || userRole == "metodists";
            btnEditScheduleTable.Visible = userRole == "admins" || userRole == "metodists" || userRole == "professors";
            dataGridViewStudents.Visible = userRole == "admins" || userRole == "metodists" || userRole == "professors";
            dataGridViewStudents.AllowUserToAddRows = true;
            cmbTables.Visible = userRole == "professors" || userRole == "admins" || userRole == "metodists";
            btnAddNewStudent.Visible = userRole == "admins" || userRole == "metodists" || userRole == "professors";
            btnEditSchedule1.Visible = userRole == "admins" || userRole == "metodists";

        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если MainForm закрывается через крестик, завершаем приложение
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit(); // Полное завершение приложения
            }
        }
        // Метод для загрузки данных в DataGridView
        private void LoadData()
        {
            // Строка подключения к базе данных
            string connectionString = @"Server=PERSONALCOMPUTE;Database=university_system;Integrated Security=True;";
            string query = "SELECT * FROM audience";  // Пример запроса, замените на нужный

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Привязка данных к DataGridView
                dataGridView.DataSource = dataTable;
            }
        }
        private void btnViewStudent_Click(object sender, EventArgs e)
        {
            try
            {
                // Строка подключения к базе данных
                string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Новый SQL-запрос
                    string query = @"
                SELECT 
                    s.id,
                    s.student_fio,
                    s.gender,
                    s.birthday_date,
                    g.group_number AS group_name,  -- Номер группы из таблицы group
                    s.group_number AS numer_of_group,  -- Номер группы из таблицы student
                    s.phone_number
                FROM 
                    [dbo].[student] s
                JOIN 
                    [dbo].[group] g ON s.group_id = g.id;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Заполняем DataTable данными из запроса
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Отображаем результат в DataGridView
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Произошла ошибка при выполнении запроса:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private DataTable ExecuteQuery(string query)
        {
            // Строка подключения к базе данных
            string connectionString = "Server=PERSONALCOMPUTE;Database=university_system;Integrated Security=True;";
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dt);
            }

            return dt;
        }
        private void lblStudentFio_Click(object sender, EventArgs e)
        {
            // Логика для обработки клика по метке (например, вывести сообщение)
            MessageBox.Show("Вы кликнули на метку с ФИО студента.");
        }
        private void BtnCalculateAvgGrade_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем ФИО студента из текстового поля
                string studentFio = txtStudentFio.Text.Trim();

                if (string.IsNullOrEmpty(studentFio))
                {
                    MessageBox.Show("Пожалуйста, введите ФИО студента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection("Server=PERSONALCOMPUTE;Database=university_system;Trusted_Connection=True;"))
                {
                    connection.Open();

                    // Выполнение хранимой процедуры
                    using (SqlCommand command = new SqlCommand("AVG_GRADE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Передаем параметр с правильным именем
                        command.Parameters.AddWithValue("@student", studentFio);

                        // Получаем результат
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            MessageBox.Show($"Средняя оценка для студента '{studentFio}': {result}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Студент с ФИО '{studentFio}' не найден.", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Произошла ошибка при выполнении запроса:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Обработчик кнопки для просмотра таблицы audience
        private void BtnViewAudience_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM audience";
            LoadData(query);
        }

        // Обработчик кнопки для просмотра таблицы group
        private void btnViewGroup_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM [Group]"; // Замените на имя вашей таблицы групп
            DataTable dt = ExecuteQuery(query);     // Выполняем запрос
            DisplayGroups(dt);                      // Выводим данные в DataGridView
        }
        private void DisplayGroups(DataTable dt)
        {
            dataGridView.DataSource = dt;

            // Скрываем ненужные столбцы, если они есть
            if (dataGridView.Columns.Contains("SomeColumn")) // Укажите название лишнего столбца
            {
                dataGridView.Columns["SomeColumn"].Visible = false;
            }
        }
        // Обработчик кнопки для просмотра таблицы professor
        private void BtnViewProfessor_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM professor";  // Замените на нужный запрос для таблицы professor
            LoadData(query);
        }

        // Обработчик кнопки для просмотра таблицы lesson
        private void BtnViewLesson_Click(object sender, EventArgs e)
        {
            try
            {
                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection("Server=PERSONALCOMPUTE;Database=university_system;Trusted_Connection=True;"))
                {
                    connection.Open();

                    // SQL-запрос для получения всех предметов
                    string query = "SELECT * FROM [dbo].[subject]";

                    // Выполнение запроса
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Отображение результата в DataGridView
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Произошла ошибка при выполнении запроса:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик кнопки для просмотра таблицы attendance
        private void BtnViewAttendance_Click(object sender, EventArgs e)
        {
            try
            {
                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection("Server=PERSONALCOMPUTE;Database=university_system;Trusted_Connection=True;"))
                {
                    connection.Open();

                    // SQL-запрос
                    string query = @"
                SELECT 
                    a.id AS attendance_id,
                    s.student_fio AS student_name,
                    g.group_number AS group_number,
                    subj.subject_name AS subject,
                    prof.professor_fio AS professor,
                    l.lesson_date AS lesson_date,
                    l.start_time AS start_time,
                    l.end_time AS end_time,
                    CASE a.audit WHEN 1 THEN 'Присутствовал' ELSE 'Отсутствовал' END AS attendance_status
                FROM 
                    [university_system].[dbo].[attendance] a
                JOIN 
                    [university_system].[dbo].[student] s ON a.student_id = s.id
                JOIN 
                    [university_system].[dbo].[group] g ON s.group_id = g.id
                JOIN 
                    [university_system].[dbo].[lesson] l ON a.lesson_id = l.id
                JOIN 
                    [university_system].[dbo].[subject] subj ON l.subject_id = subj.id
                JOIN 
                    [university_system].[dbo].[professor] prof ON l.professor_id = prof.id
                JOIN 
                    [university_system].[dbo].[audience] aud ON l.audience_id = aud.id;";

                    // Выполнение запроса
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Отображение результата в DataGridView
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Произошла ошибка при выполнении запроса:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Обработчик кнопки для просмотра таблицы grade
        private void btnViewGrades_Click(object sender, EventArgs e)
        {
            try
            {
                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection("Server=PERSONALCOMPUTE;Database=university_system;Trusted_Connection=True;"))
                {
                    connection.Open();

                    // SQL-запрос
                    string query = @"
               SELECT 
                    g.id AS grade_id,
                    s.student_fio AS student_name,
                    gr.group_number AS group_name,
                    subj.subject_name AS subject,
                    prof.professor_fio AS professor,
                    l.lesson_date AS lesson_date,
                    l.start_time AS start_time,
                    l.end_time AS end_time,
                    g.grade_num AS grade
                FROM 
                    [university_system].[dbo].[grade] g
                JOIN 
                    [university_system].[dbo].[attendance] a ON g.attendance_id = a.id
                JOIN 
                    [university_system].[dbo].[student] s ON a.student_id = s.id
                JOIN 
                    [university_system].[dbo].[group] gr ON s.group_id = gr.id
                JOIN 
                    [university_system].[dbo].[lesson] l ON a.lesson_id = l.id
                JOIN 
                    [university_system].[dbo].[subject] subj ON l.subject_id = subj.id
                JOIN 
                    [university_system].[dbo].[professor] prof ON l.professor_id = prof.id
                JOIN 
                    [university_system].[dbo].[audience] aud ON l.audience_id = aud.id;";

                    // Выполнение запроса
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Отображение результата в DataGridView
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Произошла ошибка при выполнении запроса:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Button btnViewGradesByFio;

        public MainForm()
        {
            InitializeComponent();

            this.btnViewGradesByFio = new Button();  // Инициализация кнопки
            this.btnViewGradesByFio.Text = "Просмотр оценок по ФИО";
            this.btnViewGradesByFio.Location = new System.Drawing.Point(100, 100);  // Задание местоположения
            this.btnViewGradesByFio.Size = new System.Drawing.Size(200, 30);  // Задание размера
            this.btnViewGradesByFio.Click += new System.EventHandler(this.btnViewGradesByFio_Click);  // Привязка обработчика события
            this.btnViewAttendanceByFio.Click += new System.EventHandler(this.btnViewAttendanceByFio_Click);
            this.Controls.Add(this.btnViewGradesByFio);  // Добавление кнопки на форму
            this.btnSetBadGrades.Click += new System.EventHandler(this.btnSetBadGrades_Click);
            

        }


        // Обработчик кнопки для расчета средней оценки
        private void BtnAvgGrade_Click(object sender, EventArgs e)
        {
            // Реализуйте логику вызова процедуры для расчета средней оценки
        }

        // Обработчик кнопки для показа расписания преподавателя
        private void BtnProfessorLessons_Click(object sender, EventArgs e)
        {
            // Реализуйте логику вызова процедуры для показа расписания преподавателя
        }
        private void GetGradesByFio(string studentFio)
        {
            try
            {
                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection("Server=PERSONALCOMPUTE;Database=university_system;Trusted_Connection=True;"))
                {
                    connection.Open();

                    // SQL-запрос с фильтрацией по ФИО
                    string query = @"
            SELECT 
                g.id AS grade_id,
                s.student_fio AS student_name,
                gr.group_number AS group_number,
                subj.subject_name AS subject,
                prof.professor_fio AS professor,
                aud.audience_number AS audience,
                l.lesson_date AS lesson_date,
                l.start_time AS start_time,
                l.end_time AS end_time,
                CASE a.audit WHEN 1 THEN 'Присутствовал' ELSE 'Отсутствовал' END AS attendance_status,
                g.grade_num AS grade
            FROM 
                [university_system].[dbo].[grade] g
            JOIN 
                [university_system].[dbo].[attendance] a ON g.attendance_id = a.id
            JOIN 
                [university_system].[dbo].[student] s ON a.student_id = s.id
            JOIN 
                [university_system].[dbo].[group] gr ON s.group_id = gr.id
            JOIN 
                [university_system].[dbo].[lesson] l ON a.lesson_id = l.id
            JOIN 
                [university_system].[dbo].[subject] subj ON l.subject_id = subj.id
            JOIN 
                [university_system].[dbo].[professor] prof ON l.professor_id = prof.id
            JOIN 
                [university_system].[dbo].[audience] aud ON l.audience_id = aud.id
            WHERE s.student_fio LIKE @studentFio;";

                    // Выполнение запроса
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentFio", "%" + studentFio + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Отображение результата в DataGridView
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при выполнении запроса:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnViewGradesByFio_Click(object sender, EventArgs e)
        {
            try
            {
                // Показываем окно ввода ФИО
                string studentFio = Microsoft.VisualBasic.Interaction.InputBox("Введите ФИО студента, оценки которого хотите посмотреть", "Ввод ФИО", "");

                // Проверяем, если пользователь нажал "Отмена" (null) или оставил поле пустым
                if (studentFio == null || string.IsNullOrWhiteSpace(studentFio))
                {
                    return; // Просто выходим, если ФИО не введено
                }

                // Выполняем запрос для получения оценок по введенному ФИО
                GetGradesByFio(studentFio);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Обработчик нажатия на ячейку в DataGridView
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Добавьте необходимую логику для обработки кликов на ячейки
        }

        // Метод для загрузки данных в DataGridView, принимает SQL-запрос как параметр
        private void LoadData(string query)
        {
            // Строка подключения к базе данных
            string connectionString = @"Server=PERSONALCOMPUTE;Database=university_system;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Привязка данных к DataGridView
                dataGridView.DataSource = dataTable;
            }
        }

        private void BtnShowProfessorSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем имя преподавателя из текстового поля
                string professorFio = txtProfessorFio.Text.Trim();

                if (string.IsNullOrEmpty(professorFio))
                {
                    MessageBox.Show("Пожалуйста, введите ФИО преподавателя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection("Server=PERSONALCOMPUTE;Database=university_system;Trusted_Connection=True;"))
                {
                    connection.Open();

                    // Выполнение хранимой процедуры
                    using (SqlCommand command = new SqlCommand("Professor_lessons", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Передаем параметр в хранимую процедуру
                        command.Parameters.AddWithValue("@professor", professorFio);

                        // Чтение данных с использованием SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Создаем DataTable для хранения результата
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);

                                // Отображаем данные в DataGridView
                                dataGridView.DataSource = dataTable;
                            }
                            else
                            {
                                MessageBox.Show($"Для преподавателя '{professorFio}' расписание не найдено.", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Произошла ошибка при выполнении запроса:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewAttendanceByFio_Click(object sender, EventArgs e)
        {
            try
            {
                // Показываем окно ввода ФИО
                string studentFio = Microsoft.VisualBasic.Interaction.InputBox("Введите ФИО студента, посещаемость которого хотите посмотреть", "Ввод ФИО", "");

                // Проверяем, если пользователь нажал "Отмена" или оставил поле пустым
                if (studentFio == null || string.IsNullOrWhiteSpace(studentFio))
                {
                    return; // Выход из метода, если ничего не введено
                }

                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection("Server=PERSONALCOMPUTE;Database=university_system;Trusted_Connection=True;"))
                {
                    connection.Open();

                    // SQL-запрос с фильтрацией по ФИО
                    string query = @"
                SELECT 
                    a.id AS attendance_id,
                    s.student_fio AS student_name,
                    g.group_number AS group_number,
                    subj.subject_name AS subject,
                    prof.professor_fio AS professor,
                    aud.audience_number AS audience,
                    l.lesson_date AS lesson_date,
                    l.start_time AS start_time,
                    l.end_time AS end_time,
                    CASE a.audit WHEN 1 THEN 'Присутствовал' ELSE 'Отсутствовал' END AS attendance_status
                FROM 
                    [university_system].[dbo].[attendance] a
                JOIN 
                    [university_system].[dbo].[student] s ON a.student_id = s.id
                JOIN 
                    [university_system].[dbo].[group] g ON s.group_id = g.id
                JOIN 
                    [university_system].[dbo].[lesson] l ON a.lesson_id = l.id
                JOIN 
                    [university_system].[dbo].[subject] subj ON l.subject_id = subj.id
                JOIN 
                    [university_system].[dbo].[professor] prof ON l.professor_id = prof.id
                JOIN 
                    [university_system].[dbo].[audience] aud ON l.audience_id = aud.id
                WHERE 
                    s.student_fio = @StudentFio;";

                    // Выполнение запроса
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentFio", studentFio);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Отображение результата в DataGridView
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Произошла ошибка:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetBadGrades_Click(object sender, EventArgs e)
        {
            // Проверяем роль пользователя
            if (userRole != "professors" && userRole != "admins")
            {
                MessageBox.Show("У вас недостаточно прав для выполнения этого действия.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Открываем InputBox для ввода данных
            string subjectName = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите название предмета, по которому хотите выставить оценки студентам, пропустившим занятия:",
                "Выставить оценки",
                "");

            // Проверяем, если пользователь не ввёл данные
            if (string.IsNullOrWhiteSpace(subjectName))
            {
                MessageBox.Show("Вы не ввели название предмета.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection($"Server=PERSONALCOMPUTE;Database=university_system;User Id=admin_university;Password=adm1;"))
                {
                    connection.Open();

                    // Выполнение хранимой процедуры
                    using (SqlCommand command = new SqlCommand("SetGradeForMissedClasses", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Передаем параметр с названием предмета
                        command.Parameters.AddWithValue("@SubjectName", subjectName);

                        // Выполняем запрос и загружаем результат в DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Отображаем данные в DataGridView
                        dataGridView.DataSource = dataTable;

                        MessageBox.Show("Оценки успешно выставлены. Данные обновлены в таблице.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка при выполнении SQL-запроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void lblWelcomeMessage_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnShowLessonsByGroup_Click(object sender, EventArgs e)
        {
            // Открываем InputBox для ввода названия группы
            string groupName = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите название группы",
                "Расписание по группам",
                "");

            // Проверяем, ввел ли пользователь данные
            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show("Вы не ввели название группы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Строка подключения к базе данных
                string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Выполняем хранимую процедуру
                    string query = "EXEC GetLessonsByGroupNumber @GroupName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GroupName", groupName);

                        // Читаем результат выполнения
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);

                                // Выводим результат в DataGridView
                                dataGridView.DataSource = dataTable;
                            }
                            else
                            {
                                MessageBox.Show("Расписание для данной группы не найдено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при выполнении запроса:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeRoleBasedControls() //проверка роли для кнопки "Редактирование оценок"
        {
            // Кнопка будет видима только для admins, professors, metodists
            if (userRole == "admins" || userRole == "professors" || userRole == "metodists")
            {
                btnEditGrades.Visible = true;
                btnEditGrades.Click += btnEditGrades_Click;
            }
            else
            {
                btnEditGrades.Visible = false; // Скрываем кнопку для остальных
            }
            btnSaveStudentChanges.Click += btnSaveStudentChanges_Click;
            btnDeleteStudentRecord.Click += btnDeleteStudentRecord_Click;
            btnSaveStudentChanges.Visible = userRole == "admins" || userRole == "metodists" || userRole == "professors";
            btnDeleteStudentRecord.Visible = userRole == "admins" || userRole == "metodists" || userRole == "professors";

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Проверка роли пользователя
            if (userRole != "admins")
            {
                MessageBox.Show("У вас недостаточно прав для выполнения этого действия.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Ввод данных через InputBox
                string login = Microsoft.VisualBasic.Interaction.InputBox("Введите логин пользователя:", "Добавление пользователя", "");
                if (string.IsNullOrWhiteSpace(login)) return;

                string password = Microsoft.VisualBasic.Interaction.InputBox("Введите пароль пользователя:", "Добавление пользователя", "");
                if (string.IsNullOrWhiteSpace(password)) return;

                string name = Microsoft.VisualBasic.Interaction.InputBox("Введите имя пользователя:", "Добавление пользователя", "");
                if (string.IsNullOrWhiteSpace(name)) return;

                string role = Microsoft.VisualBasic.Interaction.InputBox("Введите роль пользователя:", "Добавление пользователя", "");
                if (string.IsNullOrWhiteSpace(role)) return;

                // Используем логин и пароль администратора
                string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id=admin_university;Password=adm1;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Вызов хранимой процедуры AddLoginAndUser
                    using (SqlCommand command = new SqlCommand("AddLoginAndUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Передаем параметры процедуры
                        command.Parameters.AddWithValue("@LoginName", login);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@UserName", name);
                        command.Parameters.AddWithValue("@RoleName", role);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Пользователь успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка SQL: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e) //кнопка удаления пользователей для админов
        {
            // Показываем InputBox для ввода логина
            string userLogin = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите логин пользователя, которого хотите удалить:",
                "Удаление пользователя",
                "");

            // Проверяем, введены ли данные
            if (string.IsNullOrWhiteSpace(userLogin))
            {
                MessageBox.Show("Вы не ввели логин пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Подключение к базе данных с правами администратора
                using (SqlConnection connection = new SqlConnection($"Server=PERSONALCOMPUTE;Database=university_system;User Id=admin_university;Password=adm1;"))
                {
                    connection.Open();

                    // Выполнение хранимой процедуры
                    using (SqlCommand command = new SqlCommand("RemoveLoginAndUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LoginName", userLogin);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Пользователь '{userLogin}' успешно удалён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewAllUsers_Click(object sender, EventArgs e)
        {
            try
            {
                // Подключение к базе данных с правами администратора
                using (SqlConnection connection = new SqlConnection($"Server=PERSONALCOMPUTE;Database=university_system;User Id=admin_university;Password=adm1;"))
                {
                    connection.Open();

                    // SQL-запрос для получения всех пользователей
                    string query = @"
            SELECT 
                sp.name AS LoginName,               -- Имя для входа
                dp.name AS UserName,                -- Имя пользователя в базе данных
                pr.name AS RoleName                -- Роль пользователя
            FROM 
                sys.server_principals sp           -- Логины на сервере
            JOIN 
                sys.database_principals dp         -- Пользователи базы данных
                ON sp.sid = dp.sid                 -- Соединяем по SID (идентификатору безопасности)
            JOIN 
                sys.database_role_members drm     -- Члены ролей в базе данных
                ON dp.principal_id = drm.member_principal_id
            JOIN 
                sys.database_principals pr        -- Роли в базе данных
                ON drm.role_principal_id = pr.principal_id
            WHERE 
                dp.type IN ('S', 'U')              -- Фильтруем только пользователей (не групп и не системные объекты)
            ORDER BY 
                LoginName, UserName, RoleName;";

                    // Выполнение запроса и загрузка данных в DataTable
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Привязка данных к DataGridView
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка SQL: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveChangesToDatabase() //сохранения изменений из DataGridView. В файле MainForm.cs
        {
            try
            {
                // Подключение к базе данных
                string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Формируем команду для сохранения изменений
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    DataTable changes = ((DataTable)dataGridView.DataSource).GetChanges();

                    if (changes != null)
                    {
                        adapter.UpdateCommand = builder.GetUpdateCommand();
                        adapter.InsertCommand = builder.GetInsertCommand();
                        adapter.DeleteCommand = builder.GetDeleteCommand();

                        // Применяем изменения к базе данных
                        adapter.Update(changes);
                        MessageBox.Show("Изменения успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEditGrades_Click(object sender, EventArgs e)
        {
            try
            {
                // Подключение к базе данных
                string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для объединенной таблицы
                    string query = @"
                SELECT 
                    l.id AS lesson_id, -- ID занятия
                    s.id AS student_id, -- ID студента
                    s.student_fio, -- ФИО студента
                    g.group_number AS group_number, -- Группа из таблицы group через student
                    subj.subject_name, -- Название предмета
                    prof.professor_fio, -- Преподаватель
                    l.lesson_date, -- Дата занятия
                    l.start_time, -- Время начала
                    l.end_time, -- Время окончания
                    a.audit, -- Посещаемость
                    gr.grade_num -- Оценка
                FROM 
                    [dbo].[lesson] l
                LEFT JOIN 
                    [dbo].[attendance] a ON l.id = a.lesson_id -- Все строки из lesson
                LEFT JOIN 
                    [dbo].[student] s ON a.student_id = s.id -- Соединение с student через attendance
                LEFT JOIN 
                    [dbo].[group] g ON s.group_id = g.id -- Соединение group через student
                LEFT JOIN 
                    [dbo].[subject] subj ON l.subject_id = subj.id -- Соединение с subject
                LEFT JOIN 
                    [dbo].[professor] prof ON l.professor_id = prof.id -- Соединение с professor
                LEFT JOIN 
                    [dbo].[grade] gr ON a.id = gr.attendance_id -- Соединение с grade через attendance
                ORDER BY 
                    l.lesson_date, l.start_time;";

                    // Загрузка данных в DataGridView
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView.DataSource = table;
                    dataGridView.ReadOnly = false; // Разрешаем редактирование
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddAttendanceAndGrade_Click(object sender, EventArgs e)
        {
            try
            {
                // Чтение данных через InputBox
                string lessonId = Interaction.InputBox("Укажите lesson_id", "Ввод данных", "");
                if (string.IsNullOrWhiteSpace(lessonId)) return;

                string studentId = Interaction.InputBox("Укажите student_id", "Ввод данных", "");
                if (string.IsNullOrWhiteSpace(studentId)) return;

                string attendanceValue = Interaction.InputBox("Укажите значение посещаемости 0 или 1(Поле не должно быть пустым)", "Ввод данных", "");
                if (string.IsNullOrWhiteSpace(attendanceValue)) return;

                string gradeValue = Interaction.InputBox("Укажите оценку", "Ввод данных", "");
                if (string.IsNullOrWhiteSpace(gradeValue)) return;

                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection($"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};"))
                {
                    connection.Open();
                    // Запрос на выполнение хранимой процедуры
                    using (SqlCommand command = new SqlCommand("AddGradeForStudentsss", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@lesson_id", lessonId);
                        command.Parameters.AddWithValue("@student_id", studentId);
                        command.Parameters.AddWithValue("@audit", attendanceValue);
                        command.Parameters.AddWithValue("@grade_num", gradeValue);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка SQL: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteGrade_Click(object sender, EventArgs e)
        {
            try
            {
                // Ввод данных через InputBox
                string lessonIdInput = Microsoft.VisualBasic.Interaction.InputBox("Укажите lesson_id:", "Удаление оценки", "");
                if (string.IsNullOrWhiteSpace(lessonIdInput)) return;

                string studentIdInput = Microsoft.VisualBasic.Interaction.InputBox("Укажите student_id:", "Удаление оценки", "");
                if (string.IsNullOrWhiteSpace(studentIdInput)) return;

                int lessonId = int.Parse(lessonIdInput);
                int studentId = int.Parse(studentIdInput);

                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection($"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};"))
                {
                    connection.Open();

                    // Выполнение хранимой процедуры DeleteGrade
                    using (SqlCommand command = new SqlCommand("DeleteGrade", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@lesson_id", lessonId);
                        command.Parameters.AddWithValue("@student_id", studentId);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Оценка успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введены некорректные данные. Убедитесь, что lesson_id и student_id — числа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditScheduleTable_Click(object sender, EventArgs e)
        {
            try
            {
                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection($"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};"))
                {
                    connection.Open();

                    // SQL-запрос
                    string query = @"
                SELECT 
                    l.id AS lesson_id,
                    l.subject_id,
                    s.subject_name,
                    l.group_id, -- Берём group_id напрямую из таблицы lesson
                    g.group_number AS group_name, -- Сопоставляем group_number через group_id
                    p.id AS professor_id,
                    p.professor_fio,
                    l.lesson_date,
                    l.start_time,
                    l.end_time,
                    a.id AS audience_id,
                    a.audience_number
                FROM 
                    [university_system].[dbo].[lesson] l
                LEFT JOIN 
                    [university_system].[dbo].[subject] s ON l.subject_id = s.id
                LEFT JOIN 
                    [university_system].[dbo].[professor] p ON l.professor_id = p.id
                LEFT JOIN 
                    [university_system].[dbo].[audience] a ON l.audience_id = a.id
                LEFT JOIN 
                    [university_system].[dbo].[group] g ON l.group_id = g.id -- Прямая связь с таблицей group
                ORDER BY 
                    l.lesson_date, l.start_time;";

                    // Выполнение запроса
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязка данных к DataGridView
                    dataGridView.DataSource = dataTable;
                    dataGridView.AutoGenerateColumns = true; // Разрешаем авто-генерацию столбцов

                    // Изменяем порядок столбцов
                    ReorderColumns();  // Перемещаем столбец audience_number в конец
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReorderColumns()
        {
            // Перемещаем столбец audience_number в конец
            if (dataGridView.Columns.Contains("audience_number"))
            {
                int audienceIndex = dataGridView.Columns["audience_number"].DisplayIndex;
                dataGridView.Columns["audience_number"].DisplayIndex = dataGridView.Columns.Count - 1;
            }
        }

        private void btnSaveStudentChanges_Click(object sender, EventArgs e)
        {
            
            {
                // Проверяем, есть ли выбранная таблица
                if (cmbTables.SelectedItem == null)
                {
                    
                    return;
                }

                string tableName = cmbTables.SelectedItem.ToString();
                string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                    // Получаем изменения из DataTable
                    DataTable changes = ((DataTable)dataGridViewStudents.DataSource).GetChanges();

                    if (changes != null)
                    {
                        // Обновляем базу данных
                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        adapter.InsertCommand = commandBuilder.GetInsertCommand();
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                        adapter.Update(changes);

                        // Применяем изменения к локальной таблице
                        ((DataTable)dataGridViewStudents.DataSource).AcceptChanges();

                        MessageBox.Show("Изменения успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void btnSaveGradeChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = $"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                    DataTable changes = ((DataTable)dataGridView.DataSource).GetChanges();
                    if (changes != null)
                    {
                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        adapter.InsertCommand = commandBuilder.GetInsertCommand();
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                        adapter.Update(changes);
                        ((DataTable)dataGridView.DataSource).AcceptChanges();

                        MessageBox.Show("Изменения успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeleteStudentRecord_Click(object sender, EventArgs e)
        {
            
            {
                // Проверяем, есть ли выбранная строка для удаления
                if (dataGridViewStudents.SelectedRows.Count == 0)
                {
                    return;
                }

                // Удаляем выбранные строки из DataGridView
                foreach (DataGridViewRow row in dataGridViewStudents.SelectedRows)
                {
                    // Убедимся, что строка не новая
                    if (!row.IsNewRow)
                    {
                        dataGridViewStudents.Rows.Remove(row);
                    }
                }
            }
        }
        private void btnAddNewStudent_Click(object sender, EventArgs e)
        {
            
            {
                // Проверяем, есть ли выбранная таблица
                if (cmbTables.SelectedItem == null)
                {
                    
                    return;
                }

                // Получаем текущую таблицу из DataGridView
                DataTable dataTable = (DataTable)dataGridViewStudents.DataSource;

                if (dataTable == null)
                {
                    MessageBox.Show("Данные таблицы не загружены. Сначала выберите таблицу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Создаем новую строку
                DataRow newRow = dataTable.NewRow();
                dataTable.Rows.Add(newRow);

                // Устанавливаем фокус на новую строку
                dataGridViewStudents.CurrentCell = dataGridViewStudents.Rows[dataGridViewStudents.Rows.Count - 1].Cells[0];
                dataGridViewStudents.BeginEdit(true);
            }
        }

        private void dataGridViewStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblProfessorFIO_Click(object sender, EventArgs e)
        {

        }

        private void cmbTables_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnEditSchedule1_Click(object sender, EventArgs e)
        {
            try
            {
                // Последовательный ввод данных через InputBox
                string subjectId = Microsoft.VisualBasic.Interaction.InputBox("Укажите subject_id", "Ввод данных", "");
                if (string.IsNullOrWhiteSpace(subjectId)) return;

                string groupId = Microsoft.VisualBasic.Interaction.InputBox("Укажите group_id", "Ввод данных", "");
                if (string.IsNullOrWhiteSpace(groupId)) return;

                string professorId = Microsoft.VisualBasic.Interaction.InputBox("Укажите professor_id", "Ввод данных", "");
                if (string.IsNullOrWhiteSpace(professorId)) return;

                string lessonDate = Microsoft.VisualBasic.Interaction.InputBox("Укажите дату (в формате yyyy-MM-dd)", "Ввод данных", "");
                if (!DateTime.TryParse(lessonDate, out DateTime parsedLessonDate))
                {
                    MessageBox.Show("Некорректный формат даты. Используйте формат yyyy-MM-dd.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string startTime = Microsoft.VisualBasic.Interaction.InputBox("Укажите start_time (в формате HH:mm:ss)", "Ввод данных", "");
                if (!TimeSpan.TryParse(startTime, out TimeSpan parsedStartTime))
                {
                    MessageBox.Show("Некорректный формат времени. Используйте формат HH:mm:ss.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string endTime = Microsoft.VisualBasic.Interaction.InputBox("Укажите end_time (в формате HH:mm:ss)", "Ввод данных", "");
                if (!TimeSpan.TryParse(endTime, out TimeSpan parsedEndTime))
                {
                    MessageBox.Show("Некорректный формат времени. Используйте формат HH:mm:ss.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string audienceId = Microsoft.VisualBasic.Interaction.InputBox("Укажите audience_id", "Ввод данных", "");
                if (string.IsNullOrWhiteSpace(audienceId)) return;

                // Подключение к базе данных
                using (SqlConnection connection = new SqlConnection($"Server=PERSONALCOMPUTE;Database=university_system;User Id={login};Password={password};"))
                {
                    connection.Open();

                    // Выполнение хранимой процедуры
                    using (SqlCommand command = new SqlCommand("AddLesson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Передача параметров в хранимую процедуру
                        command.Parameters.AddWithValue("@subject_id", subjectId);
                        command.Parameters.AddWithValue("@group_id", groupId);
                        command.Parameters.AddWithValue("@professor_id", professorId);
                        command.Parameters.AddWithValue("@lesson_date", parsedLessonDate); // Используем проверенный DateTime
                        command.Parameters.AddWithValue("@start_time", parsedStartTime);  // Используем проверенный TimeSpan
                        command.Parameters.AddWithValue("@end_time", parsedEndTime);      // Используем проверенный TimeSpan
                        command.Parameters.AddWithValue("@audience_id", audienceId);

                        // Выполнение команды
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно добавлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка SQL: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


