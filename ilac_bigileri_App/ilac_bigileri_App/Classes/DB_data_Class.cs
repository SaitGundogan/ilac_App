using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class DB_data_Class
{

    public class URUN_LISTESI
    {
        public string Urun_ara { get; set; }
        public string IA_barkod { get; set; }
        public int I_Id { get; set; }
    }

    public class URUN_BILGILERI
    {
        public string URUN_ARA { get; set; }    // 0
        public int I_ID { get; set; }   //  1
        public string I_ILAC_ADI { get; set; }  // 2
        public string I_ATCKODU { get; set; }   // 3
        public string I_RECETE { get; set; }    // 4
        public string I_FIRMA { get; set; }     // 5
        public int IF_ILAC_ID { get; set; }     // 6
        public int IF_ID { get; set; }          // 7
        public string IF_Olcu { get; set; }     // 8
        public string IF_SGKETKINKODU { get; set; } // 9
        public string IF_KUB { get; set; }          // 10
        public string IA_BARKOD { get; set; }       // 11
        public string IA_AMBALAJ { get; set; }      // 12
        public string IA_FIYATTARIH { get; set; }   // 13
        public Double IA_FIYAT { get; set; }           // 14
        public Double IA_DEPOCU { get; set; }          // 15
        public Double IA_IMALATCI { get; set; }        // 16
        public Double IA_KAMUFIYATI { get; set; }      // 17
        public Double IA_KAMUODENEN { get; set; }      // 18
        public string IA_JENERIKORIJINAL { get; set; }  // 19
        public byte[] IA_AMBALAJRESIM { get; set; }     // 20
        public int IEM_ILAC_FORM_ID { get; set; }       // 21
        public int IEM_ID { get; set; }                 // 22
        public int IEM_ETKIN_MADDE { get; set; }        // 23
        public Double IEM_MIKTAR { get; set; }              // 24
        public string IEM_BIRIM { get; set; }            // 25
        public int EM_ID { get; set; }                  // 26
        public string EM_ETKINMADDE { get; set; }       // 27
        public string FI_FAVORI_BARKOD { get; set; }       // 28
    }


    public static void DataGridAyarlari_Urun_Listesi(DataGridView DGV1)
    {
        DataGridView DGV = DGV1;
        int DGV_max_gorunen_kayit_sayisi = 8;

        DGV.ClearSelection();
        DGV.CurrentCell = null;
        //DGV.DataSource = null;
        //DGV.Columns.Clear();

        DGV.AllowUserToAddRows = false;
        DGV.AllowUserToDeleteRows = false;
        DGV.AllowUserToOrderColumns = true;
        DGV.ReadOnly = true;
        DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        DGV.MultiSelect = false;
        //DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        DGV.AllowUserToResizeColumns = false;
        DGV.AllowUserToResizeRows = false;

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        //DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        DGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        //DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        //DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        //DGV.ScrollBars = ScrollBars.Both;
        //DGV.ScrollBars = ScrollBars.None;
        DGV.ScrollBars = ScrollBars.Vertical;
        //DGV.ColumnHeadersVisible = true;  //Basliklari Goster
        DGV.ColumnHeadersVisible = false;   //Basliklari Gizle
        DGV.RowHeadersVisible = false;

        //DGV.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 14, FontStyle.Bold);

        DGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        DGV.DefaultCellStyle.Font = new Font("Times New Roman", 14, FontStyle.Bold);

        DGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        DGV.GridColor = Color.DarkGray;
        DGV.BackgroundColor = Color.White;
        DGV.BorderStyle = BorderStyle.None;



        if (DGV.ColumnCount > 0)
        {
            DGV.Columns[0].Visible = true;
            // DGV.Columns[0].HeaderText = "Col 0"
            DGV.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            //DGV.Columns[0].Width = 200;
            DGV.Columns[0].Width = DGV.Width;
            DGV.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }


        //if (DGV.ColumnCount > 1)
        //{
        //    DGV.Columns[1].Visible = false;
        //    //DGV.Columns[1].HeaderText = "Col 1";
        //    DGV.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    // DGV.Columns[1].Width = 250
        //}


        //if (DGV.ColumnCount > 2)
        //{
        //    DGV.Columns[2].Visible = false;
        //    //DGV.Columns[2].HeaderText = "Col 2";
        //    DGV.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    //DGV.Columns[2].Width = 250;
        //}

        //if (DGV.ColumnCount > 3)
        //{
        //    DGV.Columns[3].Visible = false;
        //    // DGV.Columns[3].HeaderText = "Col 3";
        //    DGV.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    // DGV.Columns[3].Width = 250
        //    DGV.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
        //}

        //if (DGV.ColumnCount > 4)
        //{
        //    DGV.Columns[4].Visible = false;
        //    //DGV.Columns[4].HeaderText = "Col 4";
        //    DGV.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    //DGV.Columns[4].Width = 250;
        //}

        //if (DGV.ColumnCount > 5)
        //{
        //    DGV.Columns[5].Visible = false;
        //    //DGV.Columns[5].HeaderText = "Col 5";
        //    DGV.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    //DGV.Columns[5].Width = 250;
        //}

        for (int i = 1; i <= DGV.ColumnCount - 1; i++)
        {
            DGV.Columns[i].Visible = true;
            //DGV.Columns[i].Visible = false;
            //DGV.Columns[i].HeaderText = "Col " + i;
            DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV.Columns[i].Width = 200;
            //DGV.Columns[i].Width = DGV.Width;
            //DGV.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //DGV.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        if (DGV.Rows.Count > DGV_max_gorunen_kayit_sayisi)
        {
            DGV.Columns[0].Width = DGV.Width - 20;
        }
    }

    public static void DataGridAyarlari_Urun_Detaylari(DataGridView DGV1)
    {
        DataGridView DGV = DGV1;
        int DGV_max_gorunen_kayit_sayisi = 6;

        DGV.ClearSelection();
        DGV.CurrentCell = null;
        //DGV.DataSource = null;
        //DGV.Columns.Clear();

        DGV.AllowUserToAddRows = false;
        DGV.AllowUserToDeleteRows = false;
        DGV.AllowUserToOrderColumns = true;
        DGV.ReadOnly = true;
        DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        DGV.MultiSelect = false;
        DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        //DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        DGV.AllowUserToResizeColumns = false;
        DGV.AllowUserToResizeRows = false;

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        //DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        DGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        //DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        //DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        //DGV.ScrollBars = ScrollBars.None;
        //DGV.ScrollBars = ScrollBars.Both;
        //DGV.ScrollBars = ScrollBars.Vertical;
        DGV.ScrollBars = ScrollBars.Horizontal;

        DGV.ColumnHeadersVisible = true;  //Basliklari Goster
                                          //DGV.ColumnHeadersVisible = false;   //Basliklari Gizle
        DGV.RowHeadersVisible = false;

        //DGV.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 14, FontStyle.Bold);

        DGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        DGV.DefaultCellStyle.Font = new Font("Times New Roman", 14, FontStyle.Bold);

        DGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        DGV.GridColor = Color.DarkGray;
        DGV.BackgroundColor = Color.White;
        DGV.BorderStyle = BorderStyle.None;



        if (DGV.ColumnCount > 0)
        {
            DGV.Columns[0].Visible = true;
            // DGV.Columns[0].HeaderText = "Col 0"
            DGV.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            //DGV.Columns[0].Width = 200;
            DGV.Columns[0].Width = DGV.Width;
            DGV.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }


        //if (DGV.ColumnCount > 1)
        //{
        //    DGV.Columns[1].Visible = false;
        //    //DGV.Columns[1].HeaderText = "Col 1";
        //    DGV.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    // DGV.Columns[1].Width = 250
        //}


        //if (DGV.ColumnCount > 2)
        //{
        //    DGV.Columns[2].Visible = false;
        //    //DGV.Columns[2].HeaderText = "Col 2";
        //    DGV.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    //DGV.Columns[2].Width = 250;
        //}

        //if (DGV.ColumnCount > 3)
        //{
        //    DGV.Columns[3].Visible = false;
        //    // DGV.Columns[3].HeaderText = "Col 3";
        //    DGV.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    // DGV.Columns[3].Width = 250
        //    DGV.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
        //}

        //if (DGV.ColumnCount > 4)
        //{
        //    DGV.Columns[4].Visible = false;
        //    //DGV.Columns[4].HeaderText = "Col 4";
        //    DGV.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    //DGV.Columns[4].Width = 250;
        //}

        //if (DGV.ColumnCount > 5)
        //{
        //    DGV.Columns[5].Visible = false;
        //    //DGV.Columns[5].HeaderText = "Col 5";
        //    DGV.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGV.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        //    //DGV.Columns[5].Width = 250;
        //}

        for (int i = 1; i <= DGV.ColumnCount - 1; i++)
        {
            DGV.Columns[i].Visible = true;
            //DGV.Columns[i].Visible = false;

            if (i == 10)
            {//KUB - HTML
                //DGV.Columns[i].HeaderText = "KUB HTML ";
                //DGV.Columns[i].Visible = false;
                DGV.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }

            //DGV.Columns[i].Visible = false;
            //DGV.Columns[i].HeaderText = "Col " + i;
            DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV.Columns[i].Width = 200;
            //DGV.Columns[i].Width = DGV.Width;
            //DGV.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //DGV.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        if (DGV.Rows.Count > DGV_max_gorunen_kayit_sayisi)
        {
            DGV.Columns[0].Width = DGV.Width - 20;
        }
    }

}
