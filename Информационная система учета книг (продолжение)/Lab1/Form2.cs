using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace Lab1
{
    public partial class Form2 : Form
    {

        public static List<Form1.Book> Table2 = new List<Form1.Book>();
        public Form2()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i =0; i<Form1.ListOfBooks.Count; i++)
            {
                Form1.Book book = new Form1.Book();
                Form1.Book book2 = Form1.ListOfBooks[i];

                if (textBox20.Text == "")
                    book.Title = book2.Title;
                else
                    book.Title = textBox20.Text;
                if (textBox6.Text == "")
                    book.Author = book2.Author;
                else
                    book.Author = textBox6.Text;
                if (textBox16.Text == "")
                    book.Genre = book2.Genre;
                else
                    book.Genre = textBox16.Text;
                if (textBox17.Text == "")
                    book.Year = book2.Year;
                else
                {
                    if ((textBox17.Text.Length == 9) && (textBox17.Text[4] == '-') && Regex.IsMatch(textBox17.Text, @"^[0-9]+[-]") == true)
                    {

                        int year1 = Convert.ToInt32("" + textBox17.Text[0] + textBox17.Text[1] + textBox17.Text[2] + textBox17.Text[3]);

                        int year2 = Convert.ToInt32("" + textBox17.Text[5] + textBox17.Text[6] + textBox17.Text[7] + textBox17.Text[8]);

                        if (Convert.ToInt32(book2.Year) <= year2 && Convert.ToInt32(book2.Year) >= year1)
                            book.Year = book2.Year;
                        else
                            book.Year = textBox17.Text;
                    }
                    else
                        book.Year = textBox17.Text;
                }
                if (textBox18.Text == "")
                    book.Width = book2.Width;
                else
                {
                    int z = -1;
                    for (int n = 0; n < textBox18.Text.Length; n++)
                    {
                        if (textBox18.Text[n] == '-')
                            z = n;
                    }
                    if ((z != -1) && Regex.IsMatch(textBox18.Text, @"^[0-9]+[-]") == true)
                    {
                        string w1 = "", w2 = "";
                        for (int x = 0; x < z; x++)
                            w1 += textBox18.Text[x];
                        for (int x = z + 1; x < textBox18.Text.Length; x++)
                            w2 += textBox18.Text[x];
                        int width1 = Convert.ToInt32(w1), width2 = Convert.ToInt32(w2);
                        if (Convert.ToInt32(book2.Width) <= width2 && Convert.ToInt32(book2.Width) >= width1)
                            book.Width = book2.Width;
                        else
                            book.Width = textBox18.Text;
                    }
                    else
                        book.Width = textBox18.Text;
                }
                if (textBox21.Text == "")
                    book.Height = book2.Height;
                else
                {
                    int z = -1;
                    for (int n = 0; n < textBox21.Text.Length; n++)
                    {
                        if (textBox21.Text[n] == '-')
                            z = n;
                    }
                    if ((z != -1) && Regex.IsMatch(textBox21.Text, @"^[0-9]+[-]") == true)
                    {
                        string h1 = "", h2 = "";
                        for (int x = 0; x < z; x++)
                            h1 += textBox21.Text[x];
                        for (int x = z + 1; x < textBox21.Text.Length; x++)
                            h2 += textBox21.Text[x];
                        int height1 = Convert.ToInt32(h1), height2 = Convert.ToInt32(h2);
                        if (Convert.ToInt32(book2.Height) <= height2 && Convert.ToInt32(book2.Height) >= height1)
                            book.Height = book2.Height;
                        else
                            book.Height = textBox21.Text;
                    }
                    else
                        book.Height = textBox21.Text;
                }
                if (comboBox4.Text == "")
                    book.Binding = book2.Binding;
                else
                    book.Binding = comboBox4.Text;
                if (comboBox5.Text == "")
                    book.Source = book2.Source;
                else
                    book.Source = comboBox5.Text;
                if (textBox19.Text == "")
                    book.LibraryDate = book2.LibraryDate;
                else
                {
                    if ((textBox19.Text.Length == 21) && (textBox19.Text[10] == '-') && Regex.IsMatch(textBox19.Text, @"^[0-9]+[.]") == true)
                    {
                        string ld1 = "" + textBox19.Text[0] + textBox19.Text[1] + textBox19.Text[2] + textBox19.Text[3] + textBox19.Text[4] + textBox19.Text[5] + textBox19.Text[6] + textBox19.Text[7] + textBox19.Text[8] + textBox19.Text[9];

                        string ld2 = "" + textBox19.Text[11] + textBox19.Text[12] + textBox19.Text[13] + textBox19.Text[14] + textBox19.Text[15] + textBox19.Text[16] + textBox19.Text[17] + textBox19.Text[18] + textBox19.Text[19] + textBox19.Text[20];

                        DateTime data1 = new DateTime(Convert.ToInt32("" + ld1[6] + ld1[7] + ld1[8] + ld1[9]), Convert.ToInt32("" + ld1[3] + ld1[4]), Convert.ToInt32("" + ld1[0] + ld1[1]));

                        DateTime data2 = new DateTime(Convert.ToInt32("" + ld2[6] + ld2[7] + ld2[8] + ld2[9]), Convert.ToInt32("" + ld2[3] + ld2[4]), Convert.ToInt32("" + ld2[0] + ld2[1]));

                        DateTime datat = new DateTime(Convert.ToInt32("" + book2.LibraryDate[6] + book2.LibraryDate[7] + book2.LibraryDate[8] + book2.LibraryDate[9]), Convert.ToInt32("" + book2.LibraryDate[3] + book2.LibraryDate[4]), Convert.ToInt32("" + book2.LibraryDate[0] + book2.LibraryDate[1]));

                        if (datat >= data1 && datat <= data2)
                            book.LibraryDate = book2.LibraryDate;
                        else
                            book.LibraryDate = textBox19.Text;
                    }
                    else
                        book.LibraryDate = textBox19.Text;
                }
                if (textBox22.Text == "")
                    book.ReadDate = book2.ReadDate;
                else
                {
                    if ((textBox22.Text.Length == 21) && (textBox22.Text[10] == '-') && Regex.IsMatch(textBox22.Text, @"^[0-9]+[.]") == true)
                    {
                        string ld1 = "" + textBox22.Text[0] + textBox22.Text[1] + textBox22.Text[2] + textBox22.Text[3] + textBox22.Text[4] + textBox22.Text[5] + textBox22.Text[6] + textBox22.Text[7] + textBox22.Text[8] + textBox22.Text[9];
                        string ld2 = "" + textBox22.Text[11] + textBox22.Text[12] + textBox22.Text[13] + textBox22.Text[14] + textBox22.Text[15] + textBox22.Text[16] + textBox22.Text[17] + textBox22.Text[18] + textBox22.Text[19] + textBox22.Text[20];
                        DateTime data1 = new DateTime(Convert.ToInt32("" + ld1[6] + ld1[7] + ld1[8] + ld1[9]), Convert.ToInt32("" + ld1[3] + ld1[4]), Convert.ToInt32("" + ld1[0] + ld1[1]));
                        DateTime data2 = new DateTime(Convert.ToInt32("" + ld2[6] + ld2[7] + ld2[8] + ld2[9]), Convert.ToInt32("" + ld2[3] + ld2[4]), Convert.ToInt32("" + ld2[0] + ld2[1]));
                        DateTime datat = new DateTime(Convert.ToInt32("" + book2.ReadDate[6] + book2.ReadDate[7] + book2.ReadDate[8] + book2.ReadDate[9]), Convert.ToInt32("" + book2.ReadDate[3] + book2.ReadDate[4]), Convert.ToInt32("" + book2.ReadDate[0] + book2.ReadDate[1]));
                        if (datat >= data1 && datat <= data2)
                            book.ReadDate = book2.ReadDate;
                        else
                            book.ReadDate = textBox22.Text;
                    }
                    else
                        book.ReadDate = textBox22.Text;
                }
                book.Rating = book2.Rating;
                if (book2.Title == book.Title && book2.Author == book.Author && book2.Genre == book.Genre && book2.Year == book.Year && book2.Width == book.Width && book2.Height == book.Height && book2.Binding == book.Binding && book2.Source == book.Source && book2.LibraryDate == book.LibraryDate && book2.ReadDate == book.ReadDate)
                {
                    Table2.Add(book);
                }
            }
            

            Form1.SelfRef.Search();
            Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
