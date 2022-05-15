using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form3 : Form
    {
        public static Form1.Book bookedit = new Form1.Book();
        public Form3()
        {
            InitializeComponent();

            listView2.Items.Clear();

            listView2.Items.Add(Form1.ListOfBooks[Form1.number].Title);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].Author);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].Genre);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].Year);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].Width);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].Height);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].Binding);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].Source);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].LibraryDate);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].ReadDate);
            listView2.Items[0].SubItems.Add(Form1.ListOfBooks[Form1.number].Rating);

            Form1.SetHeight(listView2, 25);

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private async void button6_Click(object sender, EventArgs e)
        {
            var usCulture = new System.Globalization.CultureInfo("ru-RU");
            DateTime userDate, nowdate = DateTime.Now;

            if (Regex.IsMatch(textBox16.Text, @"^[0-9]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Жанр");
            else if (Regex.IsMatch(textBox6.Text, @"^[0-9]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Авторы");
            else if (Regex.IsMatch(textBox17.Text, @"^[0-9]+$") == true && (Convert.ToInt32(textBox17.Text) < 0 || Convert.ToInt32(textBox17.Text) > Convert.ToInt32(nowdate.Year)))
                MessageBox.Show("Неправильный формат данных в ячейке Год выпуска");
            else if (Regex.IsMatch(textBox18.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Ширина обложки");
            else if (Regex.IsMatch(textBox21.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Высота обложки");
            else if (Regex.IsMatch(textBox19.Text, @"^[0-9]+$") == true && DateTime.TryParse(textBox19.Text, usCulture.DateTimeFormat, System.Globalization.DateTimeStyles.None, out userDate) == false)
                MessageBox.Show("Неправильный формат данных в ячейке Дата появления в библиотеке");
            else if (Regex.IsMatch(textBox22.Text, @"^[0-9]+$") == true && DateTime.TryParse(textBox22.Text, usCulture.DateTimeFormat, System.Globalization.DateTimeStyles.None, out userDate) == false)
                MessageBox.Show("Неправильный формат данных в ячейке Дата прочтения");
            else
            {
                if (textBox20.Text != "")
                    Form1.ListOfBooks[Form1.number].Title = textBox20.Text;
                if (textBox6.Text != "")
                    Form1.ListOfBooks[Form1.number].Author = textBox6.Text;
                if (textBox16.Text != "")
                    Form1.ListOfBooks[Form1.number].Genre = textBox16.Text;
                if (textBox17.Text != "")
                    Form1.ListOfBooks[Form1.number].Year = textBox17.Text;
                if (textBox18.Text != "")
                    Form1.ListOfBooks[Form1.number].Width = textBox18.Text;
                if (textBox21.Text != "")
                    Form1.ListOfBooks[Form1.number].Height = textBox21.Text;
                if (comboBox4.Text != "")
                    Form1.ListOfBooks[Form1.number].Binding = comboBox4.Text;
                if (comboBox5.Text != "")
                    Form1.ListOfBooks[Form1.number].Source = comboBox5.Text;
                if (textBox19.Text != "")
                    Form1.ListOfBooks[Form1.number].LibraryDate = textBox19.Text;
                if (textBox22.Text != "")
                    Form1.ListOfBooks[Form1.number].ReadDate = textBox22.Text;
                if (textBox5.Text != "")
                    Form1.ListOfBooks[Form1.number].Rating = textBox5.Text;


                bookedit.Title = Form1.ListOfBooks[Form1.number].Title;
                bookedit.Author = Form1.ListOfBooks[Form1.number].Author;
                bookedit.Genre = Form1.ListOfBooks[Form1.number].Genre;
                bookedit.Year = Form1.ListOfBooks[Form1.number].Year;
                bookedit.Width = Form1.ListOfBooks[Form1.number].Width;
                bookedit.Height = Form1.ListOfBooks[Form1.number].Height;
                bookedit.Binding = Form1.ListOfBooks[Form1.number].Binding;
                bookedit.Source = Form1.ListOfBooks[Form1.number].Source;
                bookedit.LibraryDate = Form1.ListOfBooks[Form1.number].LibraryDate;
                bookedit.ReadDate = Form1.ListOfBooks[Form1.number].ReadDate;
                bookedit.Rating = Form1.ListOfBooks[Form1.number].Rating;

                textBox20.Clear();
                textBox6.Clear();
                textBox16.Clear();
                textBox17.Clear();
                textBox18.Clear();
                textBox21.Clear();
                comboBox4.ResetText();
                comboBox5.ResetText();
                textBox19.Clear();
                textBox22.Clear();
                textBox5.Clear();
                listView2.Items.Clear();

                MessageBox.Show("Книга изменена.");
                Form1.SelfRef.Edit();

                using (StreamWriter sw = new StreamWriter(Form1.path, false, System.Text.Encoding.Default))
                {
                    string line2;

                    for (int i = 0; i < Form1.ListOfBooks.Count; i++)
                    {
                        line2 = Form1.ListOfBooks[i].Title + "," + Form1.ListOfBooks[i].Author + "," + Form1.ListOfBooks[i].Genre + "," + Form1.ListOfBooks[i].Year + "," + Form1.ListOfBooks[i].Width + "," + Form1.ListOfBooks[i].Height + "," + Form1.ListOfBooks[i].Binding + "," + Form1.ListOfBooks[i].Source + "," + Form1.ListOfBooks[i].LibraryDate + "," + Form1.ListOfBooks[i].ReadDate + "," + Form1.ListOfBooks[i].Rating;
                        await sw.WriteLineAsync(line2);
                    }

                }

                Close();

            }
        }
    }
}
