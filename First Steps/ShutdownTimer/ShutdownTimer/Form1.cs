using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutdownTimer
{
    public partial class Form1 : Form{

        private int secondsFromShutdown;
        private DateTime start = DateTime.Now;
        private System.Windows.Forms.Timer timer2;
        private int counter;



        public Form1(){
            InitializeComponent();
            SetMyCustomFormat();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e){
            DateTime end = dateTimePicker1.Value;
            var resultMinutes = (int)end.Subtract(start).TotalMinutes;
            secondsFromShutdown = resultMinutes * 60;
            counter = secondsFromShutdown;



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetMyCustomFormat()
        {
            // Set the Format type and the CustomFormat string.
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM dd, yyyy - HH : mm ";
        }

        private void ShutdownConfig(int value) {
            Console.WriteLine(value);
            Process.Start("shutdown", "/s /t "+value);

        }


        private void button1_Click(object sender, EventArgs e){
            if (secondsFromShutdown == 0){
                string message = "You did not set a Date Value";
                string caption = "Error Detected in Input";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
              

            }
            else {

                ShutdownConfig(secondsFromShutdown); 

                timer2 = new System.Windows.Forms.Timer();
                timer2.Tick += new EventHandler(timer1_Tick_1);
                timer2.Interval = 1000; // 1 second
                timer2.Start();
                label3.Text = FormatSecToDate();

            }
                
        }

        private string FormatSecToDate() {
            TimeSpan t = TimeSpan.FromSeconds(counter);

            string answer = t.ToString(@"hh\:mm\:ss");
            return answer;


        }


        private void ResetShutdown() {

            Process.Start("shutdown", "-a");

        }

        private void button2_Click(object sender, EventArgs e) {ResetShutdown(); }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0)
                timer1.Stop();
            label3.Text = FormatSecToDate();
            Console.WriteLine(counter);
        }

  

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
