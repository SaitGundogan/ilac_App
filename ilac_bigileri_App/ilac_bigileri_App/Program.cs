using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace ilac_bigileri_App
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            //const string Uygulama_adi = "ilac_bilgileri";
            const string App_GUID = "3e9896d3-b397-46e5-96ce-4da2695ee7a3";
            bool Uygulama_tekli_mod_calisabilir = false;

            using (Mutex mtex = new Mutex(true, App_GUID, out Uygulama_tekli_mod_calisabilir))
            {
                if (Uygulama_tekli_mod_calisabilir)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Main());
                    mtex.ReleaseMutex();
                }
                else
                {
                    MessageBox.Show("Program şu an zaten çalışıyor", "Tekrar Çalıştırma Uyarısı");
                }
                    
            }
        }
    }
}
