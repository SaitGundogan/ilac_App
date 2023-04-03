using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using static DB_data_Class;
using static Globals;
using System.Windows.Forms;

public class SQLite_Class
{
    //public static bool IsNumeric(string s)
    //{
    //    return int.TryParse(s, out int n);
    //}

    public static bool HepsiSayiKontrolu(string Text_Data)
    {
        foreach (char karakter in Text_Data)
        {
            if (karakter < '0' || karakter > '9')
                return false;
        }
        return true;
    }

    public static string String_Test(string s)
    {
        if (String.IsNullOrEmpty(s))
            return "";
        else
            return s;
    }

    public static byte [] ByteArrayIsNull(byte[] byte_array1)
    {
        byte[] bos_byte = new byte[1];
        bos_byte[0] = 0;

        if (byte_array1 == null)
        {
            byte_array1 = bos_byte;
            return byte_array1;
        }
        else
            return byte_array1;
    }

    #region SQL_Sorgular
    public static string Urun_Listesi_SQL_Sorgu(string filter)
    {
        string query = "";
        string Filter_SQL = "";
        bool Barkod_Aramasi_Yapilacak = false;

        // Girilen tüm karakterler sayi ise kullanici ilac Barkoduna göre arama yapacak
        Barkod_Aramasi_Yapilacak = HepsiSayiKontrolu(filter);

        if (Barkod_Aramasi_Yapilacak == false)
        {
            // ilac adina gore arama
            Filter_SQL = "ILAC_ADI LIKE " + "'%" + filter + "%' ";
        }
        else
        {
            // ilac barkoduna gore arama
            Filter_SQL = "ILAC_AMBALAJ.BARKOD = " + filter + " ";
        }

        
        // urun listesi SQL Sorgu
        query = query + "SELECT ";
        //query = query + "ILACLAR.ILAC_ADI || ' ' || upper(ILAC_FORM.OLCU) || ' ' || upper(replace(ILAC_AMBALAJ.AMBALAJ, '/kutu', '')) as URUN_ARA, ";
        query = query + "ILACLAR.ILAC_ADI || ' ' || ILAC_FORM.OLCU || ' ' || replace(ILAC_AMBALAJ.AMBALAJ, '/kutu', '') as URUN_ARA, ";
        query = query + "ILAC_AMBALAJ.BARKOD, ";
        query = query + "ILACLAR.ID ";
        query = query + "FROM ";
        query = query + "ILACLAR ";
        query = query + "INNER JOIN ILAC_FORM ON ILACLAR.ID = ILAC_FORM.ILAC_ID ";
        query = query + "INNER JOIN ILAC_AMBALAJ ON ILAC_FORM.ID = ILAC_AMBALAJ.ILAC_FORM_ID ";
        query = query + "WHERE ";

        query = query + Filter_SQL;

        query = query + " ORDER BY ILACLAR.ID ASC, URUN_ARA ASC";
        return query;
    }

