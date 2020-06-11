﻿using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
namespace GUI
{
    public partial class Form3 : Form
    {
        bool check = false;
        public Form3()
        {
            InitializeComponent();
        }
        
        private void button1_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.button1, "Đóng cửa sổ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            TopSachBUS topSachBUS = new TopSachBUS();
            if (check == false)
            {
                
                List<TopSach> dsTopSach = topSachBUS.dsTopSach();
                int row = 0;
                gvTopSach.Rows.Clear();
                foreach (TopSach sach in dsTopSach)
                {
                    gvTopSach.Rows.Add();
                    gvTopSach.Rows[row].Cells[0].Value = (1 + row) + "";
                    gvTopSach.Rows[row].Cells[1].Value = sach.TenSach;
                    gvTopSach.Rows[row].Cells[2].Value = sach.TenTheLoai;
                    gvTopSach.Rows[row].Cells[3].Value = sach.SoLanMuon + "";
                    gvTopSach.Rows[row].Cells[4].Value = sach.GhiChu;
                    row++;
                }
            }
            else
            {
                DateTime start = dtpStart.Value.Date;
                DateTime end = dtpEnd.Value.Date;
                List<TopSach> dsTopSach = topSachBUS.dsTopSach(start,end);
                int row = 0;
                gvTopSach.Rows.Clear();
                foreach (TopSach sach in dsTopSach)
                {
                    gvTopSach.Rows.Add();
                    gvTopSach.Rows[row].Cells[0].Value = (1 + row) + "";
                    gvTopSach.Rows[row].Cells[1].Value = sach.TenSach;
                    gvTopSach.Rows[row].Cells[2].Value = sach.TenTheLoai;
                    gvTopSach.Rows[row].Cells[3].Value = sach.SoLanMuon + "";
                    gvTopSach.Rows[row].Cells[4].Value = sach.GhiChu;
                    row++;
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DateTime start = dtpStart.Value.Date;
            MessageBox.Show(start + "");
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            check = true;
        }
        
        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            check = true;
        }

        private void gvTopSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ExportPdf(DataGridView dataGridView)
        {

            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.TTF");

            //Create a base font object making sure to specify IDENTITY-H
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dataGridView.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Font f = new System.Drawing.Font(bf, 12, Font.NORMAL);
            //Font f = new Font(bf, 12, Font.NORMAL);
            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.NORMAL);
            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText,text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                        String s = cell.Value.ToString();
                        //PdfCe
                        PdfPCell cell2 = new PdfPCell(new Phrase(s,text));
                        pdfTable.AddCell(cell2);
                    }
                    catch { }
                }
            }

            //Exporting to PDF
            string folderPath = "C:\\PDFs\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "DataGridViewExport.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            ExportPdf(gvTopSach);
            MessageBox.Show("In thành công !");
            
        }
    }
}