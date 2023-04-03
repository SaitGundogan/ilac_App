using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using static SQLite_Class;
using static DB_data_Class;
using static Globals;
using BarcodeLib;

namespace ilac_bigileri_App
{
    public partial class Main : Form
    {
        private SQLiteManagement _sqlite;

        List<URUN_LISTESI> Urun_Liste;
        List<URUN_BILGILERI> Secilen_Urun;

        public static int bulunan_urun_kayit_sayisi = 0;
        public static int secilen_urun_satirno = 0;
        public static string secilen_urun_barkod="";
        public static string secilen_urun_ara="";
        public static string KUB_HTML_kod="";
        public static bool urun_secildi=false;
        public static byte[] urun_resim_bytes;
        public static bool secilen_urun_favori=false;

        int dgv_urun_listesi_secilen_satir = -32000;
        int dgv_urun_listesi_son_secilen_satir = -32000;
        int Aranan_urun_sayisi = 0;
        int Aranan_urun_sirasi = 0;
        string urun_ara_bosta_text = "URUN ARA...";
        List<string> lst_Aranan_barkodlar = new List<string>();

        public Main()
        {
            InitializeComponent();

        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            //this.StartPosition = FormStartPosition.Manual;
            //this.Left = 0;
            //this.Top = 0;
            //this.Width = 1366;
            //this.Height = 728;
            string Application_EXE_FilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //E:\ilac_Bilgileri C# SQLite Proje Gorevi\ilac_bilgileri_C#_App\ilac_bigileri_App\ilac_bigileri_App\bin\Debug\ilac_bilgileri_App.exe
            
            string strWorkPath = System.IO.Path.GetDirectoryName(Application_EXE_FilePath) + Convert.ToChar(92);
            //E:\ilac_Bilgileri C# SQLite Proje Gorevi\ilac_bilgileri_C#_App\ilac_bigileri_App\ilac_bigileri_App\bin\Debug


            if (!File.Exists(Database_Path + Database_FileName))
            {
                MessageBox.Show(Database_FileName + " veritabani dosyası  \r\n\r\n" + Convert.ToChar(34) + strWorkPath + Database_FileName + Convert.ToChar(34) + "\r\n\r\nkonumunda mevcut değil. Program kapatılacak.", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            _sqlite = new SQLiteManagement(connectionString);
        }

        private void frm_Main_Click(object sender, EventArgs e)
        {
            btn_HTML_Ac.Focus();
        }

        private void Secilen_urun_verileri_goster()
        {
            if (dgv_urun_listesi.SelectedRows == null || dgv_urun_listesi.SelectedRows.Count <= 0)
            {
                return;
            }

            dgv_urun_listesi_son_secilen_satir = dgv_urun_listesi_secilen_satir;
            dgv_urun_listesi_secilen_satir = dgv_urun_listesi.SelectedRows[0].Index;


            if (dgv_urun_listesi_secilen_satir == dgv_urun_listesi_son_secilen_satir)
            {
                return;
            }

            int Secilen_satir = dgv_urun_listesi.SelectedRows[0].Index;

            if (Secilen_satir >= 0)
            {
                grp_urun_bilgileri.Visible = true;
                urun_secildi = true;
                //img_urun_resim.Visible = true;
                //img_barkod_symbol.Visible = true;

                lbl_secilen_ilac_sirasi.Text = "";
                bulunan_urun_kayit_sayisi = dgv_urun_listesi.Rows.Count;
                secilen_urun_satirno = dgv_urun_listesi.SelectedRows[0].Index;


                secilen_urun_barkod = Urun_Liste[secilen_urun_satirno].IA_barkod;
                secilen_urun_ara = Urun_Liste[secilen_urun_satirno].Urun_ara;

                Urun_veri_goster_proc(secilen_urun_barkod);

            }



        }

        private void Urun_Ara_temizle()
        {
            dgv_urun_detaylari.Visible = false;
            lbl_secilen_ilac_sirasi.Visible = false;
            grp_urun_bilgileri.Visible = false;            
            urun_secildi = false;
            bulunan_urun_kayit_sayisi = 0;
            secilen_urun_satirno = 0;
            secilen_urun_barkod = "";
            secilen_urun_ara = "";
            KUB_HTML_kod = "";
            //SQL_Sorgu_Secim = 0;
            dgv_urun_listesi_secilen_satir = -32000;
            dgv_urun_listesi_son_secilen_satir = -32000;
    }

        private void Urun_liste_goster_proc()
        {
            Urun_Ara_temizle();
            Secilen_urun_verileri_temizle();

            string filter = txt_urun_ara.Text.Trim();
            bool filter_bos = string.IsNullOrEmpty(filter);
            if (filter_bos) { dgv_urun_listesi.DataSource = null; return; }


            //List<Ilac> ilaclar = _sqlite.ListIlac(filter);

            //Secilen_Urun = _sqlite.Urun_Bilgileri_Oku(filter);

            SQL_Sorgu_Secim = 1;
            Urun_Liste = _sqlite.Urun_Listesi_Oku(filter);
            dgv_urun_listesi.DataSource = null;
            dgv_urun_listesi.Columns.Clear();
            dgv_urun_listesi.DataSource = Urun_Liste;
            DB_data_Class.DataGridAyarlari_Urun_Listesi(dgv_urun_listesi);
        }

        private void Son_aranan_urunleri_listeye_ekle(string barkod_str)
        {
            Aranan_urun_sayisi += 1;
            Aranan_urun_sirasi = Aranan_urun_sayisi;
            //list_Aranan_barkodlar.Items.Add(barkod_str);
            lst_Aranan_barkodlar.Add(barkod_str);
        }


        private void img_urun_ara_Click(object sender, EventArgs e)
        {
            Urun_liste_goster_proc();
        }

        private void txt_urun_ara_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Urun_liste_goster_proc();
            }
                
        }


