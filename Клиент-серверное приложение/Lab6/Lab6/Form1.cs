using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Lab6
{
    
    public partial class Form1 : Form
    {
        static string connString = "Host=localhost;Username=postgres;Password=narutostorm3;Database=LAB 3";
        static NpgsqlConnection nc = new NpgsqlConnection(connString);
        static int kol;
        static string t1="", t2="", t3_1="", t3_2="";
        static bool flag=false;

        static int columnIndex, rowIndex;
        

        ComboBox comboBox3 = new ComboBox();
        ComboBox comboBox4 = new ComboBox();
        ComboBox comboBox5 = new ComboBox();
        ComboBox comboBox6 = new ComboBox();
        //static ComboBox comboBox4 = new ComboBox();

        public Form1()
        {
            InitializeComponent();
            
            
            nc.Open();
            if (nc.FullState == ConnectionState.Broken || nc.FullState == ConnectionState.Closed)
            {
                MessageBox.Show("Ошибка подключения!");
            }
            else
            {
                MessageBox.Show("Соединение установлено!");
            }

  

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                string query = "";
                label2.Visible = true;
                comboBox2.Visible = true;
                button2.Visible = true;

                if (comboBox1.Text == "Администратор")
                {
                    flag = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    query = "call Изменить_пользователя('Администратор')";

                }
                else if (comboBox1.Text == "Оператор")
                {
                    flag = true;
                    button7.Visible = true;
                    button6.Visible = false;
                    query = "call Изменить_пользователя('Оператор')";

                }
                else if (comboBox1.Text == "Читатель")
                {
                    flag = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button7.Visible = false;
                    button6.Visible = false;
                    query = "call Изменить_пользователя('Читатель')";
                }
                NpgsqlCommand command5 = new NpgsqlCommand(query, nc);
                int rows_changed = command5.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Сделайте выбор!");
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Список клиентов")
            {
                int n = 5;
                string[] names = {"Владелец", "Улица", "Дом", "Квартира", "Паспорт"};
                string query = "select * from Список_клиентов;";

                DataView(dataGridView1, n, names, query);

                if (flag)
                {
                    button3.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                }
                

            }
            else if (comboBox2.Text == "Список номеров")
            {
                int n = 4;
                string[] names = { "Номер", "Улица", "Дом", "Квартира" };
                string query = "select * from Список_номеров";

                DataView(dataGridView1, n, names, query);

                if (flag)
                {
                    button3.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                }
            }
            else if (comboBox2.Text == "Список услуг")
            {
                int n = 6;
                string[] names = {"Сервис", "Дата подключения", "Дата отключения", "Номер", "Тип" ,  "Владелец" };
                string query = "select * from Список_услуг";

                DataView(dataGridView1, n, names, query);

                string query2 = "select phone_number from Список_номеров";
                string[] arrayItem = DataView2(query2);
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(arrayItem);

                string query3 = "select * from Типы";
                string[] arrayItem2 = DataView2(query3);
                comboBox4.Items.Clear();
                comboBox4.Items.AddRange(arrayItem2);

                string query4 = "select owner from Список_клиентов";
                string[] arrayItem3 = DataView2(query4);
                comboBox5.Items.Clear();
                comboBox5.Items.AddRange(arrayItem3);

                string query5 = "select * from Сервисы";
                string[] arrayItem4 = DataView2(query5);
                comboBox6.Items.Clear();
                comboBox6.Items.AddRange(arrayItem4);

                if (flag)
                {
                    button3.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                }
            }
            else if (comboBox2.Text == "Полная информация")
            {
                int n = 13;
                string[] names = { "Сервис", "Дата подключения", "Дата отключения", "Номер", "Тип", "Улица", "Дом", "Квартира", "Владелец", "Паспорт" , "Улица", "Дом", "Квартира"};
                string query = "select * from Полная_информация";

                DataView(dataGridView1, n, names, query);

                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
            }
            else
            {
                MessageBox.Show("Сделайте выбор!");
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
            }

            
        }


        public static string[] DataView2(string query)
        {
            NpgsqlCommand command = new NpgsqlCommand(query, nc);

            NpgsqlDataReader reader = command.ExecuteReader();

            List<string> data = new List<string>();

            while (reader.Read())
            {
                data.Add(reader[0].ToString());

            }

            reader.Close();

            string[] arrayItem = new string[data.Count()];

            for (int i=0; i<data.Count(); i++)
            {
                arrayItem[i] = data[i];
            }

            return arrayItem;

        }

        public static void DataView(DataGridView dgv1, int n, string[] names, string query)
        {
            dgv1.Columns.Clear();
            dgv1.Rows.Clear();
            
            DataGridViewTextBoxColumn[] column = new DataGridViewTextBoxColumn[n];

            for (int i = 0; i < n; i++)
            {
                column[i] = new DataGridViewTextBoxColumn();
                column[i].HeaderText = names[i];
                column[i].Name = names[i];
                dgv1.Columns.Add(column[i]);
            }

            NpgsqlCommand command = new NpgsqlCommand(query, nc);

            NpgsqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[n]);

                for(int i=0; i<n; i++)
                {
                    data[data.Count - 1][i] = reader[i].ToString();
                }
            }

            reader.Close();

            foreach (string[] s in data)
                dgv1.Rows.Add(s);
            dgv1.AutoResizeColumns();
            kol = dgv1.RowCount;
            MessageBox.Show(Convert.ToString(kol));



        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            if (kol < dataGridView1.RowCount)
            {
                

                for (int i =0; i< dataGridView1.RowCount-kol; i++)
                {
                    string query = "";

                    if (comboBox2.Text == "Список клиентов")
                    {

                        query = "call Добавить_клиента ('"+ dataGridView1[0, kol-1+i].Value + "','" + dataGridView1[1, kol - 1 + i].Value + "'," + dataGridView1[2, kol - 1 + i].Value + "," + dataGridView1[3, kol - 1 + i].Value + ",'" + dataGridView1[4, kol - 1 + i].Value + "');";
                        MessageBox.Show("Ураааа!!!");
                        

                    }
                    else if (comboBox2.Text == "Список номеров")
                    {
                        query = "call Добавить_номер ('" + dataGridView1[0, kol - 1 + i].Value + "','" + dataGridView1[1, kol - 1 + i].Value + "'," + dataGridView1[2, kol - 1 + i].Value + "," + dataGridView1[3, kol - 1 + i].Value +  ");";
                        MessageBox.Show("Ураааа!!!");
                    }
                    else if (comboBox2.Text == "Список услуг")
                    {
                        
                        
                        if (dataGridView1[2, kol - 1 + i].Value == null || dataGridView1[2, kol - 1 + i].Value == "")
                        {
                            dataGridView1[2, kol - 1 + i].Value = "null";
                        }
                        else
                        {
                            dataGridView1[2, kol - 1 + i].Value = "'" + dataGridView1[2, kol - 1 + i].Value + "'";
                        }
                        query = "call Добавить_услугу ('" + dataGridView1[0, kol - 1 + i].Value + "','" + dataGridView1[1, kol - 1 + i].Value + "'," + dataGridView1[2, kol - 1 + i].Value + ",'" + dataGridView1[3, kol - 1 + i].Value + "','" + dataGridView1[4, kol - 1 + i].Value + "','" + dataGridView1[5, kol - 1 + i].Value + "');";

                        MessageBox.Show("Ураааа!!!");
                    }
                    else if (comboBox2.Text == "Полная информация")
                    {
                        //string query = "select * from Полная_информация";
                    }

                    NpgsqlCommand command1 = new NpgsqlCommand(query, nc);
                    int rows_changed = command1.ExecuteNonQuery();
                    try
                    {
                        
                    }
                    catch
                    {
                        MessageBox.Show("Неверный тип данных :(");
                    }



                }
            }
            button2_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "";
            int index = rowIndex;
            if (comboBox2.Text == "Список клиентов")
            {

                query = "call Изменить_клиента ('" + dataGridView1[0, index].Value + "','" + dataGridView1[1, index].Value + "'," + dataGridView1[2, index].Value + "," + dataGridView1[3, index].Value + ",'" + dataGridView1[4, index].Value + "','" + t1 + "');";
                MessageBox.Show("Ураааа!!!");


            }
            else if (comboBox2.Text == "Список номеров")
            {
                query = "call Изменить_номер ('" + dataGridView1[0, index].Value + "','" + dataGridView1[1, index].Value + "'," + dataGridView1[2, index].Value + "," + dataGridView1[3, index].Value + ",'" + t2 + "');";
                MessageBox.Show("Ураааа!!!");
            }
            else if (comboBox2.Text == "Список услуг")
            {


                if (dataGridView1[2, index].Value == null || dataGridView1[2, index].Value == "")
                {
                    dataGridView1[2, index].Value = "null";
                }
                else
                {
                    dataGridView1[2, index].Value = "'" + dataGridView1[2, index].Value + "'";
                }
                query = "call Изменить_услугу ('" + dataGridView1[0, index].Value + "','" + dataGridView1[1, index].Value + "'," + dataGridView1[2, index].Value + ",'" + dataGridView1[3, index].Value + "','" + dataGridView1[4,  index].Value + "','" + dataGridView1[5, index].Value + "','" + t3_1 + "','" + t3_2 +  "');";

                MessageBox.Show("Ураааа!!!");
            }
            else if (comboBox2.Text == "Полная информация")
            {
                //string query = "select * from Полная_информация";
            }

            NpgsqlCommand command1 = new NpgsqlCommand(query, nc);
            try
            {
                int rows_changed = command1.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Неверный тип данных :(");
            }


            button2_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n = 3;
            string[] names = { "Количество клиентов", "Количество номеров", "Среднее кол-во сервисов"};
            string query = "select * from Отчет";

            DataView(dataGridView1, n, names, query);

            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int n = 4;
            string[] names = { "Операция", "Дата", "Пользователь", "Строка" };
            string query = "select * from logs";

            DataView(dataGridView1, n, names, query);

            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Скрываем элемент
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            comboBox6.Visible = false;

            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;

            label2.Visible = false;
            comboBox2.Visible = false;
            button2.Visible = false;

            button7.Visible = false;
            button6.Visible = false;

            //Создаем обработчик события(выбор из списка)
            comboBox3.SelectedValueChanged += comboBox_SelectedValueChanged;
            comboBox4.SelectedValueChanged += comboBox_SelectedValueChanged;
            comboBox5.SelectedValueChanged += comboBox_SelectedValueChanged;
            comboBox6.SelectedValueChanged += comboBox_SelectedValueChanged;

            //Добавляем элемент
            dataGridView1.Controls.Add(comboBox3);
            dataGridView1.Controls.Add(comboBox4);
            dataGridView1.Controls.Add(comboBox5);
            dataGridView1.Controls.Add(comboBox6);

        }

        //Событие происходит всякий раз, при выборе значения из списка
        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            

            if (columnIndex == 3)
            {
                //Заносим данные в ячейку
                dataGridView1[columnIndex, rowIndex].Value = comboBox3.SelectedItem;

                //Скрываем элемент
                comboBox3.Visible = false;
            }
            else if (columnIndex == 4)
            {
                //Заносим данные в ячейку
                dataGridView1[columnIndex, rowIndex].Value = comboBox4.SelectedItem;

                //Скрываем элемент
                comboBox4.Visible = false;
            }
            else if (columnIndex == 5)
            {
                //Заносим данные в ячейку
                dataGridView1[columnIndex, rowIndex].Value = comboBox5.SelectedItem;

                //Скрываем элемент
                comboBox5.Visible = false;
            }
            else if (columnIndex == 0)
            {
                //Заносим данные в ячейку
                dataGridView1[columnIndex, rowIndex].Value = comboBox6.SelectedItem;

                //Скрываем элемент
                comboBox6.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            string query = "";

            if (comboBox2.Text == "Список клиентов")
            {
                query = "call Удалить_клиента ('" + dataGridView1[4, index].Value + "');";
                MessageBox.Show("Ураааа!!!");
            }
            else if (comboBox2.Text == "Список номеров")
            {
                query = "call Удалить_номер ('" + dataGridView1[0, index].Value + "');";
                MessageBox.Show("Ураааа!!!");
            }
            else if (comboBox2.Text == "Список услуг")
            {
                query = "call Удалить_услугу ('" + dataGridView1[0, index].Value + "','" + dataGridView1[3, index].Value + "');";
                MessageBox.Show("Ураааа!!!");

            }
            else if (comboBox2.Text == "Полная информация")
            {

            }
            NpgsqlCommand command1 = new NpgsqlCommand(query, nc);
            try
            {
                int rows_changed = command1.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Нельзя удалить эту строку, т.к. от нее зависят другие :(");
            }





            button2_Click(sender, e);
        }



        //Событие происходит всякий раз, при клике по ячейкам


        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Задаем индекс строки
            
            rowIndex = e.RowIndex;
            columnIndex = e.ColumnIndex;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            comboBox6.Visible = false;


            if (comboBox2.Text == "Список услуг")
            {
                if (dataGridView1[0, rowIndex].Value != null)
                {
                    t3_1 = dataGridView1[0, rowIndex].Value.ToString();
                }
                if (dataGridView1[3, rowIndex].Value != null)
                {
                    t3_2 = dataGridView1[3, rowIndex].Value.ToString();
                }
                    
                

                Rectangle rectangle = dataGridView1.GetCellDisplayRectangle(columnIndex, rowIndex, true);

                if (columnIndex == 3)
                {
                    //Задаем размеры и месторасположение
                    comboBox3.Size = new Size(rectangle.Width, rectangle.Height);
                    comboBox3.Location = new Point(rectangle.X, rectangle.Y);

                    //Показываем элемент
                    comboBox3.Visible = true;
                }
                else if (columnIndex == 4)
                {
                    //Задаем размеры и месторасположение
                    comboBox4.Size = new Size(rectangle.Width, rectangle.Height);
                    comboBox4.Location = new Point(rectangle.X, rectangle.Y);

                    //Показываем элемент
                    comboBox4.Visible = true;
                }
                else if (columnIndex == 5)
                {
                    //Задаем размеры и месторасположение
                    comboBox5.Size = new Size(rectangle.Width, rectangle.Height);
                    comboBox5.Location = new Point(rectangle.X, rectangle.Y);

                    //Показываем элемент
                    comboBox5.Visible = true;
                }
                else if (columnIndex == 0)
                {
                    //Задаем размеры и месторасположение
                    comboBox6.Size = new Size(rectangle.Width, rectangle.Height);
                    comboBox6.Location = new Point(rectangle.X, rectangle.Y);

                    //Показываем элемент
                    comboBox6.Visible = true;
                }
            }
            else if (comboBox2.Text == "Список клиентов")
            {
                if (dataGridView1[4, rowIndex].Value!=null)
                {
                    t1 = dataGridView1[4, rowIndex].Value.ToString();
                }
                
            }
            else if (comboBox2.Text == "Список номеров")
            {
                if (dataGridView1[0, rowIndex].Value != null)
                {
                    t2 = dataGridView1[0, rowIndex].Value.ToString();
                }
            }
                    









        }
    }
}
