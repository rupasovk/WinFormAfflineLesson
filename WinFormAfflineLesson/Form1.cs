using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormAfflineLesson
{
    public partial class Form1 : Form
    {
        DbRepository db;

        #region Конструктор
        public Form1()
        {
            InitializeComponent();

            db = new DbRepository();
            db.Players.Load();

            dataGridView1.DataSource = db.Players.Local.ToBindingList();
        }
        #endregion

        #region Добавление
        private void addButton_Click(object sender, EventArgs e)
        {
            registrForm RegForm = new registrForm();
            DialogResult result = RegForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Player player = new Player();
            player.Name = RegForm.textBox1.Text;
            player.NickName = RegForm.textBox2.Text;
            player.Birthday = RegForm.dateTimePicker1.Value;
            player.Country = RegForm.textBox3.Text;

            db.Players.Add(player);
            db.SaveChanges();

            MessageBox.Show("Пользователь успешно добавлен :)");
        }
        #endregion

        #region Редактирование
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Достаем индекс выбранной строки
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                // Достаем из базы игрока с индексом
                Player player = db.Players.Find(id);

                // Запускаем форму для редактирования
                registrForm plForm = new registrForm();

                plForm.textBox1.Text = player.Name;
                plForm.textBox2.Text = player.NickName;
                plForm.dateTimePicker1.Value = player.Birthday;
                plForm.textBox3.Text = player.Country;

                // Читаем результат диалога с пользователем - нажата ОК/нажата Cancel
                DialogResult result = plForm.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                player.Name = plForm.textBox1.Text;
                player.NickName = plForm.textBox2.Text;
                player.Birthday = plForm.dateTimePicker1.Value;
                player.Country = plForm.textBox3.Text;

                db.SaveChanges();
                dataGridView1.Refresh(); // обновляем грид
                MessageBox.Show("Объект обновлен");

            }
        }
        #endregion

        #region Удаление
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Достаем индекс выбранной строки
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                // Достаем из базы игрока с индексом
                Player player = db.Players.Find(id);

                db.Players.Remove(player);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }
        #endregion
    }
}
