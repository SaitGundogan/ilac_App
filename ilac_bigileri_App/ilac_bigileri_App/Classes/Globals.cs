using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Globals
{
    public static string Database_FileName = "rxsample.db";
    public static string Database_Path = ".\\";
  
    public static string connectionString = "Data Source=" + Database_Path + Database_FileName;
    public static int SQL_Sorgu_Secim = 0;  // 1= Urun Listele SQL Sorgu,  2= Urun Bilgileri SQL Sorgu
}
