using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace ExcelApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double kayit = 0; //OTURUM KAYIT SAYISI
        OleDbConnection baglanti = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =C:\Users\casper\Desktop\exceldeneme\ExcelApp\veriler.xlsx; Extended Properties = 'Excel 12.0 Xml;HDR=YES;'");
        void Veriler() 
        {
            //EXCEL BAĞLANTISI KURULUYOR VE DATAGRİDWİEW E AKTARILIYOR
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM[Sayfa1$]", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void BuyukHarf()
        {
            //EĞER YAZILAR KÜÇÜK HARFSE BÜYÜK HARFE ÇEVİRİYOR
            if (adSoyad.Text.Any(char.IsLower))
            {
                adSoyad.Text = adSoyad.Text.ToUpper();
            }
            if (kimeGeldi.Text.Any(char.IsLower))
            {
                kimeGeldi.Text = kimeGeldi.Text.ToUpper();
            }
            if (gorevliAdi.Text.Any(char.IsLower))
            {
                gorevliAdi.Text = gorevliAdi.Text.ToUpper();
            }
        }

        void TarihSaat()
        {
            //EĞER TARİH SAAT BOŞSA ŞUANKİ TARİH SAATİ EKLİYOR
            if (tarih.Text == null)
            {
                tarih.Text= DateTime.Now.ToString("MM.dd.yyyy");
            }

            if (cikisTarihi.Text == null)
            {
                cikisTarihi.Text = DateTime.Now.ToString("MM.dd.yyyy");
            }

            if (girisSaati.Text == null)
            {
                girisSaati.Text = DateTime.Now.ToString("HH:mm");
            }

            if (cikisSaati.Text == null)
            {
                cikisSaati.Text = DateTime.Now.ToString("HH:mm");
            }
        }

        private void button1_Click(object sender, EventArgs e) //KAYDET BUTONU
        {
            BuyukHarf();
            TarihSaat();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into [Sayfa1$] (TARİH,ZİYARETÇİ_ADI_SOYADI,GİRİŞ_SAATİ,GELDİĞİ_KİŞİ_BÖLÜM,ÇIKIŞ_TARİHİ,ÇIKIŞ_SAATİ,GÜVENLİK_GÖREVLİSİNİN_ADI_SOYADI,PLAKA) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);
            komut.Parameters.AddWithValue("@p1", tarih.Text);
            komut.Parameters.AddWithValue("@p2", adSoyad.Text);
            komut.Parameters.AddWithValue("@p3", girisSaati.Text);
            komut.Parameters.AddWithValue("@p4", kimeGeldi.Text);
            komut.Parameters.AddWithValue("@p5", cikisTarihi.Text);
            komut.Parameters.AddWithValue("@p6", cikisSaati.Text);
            komut.Parameters.AddWithValue("@p7", gorevliAdi.Text);
            komut.Parameters.AddWithValue("@p7", plaka.Text);
            //İLGİLİ SÜTUNLARA TXTLER EKLENİYOR.
            komut.ExecuteNonQuery();
            baglanti.Close();
            //BAĞLANTI KAPATILIP KAYIT EKLENDİ YAZISI ÇIKARILIYOR
            MessageBox.Show("Kayıt eklendi!");
            for(int i = 0; i < 1; i++)
            {
                kayit++;
                kayitNo.Text = kayit.ToString();
            }
            //KAYIT NO EKRANA GİRİLİYOR
            Veriler();     
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DATAGRİDDE TIKLADIKÇA TXTBOXLAR DOLUYOR
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            tarih.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            adSoyad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            girisSaati.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            kimeGeldi.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cikisTarihi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cikisSaati.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            gorevliAdi.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            plaka.Text= dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Veriler();
        }

        private void btnGuncelle_Click(object sender, EventArgs e) //GÜNCELLE BUTONU
        {
            BuyukHarf(); 
            TarihSaat();
            baglanti.Open();
            //GÜNCELLEMENİN TEK FARKI INSERT DEĞİL UPDATE YAPILIYOR İLGİLİ BİLGİLER
            OleDbCommand komut = new OleDbCommand("update [Sayfa1$] set TARİH=@p1,GİRİŞ_SAATİ=@p3,GELDİĞİ_KİŞİ_BÖLÜM=@p4,ÇIKIŞ_TARİHİ=@p5,ÇIKIŞ_SAATİ=@p6,GÜVENLİK_GÖREVLİSİNİN_ADI_SOYADI=@p7,PLAKA=@p8 where ZİYARETÇİ_ADI_SOYADI=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", tarih.Text);
            komut.Parameters.AddWithValue("@p3", girisSaati.Text);
            komut.Parameters.AddWithValue("@p4", kimeGeldi.Text);
            komut.Parameters.AddWithValue("@p5", cikisTarihi.Text);
            komut.Parameters.AddWithValue("@p6", cikisSaati.Text);
            komut.Parameters.AddWithValue("@p7", gorevliAdi.Text);
            komut.Parameters.AddWithValue("@p8", plaka.Text);
            komut.Parameters.AddWithValue("@p2", adSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            //BAĞLANTI KAPATILIYOR VE KAYIT GÜNCELLENDİ YAZILIYOR
            int a = 0;
            while (a <= 0)
            {
                MessageBox.Show("Kayıt güncellendi!");
                a++;
            }
            
            Veriler();
        }


    }
}