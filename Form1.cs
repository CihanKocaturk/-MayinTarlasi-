using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace __MayinTarlisi__
{
    public partial class Form1 : Form
    {       
        public Form1()
        {
            InitializeComponent();
        }
        MayinTarlasiOyunEkrani mayinTarlasi = new MayinTarlasiOyunEkrani();
        Skorlar skorTablosu = new Skorlar();

        private void Form1_Load(object sender, EventArgs e)
        {
            AcilisEfekti();
        }
        public void AcilisEfekti()
        {
            SoundPlayer geriSayim = new SoundPlayer();
            string path = Application.StartupPath + "/geriSayim.wav";
            geriSayim.SoundLocation = path;
            geriSayim.Play();
        }
        public void GirisEfekti()
        {
            SoundPlayer giris = new SoundPlayer();
            string path = Application.StartupPath + "/girisSound.wav";
            giris.SoundLocation = path;
            giris.Play();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            GirisEfekti();

            if (!string.IsNullOrEmpty(textBox1.Text)&& !string.IsNullOrEmpty(textBox2.Text)&& !string.IsNullOrEmpty(textBox3.Text))
            { 
                mayinTarlasi.SatirSayisi = Convert.ToInt16(textBox2.Text);
                mayinTarlasi.SutunSayisi = Convert.ToInt32(textBox3.Text);
                mayinTarlasi.kolay = radioButton1.Checked;
                mayinTarlasi.orta = radioButton2.Checked;
                mayinTarlasi.zor = radioButton3.Checked;
                mayinTarlasi.isim = textBox1.Text;

                int SatirSayisi = mayinTarlasi.SatirSayisi;
                int SutunSayisi = mayinTarlasi.SutunSayisi;

                if (SatirSayisi > 4 & SatirSayisi < 11 & SutunSayisi > 4 & SutunSayisi < 11)
                {
                    mayinTarlasi.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Girdiğiniz satır ve sütun sayıları 5 ile 10 arasında olmalıdır.");
                }
            }
            else
            {
                MessageBox.Show("Tüm bilgilerinizi girmeden oyuna başlayamazsınız!");
            }
        }
            

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = e.KeyChar; // basılan tuşdaki karakteri a değişkenine atıyoruz
            switch (a)
            {
                case '1': // istediğimiz karakterleri belirtiyoruz...
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '0':
                /*case '\b':*/ // geri silme tuşunu da ekledik....
                    e.Handled = true; // burada istenen tuşlara izin veriliyor
                    break;
                default:
                    e.Handled = false; // burada ise diğer bütün tuşlar reddediliyor...
                    break;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = e.KeyChar; // basılan tuşdaki karakteri a değişkenine atıyoruz
            switch (a)
            {
                case '1': // istediğimiz karakterleri belirtiyoruz...
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '0':
                case '\b': // geri silme tuşunu da ekledik....
                    e.Handled = false; // burada istenen tuşlara izin veriliyor
                    break;
                default:
                    e.Handled = true; // burada ise diğer bütün tuşlar reddediliyor...
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skorTablosu.ShowDialog();
        }
    }
}
