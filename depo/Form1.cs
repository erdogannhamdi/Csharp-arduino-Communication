using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;
namespace depo
{
    public partial class Form1 : Form
    {
        String[] portlar = SerialPort.GetPortNames();
        int sayac1 = 0, sayac2 = 0, sayac3 = 0, saniyeSari = 0, saniyeKirmizi = 0, saniyeYesil = 0;
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti =new SqlConnection("Data Source=HAMDI\\SQLEXPRESS;Initial Catalog=ledKontrol;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            String kayit="insert into led(renk,durum,sure) values(@renk,@durum,@sure)";
            SqlCommand komut1 = new SqlCommand(kayit,baglanti);
            komut1.Parameters.AddWithValue("@renk", "Sarı");
            komut1.Parameters.AddWithValue("@durum", label1.Text);
            komut1.Parameters.AddWithValue("@sure", saniyeSari);
            komut1.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand(kayit, baglanti);
            komut2.Parameters.AddWithValue("@renk", "Kırmızı");
            komut2.Parameters.AddWithValue("@durum", label2.Text);
            komut2.Parameters.AddWithValue("@sure", saniyeKirmizi);
            komut2.ExecuteNonQuery();

            SqlCommand komut3 = new SqlCommand(kayit, baglanti);
            komut3.Parameters.AddWithValue("@renk", "Yeşil");
            komut3.Parameters.AddWithValue("@durum", label3.Text);
            komut3.Parameters.AddWithValue("@sure", saniyeYesil);
            komut3.ExecuteNonQuery();

            baglanti.Close();

            label1.Text = "Aktif Değil !";
            label2.Text = "Aktif Değil !";
            label3.Text = "Aktif Değil !";
            foreach (String port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;
            }

            timer4.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen == false)
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen==true)
            {
                serialPort1.DiscardInBuffer();
                serialPort1.Close();
            }
            timer4.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen==true)
            {
                if(sayac1==0)
                {
                    timer1.Start();
                    byte[] data = BitConverter.GetBytes(11);
                    serialPort1.Write(data, 0, 4);
                    sayac1++;
                    button3.Text = "Sarı Söndür";

                    baglanti.Open();
                    String kayit = "update led set durum=@durum where renk='Sarı'";
                    SqlCommand komut = new SqlCommand(kayit, baglanti);
                    komut.Parameters.AddWithValue("@durum", "Aktif !");
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                }
                else
                {
                    timer1.Stop();
                    byte[] data = BitConverter.GetBytes(12);
                    serialPort1.Write(data, 0, 4);
                    sayac1=0;
                    button3.Text = "Sarı Yak";
                    label1.Text = saniyeSari.ToString() + " Saniye Aktif Kaldı.";

                    baglanti.Open();
                    String kayit = "update led set durum=@durum,sure=@sure where renk='Sarı'";
                    SqlCommand komut = new SqlCommand(kayit, baglanti);
                    komut.Parameters.AddWithValue("@durum", "Aktif Değil !");
                    komut.Parameters.AddWithValue("@sure", label1.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                }
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                if (sayac2 == 0)
                {
                    timer2.Start();
                    byte[] data = BitConverter.GetBytes(13);
                    serialPort1.Write(data, 0, 4);
                    sayac2++;
                    button4.Text = "Kırmızı Söndür";

                    baglanti.Open();
                    String kayit = "update led set durum=@durum where renk='Kırmızı'";
                    SqlCommand komut = new SqlCommand(kayit, baglanti);
                    komut.Parameters.AddWithValue("@durum", "Aktif !");
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                }
                else
                {
                    timer2.Stop();
                    byte[] data = BitConverter.GetBytes(14);
                    serialPort1.Write(data, 0, 4);
                    sayac2 = 0;
                    button4.Text = "Kırmızı Yak";
                    label2.Text = saniyeKirmizi.ToString() + " Saniye Aktif Kaldı.";

                    baglanti.Open();
                    String kayit = "update led set durum=@durum,sure=@sure where renk='Kırmızı'";
                    SqlCommand komut = new SqlCommand(kayit, baglanti);
                    komut.Parameters.AddWithValue("@durum", "Aktif Değil !");
                    komut.Parameters.AddWithValue("@sure", label2.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                if (sayac3 == 0)
                {
                    timer3.Start();
                    byte[] data = BitConverter.GetBytes(15);
                    serialPort1.Write(data, 0, 4);
                    sayac3++;
                    button5.Text = "Yeşil Söndür";

                    baglanti.Open();
                    String kayit = "update led set durum=@durum where renk='Yeşil'";
                    SqlCommand komut = new SqlCommand(kayit, baglanti);
                    komut.Parameters.AddWithValue("@durum", "Aktif !");
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                }
                else
                {
                    timer3.Stop();
                    byte[] data = BitConverter.GetBytes(16);
                    serialPort1.Write(data, 0, 4);
                    sayac3 = 0;
                    button5.Text = "Yeşil Yak";
                    label3.Text = saniyeYesil.ToString() + " Saniye Aktif Kaldı.";

                    baglanti.Open();
                    String kayit = "update led set durum=@durum,sure=@sure where renk='Yeşil'";
                    SqlCommand komut = new SqlCommand(kayit, baglanti);
                    komut.Parameters.AddWithValue("@durum", "Aktif Değil !");
                    komut.Parameters.AddWithValue("@sure", label3.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                }

            }
        }
        private void listele()
        {
            listView1.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select renk from led where durum='Aktif !'", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                listView1.Items.Add(reader["renk"].ToString());
            }
            baglanti.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled=true;
            saniyeSari++;
            label1.Text = saniyeSari.ToString() + " Saniyedir Aktif";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.DiscardInBuffer();
                serialPort1.Close();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 1000;
            timer2.Enabled = true;
            saniyeKirmizi++;
            label2.Text = saniyeKirmizi.ToString() + " Saniyedir Aktif";           
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Interval = 1000;
            timer3.Enabled = true;
            saniyeYesil++;
            label3.Text = saniyeYesil.ToString() + " Saniyedir Aktif";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Interval = 100;
            timer4.Enabled = true;
            listele();
        }
    }
}
