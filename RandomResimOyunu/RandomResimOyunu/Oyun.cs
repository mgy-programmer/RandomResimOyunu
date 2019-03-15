using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GorselProje
{
    public partial class Oyun : Form
    {
        public Oyun()
        {
            InitializeComponent();
        }

        private void btn_kapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_geri_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void Oyun_Load(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-Q9PA9HF; Initial Catalog=GorselProjesi; Integrated Security=true");
            getir();
        }
        public SqlConnection baglanti;
        private void btn_yenile_Click(object sender, EventArgs e)
        {
            Oyun oyun = new Oyun();
            this.Hide();
            oyun.Show();
        }
        string aciklama, aciklama2, aciklama3, aciklama4;

        private void image_3_Click(object sender, EventArgs e)
        {
            if (aciklama3 == lbl_soru.Text)
            {
                group_3.BackColor = Color.Green;
            }
            else
            {
                group_3.BackColor = Color.Red;
            }
        }

        private void image_4_Click(object sender, EventArgs e)
        {
            if (aciklama4 == lbl_soru.Text)
            {
                group_4.BackColor = Color.Green;
            }
            else
            {
                group_4.BackColor = Color.Red;
            }
        }

        private void image_2_Click(object sender, EventArgs e)
        {
            if (aciklama2 == lbl_soru.Text)
            {
                group_2.BackColor = Color.Green;
            }
            else
            {
                group_2.BackColor = Color.Red;
            }
        }

        void getir()
        {
            string[] dizi = Form1.sumDizi;
            
            int[] size = new int[dizi.Length];
            for(int i = 0; i < dizi.Length; i++)
            {
                size[i] = int.Parse(dizi[i]);
            }
            Random rd = new Random();
            int say1, say2, say3, say4;
            say1 = size[rd.Next(1, size.Length)];
            say2 = size[rd.Next(1, size.Length)];
            
            while (true)
            {
                if (say1 == say2)
                {
                    say2 = size[rd.Next(1, size.Length)];
                    continue;
                }
                else
                {
                    say3 = size[rd.Next(1, size.Length)];
                    if (say1 == say3 || say2 == say3)
                    {
                        continue;
                    }
                    else
                    {
                        say4 = size[rd.Next(1, size.Length)];
                        if (say1 == say4 || say2 == say4 || say3 == say4)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM resim where id=@id1", baglanti);
            cmd.Parameters.AddWithValue("@id1", say1);
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM resim where id=@id2", baglanti);
            cmd2.Parameters.AddWithValue("@id2", say2);
            SqlCommand cmd3 = new SqlCommand("SELECT * FROM resim where id=@id3", baglanti);
            cmd3.Parameters.AddWithValue("@id3", say3);
            SqlCommand cmd4 = new SqlCommand("SELECT * FROM resim where id=@id4", baglanti);
            cmd4.Parameters.AddWithValue("@id4", say4);
            SqlDataReader dr = cmd.ExecuteReader();
            aciklama = " ";
            string resim_yolu = " ";
            while (dr.Read())
            {
                resim_yolu = dr["resim_yolu"].ToString();
                aciklama = dr["aciklama"].ToString();
            }
            dr.Close();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            aciklama2 = " ";
            string resim_yolu2 = " ";
            while (dr2.Read())
            {
                resim_yolu2 = dr2["resim_yolu"].ToString();
                aciklama2 = dr2["aciklama"].ToString();
            }
            dr2.Close();
            SqlDataReader dr3 = cmd3.ExecuteReader();
            aciklama3 = " ";
            string resim_yolu3 = " ";
            while (dr3.Read())
            {
                resim_yolu3 = dr3["resim_yolu"].ToString();
                aciklama3 = dr3["aciklama"].ToString();
            }
            dr3.Close();
            SqlDataReader dr4 = cmd4.ExecuteReader();
            aciklama4 = " ";
            string resim_yolu4 = " ";
            while (dr4.Read())
            {
                resim_yolu4 = dr4["resim_yolu"].ToString();
                aciklama4 = dr4["aciklama"].ToString();
            }
            dr4.Close();

            int sayi = rd.Next(0, 4);
            if(sayi == 0)
            {
                lbl_soru.Text = aciklama;
            }
            else
            {
                if (sayi == 1)
                {
                    lbl_soru.Text = aciklama2;
                }
                else
                {
                    if (sayi == 2)
                    {
                        lbl_soru.Text = aciklama3;
                    }
                    else
                    {
                        if (sayi == 3)
                        {
                            lbl_soru.Text = aciklama4;
                        }
                    }
                }
            }
            while (true)
            {
                Baslat:
                if (image_1.ImageLocation == null)
                {
                    image_1.ImageLocation = resim_yolu;
                    goto Baslat;
                }
                else
                {
                    if (image_2.ImageLocation == null)
                    {
                        image_2.ImageLocation = resim_yolu2;
                        goto Baslat;
                    }
                    else
                    {
                        if (image_3.ImageLocation == null)
                        {
                            image_3.ImageLocation = resim_yolu3;
                            goto Baslat;
                        }
                        else
                        {
                            if (image_4.ImageLocation == null)
                            {
                                image_4.ImageLocation = resim_yolu4;
                                break;
                            }
                        }
                    }
                }
            }

            baglanti.Close();
        }

        private void image_1_Click(object sender, EventArgs e)
        {
            if(aciklama == lbl_soru.Text)
            {
                group_1.BackColor = Color.Green;
            }
            else
            {
                group_1.BackColor = Color.Red;
            }
        }
    }
}
