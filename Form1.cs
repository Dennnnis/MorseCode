using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace MorseCode
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> ToText = new Dictionary<string, string>();
        Dictionary<char, string> ToMorse = new Dictionary<char, string>();

        public Form1()
        {
            string[] lines = File.ReadAllLines("Translation.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split('>');
                ToMorse.Add(parts[1][0], parts[0]);
                ToText.Add(parts[0], parts[1]);
            }

            ToMorse.Add('\n',$"{Environment.NewLine}");
            ToMorse.Add('\r', "");
            ToText.Add("~", Environment.NewLine);
            ToText.Add("","");

            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //Text input/output
        {
            if (!textBox1.Focused) return;
            string buffer = string.Empty;
            foreach (char c in textBox1.Text.ToUpper())
                buffer += (ToMorse.ContainsKey(c) ? ToMorse[c] : "?") + (c != '\n' && c != '\r'? " " : "");
            textBox2.Text = buffer.Trim(' ');
        }

        private void textBox2_TextChanged(object sender, EventArgs e) //Morse code input/output
        {
            if (!textBox2.Focused) return;
            string buffer = string.Empty;
            foreach (string m in textBox2.Text.Replace($"{Environment.NewLine}"," ~ ").Split(' '))
                buffer += ToText.ContainsKey(m) ? ToText[m] : "?";
            textBox1.Text = buffer;
        }
    }
}