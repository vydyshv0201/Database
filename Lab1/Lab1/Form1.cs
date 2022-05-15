using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Lab1
{
    public partial class Form1 : Form
    {
        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }
            public string Year { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
            public string Binding { get; set; }
            public string Source { get; set; }
            public string LibraryDate { get; set; }
            public string ReadDate { get; set; }
            public string Rating { get; set; }

            public void Divide(string line)
            {
                string[] info = line.Split(',');  
                Title = info[0];
                Author = info[1];
                Genre = info[2];
                Year = info[3];
                Width = info[4];
                Height = info[5];
                Binding = info[6];
                Source = info[7];
                LibraryDate = info[8];
                ReadDate = info[9];
                Rating = info[10];
            }

            public static List<Book> ReadFile(string path)
            {
                List<Book> ListOfBooks = new List<Book>();
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line;
                  
                    while ((line = sr.ReadLine()) != null)
                    {
                        Book book = new Book();
                        book.Divide(line);
                        ListOfBooks.Add(book);
                    }
                }
                return ListOfBooks;
            }

            public static void WriteFile(Book newbook, string path)
            {
                string line = newbook.Title + "," + newbook.Author + "," + newbook.Genre + "," + newbook.Year + "," + newbook.Width + "," + newbook.Height + "," + newbook.Binding + "," + newbook.Source + "," + newbook.LibraryDate + "," + newbook.ReadDate + "," + newbook.Rating;

                using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                    sw.WriteLine(line);
            }

            public static void EditFile(string EditTitle, string param, string text, string path)
            {
                List<Book> ListOfBooks = new List<Book>();
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line1;
           
                    while ((line1 = sr.ReadLine()) != null)
                    {
                        Book book = new Book();
                        book.Divide(line1);
                        if (book.Title == EditTitle)
                            if (param == "Название книги")
                                book.Title = text;
                            else if (param == "Авторы")
                                book.Author = text;
                            else if (param == "Жанр")
                                book.Genre = text;
                            else if (param == "Год выпуска")
                                book.Year = text;
                            else if (param == "Ширина обложки")
                                book.Width = text;
                            else if (param == "Высота обложки")
                                book.Height = text;
                            else if (param == "Формат переплета")
                                book.Binding = text;
                            else if (param == "Источник появления")
                                book.Source = text;
                            else if (param == "Дата появления в библиотеке")
                                book.LibraryDate = text;
                            else if (param == "Дата прочтения")
                                book.ReadDate = text;
                            else if (param == "Оценка c комментарием")
                                book.Rating = text;

                        ListOfBooks.Add(book);
                    }
                }

                

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    string line2;

                    for (int i = 0; i < ListOfBooks.Count; i++)
                    {
                        line2 = ListOfBooks[i].Title + "," + ListOfBooks[i].Author + "," + ListOfBooks[i].Genre + "," + ListOfBooks[i].Year + "," + ListOfBooks[i].Width + "," + ListOfBooks[i].Height + "," + ListOfBooks[i].Binding + "," + ListOfBooks[i].Source + "," + ListOfBooks[i].LibraryDate + "," + ListOfBooks[i].ReadDate + "," + ListOfBooks[i].Rating;
                        sw.WriteLine(line2);
                    }
      
                }

            }

            public static void Delete(string DeleteTitle, string path)
            {
                List<Book> ListOfBooks = new List<Book>();
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line1;

                    while ((line1 = sr.ReadLine()) != null)
                    {
                        Book book = new Book();
                        book.Divide(line1);
                        if (book.Title != DeleteTitle)
                            ListOfBooks.Add(book);
                    }
                }


                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    string line2;

                    for (int i = 0; i < ListOfBooks.Count; i++)
                    {
                        line2 = ListOfBooks[i].Title + "," + ListOfBooks[i].Author + "," + ListOfBooks[i].Genre + "," + ListOfBooks[i].Year + "," + ListOfBooks[i].Width + "," + ListOfBooks[i].Height + "," + ListOfBooks[i].Binding + "," + ListOfBooks[i].Source + "," + ListOfBooks[i].LibraryDate + "," + ListOfBooks[i].ReadDate + "," + ListOfBooks[i].Rating;
                        sw.WriteLine(line2);
                    }

                }

            }
        }




        string path = "books.csv";
        string EditTitle;

        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            List<Book> Table = new List<Book>();
            Table = Book.ReadFile(path);
            listView1.Items.Clear();
            for (int i = 0; i < Table.Count; i++)
            {
                listView1.Items.Add(Table[i].Title);
                listView1.Items[i].SubItems.Add(Table[i].Author);
                listView1.Items[i].SubItems.Add(Table[i].Genre);
                listView1.Items[i].SubItems.Add(Table[i].Year);
                listView1.Items[i].SubItems.Add(Table[i].Width);
                listView1.Items[i].SubItems.Add(Table[i].Height);
                listView1.Items[i].SubItems.Add(Table[i].Binding);
                listView1.Items[i].SubItems.Add(Table[i].Source);
                listView1.Items[i].SubItems.Add(Table[i].LibraryDate);
                listView1.Items[i].SubItems.Add(Table[i].ReadDate);
                listView1.Items[i].SubItems.Add(Table[i].Rating);

            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string text = textBox12.Text;
            List<Book> Table = new List<Book>();
            Table = Book.ReadFile(path);
            listView1.Items.Clear();
            int j = 0;
            for (int i = 0; i < Table.Count; i++)
            {
                
                if (Table[i].Title == text || Table[i].Author == text || Table[i].Genre == text)
                {
                    listView1.Items.Add(Table[i].Title);
                    listView1.Items[j].SubItems.Add(Table[i].Author);
                    listView1.Items[j].SubItems.Add(Table[i].Genre);
                    listView1.Items[j].SubItems.Add(Table[i].Year);
                    listView1.Items[j].SubItems.Add(Table[i].Width);
                    listView1.Items[j].SubItems.Add(Table[i].Height);
                    listView1.Items[j].SubItems.Add(Table[i].Binding);
                    listView1.Items[j].SubItems.Add(Table[i].Source);
                    listView1.Items[j].SubItems.Add(Table[i].LibraryDate);
                    listView1.Items[j].SubItems.Add(Table[i].ReadDate);
                    listView1.Items[j].SubItems.Add(Table[i].Rating);
                    j++;
                }
            }
            textBox12.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Book newbook = new Book();

            if (Regex.IsMatch(textBox9.Text, @"^[0-9]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Жанр");
            else if (Regex.IsMatch(textBox8.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Год выпуска");
            else if (Regex.IsMatch(textBox7.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Ширина обложки");
            else if (Regex.IsMatch(textBox2.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Высота обложки");
            else if (Regex.IsMatch(textBox4.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Дата появления в библиотеке");
            else if (Regex.IsMatch(textBox1.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Дата прочтения");
            else if (Regex.IsMatch(textBox10.Text, @"^[0-9]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Жанр");
            else
            {
                newbook.Title = textBox3.Text;
                newbook.Author = textBox10.Text;
                newbook.Genre = textBox9.Text;
                newbook.Year = textBox8.Text;
                newbook.Width = textBox7.Text;
                newbook.Height = textBox2.Text;
                newbook.Binding = comboBox3.Text;
                newbook.Source = comboBox2.Text;
                newbook.LibraryDate = textBox4.Text;
                newbook.ReadDate = textBox1.Text;
                newbook.Rating = textBox11.Text;
                Book.WriteFile(newbook, path);

                textBox3.Clear();
                textBox10.Clear();
                textBox9.Clear();
                textBox8.Clear();
                textBox7.Clear();
                textBox2.Clear();
                comboBox3.ResetText();
                comboBox2.ResetText();
                textBox4.Clear();
                textBox1.Clear();
                textBox11.Clear();


                MessageBox.Show("Книга добавлена.");
            }




        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            EditTitle = textBox14.Text;
            List<Book> Table = new List<Book>();
            Table = Book.ReadFile(path);
            listView2.Items.Clear();
            for (int i = 0; i < Table.Count; i++)
            {
                if (Table[i].Title == EditTitle)
                {
                    listView2.Items.Add(Table[i].Title);
                    listView2.Items[0].SubItems.Add(Table[i].Author);
                    listView2.Items[0].SubItems.Add(Table[i].Genre);
                    listView2.Items[0].SubItems.Add(Table[i].Year);
                    listView2.Items[0].SubItems.Add(Table[i].Width);
                    listView2.Items[0].SubItems.Add(Table[i].Height);
                    listView2.Items[0].SubItems.Add(Table[i].Binding);
                    listView2.Items[0].SubItems.Add(Table[i].Source);
                    listView2.Items[0].SubItems.Add(Table[i].LibraryDate);
                    listView2.Items[0].SubItems.Add(Table[i].ReadDate);
                    listView2.Items[0].SubItems.Add(Table[i].Rating);
                }
            }
            textBox14.Clear();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox9.Text, @"^[0-9]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Жанр");
            else if (Regex.IsMatch(textBox16.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Год выпуска");
            else if (Regex.IsMatch(textBox17.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Ширина обложки");
            else if (Regex.IsMatch(textBox18.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Высота обложки");
            else if (Regex.IsMatch(textBox21.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Дата появления в библиотеке");
            else if (Regex.IsMatch(textBox19.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Дата прочтения");
            else if (Regex.IsMatch(textBox22.Text, @"^[0-9]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Жанр");
            else
            {
                List<Book> ListOfBooks = new List<Book>();
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line1;

                    while ((line1 = sr.ReadLine()) != null)
                    {
                        Book book = new Book();
                        book.Divide(line1);
                        if (book.Title == EditTitle)
                            if (textBox20.Text != "")
                                book.Title = textBox20.Text;
                            else if (textBox6.Text != "")
                                book.Author = textBox6.Text;
                            else if (textBox16.Text != "")
                                book.Genre = textBox16.Text;
                            else if (textBox17.Text != "")
                                book.Year = textBox17.Text;
                            else if (textBox18.Text != "")
                                book.Width = textBox18.Text;
                            else if (textBox21.Text != "")
                                book.Height = textBox21.Text;
                            else if (comboBox4.Text != "")
                                book.Binding = comboBox4.Text;
                            else if (comboBox5.Text != "")
                                book.Source = comboBox5.Text;
                            else if (textBox19.Text != "")
                                book.LibraryDate = textBox19.Text;
                            else if (textBox22.Text != "")
                                book.ReadDate = textBox22.Text;
                            else if (textBox5.Text != "")
                                book.Rating = textBox5.Text;

                        ListOfBooks.Add(book);
                    }
                }



                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    string line2;

                    for (int i = 0; i < ListOfBooks.Count; i++)
                    {
                        line2 = ListOfBooks[i].Title + "," + ListOfBooks[i].Author + "," + ListOfBooks[i].Genre + "," + ListOfBooks[i].Year + "," + ListOfBooks[i].Width + "," + ListOfBooks[i].Height + "," + ListOfBooks[i].Binding + "," + ListOfBooks[i].Source + "," + ListOfBooks[i].LibraryDate + "," + ListOfBooks[i].ReadDate + "," + ListOfBooks[i].Rating;
                        sw.WriteLine(line2);
                    }

                }


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
            }

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string DeleteTitle = textBox13.Text;
            Book.Delete(DeleteTitle, path);
            textBox13.Clear();
        }
    }
}
