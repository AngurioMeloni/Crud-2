
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
        public struct Prodotto //dichiarazione del prodotto
        {
            public string nome; //dichiarazione della stringa
            public float prezzo;//prezzo del prodotto
        }
        public string fileName = @"testo.csv"; //filename
        public Prodotto prodotto; //prodotto
        public int dim; //dimensione
        #endregion

        #region Pulsanti
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e) //funzioni del primo pulsante
        {
            prodotto.nome = textBox1.Text;
            prodotto.prezzo = float.Parse(textBox2.Text); //conversione del prodotto.prezzo in un float
            Scrittura(prodotto); //richiamo della funzione scrittura
            textBox1.Clear();//pulire la tetxbox 
            textBox2.Clear();//pulire la tetxbox 
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e) //funzioni secondo pulsante
        {
            listView1.Clear(); //pulire la listview
            AperturaFile(); // richiamo della funzione di apertura del file
        }

        private void button3_Click(object sender, EventArgs e) //funzioni terzo pulsante
        {
            listView1.Clear();//pulire la listview

            using (StreamWriter writer = new StreamWriter(fileName, append: false))//creazione del file
            {
                writer.Close();//chiusura del file
            }
        }
        private void button4_Click(object sender, EventArgs e) //funzioni quarto pulsante
        {
            string oggetto = textBox1.Text;//dichiarazione della stringa oggeto uguale alla textbox
            Cancellazione(oggetto);//richiamo della funzione di cancellazione
        }

        private void button5_Click(object sender, EventArgs e)//funzioni quinto pulsante
        {
            string parola = textBox1.Text;//dichiarazione della stringa parola uguale alla textbox
            string modificato = textBox3.Text;//dichiarazione della stringa modificato uguale alla textbox

            Modifica(parola.ToString(), modificato.ToString());//richiamo della funzione di modifica
        }
        #endregion

        #region funzioni 
        private void Scrittura(Prodotto prodotto) //funzione di scrittura
        {
            using (StreamWriter writer = new StreamWriter(fileName, append: true))// modifica del file
            {
                writer.WriteLine("Nome: " + prodotto.nome + ";" + " Prezzo: " + prodotto.prezzo + "€"); //scrittura del nome del prodotto e del prezzo
                writer.Close();//chiusura del file
            }

        }

        private void Cancellazione(string oggetto)//funzione di cancellazione
        {

            using (StreamReader reader = File.OpenText(fileName))//lettura del file
            {
                string s = ""; //dichiarazione della stinga s
                while ((s = reader.ReadLine()) != null)//ciclo while
                {
                    string[] split1 = s.Split(';');//dichiarazione dell'array di stringhe
                    string[] split2 = split1[0].Split(' ');//dichiarazione dell'array di stringhe
                    using (StreamWriter writer = new StreamWriter(@"appoggio.csv", append: true))// modifica del file
                    {
                        if (oggetto != split2[1]) //condizione
                        {
                            writer.WriteLine(s);//stampa della stringa s
                        }
                        writer.Close();//chiusura del file
                    }
                }
                reader.Close();//chiusura del file
            }
            File.Delete(@"testo.csv");//cancellazione del file di testo 
            File.Move(@"appoggio.csv", @"testo.csv");//spostamento dei file
            listView1.Clear();//pulire la listview
            AperturaFile();//richiamo alla funzione di apertura del file
        }
        private void AperturaFile()//funzione di apertura del file
        {
            using (StreamReader sr = File.OpenText(fileName))//lettura del file
            {
                string s = "";//dichiarazione della stringa s
                while ((s = sr.ReadLine()) != null)//ciclo while
                {
                    listView1.Items.Add(s);//aggiunta di elementi alla listview
                }
                sr.Close();//chiusura dello streamreader
            }
        }
        private void Modifica(string parola, string oggetto)//funzione di modifica
        {

            using (StreamReader reader = File.OpenText(fileName))//letturaq del file
            {
                string s = "";//dichiarazione della stinga s
                while ((s = reader.ReadLine()) != null)//ciclo while
                {
                    string[] split1 = s.Split(';');//dichiarazione dell'array di stringhe
                    string[] split2 = split1[0].Split(' ');//dichiarazione dell'array di stringhe
                    using (StreamWriter writer = new StreamWriter(@"appoggio.csv", append: true))// modifica del file
                    {
                        if (parola == split2[1])//condizione if
                        {
                            writer.WriteLine(split2[0] + " " + oggetto + ";" + split1[1]);//stampa 
                        }
                        else
                        {
                            writer.WriteLine(s);//stampa
                        }
                        writer.Close();//chiusura dello streamwriter
                    }
                }
                reader.Close();//chiusura dello streamreader
            }
            File.Delete(@"testo.csv");//cancellazione del file
            File.Move(@"appoggio.csv", @"testo.csv");//spostamento del file
            listView1.Clear();//pulire la listview
            AperturaFile();//richiamo alla funzione di apertura del file
        }
        #endregion 
    }
}

