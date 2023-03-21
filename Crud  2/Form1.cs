
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Crud__2
{
    public partial class Form1 : Form
    {
        #region Dichiarazioni variabili
        public struct Prodotto
        {
            public string nome;
            public float prezzo;
            //public int qnt;
        }
        public string fileName = @"testo.csv";
        public Prodotto prodotto;
        public int dim;
        #endregion

        #region Pulsanti
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            prodotto.nome = textBox1.Text;
            prodotto.prezzo = float.Parse(textBox2.Text);
            Scrittura(prodotto);
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            AperturaFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Clear();

            using (StreamWriter writer = new StreamWriter(fileName, append: false))
            {
                writer.Close();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string oggetto = textBox1.Text;
            Cancellazione(oggetto);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string parola = textBox1.Text;
            string modificato = textBox3.Text;

            Modifica(parola.ToString(), modificato.ToString());
        }
        #endregion

        #region funzioni 
        private void Scrittura(Prodotto prodotto)
        {
            using (StreamWriter writer = new StreamWriter(fileName, append: true))
            {
                writer.WriteLine("Nome: " + prodotto.nome + ";" + " Prezzo: " + prodotto.prezzo + "€");
                writer.Close();
            }

        }

        private void Cancellazione(string oggetto)
        {

            using (StreamReader reader = File.OpenText(fileName))
            {
                string s = "";
                while ((s = reader.ReadLine()) != null)
                {
                    string[] splittaggio1 = s.Split(';');
                    string[] splittaggio2 = splittaggio1[0].Split(' ');
                    using (StreamWriter writer = new StreamWriter(@"appoggio.csv", append: true))
                    {
                        if (oggetto != splittaggio2[1])
                        {
                            writer.WriteLine(s);
                        }
                        writer.Close();
                    }
                }
                reader.Close();
            }
            File.Delete(@"testo.csv");
            File.Move(@"appoggio.csv", @"testo.csv");
            listView1.Clear();
            AperturaFile();
        }
        private void AperturaFile()
        {
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    listView1.Items.Add(s);
                }
                sr.Close();
            }
        }
        private void Modifica(string parola, string oggetto)
        {

            using (StreamReader reader = File.OpenText(fileName))
            {
                string s = "";
                while ((s = reader.ReadLine()) != null)
                {
                    string[] splittaggio1 = s.Split(';');
                    string[] splittaggio2 = splittaggio1[0].Split(' ');
                    using (StreamWriter writer = new StreamWriter(@"appoggio.csv", append: true))
                    {
                        if (parola == splittaggio2[1])
                        {
                            writer.WriteLine(splittaggio2[0] + " " + oggetto + ";" + splittaggio1[1]);
                        }
                        else
                        {
                            writer.WriteLine(s);
                        }
                        writer.Close();
                    }
                }
                reader.Close();
            }
            File.Delete(@"testo.csv");
            File.Move(@"appoggio.csv", @"testo.csv");
            listView1.Clear();
            AperturaFile();
        }
    }
}
#endregion 