    public static string Urun_Bilgileri_SQL_Sorgu(string filter)
    {
        string query = "";
        string Filter_SQL = "";
        bool Barkod_Aramasi_Yapilacak = false;

        // Girilen tüm karakterler sayi ise kullanici ilac Barkoduna göre arama yapacak
        Barkod_Aramasi_Yapilacak = HepsiSayiKontrolu(filter);

        if (Barkod_Aramasi_Yapilacak == true)
        {
            // ilac barkoduna gore arama
            Filter_SQL = "ILAC_AMBALAJ.BARKOD = " + filter + " ";
        }
        else
        {
            return query;
        }
       
        string sec_urun_barkod = "";
        sec_urun_barkod = filter;

        // secili urun bilgileri SQL Sorgu
        query = query + "SELECT ";
        query = query + "IFNULL(ILACLAR.ILAC_ADI || ' ' || ILAC_FORM.OLCU || ' ' || replace(ILAC_AMBALAJ.AMBALAJ, '/kutu', ''),'') as URUN_ARA, ";
        query = query + "IFNULL(ILACLAR.ID,0) I_ID, IFNULL(ILACLAR.ILAC_ADI,'') I_ILAC_ADI, IFNULL(ILACLAR.ATCKODU,'') I_ATCKODU, IFNULL(ILACLAR.RECETE,'') I_RECETE, IFNULL(ILACLAR.FIRMA,'') I_FIRMA, ";
        query = query + "IFNULL(ILAC_FORM.ILAC_ID,0) IF_ILAC_ID, IFNULL(ILAC_FORM.ID,0) IF_ID, IFNULL(ILAC_FORM.OLCU,'') IF_OLCU, IFNULL(ILAC_FORM.SGKETKINKODU,'') IF_SGKETKINKODU, IFNULL(ILAC_FORM.KUB,'') IF_KUB, ";
        query = query + "IFNULL(ILAC_AMBALAJ.BARKOD,'') IA_BARKOD, IFNULL(ILAC_AMBALAJ.AMBALAJ,'') IA_AMBALAJ, IFNULL(ILAC_AMBALAJ.FIYATTARIH,'') IA_FIYATTARIH, IFNULL(ILAC_AMBALAJ.FIYAT,0) IA_FIYAT, ";
        query = query + "IFNULL(ILAC_AMBALAJ.DEPOCU,0) IA_DEPOCU, IFNULL(ILAC_AMBALAJ.IMALATCI,0) IA_IMALATCI, IFNULL(ILAC_AMBALAJ.KAMUFIYATI,0) IA_KAMUFIYATI, ";
        query = query + "IFNULL(ILAC_AMBALAJ.KAMUODENEN,0) IA_KAMUODENEN, IFNULL(ILAC_AMBALAJ.JENERIKORIJINAL,'') IA_JENERIKORIJINAL, ILAC_AMBALAJ.AMBALAJRESIM as IA_AMBALAJRESIM, ";
        query = query + "IFNULL(ILAC_ETKIN_MADDELER.ILAC_FORM_ID,0) IEM_ILAC_FORM_ID, IFNULL(ILAC_ETKIN_MADDELER.ID,0) IEM_ID, IFNULL(ILAC_ETKIN_MADDELER.ETKIN_MADDE,0) IEM_ETKIN_MADDE, IFNULL(ILAC_ETKIN_MADDELER.MIKTAR,0) IEM_MIKTAR, IFNULL(ILAC_ETKIN_MADDELER.BIRIM,'') IEM_BIRIM, ";
        query = query + "IFNULL(ETKIN_MADDELER.ID,0) EM_ID, IFNULL(ETKIN_MADDELER.ETKINMADDE,'') EM_ETKINMADDE, ";
        query = query + "IFNULL((SELECT IFNULL(FAVORI_ILACLAR.FAVORI_BARKOD,'') FROM FAVORI_ILACLAR WHERE FAVORI_BARKOD = " + sec_urun_barkod + "),'-1') AS FI_BARKOD ";
        query = query + "FROM ";
        query = query + "ILACLAR ";
        query = query + "INNER JOIN ILAC_FORM ON ILACLAR.ID = ILAC_FORM.ILAC_ID ";
        query = query + "INNER JOIN ILAC_AMBALAJ ON ILAC_FORM.ID = ILAC_AMBALAJ.ILAC_FORM_ID ";
        query = query + "INNER JOIN ILAC_ETKIN_MADDELER ON ILAC_FORM.ID = ILAC_ETKIN_MADDELER.ILAC_FORM_ID ";
        query = query + "INNER JOIN ETKIN_MADDELER ON ILAC_ETKIN_MADDELER.ETKIN_MADDE = ETKIN_MADDELER.ID ";
        query = query + "WHERE ";

        query = query + Filter_SQL;

        query = query + " ORDER BY ILACLAR.ID ASC, URUN_ARA ASC";

        return query;
    }

    public static string Urun_Favori_Ekle_SQL_Sorgu(string Barkod_str)
    {
        string query = "";
        // Favori barkod ekle SQL Sorgu
        query = query + "INSERT INTO ";
        query = query + "FAVORI_ILACLAR ";
        query = query + "(" + "FAVORI_BARKOD" + ") ";
        query = query + "VALUES";
        query = query + "('" + Barkod_str + "')";
        return query;
    }

