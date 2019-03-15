using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace GorselProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_sonraki_Click(object sender, EventArgs e)
        {
            Oyun oyun = new Oyun();
            oyun.Show();
            this.Hide();
        }

        private void btn_resimSec_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dosya = new OpenFileDialog();
                dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Tüm Dosyalar |*.*";
                dosya.Title = "Görsel Programlama";
                dosya.ShowDialog();
                string ResimNereyeKaydedilecek = @"C:\Users\PC\Desktop\GorselProje\GorselProje\bin\Debug\images";
                string hedef = Path.Combine(ResimNereyeKaydedilecek, Path.GetFileName(dosya.FileName));
                File.Copy(dosya.FileName, hedef);
                txt_resimYolu.Text = hedef;
                pictureBox1.ImageLocation = hedef;
            }
            catch (Exception)
            {
                MessageBox.Show("Resim mevcut olabilir, Lütfen başka bir resim seçin");
            }
        }

        private void btn_kapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public SqlConnection baglanti;
        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-Q9PA9HF; Initial Catalog=GorselProjesi; Integrated Security=true");
            Getir();
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO resim (resim_yolu,aciklama) VALUES (@resim,@aciklama)", baglanti);
                cmd.Parameters.AddWithValue("@resim", txt_resimYolu.Text);
                cmd.Parameters.AddWithValue("@aciklama", txt_aciklama.Text);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Eklendi");
                Getir();
                temizle();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata");
            }
        }
        public void temizle()
        {
            txt_aciklama.Clear();
            txt_resimYolu.Clear();
            pictureBox1.Image = null;
        }
        public void Getir()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM resim", baglanti);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            adp.Fill(dtable);
            dataGridView1.DataSource = dtable;
            baglanti.Close();
            size();
        }
        int sum;
        public static string[] sumDizi;
        void size()
        {
            sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    sum++;
                }
            }
            sumDizi = new string[sum];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    sumDizi[i] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dataGridView1.Rows[ind];
            txt_id.Text = selectedRows.Cells[0].Value.ToString();
            txt_resimYolu.Text = selectedRows.Cells[1].Value.ToString();
            txt_aciklama.Text = selectedRows.Cells[2].Value.ToString();
            btn_guncelle.Enabled = true;
            btn_sil.Enabled = true;
            btn_kaydet.Enabled = false;
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            temizle();
            btn_guncelle.Enabled = false;
            btn_kaydet.Enabled = true;
            btn_sil.Enabled = false;
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            btn_kaydet.Enabled = true;

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            SqlCommand cmd = new SqlCommand("UPDATE resim SET resim_yolu=@resim_yolu,aciklama=@aciklama WHERE id=@id ", baglanti);
            cmd.Parameters.AddWithValue("@id", txt_id.Text);
            cmd.Parameters.AddWithValue("@resim_yolu", txt_resimYolu.Text);
            cmd.Parameters.AddWithValue("@aciklama", txt_aciklama.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            Getir();
            MessageBox.Show("Güncellendi");

            temizle();
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM resim WHERE id=@id", baglanti);
                cmd.Parameters.AddWithValue("@id", txt_id.Text);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                Getir();
                MessageBox.Show("Silindi");
                string yol = txt_resimYolu.Text;
                if (File.Exists(yol))
                {
                    File.Delete(yol);
                }
                temizle();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata");
            }
            btn_kaydet.Enabled = true;
        }
    }
}
