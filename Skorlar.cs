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

namespace __MayinTarlisi__
{
    public partial class Skorlar : Form
    {
        public Skorlar()
        {
            InitializeComponent();
        }

        string[] skor = File.ReadAllLines(@"skorTablosu.txt", System.Text.Encoding.UTF8);

        private void Skorlar_Load(object sender, EventArgs e)
        {                       
            foreach (string item in skor)
            {
                listBox1.Items.Add(item);       
            }                        
        }
    }
}
