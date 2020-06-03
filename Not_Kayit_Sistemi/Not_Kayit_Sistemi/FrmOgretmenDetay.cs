using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DT8N7BH;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) values (@P1,@P2,@P3)", baglanti);
            komut.Parameters.AddWithValue("@P1",MaskedTxtNumara.Text);
            komut.Parameters.AddWithValue("@P2", TxtAd.Text);
            komut.Parameters.AddWithValue("@P3", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            MaskedTxtNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            int VerSay = dataGridView1.RowCount - 2;
            int kalan = 0;
            int gecen = 0;
            s1 = Convert.ToDouble(TxtSinav1.Text);
            s2 = Convert.ToDouble(TxtSinav2.Text);
            s3 = Convert.ToDouble(TxtSinav3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text = ortalama.ToString();

            if(ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            for (int a = 0; a <= VerSay; a++)

            {

                if (dataGridView1.Rows[a].Cells[8].Value.ToString() == "False")

                    kalan++;

                else gecen++;

            }
            LblGecenSayisi.Text = gecen.ToString();

            LblKalanSayisi.Text = kalan.ToString();

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtSinav1.Text);
            komut.Parameters.AddWithValue("@P2", TxtSinav2.Text);
            komut.Parameters.AddWithValue("@P3", TxtSinav3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", MaskedTxtNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);


        }
    }
}
