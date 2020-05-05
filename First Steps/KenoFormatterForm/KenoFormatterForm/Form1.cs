using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KenoFormatterForm
{
    public partial class Form1 : Form
    {
        private String text;
        private readonly String KENO_PREFIX = "A KENO eheti nyerőszámai: "; 
        private readonly String KENO_POSTFIX = " //mti";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            if (text.Length == 0) { MessageBox.Show("no input found!"); }
            else { 
                var response = MessageBox.Show(KENO_PREFIX + text + KENO_POSTFIX, "", MessageBoxButtons.OKCancel);

                //var response = MessageBox.Show("...\r\n\r\nCopy to clipboard?", "", MessageBoxButtons.YesNo);
                if (response == DialogResult.OK)
                {
                    Clipboard.SetText(KENO_PREFIX + text + KENO_POSTFIX);
                    ChangeToDefaultPosition();
                }
                else { ChangeBack(); }

            }
           
        }

        private void ChangeBack()
        {
            richTextBox1.Text = "";
            text = "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length == 0) { Console.WriteLine("no input"); }
            else {text = Format(richTextBox1.Text); }
        }

        private void ChangeToDefaultPosition()
        {
            richTextBox1.Text = "Text copied to the clipboard!";
            text = "";
        }

        private String Format(String textToFormat) {
            return Util.KenoFormatterUtilFunc(textToFormat);
        }


    }
}