        private void Secilen_urun_verileri_temizle()
        {
            //lbl_secilen_ilac_sirasi.Visible = false;
            //pnl_ilac_adi.Visible = false;
            lbl_secilen_ilac_sirasi.Text = "";
            lbl_ilac_adi.Text = "";
            lbl_olcu.Text = "";
            lbl_ambalaj.Text = "";
            lbl_barkod.Text = "";
            lbl_Firma.Text = "";
            lbl_Fiyat.Text = "";
            lbl_Fiyat_tarih.Text = "";
            lbl_Kamu_fiyati.Text = "";
            lbl_Kamu_odenen.Text = "";
            lbl_Fiyat_Farki.Text = "";
            lbl_Depocu_fiyat.Text = "";
            lbl_imalatci_fiyati.Text = "";
            lbl_KDV_yuzde.Text = "";
            lbl_jenerikorijinal.Text = "";
            lbl_SGK_kodu.Text = "";
            lbl_ATC_kodu.Text = "";
            lbl_Recete.Text = "";
            lbl_Etkin_madde1.Text = "";
            lbl_Etkin_madde1_miktar.Text = "";
            lbl_Etkin_madde2.Text = "";
            lbl_Etkin_madde2_miktar.Text = "";

            KUB_HTML_kod = "";
            btn_HTML_Ac.Visible = false;
            btn_Favori_ekle_cikar.Visible = false;
            btn_Onceki_ilac.Visible = false;
            btn_Sonraki_ilac.Visible = false;
            //btn_HTML_Ac.Enabled = false;
            urun_secildi = false;
            secilen_urun_favori = false;
            btn_Favori_ekle_cikar.Image = Properties.Resources.favoriteBlack;

            lbl_barkod_symbol_str.Text = "";
            img_barkod_symbol.Visible = grp_urun_bilgileri.Visible;
            img_urun_resim.BackgroundImage = null;
            img_urun_resim.Visible = false;
            grp_urun_resim.Visible = false;
            img_barkod_symbol.Visible = false;
            grp_etkin_maddeler.Visible = false;
        }

