
namespace UniversityManagementApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnViewGradesByFio = new System.Windows.Forms.Button();
            this.lblStudentFio = new System.Windows.Forms.Label();
            this.txtStudentFio = new System.Windows.Forms.TextBox();
            this.btnAvgGrade = new System.Windows.Forms.Button();
            this.lblProfessorFIO = new System.Windows.Forms.Label();
            this.txtProfessorFio = new System.Windows.Forms.TextBox();
            this.btnProfessorLessons = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btnViewAudience = new System.Windows.Forms.Button();
            this.btnViewGroup = new System.Windows.Forms.Button();
            this.btnViewProfessor = new System.Windows.Forms.Button();
            this.btnViewLesson = new System.Windows.Forms.Button();
            this.btnViewAttendance = new System.Windows.Forms.Button();
            this.btnViewGrade = new System.Windows.Forms.Button();
            this.btnViewStudent = new System.Windows.Forms.Button();
            this.btnViewAttendanceByFio = new System.Windows.Forms.Button();
            this.btnSetBadGrades = new System.Windows.Forms.Button();
            this.lblWelcomeMessage = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnShowLessonsByGroup = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnViewAllUsers = new System.Windows.Forms.Button();
            this.btnEditGrades = new System.Windows.Forms.Button();
            this.btnAddAttendanceAndGrade = new System.Windows.Forms.Button();
            this.btnDeleteGrade = new System.Windows.Forms.Button();
            this.btnEditScheduleTable = new System.Windows.Forms.Button();
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.btnSaveStudentChanges = new System.Windows.Forms.Button();
            this.btnDeleteStudentRecord = new System.Windows.Forms.Button();
            this.btnAddNewStudent = new System.Windows.Forms.Button();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.btnEditSchedule1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // btnViewGradesByFio
            // 
            this.btnViewGradesByFio.Location = new System.Drawing.Point(295, 73);
            this.btnViewGradesByFio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewGradesByFio.Name = "btnViewGradesByFio";
            this.btnViewGradesByFio.Size = new System.Drawing.Size(180, 76);
            this.btnViewGradesByFio.TabIndex = 0;
            this.btnViewGradesByFio.Text = "Просмотр оценок по ФИО";
            this.btnViewGradesByFio.UseVisualStyleBackColor = true;
            this.btnViewGradesByFio.Click += new System.EventHandler(this.btnViewGradesByFio_Click);
            // 
            // lblStudentFio
            // 
            this.lblStudentFio.AutoSize = true;
            this.lblStudentFio.Location = new System.Drawing.Point(478, 73);
            this.lblStudentFio.Name = "lblStudentFio";
            this.lblStudentFio.Size = new System.Drawing.Size(169, 17);
            this.lblStudentFio.TabIndex = 0;
            this.lblStudentFio.Text = "Введите ФИО студента:";
            // 
            // txtStudentFio
            // 
            this.txtStudentFio.Location = new System.Drawing.Point(481, 94);
            this.txtStudentFio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStudentFio.Name = "txtStudentFio";
            this.txtStudentFio.Size = new System.Drawing.Size(348, 22);
            this.txtStudentFio.TabIndex = 1;
            // 
            // btnAvgGrade
            // 
            this.btnAvgGrade.Location = new System.Drawing.Point(481, 120);
            this.btnAvgGrade.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAvgGrade.Name = "btnAvgGrade";
            this.btnAvgGrade.Size = new System.Drawing.Size(348, 29);
            this.btnAvgGrade.TabIndex = 2;
            this.btnAvgGrade.Text = "Рассчитать среднюю оценку указанного студента";
            this.btnAvgGrade.UseVisualStyleBackColor = true;
            this.btnAvgGrade.Click += new System.EventHandler(this.BtnCalculateAvgGrade_Click);
            // 
            // lblProfessorFIO
            // 
            this.lblProfessorFIO.AutoSize = true;
            this.lblProfessorFIO.Location = new System.Drawing.Point(483, 231);
            this.lblProfessorFIO.Name = "lblProfessorFIO";
            this.lblProfessorFIO.Size = new System.Drawing.Size(211, 17);
            this.lblProfessorFIO.TabIndex = 3;
            this.lblProfessorFIO.Text = "Введите ФИО преподавателя:";
            this.lblProfessorFIO.Click += new System.EventHandler(this.lblProfessorFIO_Click);
            // 
            // txtProfessorFio
            // 
            this.txtProfessorFio.Location = new System.Drawing.Point(483, 250);
            this.txtProfessorFio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtProfessorFio.Name = "txtProfessorFio";
            this.txtProfessorFio.Size = new System.Drawing.Size(356, 22);
            this.txtProfessorFio.TabIndex = 4;
            // 
            // btnProfessorLessons
            // 
            this.btnProfessorLessons.Location = new System.Drawing.Point(481, 276);
            this.btnProfessorLessons.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProfessorLessons.Name = "btnProfessorLessons";
            this.btnProfessorLessons.Size = new System.Drawing.Size(358, 29);
            this.btnProfessorLessons.TabIndex = 5;
            this.btnProfessorLessons.Text = "Показать расписание указанного преподавателя";
            this.btnProfessorLessons.UseVisualStyleBackColor = true;
            this.btnProfessorLessons.Click += new System.EventHandler(this.BtnShowProfessorSchedule_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 353);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(1715, 633);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick_1);
            // 
            // btnViewAudience
            // 
            this.btnViewAudience.Location = new System.Drawing.Point(12, 45);
            this.btnViewAudience.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewAudience.Name = "btnViewAudience";
            this.btnViewAudience.Size = new System.Drawing.Size(237, 28);
            this.btnViewAudience.TabIndex = 7;
            this.btnViewAudience.Text = "Просмотр аудиторий";
            this.btnViewAudience.UseVisualStyleBackColor = true;
            this.btnViewAudience.Click += new System.EventHandler(this.BtnViewAudience_Click);
            // 
            // btnViewGroup
            // 
            this.btnViewGroup.Location = new System.Drawing.Point(12, 79);
            this.btnViewGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewGroup.Name = "btnViewGroup";
            this.btnViewGroup.Size = new System.Drawing.Size(237, 28);
            this.btnViewGroup.TabIndex = 8;
            this.btnViewGroup.Text = "Просмотр групп";
            this.btnViewGroup.UseVisualStyleBackColor = true;
            this.btnViewGroup.Click += new System.EventHandler(this.btnViewGroup_Click);
            // 
            // btnViewProfessor
            // 
            this.btnViewProfessor.Location = new System.Drawing.Point(12, 114);
            this.btnViewProfessor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewProfessor.Name = "btnViewProfessor";
            this.btnViewProfessor.Size = new System.Drawing.Size(237, 28);
            this.btnViewProfessor.TabIndex = 9;
            this.btnViewProfessor.Text = "Просмотр преподавателей";
            this.btnViewProfessor.UseVisualStyleBackColor = true;
            this.btnViewProfessor.Click += new System.EventHandler(this.BtnViewProfessor_Click);
            // 
            // btnViewLesson
            // 
            this.btnViewLesson.Location = new System.Drawing.Point(12, 147);
            this.btnViewLesson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewLesson.Name = "btnViewLesson";
            this.btnViewLesson.Size = new System.Drawing.Size(237, 33);
            this.btnViewLesson.TabIndex = 10;
            this.btnViewLesson.Text = "Просмотр предметов";
            this.btnViewLesson.UseVisualStyleBackColor = true;
            this.btnViewLesson.Click += new System.EventHandler(this.BtnViewLesson_Click);
            // 
            // btnViewAttendance
            // 
            this.btnViewAttendance.Location = new System.Drawing.Point(12, 186);
            this.btnViewAttendance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewAttendance.Name = "btnViewAttendance";
            this.btnViewAttendance.Size = new System.Drawing.Size(237, 31);
            this.btnViewAttendance.TabIndex = 11;
            this.btnViewAttendance.Text = "Просмотр посещаемости";
            this.btnViewAttendance.UseVisualStyleBackColor = true;
            this.btnViewAttendance.Click += new System.EventHandler(this.BtnViewAttendance_Click);
            // 
            // btnViewGrade
            // 
            this.btnViewGrade.Location = new System.Drawing.Point(12, 223);
            this.btnViewGrade.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewGrade.Name = "btnViewGrade";
            this.btnViewGrade.Size = new System.Drawing.Size(237, 30);
            this.btnViewGrade.TabIndex = 12;
            this.btnViewGrade.Text = "Просмотр оценок";
            this.btnViewGrade.UseVisualStyleBackColor = true;
            this.btnViewGrade.Click += new System.EventHandler(this.btnViewGrades_Click);
            // 
            // btnViewStudent
            // 
            this.btnViewStudent.Location = new System.Drawing.Point(12, 258);
            this.btnViewStudent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewStudent.Name = "btnViewStudent";
            this.btnViewStudent.Size = new System.Drawing.Size(237, 30);
            this.btnViewStudent.TabIndex = 13;
            this.btnViewStudent.Text = "Просмотр студентов";
            this.btnViewStudent.UseVisualStyleBackColor = true;
            this.btnViewStudent.Click += new System.EventHandler(this.btnViewStudent_Click);
            // 
            // btnViewAttendanceByFio
            // 
            this.btnViewAttendanceByFio.Location = new System.Drawing.Point(295, 153);
            this.btnViewAttendanceByFio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewAttendanceByFio.Name = "btnViewAttendanceByFio";
            this.btnViewAttendanceByFio.Size = new System.Drawing.Size(180, 76);
            this.btnViewAttendanceByFio.TabIndex = 14;
            this.btnViewAttendanceByFio.Text = "Просмотр посещаемости по ФИО";
            this.btnViewAttendanceByFio.UseVisualStyleBackColor = true;
            this.btnViewAttendanceByFio.Click += new System.EventHandler(this.btnViewAttendanceByFio_Click);
            // 
            // btnSetBadGrades
            // 
            this.btnSetBadGrades.Location = new System.Drawing.Point(1734, 526);
            this.btnSetBadGrades.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetBadGrades.Name = "btnSetBadGrades";
            this.btnSetBadGrades.Size = new System.Drawing.Size(179, 101);
            this.btnSetBadGrades.TabIndex = 15;
            this.btnSetBadGrades.Text = "Выставить оценку 2 студентам, которые пропустили конкретный предмет";
            this.btnSetBadGrades.UseVisualStyleBackColor = true;
            this.btnSetBadGrades.Click += new System.EventHandler(this.btnSetBadGrades_Click);
            // 
            // lblWelcomeMessage
            // 
            this.lblWelcomeMessage.AutoSize = true;
            this.lblWelcomeMessage.Location = new System.Drawing.Point(12, 9);
            this.lblWelcomeMessage.Name = "lblWelcomeMessage";
            this.lblWelcomeMessage.Size = new System.Drawing.Size(46, 17);
            this.lblWelcomeMessage.TabIndex = 17;
            this.lblWelcomeMessage.Text = "label1";
            this.lblWelcomeMessage.Click += new System.EventHandler(this.lblWelcomeMessage_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 292);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(237, 32);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "Выход из системы";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnShowLessonsByGroup
            // 
            this.btnShowLessonsByGroup.Location = new System.Drawing.Point(295, 234);
            this.btnShowLessonsByGroup.Name = "btnShowLessonsByGroup";
            this.btnShowLessonsByGroup.Size = new System.Drawing.Size(178, 71);
            this.btnShowLessonsByGroup.TabIndex = 19;
            this.btnShowLessonsByGroup.Text = "Просмотр расписания по группам";
            this.btnShowLessonsByGroup.UseVisualStyleBackColor = true;
            this.btnShowLessonsByGroup.Click += new System.EventHandler(this.btnShowLessonsByGroup_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(867, 241);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(232, 57);
            this.btnAddUser.TabIndex = 20;
            this.btnAddUser.Text = "Добавить пользователя в систему";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(867, 304);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(232, 43);
            this.btnDeleteUser.TabIndex = 21;
            this.btnDeleteUser.Text = "Удалить пользователя из системы";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnViewAllUsers
            // 
            this.btnViewAllUsers.Location = new System.Drawing.Point(867, 186);
            this.btnViewAllUsers.Name = "btnViewAllUsers";
            this.btnViewAllUsers.Size = new System.Drawing.Size(232, 52);
            this.btnViewAllUsers.TabIndex = 22;
            this.btnViewAllUsers.Text = "Просмотр пользователей системы";
            this.btnViewAllUsers.UseVisualStyleBackColor = true;
            this.btnViewAllUsers.Click += new System.EventHandler(this.btnViewAllUsers_Click);
            // 
            // btnEditGrades
            // 
            this.btnEditGrades.Location = new System.Drawing.Point(1733, 390);
            this.btnEditGrades.Name = "btnEditGrades";
            this.btnEditGrades.Size = new System.Drawing.Size(179, 66);
            this.btnEditGrades.TabIndex = 23;
            this.btnEditGrades.Text = "Таблица для редактирования успеваемотси";
            this.btnEditGrades.UseVisualStyleBackColor = true;
            this.btnEditGrades.Click += new System.EventHandler(this.btnEditGrades_Click);
            // 
            // btnAddAttendanceAndGrade
            // 
            this.btnAddAttendanceAndGrade.Location = new System.Drawing.Point(1734, 462);
            this.btnAddAttendanceAndGrade.Name = "btnAddAttendanceAndGrade";
            this.btnAddAttendanceAndGrade.Size = new System.Drawing.Size(179, 59);
            this.btnAddAttendanceAndGrade.TabIndex = 24;
            this.btnAddAttendanceAndGrade.Text = "Добавить(Изменить) оценку и отметку посещаемости";
            this.btnAddAttendanceAndGrade.UseVisualStyleBackColor = true;
            this.btnAddAttendanceAndGrade.Click += new System.EventHandler(this.btnAddAttendanceAndGrade_Click);
            // 
            // btnDeleteGrade
            // 
            this.btnDeleteGrade.Location = new System.Drawing.Point(1733, 632);
            this.btnDeleteGrade.Name = "btnDeleteGrade";
            this.btnDeleteGrade.Size = new System.Drawing.Size(179, 48);
            this.btnDeleteGrade.TabIndex = 25;
            this.btnDeleteGrade.Text = "Удалить оценку";
            this.btnDeleteGrade.UseVisualStyleBackColor = true;
            this.btnDeleteGrade.Click += new System.EventHandler(this.btnDeleteGrade_Click);
            // 
            // btnEditScheduleTable
            // 
            this.btnEditScheduleTable.Location = new System.Drawing.Point(1733, 761);
            this.btnEditScheduleTable.Name = "btnEditScheduleTable";
            this.btnEditScheduleTable.Size = new System.Drawing.Size(179, 61);
            this.btnEditScheduleTable.TabIndex = 26;
            this.btnEditScheduleTable.Text = "Таблица для редактирования расписания";
            this.btnEditScheduleTable.UseVisualStyleBackColor = true;
            this.btnEditScheduleTable.Click += new System.EventHandler(this.btnEditScheduleTable_Click);
            // 
            // dataGridViewStudents
            // 
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudents.Location = new System.Drawing.Point(1105, 3);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.RowHeadersWidth = 51;
            this.dataGridViewStudents.RowTemplate.Height = 24;
            this.dataGridViewStudents.Size = new System.Drawing.Size(812, 344);
            this.dataGridViewStudents.TabIndex = 27;
            this.dataGridViewStudents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStudents_CellContentClick);
            // 
            // btnSaveStudentChanges
            // 
            this.btnSaveStudentChanges.Location = new System.Drawing.Point(920, 36);
            this.btnSaveStudentChanges.Name = "btnSaveStudentChanges";
            this.btnSaveStudentChanges.Size = new System.Drawing.Size(179, 31);
            this.btnSaveStudentChanges.TabIndex = 28;
            this.btnSaveStudentChanges.Text = "Сохранить изменения";
            this.btnSaveStudentChanges.UseVisualStyleBackColor = true;
            this.btnSaveStudentChanges.Click += new System.EventHandler(this.btnSaveStudentChanges_Click);
            // 
            // btnDeleteStudentRecord
            // 
            this.btnDeleteStudentRecord.Location = new System.Drawing.Point(920, 108);
            this.btnDeleteStudentRecord.Name = "btnDeleteStudentRecord";
            this.btnDeleteStudentRecord.Size = new System.Drawing.Size(179, 29);
            this.btnDeleteStudentRecord.TabIndex = 29;
            this.btnDeleteStudentRecord.Text = "Удалить запись";
            this.btnDeleteStudentRecord.UseVisualStyleBackColor = true;
            this.btnDeleteStudentRecord.Click += new System.EventHandler(this.btnDeleteStudentRecord_Click);
            // 
            // btnAddNewStudent
            // 
            this.btnAddNewStudent.Location = new System.Drawing.Point(920, 73);
            this.btnAddNewStudent.Name = "btnAddNewStudent";
            this.btnAddNewStudent.Size = new System.Drawing.Size(179, 29);
            this.btnAddNewStudent.TabIndex = 30;
            this.btnAddNewStudent.Text = "Добавить запись";
            this.btnAddNewStudent.UseVisualStyleBackColor = true;
            this.btnAddNewStudent.Click += new System.EventHandler(this.btnAddNewStudent_Click);
            // 
            // cmbTables
            // 
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(920, 6);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(179, 24);
            this.cmbTables.TabIndex = 31;
            this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged_1);
            // 
            // btnEditSchedule1
            // 
            this.btnEditSchedule1.Location = new System.Drawing.Point(1733, 828);
            this.btnEditSchedule1.Name = "btnEditSchedule1";
            this.btnEditSchedule1.Size = new System.Drawing.Size(178, 47);
            this.btnEditSchedule1.TabIndex = 32;
            this.btnEditSchedule1.Text = "Редактировать расписание";
            this.btnEditSchedule1.UseVisualStyleBackColor = true;
            this.btnEditSchedule1.Click += new System.EventHandler(this.btnEditSchedule1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 997);
            this.Controls.Add(this.btnEditSchedule1);
            this.Controls.Add(this.cmbTables);
            this.Controls.Add(this.btnAddNewStudent);
            this.Controls.Add(this.btnDeleteStudentRecord);
            this.Controls.Add(this.btnSaveStudentChanges);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.btnEditScheduleTable);
            this.Controls.Add(this.btnDeleteGrade);
            this.Controls.Add(this.btnAddAttendanceAndGrade);
            this.Controls.Add(this.btnEditGrades);
            this.Controls.Add(this.btnViewAllUsers);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.btnShowLessonsByGroup);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblWelcomeMessage);
            this.Controls.Add(this.btnSetBadGrades);
            this.Controls.Add(this.btnViewAttendanceByFio);
            this.Controls.Add(this.btnViewGradesByFio);
            this.Controls.Add(this.btnViewStudent);
            this.Controls.Add(this.btnViewGrade);
            this.Controls.Add(this.btnViewAttendance);
            this.Controls.Add(this.btnViewLesson);
            this.Controls.Add(this.btnViewProfessor);
            this.Controls.Add(this.btnViewGroup);
            this.Controls.Add(this.btnViewAudience);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnProfessorLessons);
            this.Controls.Add(this.txtProfessorFio);
            this.Controls.Add(this.lblProfessorFIO);
            this.Controls.Add(this.btnAvgGrade);
            this.Controls.Add(this.txtStudentFio);
            this.Controls.Add(this.lblStudentFio);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStudentFio;
        private System.Windows.Forms.TextBox txtStudentFio;
        private System.Windows.Forms.Button btnAvgGrade;
        private System.Windows.Forms.Label lblProfessorFIO;
        private System.Windows.Forms.TextBox txtProfessorFio;
        private System.Windows.Forms.Button btnProfessorLessons;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnViewAudience;
        private System.Windows.Forms.Button btnViewGroup;
        private System.Windows.Forms.Button btnViewProfessor;
        private System.Windows.Forms.Button btnViewLesson;
        private System.Windows.Forms.Button btnViewAttendance;
        private System.Windows.Forms.Button btnViewGrade;
        private System.Windows.Forms.Button btnViewStudent;
        private System.Windows.Forms.Button btnViewAttendanceByFio;
        private System.Windows.Forms.Button btnSetBadGrades;
        private System.Windows.Forms.Label lblWelcomeMessage;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnShowLessonsByGroup;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnViewAllUsers;
        private System.Windows.Forms.Button btnEditGrades;
        private System.Windows.Forms.Button btnAddAttendanceAndGrade;
        private System.Windows.Forms.Button btnDeleteGrade;
        private System.Windows.Forms.Button btnEditScheduleTable;
        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.Button btnSaveStudentChanges;
        private System.Windows.Forms.Button btnDeleteStudentRecord;
        private System.Windows.Forms.Button btnAddNewStudent;
        private System.Windows.Forms.ComboBox cmbTables;
        private System.Windows.Forms.Button btnEditSchedule1;
    }
}


