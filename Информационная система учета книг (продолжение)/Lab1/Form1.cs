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
using System.Threading;

namespace Lab1
{

    public partial class Form1 : Form
    {
        public static Form1 SelfRef
        {
            get; set;
        }


        public static string path = "C:\\books.csv";
        public static int number, number2 = 0;
        public static List<Book> ListOfBooks = new List<Book>();
        public static Book bookdel = new Book();
        public static bool lv1use = true;

        FileInfo filepath = new FileInfo(path);
        FileInfo filepath2 = new FileInfo(path);
        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }
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





        }
        public Form1()
        {

            SelfRef = this;
            InitializeComponent();
            listView1.Items.Clear();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {

                    Book book = new Book();
                    book.Divide(line);
                    ListOfBooks.Add(book);
                    listView1.Items.Add(book.Title);
                    listView1.Items[i].SubItems.Add(book.Author);
                    listView1.Items[i].SubItems.Add(book.Genre);
                    listView1.Items[i].SubItems.Add(book.Year);
                    listView1.Items[i].SubItems.Add(book.Width);
                    listView1.Items[i].SubItems.Add(book.Height);
                    listView1.Items[i].SubItems.Add(book.Binding);
                    listView1.Items[i].SubItems.Add(book.Source);
                    listView1.Items[i].SubItems.Add(book.LibraryDate);
                    listView1.Items[i].SubItems.Add(book.ReadDate);
                    listView1.Items[i].SubItems.Add(book.Rating);
                    i++;
                }
            }
            SetHeight(listView1, 25);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1_ReLoad();
        }

        public static void SetHeight(ListView listView, int height)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            listView.SmallImageList = imgList;
        }

  

        private void button3_Click(object sender, EventArgs e)
        {
            Form1_ReLoad();
            Form2 form2 = new Form2();
            form2.Show();

        }

        public void Search()
        {
    
            List<Book> Table = new List<Book>();
            Table = Form2.Table2;
            lv1use = false;

            listView3.Items.Clear();
            listView3.Visible = true;
            for (int i = 0; i < Table.Count; i++)
            {
                listView3.Items.Add(Table[i].Title);
                listView3.Items[i].SubItems.Add(Table[i].Author);
                listView3.Items[i].SubItems.Add(Table[i].Genre);
                listView3.Items[i].SubItems.Add(Table[i].Year);
                listView3.Items[i].SubItems.Add(Table[i].Width);
                listView3.Items[i].SubItems.Add(Table[i].Height);
                listView3.Items[i].SubItems.Add(Table[i].Binding);
                listView3.Items[i].SubItems.Add(Table[i].Source);
                listView3.Items[i].SubItems.Add(Table[i].LibraryDate);
                listView3.Items[i].SubItems.Add(Table[i].ReadDate);
                listView3.Items[i].SubItems.Add(Table[i].Rating);

            }
            SetHeight(listView3, 25);
            Form2.Table2.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            while (IsFileLocked(filepath)) ;
            Book newbook = new Book();

            var usCulture = new System.Globalization.CultureInfo("ru-RU");
            DateTime userDate, nowdate = DateTime.Now;

            if (Regex.IsMatch(textBox9.Text, @"^[0-9]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Жанр");
            else if (Regex.IsMatch(textBox8.Text, @"^[0-9]+$") == true && (Convert.ToInt32(textBox8.Text) < 0 || Convert.ToInt32(textBox8.Text) > Convert.ToInt32(nowdate.Year)))
                MessageBox.Show("Неправильный формат данных в ячейке Год выпуска");
            else if (Regex.IsMatch(textBox7.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Ширина обложки");
            else if (Regex.IsMatch(textBox2.Text, @"^[а-яА-Яa-zA-Z]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Высота обложки");
            else if (!DateTime.TryParse(textBox4.Text, usCulture.DateTimeFormat, System.Globalization.DateTimeStyles.None, out userDate))
                MessageBox.Show("Неправильный формат данных в ячейке Дата появления в библиотеке");
            else if (!DateTime.TryParse(textBox1.Text, usCulture.DateTimeFormat, System.Globalization.DateTimeStyles.None, out userDate))
                MessageBox.Show("Неправильный формат данных в ячейке Дата прочтения");
            else if (Regex.IsMatch(textBox10.Text, @"^[0-9]+$") == true)
                MessageBox.Show("Неправильный формат данных в ячейке Авторы");
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
                ListOfBooks.Add(newbook);
               
                string line = newbook.Title + "," + newbook.Author + "," + newbook.Genre + "," + newbook.Year + "," + newbook.Width + "," + newbook.Height + "," + newbook.Binding + "," + newbook.Source + "," + newbook.LibraryDate + "," + newbook.ReadDate + "," + newbook.Rating;

                using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                    sw.WriteLine(line);

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
                int i = listView1.Items.Count;

                listView1.Items.Add(newbook.Title);
                listView1.Items[i].SubItems.Add(newbook.Author);
                listView1.Items[i].SubItems.Add(newbook.Genre);
                listView1.Items[i].SubItems.Add(newbook.Year);
                listView1.Items[i].SubItems.Add(newbook.Width);
                listView1.Items[i].SubItems.Add(newbook.Height);
                listView1.Items[i].SubItems.Add(newbook.Binding);
                listView1.Items[i].SubItems.Add(newbook.Source);
                listView1.Items[i].SubItems.Add(newbook.LibraryDate);
                listView1.Items[i].SubItems.Add(newbook.ReadDate);
                listView1.Items[i].SubItems.Add(newbook.Rating);


                SetHeight(listView1, 25);
            }




        }


      

        private void button4_Click(object sender, EventArgs e)
        {
            while (IsFileLocked(filepath)) ;
            int number2=0;

  
            
            if (lv1use == true)
            {
                listView1.Items[number].Remove();
                ListOfBooks.RemoveAt(number);
                SetHeight(listView1, 25);
            }
            else
            {
                for (int i = 0; i < ListOfBooks.Count; i++)
                {
                    if (listView3.Items[number].Text == ListOfBooks[i].Title)
                        if (listView3.Items[number].SubItems[1].Text == ListOfBooks[i].Author && listView3.Items[number].SubItems[2].Text == ListOfBooks[i].Genre && listView3.Items[number].SubItems[3].Text == ListOfBooks[i].Year && listView3.Items[number].SubItems[4].Text == ListOfBooks[i].Width && listView3.Items[number].SubItems[5].Text == ListOfBooks[i].Height && listView3.Items[number].SubItems[6].Text == ListOfBooks[i].Binding && listView3.Items[number].SubItems[7].Text == ListOfBooks[i].Source && listView3.Items[number].SubItems[8].Text == ListOfBooks[i].LibraryDate && listView3.Items[number].SubItems[9].Text == ListOfBooks[i].ReadDate && listView3.Items[number].SubItems[10].Text == ListOfBooks[i].Rating)
                        {
                            number2 = i;
                            break;
                        }

                }
                listView3.Items[number].Remove();
                listView1.Items[number2].Remove();
                ListOfBooks.RemoveAt(number2);
                SetHeight(listView1, 25);
                SetHeight(listView3, 25);
            }

           

            MessageBox.Show("Книга удалена");


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

        ListViewItem.ListViewSubItem CurrentSubItem = default(ListViewItem.ListViewSubItem);
        ListViewItem CurrentItem = default(ListViewItem);
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form1_ReLoad();

            CurrentItem = listView1.GetItemAt(e.X, e.Y);
            if (CurrentItem == null)
                return;

            CurrentSubItem = CurrentItem.GetSubItemAt(e.X, e.Y);
            number = listView1.Items.IndexOf(CurrentItem);

            MessageBox.Show("Книга выбрана для удаления/редактирования" );
        }

        private void listView3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form1_ReLoad();
            CurrentItem = listView3.GetItemAt(e.X, e.Y);
            if (CurrentItem == null)
                return;

            CurrentSubItem = CurrentItem.GetSubItemAt(e.X, e.Y);
            number = listView3.Items.IndexOf(CurrentItem);

            MessageBox.Show("Книга выбрана для удаления/редактирования");
        }
        private void button8_Click(object sender, EventArgs e)
        {
            

            if (lv1use == true)
            {

            }
            else
            {
                for (int i = 0; i < ListOfBooks.Count; i++)
                {
                    if (listView3.Items[number].Text == ListOfBooks[i].Title)
                        if (listView3.Items[number].SubItems[1].Text == ListOfBooks[i].Author && listView3.Items[number].SubItems[2].Text == ListOfBooks[i].Genre && listView3.Items[number].SubItems[3].Text == ListOfBooks[i].Year && listView3.Items[number].SubItems[4].Text == ListOfBooks[i].Width && listView3.Items[number].SubItems[5].Text == ListOfBooks[i].Height && listView3.Items[number].SubItems[6].Text == ListOfBooks[i].Binding && listView3.Items[number].SubItems[7].Text == ListOfBooks[i].Source && listView3.Items[number].SubItems[8].Text == ListOfBooks[i].LibraryDate && listView3.Items[number].SubItems[9].Text == ListOfBooks[i].ReadDate && listView3.Items[number].SubItems[10].Text == ListOfBooks[i].Rating)
                        {
                            number2 = i;
                            break;
                        }

                }
                int num = number;
                number = number2;
                number2 = num;
            }
            Form3 form3 = new Form3();
            form3.Show();

        }

        public void Form1_ReLoad()
        {
            lv1use = true;
            listView3.Visible = false;
            listView1.Items.Clear();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {

                    Book book = new Book();
                    book.Divide(line);
                    ListOfBooks.Add(book);
                    listView1.Items.Add(book.Title);
                    listView1.Items[i].SubItems.Add(book.Author);
                    listView1.Items[i].SubItems.Add(book.Genre);
                    listView1.Items[i].SubItems.Add(book.Year);
                    listView1.Items[i].SubItems.Add(book.Width);
                    listView1.Items[i].SubItems.Add(book.Height);
                    listView1.Items[i].SubItems.Add(book.Binding);
                    listView1.Items[i].SubItems.Add(book.Source);
                    listView1.Items[i].SubItems.Add(book.LibraryDate);
                    listView1.Items[i].SubItems.Add(book.ReadDate);
                    listView1.Items[i].SubItems.Add(book.Rating);
                    i++;
                }
            }

            SetHeight(listView1, 25);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            while (IsFileLocked(filepath));
            string path2 = "random.csv";
            string[] authors = new string[100];
            string[] names = new string[100];
            string[] genre = new string[] {"Роман", "Повесть", "Рассказ", "Драма", "Комедия", "Стихотворения", "Учебник", "Научная лит-ра" , "Фантастика", "Психология" };
            string[] binding = new string[] {"Мягкий", "Твердый"};
            string[] source = new string[] {"Наследство", "Подарок", "Покупка"};
            string[] rating = new string[] {"Замечательная книга, в ней все идеально", "Ужасная книга, не рекомендую", "Хорошая книга, стоит потраченного времени", "Неплохя книга, но не хватает...", "Сложная для понимания книга" };

            List<Book> ListOfBooks2 = new List<Book>();
            using (StreamReader sr = new StreamReader(path2, Encoding.Default))
            {
                string line;
                line = sr.ReadLine();
                authors = line.Split(',');
                line = sr.ReadLine();
                names = line.Split(',');

            }
            Random rnd = new Random();
            Random gen = new Random();
            DateTime RandomDay()
            {
                DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(gen.Next(range));
            }
            int n = 0;
            for (int i = 0; i<100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Book book = new Book();
                    book.Title = names[j];
                    book.Author = authors[i];
                    book.Genre = genre[rnd.Next(0, 10)];
                    book.Year = Convert.ToString(rnd.Next(1400, 2021));
                    book.Width = Convert.ToString(rnd.Next(10, 101));
                    book.Height = Convert.ToString(rnd.Next(10, 101));
                    book.Binding = binding[rnd.Next(0, 2)];
                    book.Source = source[rnd.Next(0, 3)];
                    book.LibraryDate = Convert.ToString(RandomDay().ToShortDateString());
                    book.ReadDate = Convert.ToString(RandomDay().ToShortDateString());
                    book.Rating = rating[rnd.Next(0, 5)];
                    ListOfBooks2.Add(book);
                    n++;

                }
            }
                
            foreach (var i in ListOfBooks2.AsRandom())
                ListOfBooks.Add(i);

            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                string line2;

                for (int i = 0; i < ListOfBooks.Count; i++)
                {
                    line2 = ListOfBooks[i].Title + "," + ListOfBooks[i].Author + "," + ListOfBooks[i].Genre + "," + ListOfBooks[i].Year + "," + ListOfBooks[i].Width + "," + ListOfBooks[i].Height + "," + ListOfBooks[i].Binding + "," + ListOfBooks[i].Source + "," + ListOfBooks[i].LibraryDate + "," + ListOfBooks[i].ReadDate + "," + ListOfBooks[i].Rating;
                    sw.WriteLine(line2);
                }

            }
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {

                    Book book = new Book();
                    book.Divide(line);
                    ListOfBooks.Add(book);
                    listView1.Items.Add(book.Title);
                    listView1.Items[i].SubItems.Add(book.Author);
                    listView1.Items[i].SubItems.Add(book.Genre);
                    listView1.Items[i].SubItems.Add(book.Year);
                    listView1.Items[i].SubItems.Add(book.Width);
                    listView1.Items[i].SubItems.Add(book.Height);
                    listView1.Items[i].SubItems.Add(book.Binding);
                    listView1.Items[i].SubItems.Add(book.Source);
                    listView1.Items[i].SubItems.Add(book.LibraryDate);
                    listView1.Items[i].SubItems.Add(book.ReadDate);
                    listView1.Items[i].SubItems.Add(book.Rating);
                    i++;
                }
            }
            SetHeight(listView1, 25);
            MessageBox.Show("Сгенерировано 10 тысяч книг");

        }
        public void Edit()
        {

            if (lv1use == true)
            {
                listView1.Items[number].Remove();
                listView1.Items.Insert(number, Form3.bookedit.Title);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Author);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Genre);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Year);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Width);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Height);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Binding);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Source);
                listView1.Items[number].SubItems.Add(Form3.bookedit.LibraryDate);
                listView1.Items[number].SubItems.Add(Form3.bookedit.ReadDate);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Rating);
                SetHeight(listView1, 25);
            }
            else
            {

                listView1.Items[number].Remove();
                listView1.Items.Insert(number, Form3.bookedit.Title);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Author);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Genre);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Year);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Width);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Height);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Binding);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Source);
                listView1.Items[number].SubItems.Add(Form3.bookedit.LibraryDate);
                listView1.Items[number].SubItems.Add(Form3.bookedit.ReadDate);
                listView1.Items[number].SubItems.Add(Form3.bookedit.Rating);
                listView3.Items[number2].Remove();
                listView3.Items.Insert(number2, Form3.bookedit.Title);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.Author);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.Genre);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.Year);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.Width);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.Height);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.Binding);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.Source);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.LibraryDate);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.ReadDate);
                listView3.Items[number2].SubItems.Add(Form3.bookedit.Rating);
          
                SetHeight(listView1, 25);
                SetHeight(listView3, 25);
            }

        }
    }
    public static class MyExtensions
    {
        public static IEnumerable<T> AsRandom<T>(this IList<T> list)
        {
            int[] indexes = Enumerable.Range(0, list.Count).ToArray();
            Random generator = new Random();

            for (int i = 0; i < list.Count; ++i)
            {
                int position = generator.Next(i, list.Count);

                yield return list[indexes[position]];

                indexes[position] = indexes[i];
            }
        }
    }
}
