using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace __MayinTarlisi__
{
    public partial class MayinTarlasiOyunEkrani : Form
    {

        public MayinTarlasiOyunEkrani()
        {
            InitializeComponent();
        }

        #region Global Değişkenler
        public int SatirSayisi;
        public int SutunSayisi;
        public bool kolay;
        public bool orta;
        public bool zor;
        public string isim = string.Empty;


        Random rnd = new Random();
        Button[] butonDizisi;
        List<Button> mayinList = new List<Button>();
        public int toplamSkor;
        public int kolayGeriSayim = 30;
        public int ortaGeriSayim = 60;
        public int zorGeriSayim = 90;
        int kenarlardanBosluk = 20;
        int YukarıdanBosluk = 40;

        #endregion

        public void setMine()
        {
            this.Controls.Clear();
            //if (kolay == true)
            //{
            //    this.BackColor = Color.OrangeRed;
            //}
            toplamSkor = 0;          
            this.Height = 0;
            this.Width = 0;
            kolayGeriSayim = 30;
            ortaGeriSayim = 60;
            zorGeriSayim = 90;
            
            timer1.Start();
            
          
            int butonSayisi = SatirSayisi * SutunSayisi;
            butonDizisi = new Button[SatirSayisi * SutunSayisi];

            // Skorun tutulacağı ve geri sayımın yapılacağı dinamik label'lar oluşturuluyor.
            Label lblSkor = new Label();
            lblSkor.Name = "lblSkor";
            lblSkor.Location = new Point(5, 5);
            this.Controls.Add(lblSkor);
            Label lblGeriSayim = new Label();
            lblGeriSayim.Name = "lblGeriSayim";
            lblGeriSayim.Location = new Point(130, 5);
            this.Controls.Add(lblGeriSayim); 
            
            // Dinamik olarak butonlar oluşturulup tutulacağı diziye atılıyor. Butonların form üzerindeki lokasyonları giriliyor.
            for (int i = 0; i < butonSayisi; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(43, 39);
                btn.Name = "btn_" + i.ToString();
                btn.Location = new Point(kenarlardanBosluk, YukarıdanBosluk);
                
                kenarlardanBosluk += btn.Width - 1;
                if (kenarlardanBosluk > (btn.Width - 1) * SutunSayisi - 1)
                {
                    kenarlardanBosluk = 20;
                    YukarıdanBosluk += btn.Height;
                }
                butonDizisi[i] = btn;
                
                btn.Click += Butonlar_Click;
            }
            #region Mayın Atama
            // Seçilen zorluk seviyesine göre random şekilde mayınlar oluşturulup mayın dizisine atılıyor.
            if (kolay == true)
            {
                this.BackColor = Color.OrangeRed;
                for (int i = 0; i < 5; i++)
                {
                    int rndmayin = rnd.Next(0, butonDizisi.Length);

                    if (mayinList.Contains(butonDizisi[rndmayin]))
                    {
                        i--;
                    }
                    else
                    {
                        mayinList.Add(butonDizisi[rndmayin]);
                    }
                }
            }
            if (orta == true)
            {
                this.BackColor = Color.DarkGray;
                for (int i = 0; i < 10; i++)
                {
                    int rndmayin = rnd.Next(0, butonDizisi.Length);

                    if (mayinList.Contains(butonDizisi[rndmayin]))
                    {
                        i--;
                    }
                    else
                    {
                        mayinList.Add(butonDizisi[rndmayin]);
                    }
                }
            }
            if (zor == true)
            {
                this.BackColor = Color.DarkRed;
                for (int i = 0; i < 15; i++)
                {
                    int rndmayin = rnd.Next(0, butonDizisi.Length);

                    if (mayinList.Contains(butonDizisi[rndmayin]))
                    {
                        i--;
                    }
                    else
                    {
                        mayinList.Add(butonDizisi[rndmayin]);
                    }
                }
            } 
            #endregion
            this.Height = YukarıdanBosluk + 50;
            this.Width = butonDizisi[1].Width * SutunSayisi + kenarlardanBosluk * 3;
        }

        private void MayinTarlasiOyunEkrani_Load(object sender, EventArgs e)
        {
            setMine();
        }

        private void Butonlar_Click(object sender, EventArgs e)
        {
            Button tiklanan = (Button)sender;
            timer2.Start();
            #region Kolay
            if (kolay == true)
            {
                if (!mayinList.Contains(tiklanan))
                {
                    tiklanan.Text = 5.ToString();
                    tiklanan.BackColor = Color.Blue;
                    tiklanan.Enabled = false;
                    toplamSkor += Convert.ToInt32(tiklanan.Text);
                }
                else
                {
                    skorKaydet();
                    PatlamaEfekti();
                    for (int j = 0; j < butonDizisi.Length; j++)
                    {
                        if (mayinList.Contains(butonDizisi[j]))
                        {
                            butonDizisi[j].Text = null;
                            butonDizisi[j].BackgroundImageLayout = ImageLayout.Stretch;
                            string myinpath = (Application.StartupPath + "/photo/photo.jpg");
                            butonDizisi[j].BackgroundImage = Image.FromFile(myinpath);
                            timer2.Stop();
                            timer3.Start();
                        }
                    }
                    DialogResult result = MessageBox.Show(toplamSkor.ToString() + " puan yaptın !\nTekrar oynamak ister misin ?", "GAME OVER", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        timer3.Stop();
                        setMine();
                        GirisEfekti();
                    }
                    else
                    {
                        timer3.Stop();
                        this.Close();
                    }
                }
            }
            #endregion

            #region Orta
            if (orta == true)
            {
                if (!mayinList.Contains(tiklanan))
                {
                    tiklanan.Text = 10.ToString();
                    tiklanan.BackColor = Color.Blue;
                    tiklanan.Enabled = false;
                    toplamSkor += Convert.ToInt32(tiklanan.Text);
                }
                else
                {
                    skorKaydet();
                    PatlamaEfekti();
                    for (int j = 0; j < butonDizisi.Length; j++)
                    {
                        if (mayinList.Contains(butonDizisi[j]))
                        {
                            butonDizisi[j].Text = null;
                            butonDizisi[j].BackgroundImageLayout = ImageLayout.Stretch;
                            string myinpath = (Application.StartupPath + "/photo/photo.jpg");
                            butonDizisi[j].BackgroundImage = Image.FromFile(myinpath);
                            timer2.Stop();
                            timer3.Start();
                        }
                    }
                    DialogResult result = MessageBox.Show(toplamSkor.ToString() + " puan yaptın !\nTekrar oynamak ister misin ?", "GAME OVER", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        timer3.Stop();
                        setMine();
                        GirisEfekti();
                    }
                    else
                    {
                        timer3.Stop();
                        this.Close();
                    }
                }
            }
            #endregion

            #region Zor
            if (zor == true)
            {
                if (!mayinList.Contains(tiklanan))
                {
                    tiklanan.Text = 15.ToString();
                    tiklanan.BackColor = Color.Blue;
                    tiklanan.Enabled = false;
                    toplamSkor += Convert.ToInt32(tiklanan.Text);
                }
                else
                {
                    skorKaydet();
                    PatlamaEfekti();
                    for (int j = 0; j < butonDizisi.Length; j++)
                    {
                        if (mayinList.Contains(butonDizisi[j]))
                        {
                            butonDizisi[j].Text = null;
                            butonDizisi[j].BackgroundImageLayout = ImageLayout.Stretch;
                            string myinpath = (Application.StartupPath + "/photo/photo.jpg");
                            butonDizisi[j].BackgroundImage = Image.FromFile(myinpath);
                            timer2.Stop();
                            timer3.Start();

                        }
                    }
                    DialogResult result = MessageBox.Show(toplamSkor.ToString() + " puan yaptın !\nTekrar oynamak ister misin ?", "GAME OVER", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        timer3.Stop();
                        setMine();
                        GirisEfekti();
                    }
                    else
                    {
                        timer3.Stop();
                        this.Close();
                    }
                }
            } 
            #endregion
            // Skorlar skor label'ına yazdırılıyor
            foreach (Control item in this.Controls)
            {
                if (item is Label && item.Name == "lblSkor")
                {                                       
                    item.Text = "Skorlar:" + toplamSkor.ToString();    
                }
            }
        }

        public void skorKaydet()
        {
            string[] eskiSkorlar = File.ReadAllLines(@"skorTablosu.txt", System.Text.Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            foreach (string skor in eskiSkorlar)
            {
                sb.AppendLine(skor);
            }
            sb.AppendLine(isim + " = " + toplamSkor.ToString());

            File.WriteAllText(@"skorTablosu.txt", sb.ToString());
        }

        public void PatlamaEfekti()
        {
            SoundPlayer bomba = new SoundPlayer();
            string path = Application.StartupPath + "/KotuGulus.wav";
            bomba.SoundLocation = path;
            bomba.Play();
        }
        public void GirisEfekti()
        {
            SoundPlayer giris = new SoundPlayer();
            string path = Application.StartupPath + "/girisSound.wav";
            giris.SoundLocation = path;
            giris.Play();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Formun efektif olarak açılması burada gerçekleşiyor.
            this.Height = 0;
            this.Width = 0;
            while (this.Height <= YukarıdanBosluk + 50)
            {                
                this.Height++;
            }
            YukarıdanBosluk = 40; // bu değişken arttığı için burda default olarak belirlenen değerine geri döndürüyoruz.
            while (this.Width <= butonDizisi[1].Width * SutunSayisi + kenarlardanBosluk * 3)
            {                
                this.Width++;
            }       
            this.Controls.AddRange(butonDizisi);    
            timer1.Stop();          
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            // Zorluk seviyesine göre belirlenen süreden geri sayım burada gerçekleşip dinamik olarak oluşturulmuş geri sayım label'ına yazdırılıyor.
            if (kolay == true)
            {
                kolayGeriSayim--;
                foreach (Control item in this.Controls)
                {
                    if (item is Label && item.Name == "lblGeriSayim")
                    {                            
                            item.Text = "Kalan Zamanınız:" + kolayGeriSayim.ToString();
                        if (kolayGeriSayim < 0)
                        {
                            item.Text = "Zaman Doldu!!";
                        }                                                 
                    }
                    if (kolayGeriSayim == -1)
                    {
                        timer2.Stop();
                        timer3.Start();
                        for (int i = 0; i < mayinList.Count; i++)
                        {
                            mayinList[i].BackgroundImage = Image.FromFile("c:/users/cihan/documents/visual studio 2015/projects/__mayintarlisi__/__mayintarlisi__/photo/photo.jpg");
                        }                        
                        DialogResult result = MessageBox.Show(toplamSkor.ToString() + " puan yaptın !\nTekrar oynamak ister misin ?", "Süreniz Sona Erdi!", MessageBoxButtons.YesNo);                        
                        if (result == DialogResult.Yes)
                        {
                            timer3.Stop();
                            setMine();
                            GirisEfekti();
                            
                        }
                        else
                        {
                            timer3.Stop();
                            this.Close();
                        }
                        return;  
                    }                                                      
                }

            }            
            if (orta == true)
            {
                ortaGeriSayim--;
                foreach (Control item in this.Controls)
                {
                    if (item is Label && item.Name == "lblGeriSayim")
                    {
                        item.Text = "Kalan Zamanınız:" + ortaGeriSayim.ToString();
                        if (ortaGeriSayim < 0)
                        {
                            item.Text = "Zaman Doldu!!";
                        }
                    }
                    if (ortaGeriSayim == -1)
                    {
                        timer3.Start();
                        for (int i = 0; i < mayinList.Count; i++)
                        {
                            mayinList[i].BackgroundImage = Image.FromFile("c:/users/cihan/documents/visual studio 2015/projects/__mayintarlisi__/__mayintarlisi__/photo/photo.jpg");
                        }
                        DialogResult result = MessageBox.Show(toplamSkor.ToString() + " puan yaptın !\nTekrar oynamak ister misin ?", "Süreniz Sona Erdi!", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            timer3.Stop();
                            setMine();
                            GirisEfekti();
                        }
                        else
                        {
                            timer3.Stop();
                            this.Close();
                        }
                    }
                }
            }
            if (zor == true)
            {
                zorGeriSayim--;
                foreach (Control item in this.Controls)
                {
                    if (item is Label && item.Name == "lblGeriSayim")
                    {
                        item.Text = "Kalan Zamanınız:" + zorGeriSayim.ToString();
                        if (zorGeriSayim < 0)
                        {
                            item.Text = "Zaman Doldu!!";
                        }
                    }
                    if (zorGeriSayim == -1)
                    {
                        timer3.Start();
                        for (int i = 0; i < mayinList.Count; i++)
                        {
                            mayinList[i].BackgroundImage = Image.FromFile("c:/users/cihan/documents/visual studio 2015/projects/__mayintarlisi__/__mayintarlisi__/photo/photo.jpg");
                        }

                        DialogResult result = MessageBox.Show(toplamSkor.ToString() + " puan yaptın !\nTekrar oynamak ister misin ?", "Süreniz Sona Erdi!", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            timer3.Stop();
                            setMine();
                            GirisEfekti();
                        }
                        else
                        {
                            timer3.Stop();
                            this.Close();
                        }
                    }
                }
            }

        }
        int zaman =0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            zaman++;
            if (zaman > 1)
            {
                this.BackColor = Color.Black;
                foreach (Control item in this.Controls)
                {
                    if (item is Label)
                    {
                        item.ForeColor = Color.White;
                    }
                }
            }
        }
    }     
}


       