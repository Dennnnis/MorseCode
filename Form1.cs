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

namespace MorseCode
{
    public partial class Form1 : Form
    {
        public class Pair
        {
            public string morse;
            public string text;
        }
        List<Pair> morse = new List<Pair>();

        public Form1()
        {
            string[] lines = File.ReadAllLines("Translation.txt");
            foreach (string line in lines)
            {
                morse.Add(new Pair() { morse = line.Split(' ')[0], text = line.Split(' ')[1]}) ;
            }

            InitializeComponent();
        }

        //Text input/output
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!textBox1.Focused) { return; }
            string final = "";
            string buffer = "";
            foreach (char c in textBox1.Text)
            {
                if (c == ' ') { final += "/ ";  buffer = ""; continue; };
                buffer += c.ToString().ToUpper();

                foreach (Pair p in morse)
                {
                    if (p.text == buffer)
                    {
                        final += $"{p.morse} ";
                        buffer = "";
                        break;
                    }                
                }
            }
            textBox2.Text = final;
        }

        //Morse code input/output
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!textBox2.Focused) { return; }
            string debug = "";
            string final = "";
            string buffer = "";
            foreach (char c in textBox2.Text + " ")
            {
                debug += c;
                if (c == '/') { final += ' ';  }
                if (c == ' ')
                {
                    foreach (Pair p in morse)
                    {
                        if (p.morse == buffer)
                        {
                            debug += p.text;
                            final += p.text;
                        }
                    }
                    buffer = "";
                    continue;
                };
                buffer += c.ToString().ToUpper();
            }
            textBox1.Text = final;
        }
    }
}
