using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using COMExcel = Microsoft.Office.Interop.Excel;
using QuanLyDiem.Properties;
using QuanLyDiem;
using Microsoft.Office.Interop.Excel;

namespace QuanLyDiem
{
    public partial class FrmInBangDiemSV : Form
    {
        System.Data.DataTable tblInBangDiemSV;
        string sql;
        public FrmInBangDiemSV()
        {
            InitializeComponent();
        }
        private void FrmInBangDiemSV_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            txtTenMon.Enabled = false;
            rdoMaSV.Checked = true;
            Load_DataGridView();
            DAO.OpenConnection();
            string sql = "select MaLop from Lop";
            DAO.FillDataToCombo(sql, cmbMaLop, "MaLop", "MaLop");
            sql = "select MaMon from MonHoc";
            DAO.FillDataToCombo(sql, cmbMaMon, "MaMon", "MaMon");
            Load_DataGridView();
            cmbMaLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMaMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            txtTenMon.Enabled = false;
            ResetValues1();
            ResetValues2();
            DAO.CloseConnection();
        }
        private void Load_DataGridView()
        {
            try
            {
                DAO.OpenConnection();
                btnLamMoi.Enabled = false;
                sql = "SELECT * FROM Diem ";
                tblInBangDiemSV = DAO.GetDataToTable(sql);
                GridViewInBangDiemSV.DataSource = tblInBangDiemSV;
                GridViewInBangDiemSV.Enabled = true;
                // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
                GridViewInBangDiemSV.AllowUserToAddRows = false;
                // Không cho phép sửa dữ liệu trực tiếp trên lưới
                GridViewInBangDiemSV.EditMode = DataGridViewEditMode.EditProgrammatically;
                btnLamMoi.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                DAO.CloseConnection();
            }
        }
        private void ResetValues1()
        {
            txtMaSV.Text = "";
            txtMaSV.Focus();
            return;
        }
        private void ResetValues2()
        {
            cmbMaMon.SelectedIndex = -1;
            txtTenMon.Text = "";
            cmbMaLop.SelectedIndex = -1;
            txtLanThi.Text = "";
        }
        private int CheckRdoMaSV()
        {
            int kt = 0;
            if (txtMaSV.Text == "")
            {
                MessageBox.Show("Vui lòng chọn mã sinh viên trước!");
                ResetValues1();
                kt = -1;
            }
            else
            {
                string sql = "select MaSV from SinhVien where MaSV='" + txtMaSV.Text.Trim() + "'";
                tblInBangDiemSV = DAO.GetDataToTable(sql);
                if (tblInBangDiemSV.Rows.Count == 0)
                {
                    MessageBox.Show("Mã sinh viên không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ResetValues1();
                    kt = -1;
                }
                else
                {
                    sql = "select * from Diem where MaSV=N'" + txtMaSV.Text + "'";
                    if (DAO.CheckKeyExist(sql) == false)
                    {
                        MessageBox.Show("Mã sinh viên chưa có điểm , hãy nhập mã khác để in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ResetValues1();
                        kt = -1;
                    }
                }
            }            
            return kt;
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            if (rdoMaSV.Checked == true)
            {
                if (CheckRdoMaSV() == 0)
                {
                    MessageBox.Show("Vui lòng chờ... Đang cập nhật dữ liệu");
                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
                    COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
                    COMExcel.Range exRange;
                    int hang = 0, cot = 0;
                    exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
                    exSheet = exBook.Worksheets[1];

                    exRange = exSheet.Cells[1, 1];
                    exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
                    exRange.Range["A1:B3"].Font.Size = 10;
                    exRange.Range["A1:B3"].Font.Bold = true;
                    exRange.Range["A1:B3"].Font.ColorIndex = 25; //Màu navy
                    exRange.Range["A1:A1"].ColumnWidth = 7;
                    exRange.Range["B1:B1"].ColumnWidth = 15;
                    exRange.Range["A1:D1"].MergeCells = true;
                    exRange.Range["A1:D1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
                    exRange.Range["A1:D1"].Value = "Banking Acedemy Vietnam";
                    exRange.Range["A2:D2"].MergeCells = true;
                    exRange.Range["A2:D2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
                    exRange.Range["A2:D2"].Value = "12 Chua Boc, Quang Trung, Dong Da, Hanoi, Vietnam";

                    exRange.Range["A5:G5"].Font.Size = 20;
                    exRange.Range["A5:G5"].Font.Bold = true;
                    exRange.Range["A5:G5"].Font.ColorIndex = 9; //Màu nâu
                    exRange.Range["A5:G5"].MergeCells = true;
                    exRange.Range["A5:G5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    exRange.Range["A5:G5"].Value = "ĐIỂM SINH VIÊN";

                    exRange.Range["B7:F8"].MergeCells = true;
                    exRange.Range["B7:F7"].Font.ColorIndex = 56;
                    exRange.Range["B7:F7"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    exRange.Range["B7:B7"].Value = "Thông tin sinh viên \nMã sinh viên: " + txtMaSV.Text.Trim();

                    int hk = 1;
                    int row = 10;
                    sql = "select MAX(HocKy) from Diem where MaSV=N'" + txtMaSV.Text.Trim() + "'";
                    string SoLuongHK = DAO.GetFieldValues(sql);
                    for (hk = 1; hk <= Convert.ToInt32(SoLuongHK); hk++)
                    {
                        exRange = exSheet.Cells[1][hk + row];
                        sql = "SELECT a.MaMon,b.TenMon,b.DVHT,a.LanThi,a.Diem FROM Diem as a inner join MonHoc as b on a.MaMon=b.MaMon where MaSV = N'" + txtMaSV.Text.Trim() + "'and HocKy=" + hk + " order by MaMon, LanThi";
                        tblInBangDiemSV = DAO.GetDataToTable(sql);
                        exRange.Range["A1:K1"].Font.Bold = true;
                        exRange.Range["A1:G1"].MergeCells = true;
                        exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
                        exRange.Range["A1:A1"].Value = "Học kỳ " + hk;
                        exRange.Range["A1:A1"].Interior.Color = System.Drawing.Color.Bisque;
                        row++;
                        exRange = exSheet.Cells[2][hk + row];
                        exRange.Range["A1:K1"].Font.Bold = true;
                        exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                        exRange.Range["A1:A1"].Value = "STT";
                        exRange.Range["A1:A1"].ColumnWidth = 5;
                        exRange.Range["B1:B1"].Value = "Mã môn";
                        exRange.Range["C1:C1"].Value = "Tên môn";
                        exRange.Range["C1:C1"].ColumnWidth = 40;
                        exRange.Range["D1:D1"].Value = "ĐVHT";
                        exRange.Range["E1:E1"].Value = "Lần thi";
                        exRange.Range["F1:F1"].Value = "Điểm";
                        row++;
                        for (hang = 0; hang < tblInBangDiemSV.Rows.Count; hang++)
                        {
                            //Điền số thứ tự vào cột 2 từ dòng 11
                            exSheet.Cells[2][hang + hk + row] = hang + 1;
                            for (cot = 0; cot < tblInBangDiemSV.Columns.Count; cot++) //Điền thông tin hàng từ cột thứ 2, dòng 12
                            {
                                exSheet.Cells[cot + 3][hang + hk + row] = tblInBangDiemSV.Rows[hang][cot].ToString();
                            }
                        }
                        row += tblInBangDiemSV.Rows.Count;
                    }
                    exSheet.Name = "Điểm sinh viên";
                    exApp.Visible = true;
                }
            }
            else if (rdoTim.Checked == true)
            {

                MessageBox.Show("Vui lòng chờ... Đang cập nhật dữ liệu");

                COMExcel.Application exApp = new COMExcel.Application();
                COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
                COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
                COMExcel.Range exRange;
                string sql;
                int hang = 0, cot = 0;
                exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
                exSheet = exBook.Worksheets[1];

                exRange = exSheet.Cells[1, 1];
                exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
                exRange.Range["A1:B3"].Font.Size = 10;
                exRange.Range["A1:B3"].Font.Bold = true;
                exRange.Range["A1:B3"].Font.ColorIndex = 25; //Màu navy
                exRange.Range["A1:A1"].ColumnWidth = 7;
                exRange.Range["B1:B1"].ColumnWidth = 15;
                exRange.Range["A1:B1"].MergeCells = true;
                exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["A1:B1"].Value = "Banking Acedemy Vietnam";
                exRange.Range["A2:E2"].MergeCells = true;
                exRange.Range["A2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["A2:E2"].Value = "12 Chua Boc Street, Quang Trung Ward, Dong Da District, Hanoi, Vietnam";

                exRange.Range["C5:F5"].Font.Size = 20;
                exRange.Range["C5:F5"].Font.Bold = true;
                exRange.Range["C5:F5"].Font.ColorIndex = 9; //Màu nâu
                exRange.Range["C5:F5"].MergeCells = true;
                exRange.Range["C5:F5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C5:F5"].Value = "DANH SÁCH ĐIỂM SINH VIÊN";

                sql = RdoTim_Sql();
                tblInBangDiemSV = DAO.GetDataToTable(sql);
                exRange.Range["A7:K7"].Font.Bold = true;
                exRange.Range["A7:K7"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C7:F7"].ColumnWidth = 12;
                exRange.Range["G7"].ColumnWidth = 16;
                exRange.Range["I7"].ColumnWidth = 13;
                exRange.Range["J7"].ColumnWidth = 12;
                exRange.Range["K7"].ColumnWidth = 12;

                exRange.Range["A7:G7"].Interior.Color = System.Drawing.Color.Bisque;
                exRange.Range["A7:A7"].Value = "STT";
                exRange.Range["A7:A7"].ColumnWidth = 5;
                exRange.Range["B7:B7"].Value = "Mã sinh viên";
                exRange.Range["C7:C7"].Value = "Mã lớp";
                exRange.Range["D7:D7"].Value = "Mã môn";
                exRange.Range["E7:E7"].Value = "Học kỳ";
                exRange.Range["F7:F7"].Value = "Lần thi";
                exRange.Range["G7:G7"].Value = "Điểm";

                for (hang = 0; hang < tblInBangDiemSV.Rows.Count; hang++)
                {
                    //Điền số thứ tự vào cột 1 từ dòng 8
                    exSheet.Cells[1][hang + 8] = hang + 1;
                    for (cot = 0; cot < tblInBangDiemSV.Columns.Count; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 8
                    {
                        exSheet.Cells[cot + 2][hang + 8] = tblInBangDiemSV.Rows[hang][cot].ToString();
                        if (cot == 3) exSheet.Cells[cot + 2][hang + 8] = tblInBangDiemSV.Rows[hang][cot].ToString();
                    }
                }
                exSheet.Name = "Danh Sách Điểm Sinh viên";
                exApp.Visible = true;
            }
        }    
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetValues1();
            ResetValues2();
            Load_DataGridView();
        }
        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {
            string sql = "select * from Diem where MaSV='" + txtMaSV.Text.Trim() + "'";
            tblInBangDiemSV = DAO.GetDataToTable(sql);
            GridViewInBangDiemSV.DataSource = tblInBangDiemSV;
            sql = "select TenSV from SinhVien where MaSV='" + txtMaSV.Text.Trim() + "'";

        }
        private void txtLanThi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void rdoMaSV_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMaSV.Checked == true)
            {
                txtMaSV.Enabled = true;
                cmbMaLop.Enabled = false;
                cmbMaMon.Enabled = false;
                txtLanThi.Enabled = false;
                ResetValues2();
            }
            else if (rdoTim.Checked == true)
            {
                txtMaSV.Enabled = false;
                cmbMaLop.Enabled = true;
                cmbMaMon.Enabled = true;
                txtLanThi.Enabled = true;
                ResetValues1();
            }
        }
        private string RdoTim_Sql()
        {
            sql = "select * from Diem where 1=1";
            if (cmbMaLop.Text != "")
            {
                sql = sql + " AND MaLop ='" + cmbMaLop.SelectedValue + "'";
            }
            if (cmbMaMon.Text != "")
            {
                sql = sql + " AND MaMon ='" + cmbMaMon.SelectedValue + "'";
            }
            if (txtLanThi.Text != "")
            {
                sql = sql + " AND LanThi =" + txtLanThi.Text;
            }
            sql = sql + " order by MaSV, MaLop, HocKy, MaMon, LanThi";
            return sql;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            btnLamMoi.Enabled = true;
            if(rdoMaSV.Checked)
            {
                 CheckRdoMaSV();
                 sql = "select * from Diem where MaSV='" + txtMaSV.Text.Trim()+"'";          
            }
            else if(rdoTim.Checked)
            {
                sql = RdoTim_Sql();
            }
             tblInBangDiemSV = DAO.GetDataToTable(sql);
             GridViewInBangDiemSV.DataSource = tblInBangDiemSV;
        }

        private void cmbMaMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            sql = "select TenMon from MonHoc where MaMon='" + cmbMaMon.SelectedValue + "'";
            txtTenMon.Text = DAO.GetFieldValues(sql);
        }
    }
}