        private void Urun_veri_goster_proc(string aranan_urun_barkod, bool Arananlar_Listesine_Ekle=true)
        {
            Secilen_urun_verileri_temizle();
            //lbl_secilen_ilac_sirasi.Text = (secilen_urun_satirno + 1) + ". ilaç, " + secilen_urun_ara + ", barkod: " + secilen_urun_barkod;

            //string filter = secilen_urun_barkod;
            string filter = aranan_urun_barkod;
            bool filter_bos = string.IsNullOrEmpty(filter);

            if (filter_bos)
            {
                //dgv_urun_detaylari.DataSource = null;
                return;
            }

            secilen_urun_barkod = filter;

            //List<Ilac> ilaclar = _sqlite.ListIlac(filter);
            //SQL_Sorgu_Secim = 1;

            Secilen_Urun = _sqlite.Urun_Bilgileri_Oku(filter);
            //dgv_urun_detaylari.DataSource = null;
            //dgv_urun_detaylari.Columns.Clear();
            //dgv_urun_detaylari.DataSource = Secilen_Urun;
            //DB_data_Class.DataGridAyarlari_Urun_Detaylari(dgv_urun_detaylari);

            lbl_ilac_adi.Text = Secilen_Urun[0].I_ILAC_ADI;
            lbl_olcu.Text = Secilen_Urun[0].IF_Olcu.ToUpper();
            lbl_ambalaj.Text = Secilen_Urun[0].IA_AMBALAJ.Replace("/kutu", " / kutu");
            lbl_barkod.Text = Secilen_Urun[0].IA_BARKOD;
            lbl_Firma.Text = Secilen_Urun[0].I_FIRMA;

            Barcode_to_image(lbl_barkod.Text);

            lbl_Fiyat.Text = string.Format("{0:0.00}",  Secilen_Urun[0].IA_FIYAT) + " TL";
            lbl_Fiyat_tarih.Text = "(" + string.Format("{0:0.00}", Secilen_Urun[0].IA_FIYATTARIH)  + ")";
            lbl_Kamu_fiyati.Text = string.Format("{0:0.00}", Secilen_Urun[0].IA_KAMUFIYATI) + " TL";
            lbl_Kamu_odenen.Text = string.Format("{0:0.00}", Secilen_Urun[0].IA_KAMUODENEN) + " TL";

            double temp_fiyat_farki = Secilen_Urun[0].IA_KAMUFIYATI - Secilen_Urun[0].IA_KAMUODENEN;
            lbl_Fiyat_Farki.Text = "(" + string.Format("{0:0.00}", temp_fiyat_farki) + " TL" + " FİYAT FARKI)";

            lbl_Depocu_fiyat.Text = string.Format("{0:0.00}", Secilen_Urun[0].IA_DEPOCU) + " TL  +  KDV";
            lbl_imalatci_fiyati.Text = string.Format("{0:0.00}", Secilen_Urun[0].IA_IMALATCI) + " TL  +  KDV";
            lbl_KDV_yuzde.Text = "%8";
            lbl_jenerikorijinal.Text = Secilen_Urun[0].IA_JENERIKORIJINAL;
            lbl_SGK_kodu.Text = Secilen_Urun[0].IF_SGKETKINKODU;
            lbl_ATC_kodu.Text = Secilen_Urun[0].I_ATCKODU;
            lbl_Recete.Text = Secilen_Urun[0].I_RECETE;
            lbl_Etkin_madde1.Text = Secilen_Urun[0].EM_ETKINMADDE;
            lbl_Etkin_madde1_miktar.Text = string.Format("{0:0.0}", Secilen_Urun[0].IEM_MIKTAR) +" " + Secilen_Urun[0].IEM_BIRIM;
            if (Secilen_Urun.Count>1)
            {
                lbl_Etkin_madde2.Text = Secilen_Urun[1].EM_ETKINMADDE;
                lbl_Etkin_madde2_miktar.Text = string.Format("{0:0.0}", Secilen_Urun[1].IEM_MIKTAR) + " " + Secilen_Urun[1].IEM_BIRIM; ;
            }

            if (Secilen_Urun[0].FI_FAVORI_BARKOD == "-1")
            {
                secilen_urun_favori = false;
                btn_Favori_ekle_cikar.Image = Properties.Resources.favoriteBlack;
            }
            else
            {
                secilen_urun_favori = true;
                btn_Favori_ekle_cikar.Image = Properties.Resources.favoriteRed;
            }


            KUB_HTML_kod = Secilen_Urun[0].IF_KUB;
            //btn_HTML_Ac.Enabled = true;
            btn_HTML_Ac.Visible = grp_urun_bilgileri.Visible;
            btn_Favori_ekle_cikar.Visible = grp_urun_bilgileri.Visible;
            btn_Onceki_ilac.Visible = grp_urun_bilgileri.Visible;
            btn_Sonraki_ilac.Visible = grp_urun_bilgileri.Visible;

            urun_secildi = true;

            urun_resim_bytes = null;
            urun_resim_bytes = Secilen_Urun[0].IA_AMBALAJRESIM;
            Urun_Resim_Goster();

            //lbl_secilen_ilac_sirasi.Visible = grp_urun_bilgileri.Visible;
            //dgv_urun_detaylari.Visible = grp_urun_bilgileri.Visible;
            pnl_ilac_adi.Visible = grp_urun_bilgileri.Visible;
            lbl_barkod_symbol_str.Text = lbl_barkod.Text;
            img_barkod_symbol.Visible = grp_urun_bilgileri.Visible;
            img_urun_resim.Visible = grp_urun_bilgileri.Visible;
            grp_urun_resim.Visible = grp_urun_bilgileri.Visible;
            img_barkod_symbol.Visible = grp_urun_bilgileri.Visible;
            grp_etkin_maddeler.Visible = grp_urun_bilgileri.Visible;

            if (Arananlar_Listesine_Ekle)
            {
                Son_aranan_urunleri_listeye_ekle(Secilen_Urun[0].IA_BARKOD);
            }
            

        }