    public static string Urun_Favori_Cikar_SQL_Sorgu(string Barkod_str)
    {
        string query = "";
        // favori barkod cikar SQL Sorgu
        query = query + "DELETE ";
        query = query + "FROM ";
        query = query + "FAVORI_ILACLAR ";
        query = query + "WHERE ";
        query = query + "FAVORI_ILACLAR.FAVORI_BARKOD" + " = " + "'" + Barkod_str + "'";
        return query;
    }
    #endregion
    public class SQLiteManagement
    {
        private SQLiteConnection _connection;

        public SQLiteManagement(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
        }

        public List<URUN_LISTESI> Urun_Listesi_Oku(string filter)
        {
            List<URUN_LISTESI> Okunan_Data = new List<URUN_LISTESI>();
            string query = "";
                query = Urun_Listesi_SQL_Sorgu(filter);

            using (SQLiteCommand command = new SQLiteCommand(query, _connection))
            {
                _connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                            URUN_LISTESI SQL_Data = new URUN_LISTESI()
                            {
                                Urun_ara = reader.GetString(0),
                                IA_barkod = reader.GetString(1),
                                I_Id = reader.GetInt32(2)
                            };
                       

                        Okunan_Data.Add(SQL_Data);
                    }
                    reader.Close();
                }
                _connection.Close();
            }

            return Okunan_Data;
        }
        public List<URUN_BILGILERI> Urun_Bilgileri_Oku(string filter)
        {
            List<URUN_BILGILERI> Okunan_Data = new List<URUN_BILGILERI>();
            string query = "";

            query = Urun_Bilgileri_SQL_Sorgu(filter);

            using (SQLiteCommand command = new SQLiteCommand(query, _connection))
            {
                _connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        URUN_BILGILERI SQL_Data = new URUN_BILGILERI()
                        {
                            URUN_ARA = reader.GetString(0),
                            I_ID = reader.GetInt32(1),
                            I_ILAC_ADI = reader.GetString(2),
                            I_ATCKODU = reader.GetString(3),
                            I_RECETE = reader.GetString(4),
                            I_FIRMA = reader.GetString(5),
                            IF_ILAC_ID = reader.GetInt32(6),
                            IF_ID = reader.GetInt32(7),
                            IF_Olcu = reader.GetString(8),
                            IF_SGKETKINKODU = reader.GetString(9),
                            IF_KUB = reader.GetString(10),
                            IA_BARKOD = reader.GetString(11),
                            IA_AMBALAJ = reader.GetString(12),
                            IA_FIYATTARIH = reader.GetString(13),
                            IA_FIYAT = reader.GetDouble(14),
                            IA_DEPOCU = reader.GetDouble(15),
                            IA_IMALATCI = reader.GetDouble(16),
                            IA_KAMUFIYATI = reader.GetDouble(17),
                            IA_KAMUODENEN = reader.GetDouble(18),
                            IA_JENERIKORIJINAL = reader.GetString(19),
                            //IA_AMBALAJRESIM =  (byte[])reader[20]
                            IEM_ILAC_FORM_ID = reader.GetInt32(21),
                            IEM_ID = reader.GetInt32(22),
                            IEM_ETKIN_MADDE = reader.GetInt32(23),
                            IEM_MIKTAR = reader.GetDouble(24),
                            IEM_BIRIM = reader.GetString(25),
                            EM_ID = reader.GetInt32(26),
                            EM_ETKINMADDE = reader.GetString(27),
                            FI_FAVORI_BARKOD = reader.GetString(28)
                        };

                        if (reader[20] != System.DBNull.Value)
                        {
                            SQL_Data.IA_AMBALAJRESIM = (byte[])reader[20];
                        }

                        Okunan_Data.Add(SQL_Data);
                    }
                    reader.Close();
                }
                _connection.Close();
            }

            return Okunan_Data;
        }


        public bool SQLite_Komut_Calistir(string SQL_komut)
        {
            string query = "";
            query = SQL_komut;
            int sonuc = 0;

            SQLiteCommand command = new SQLiteCommand(query, _connection);

            _connection.Open();
            sonuc = command.ExecuteNonQuery();

            _connection.Close();

            if (sonuc == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