        private void Barcode_to_image(string Barkod_kodu_str)
        {
            if (Barkod_kodu_str == null)
            {
                return;
            }
            // urun barkodu resme donustur
            Barcode barkod = new Barcode();
            Color Yazi_rengi = Color.Black;
            Color ArkaFon_rengi = Color.Transparent;
            Image img_barkod = barkod.Encode(TYPE.CODE128, Barkod_kodu_str, Yazi_rengi, ArkaFon_rengi, (int)(img_barkod_symbol.Width * 0.8), (int)(img_barkod_symbol.Height * 0.8));
            img_barkod_symbol.Image = img_barkod;
        }


        private void dgv_urun_listesi_Click(object sender, EventArgs e)
        {
            //    Secilen_urun_verileri_goster();
        }

        private void dgv_urun_listesi_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Secilen_urun_verileri_goster();
        }

        private void btn_barkod_kopyala_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbl_barkod.Text) == false)
            {
                Clipboard.SetText(lbl_barkod.Text);
                grp_urun_bilgileri.Select();
            }
                
        }

       

        private void btn_HTML_Ac_Click(object sender, EventArgs e)
        {
            if (urun_secildi == false || string.IsNullOrEmpty(KUB_HTML_kod) == true )
            {
                return;
            }

            // Yeni bir Popup Form oluşturun ve özelliklerini girin
            Form popUpForm = new Form();
            popUpForm.Text = "KISA URUN BILGISI";
            popUpForm.Size = new Size(700, 400);
            popUpForm.StartPosition = FormStartPosition.CenterScreen;
            popUpForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            popUpForm.MinimizeBox = false;
            popUpForm.MaximizeBox = false;
            
            // Yeni Popup Form Load Event
            popUpForm.Load += (s, args) =>
            {
                // WebBrowser kontrolünü oluşturun ve HTML kodunu yükleyin
                WebBrowser webBrowser = new WebBrowser();
                webBrowser.DocumentText = KUB_HTML_kod;

                // WebBrowser kontrolünü Form'a ekleyin
                popUpForm.Controls.Add(webBrowser);
                webBrowser.Size = new Size(700, 400);

                // Formun boyutunu ayarlayın ve pop-up şeklinde gösterin
                popUpForm.ClientSize = new Size(700, 400);
                //popUpForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                //popUpForm.StartPosition = FormStartPosition.CenterScreen;
            };

            // Yeni Popup formu gösterin
            popUpForm.ShowDialog();
        }

        private void Urun_Resim_Goster()
        {
            if (urun_secildi == false || urun_resim_bytes == null)
            {
                return;
            }

            using (MemoryStream ms = new MemoryStream(Secilen_Urun[0].IA_AMBALAJRESIM))
            {
                Image image = Image.FromStream(ms);

                img_urun_resim.BackgroundImageLayout = ImageLayout.Zoom;
                img_urun_resim.BackgroundImage = image;
            }
        }

        private void btn_app_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_app_maximize_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }

        private void btn_app_close_Click(object sender, EventArgs e)
        {
            DialogResult Cevap =MessageBox.Show("Programı Kapatmak istediğinize emin misiniz?", "Program Kapatma Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Cevap == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                
            }
            
        }

        private void lbl_Dolorex_Kap_DoubleClick(object sender, EventArgs e)
        {
            txt_urun_ara.Text = "DOLOREX KAP";
        }

        private void txt_urun_ara_Enter(object sender, EventArgs e)
        {
            if (txt_urun_ara.Text ==  urun_ara_bosta_text)
            {
                txt_urun_ara.Text = "";
            }
        }

        private void txt_urun_ara_Leave(object sender, EventArgs e)
        {
            if (txt_urun_ara.Text.Trim() == "")
            {
                txt_urun_ara.Text = urun_ara_bosta_text;
            }
        }

        private void list_Aranan_barkodlar_DoubleClick(object sender, EventArgs e)
        {
            //list_Aranan_barkodlar.Items.Clear();
            lst_Aranan_barkodlar.Clear();
        }

        private void btn_Onceki_ilac_Click(object sender, EventArgs e)
        {
            if (Aranan_urun_sirasi < 2)
            {
                MessageBox.Show("Arama yapılan ilk ürüne ulaştınız.", "Arama Navigasyon Uyarı");
                return;
            }

            int onceki_urun_barkod=0;
            onceki_urun_barkod = Aranan_urun_sirasi - 1;
            //string onceki_urun_barkod_str = list_Aranan_barkodlar.Items[onceki_urun_barkod-1].ToString();
            string onceki_urun_barkod_str = lst_Aranan_barkodlar[onceki_urun_barkod - 1].ToString();
            //MessageBox.Show(onceki_urun_barkod_str  + " , urun sıra no: " + onceki_urun_barkod);
            Urun_veri_goster_proc(onceki_urun_barkod_str,false);
            txt_urun_ara.Text = urun_ara_bosta_text ;
            dgv_urun_listesi.DataSource = null;
            Aranan_urun_sirasi = Aranan_urun_sirasi - 1;
        }

        private void btn_Sonraki_ilac_Click(object sender, EventArgs e)
        {
            if (Aranan_urun_sirasi >= Aranan_urun_sayisi)
            {
                MessageBox.Show("Arama yapılan son ürüne ulaştınız. Arama Adedi: " + Aranan_urun_sayisi, "Arama Navigasyon Uyarı");
                return;
            }

            int sonraki_urun_barkod = 0;
            sonraki_urun_barkod = Aranan_urun_sirasi + 1;
            string sonraki_urun_barkod_str = lst_Aranan_barkodlar[sonraki_urun_barkod - 1].ToString();
            //MessageBox.Show(sonraki_urun_barkod_str + " , urun sıra no: " + sonraki_urun_barkod);
            Urun_veri_goster_proc(sonraki_urun_barkod_str, false);
            txt_urun_ara.Text = urun_ara_bosta_text ;
            dgv_urun_listesi.DataSource = null;
            Aranan_urun_sirasi = Aranan_urun_sirasi + 1;
        }

        private void btn_Favori_ekle_cikar_Click(object sender, EventArgs e)
        {
            if (urun_secildi == false)  // urun secilmediyse
            {
                return;
            }

            bool urun_favori_kontrol = false;
            urun_favori_kontrol = secilen_urun_favori;

            if (urun_favori_kontrol == false)   //Secilen urun Favorilere eklenmemisse
            {
                string SQL_komut_str = Urun_Favori_Ekle_SQL_Sorgu(secilen_urun_barkod);
                bool islem_sonuc1 = _sqlite.SQLite_Komut_Calistir(SQL_komut_str);

                if (islem_sonuc1)
                {
                    secilen_urun_favori = true;
                    btn_Favori_ekle_cikar.Image = Properties.Resources.favoriteRed;
                    MessageBox.Show(secilen_urun_barkod + " barkodlu " + lbl_ilac_adi.Text + " ürünü Favorilere Ekleme İşlemi Tamamlandı", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show(secilen_urun_barkod + " barkodlu " + lbl_ilac_adi.Text + " ürünü Favorilere Eklenemedi", "İşlem Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (urun_favori_kontrol == true)   //Secilen urun Favorilere eklenmisse
            {
                string SQL_komut_str = Urun_Favori_Cikar_SQL_Sorgu(secilen_urun_barkod);
                bool islem_sonuc2 = _sqlite.SQLite_Komut_Calistir(SQL_komut_str);

                if (islem_sonuc2)
                {
                    secilen_urun_favori = false;
                    btn_Favori_ekle_cikar.Image = Properties.Resources.favoriteBlack;
                    MessageBox.Show(secilen_urun_barkod + " barkodlu " + lbl_ilac_adi.Text + " " + lbl_ilac_adi.Text + " ürünü Favorilerden Çıkarma İşlemi Tamamlandı", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show(secilen_urun_barkod + " barkodlu " + lbl_ilac_adi.Text + " ürünü Favorilerden Çıkarılamadı", "İşlem Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }
    }
}
