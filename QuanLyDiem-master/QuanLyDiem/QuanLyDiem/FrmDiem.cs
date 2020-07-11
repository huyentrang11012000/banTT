using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace QuanLyDiem
{
    public partial class FrmDiem : Form
    {
        DataTable tblDiem;
        public FrmDiem()
        {
            InitializeComponent();
        }
        private void FrmDiem_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            //Load_DataGridView();
            checkupdate();
            string sql = "select distinct MaLop from Thoi_Khoa_Bieu";
            DAO.FillDataToCombo(sql, cmbMaLop, "MaLop", "MaLop");
            sql = "select distinct Thoi_Khoa_Bieu.MaMon, TenMon from MonHoc inner join Thoi_Khoa_Bieu on MonHoc.MaMon=Thoi_Khoa_Bieu.MaMon";
            DAO.FillDataToCombo(sql, cmbMonHoc, "MaMon", "TenMon");
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnQuayLai.Enabled = false;
            btnSua.Enabled = false;
            txtMaSV.Enabled = false;
            txtTenSV.Enabled = false;
            txtDiem.Enabled = false;
            ResetValues();
            cmbMaLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            //txtLanThi.MaxLength = 1;
            GridViewDiem.AllowUserToAddRows = false;
        }
        private void checkupdate()
        {
            string d1 = "delete from Diem where MaSV =(SELECT distinct Diem.MaSV FROM SinhVien inner join Diem on SinhVien.MaSV=Diem.MaSV where SinhVien.MaLop != Diem.MaLop)";
            // MessageBox.Show(d1);
            DAO.RunSql(d1);
            string d = "INSERT INTO Diem(MaSV,MaLop,MaMon,HocKy,LanThi,Diem) SELECT distinct MaSV, Thoi_Khoa_Bieu.MaLop, MaMon, HocKy,1, null FROM SinhVien inner join Thoi_Khoa_Bieu on SinhVien.MaLop = Thoi_Khoa_Bieu.MaLop WHERE MaSV=(SELECT distinct Diem.MaSV FROM SinhVien inner join Diem on SinhVien.MaSV=Diem.MaSV where SinhVien.MaLop != Diem.MaLop)";
            DAO.RunSql(d);
            //MessageBox.Show(d);
            string str;
            str = "INSERT INTO Diem(MaSV,MaLop,MaMon,HocKy,LanThi,Diem) SELECT distinct MaSV, Thoi_Khoa_Bieu.MaLop, MaMon, HocKy,1, null FROM SinhVien inner join Thoi_Khoa_Bieu on SinhVien.MaLop = Thoi_Khoa_Bieu.MaLop WHERE MaSV NOT IN(SELECT MaSV FROM Diem)";
            DAO.RunSql(str);
        }
        private void ResetValues()
        {
            txtMaSV.Text = "";
            txtDiem.Text = "";
            txtLanThi.Text = "";
            txtTenSV.Text = "";
            cmbMaLop.SelectedIndex = -1;
            cmbMonHoc.SelectedIndex = -1;
            cmbHocKy.SelectedIndex = -1;
        }
        private void Load_DataGridView()
        {
            try
            {
                DAO.OpenConnection();
                string str;
                str = " select MaSV,Diem from Diem where MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "'";
                tblDiem = DAO.GetDataToTable(str);
                GridViewDiem.DataSource = tblDiem;

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
        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            if (cmbMaLop.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn mã lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbHocKy.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn học kỳ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbMonHoc.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn môn học", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtLanThi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập lần thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLanThi.Focus();
                return;
            }
            if (Convert.ToInt32(txtLanThi.Text.Trim())== 0)
            {
                MessageBox.Show("Lân thi không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLanThi.Focus();
                txtLanThi.Text = "";
                return;
            }
            string str;
            str = " select MaSV,Diem from Diem where MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "'";
            tblDiem = DAO.GetDataToTable(str);
            GridViewDiem.DataSource = tblDiem;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnQuayLai.Enabled = true;
            txtMaSV.Text = "";
            txtDiem.Text = "";
        }

        private void cmbMaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "select distinct HocKy from Thoi_Khoa_Bieu where MaLop = '" + cmbMaLop.SelectedValue + "'";
            DAO.FillDataToCombo(str, cmbHocKy, "HocKy", "HocKy");
            cmbHocKy.SelectedIndex = -1;
        }

        private void cmbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "select Thoi_Khoa_Bieu.MaMon,TenMon from Thoi_Khoa_Bieu inner join MonHoc on Thoi_Khoa_Bieu.MaMon = MonHoc.MaMon where MaLop = '" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' group by Thoi_Khoa_Bieu.MaMon,TenMon";
            DAO.FillDataToCombo(str, cmbMonHoc, "MaMon", "TenMon");
        }

        public void cell()
        {
            txtMaSV.Text = GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString();
            txtDiem.Text = GridViewDiem.CurrentRow.Cells["clmDiem"].Value.ToString();
            GridViewDiem.CurrentRow.Cells["clmMaSV"].ReadOnly = true;
            btnHuy.Enabled = true;
            string ss = (DAO.GetFieldValues("Select Diem from Diem where MaSV = '" + GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() + "'and MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "'"));
            if (GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() != "" && ss == "")
            {
                btnLuu.Enabled = true;
                btnSua.Enabled = false;
                btnDanhSach.Enabled = false;
                cmbMaLop.Enabled = false;
                txtLanThi.Enabled = false;
                cmbHocKy.Enabled = false;
                cmbMonHoc.Enabled = false;
                btnQuayLai.Enabled = false;
                btnThoat.Enabled = false;
            }
            else if (GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() != "" && ss != "")
            {

                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                btnDanhSach.Enabled = false;
                cmbMaLop.Enabled = false;
                txtLanThi.Enabled = false;
                cmbHocKy.Enabled = false;
                cmbMonHoc.Enabled = false;
                btnQuayLai.Enabled = false;
                btnThoat.Enabled = false;
            }
        }
        public void kt1()
        {
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            string ss = (DAO.GetFieldValues("Select Diem from Diem where MaSV = '" + GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() + "'and MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "'"));
            for (int i = 0; i < tblDiem.Rows.Count; i++)
            {
                if (GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() != "" && ss != "" && (Convert.ToString(GridViewDiem.Rows[i].Cells["clmDiem"].Value) != (DAO.GetFieldValues("Select Diem from Diem where MaSV = '" + GridViewDiem.Rows[i].Cells["clmMaSV"].Value.ToString() + "'and MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "'"))))
                {
                    MessageBox.Show("Bạn chưa lưu điểm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GridViewDiem.CurrentCell = GridViewDiem.Rows[i].Cells["clmDiem"];
                    break;
                }

            }
        }
        public void kt2()
        {
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            for (int i = 0; i < tblDiem.Rows.Count; i++)
            {
                if ((Convert.ToString(GridViewDiem.Rows[i].Cells["clmDiem"].Value) != (DAO.GetFieldValues("Select Diem from Diem where MaSV = '" + GridViewDiem.Rows[i].Cells["clmMaSV"].Value.ToString() + "'and MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "'"))) && GridViewDiem.CurrentCell != GridViewDiem.Rows[i].Cells["clmDiem"])
                {
                    MessageBox.Show("Bạn chưa sửa xong điểm sinh viên này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GridViewDiem.CurrentCell = GridViewDiem.Rows[i].Cells["clmDiem"];
                }
            }
        }

        private void GridViewDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((btnQuayLai.Enabled == true && btnDanhSach.Enabled == true) || (btnQuayLai.Enabled == false && btnDanhSach.Enabled == false))
            {
                if (btnSua.Enabled == true)
                {
                    kt2();
                }
                else if (btnLuu.Enabled == true)
                {
                    kt1();
                }
                cell();
                GridViewDiem.ReadOnly = false;
            }
            else if (btnQuayLai.Enabled == false && btnDanhSach.Enabled == true)
            {
                GridViewDiem.ReadOnly = true;
            }
        }

        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (txtMaSV.Text == "")
            {
                txtTenSV.Text = "";
            }
            str = "Select TenSV from SinhVien where MaSV = '" + txtMaSV.Text.Trim() + "'";
            txtTenSV.Text = DAO.GetFieldValues(str);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            string sql;
            for (int i = 0; i < tblDiem.Rows.Count; i++)
            {
                if ((GridViewDiem.Rows[i].Cells["clmDiem"].Value.ToString().Trim() != "") && (Convert.ToDouble(GridViewDiem.Rows[i].Cells["clmDiem"].Value) > 10))
                {
                    MessageBox.Show("Bạn nhập sai điểm.Nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GridViewDiem.CurrentCell = GridViewDiem.Rows[i].Cells["clmDiem"];
                    return;
                }
            }

            for (int i = 0; i < tblDiem.Rows.Count; i++)
            {
                string ss = (DAO.GetFieldValues("Select Diem from Diem where MaSV = '" + GridViewDiem.Rows[i].Cells["clmMaSV"].Value.ToString() + "'and MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "'"));
                if (Convert.ToString(GridViewDiem.Rows[i].Cells["clmDiem"].Value) != ss && ss == "")
                {
                    int t = 0;
                    if (Convert.ToDouble(GridViewDiem.Rows[i].Cells["clmDiem"].Value) < Convert.ToInt32(GridViewDiem.Rows[i].Cells["clmDiem"].Value))
                    {
                        t = Convert.ToInt32(GridViewDiem.Rows[i].Cells["clmDiem"].Value);
                    }
                    else if ((Convert.ToDouble(GridViewDiem.Rows[i].Cells["clmDiem"].Value) - Convert.ToInt32(GridViewDiem.Rows[i].Cells["clmDiem"].Value)) == 0.5)
                    {
                        t = Convert.ToInt32(GridViewDiem.Rows[i].Cells["clmDiem"].Value) + 1;
                    }
                    else if ((Convert.ToDouble(GridViewDiem.Rows[i].Cells["clmDiem"].Value) - Convert.ToInt32(GridViewDiem.Rows[i].Cells["clmDiem"].Value)) < 0.5)
                    {
                        t = Convert.ToInt32(GridViewDiem.Rows[i].Cells["clmDiem"].Value);
                    }
                    else if (Convert.ToDouble(GridViewDiem.Rows[i].Cells["clmDiem"].Value) == Convert.ToInt32(GridViewDiem.Rows[i].Cells["clmDiem"].Value))
                    {
                        t = Convert.ToInt32(GridViewDiem.Rows[i].Cells["clmDiem"].Value);
                    }
                    sql = "UPDATE Diem SET Diem = " + t + " WHERE MaSV=N'" + GridViewDiem.Rows[i].Cells["clmMaSV"].Value.ToString() + "'AND MaLop=N'" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "'AND MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "'";
                    DAO.RunSql(sql);

                    if (t < 5)
                    {
                        sql = "INSERT INTO Diem VALUES(N'" + GridViewDiem.Rows[i].Cells["clmMaSV"].Value.ToString() + "',N'" + cmbMaLop.SelectedValue + "','" + cmbMonHoc.SelectedValue + "',"
                             + cmbHocKy.Text + "," + (Convert.ToInt32(txtLanThi.Text.Trim()) + 1) + ",null)";
                        DAO.RunSql(sql);
                    }

                }
            }
            string a1 = (DAO.GetFieldValues("Select count(*) from Diem where MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "' and diem<5"));
            if (Convert.ToInt32(a1) > 0)
            {
                MessageBox.Show("Lớp có " + Convert.ToInt32(a1) + " sinh viên phải thi lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Load_DataGridView();
            btnDanhSach.Enabled = true;
            cmbMaLop.Enabled = true;
            txtLanThi.Enabled = true;
            cmbHocKy.Enabled = true;
            cmbMonHoc.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnDanhSach.Enabled = true;
            btnQuayLai.Enabled = true;
            txtMaSV.Text = "";
            txtDiem.Text = "";
            DAO.CloseConnection();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnQuayLai.Enabled = true;
            btnThoat.Enabled = true;
            txtMaSV.Text = "";
            txtDiem.Text = "";
            Load_DataGridView();
            btnDanhSach.Enabled = true;
            cmbMaLop.Enabled = true;
            txtLanThi.Enabled = true;
            cmbHocKy.Enabled = true;
            cmbMonHoc.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            Load_DataGridView();
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnQuayLai.Enabled = false;
            btnSua.Enabled = false;
            btnDanhSach.Enabled = true;
            cmbMaLop.Enabled = true;
            txtLanThi.Enabled = true;
            cmbHocKy.Enabled = true;
            cmbMonHoc.Enabled = true;
        }
        private void txtLanThi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }


        private void GridViewDiem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }
        public void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == '.') || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            string sql;

            string s4 = "SELECT MaSV, MaLop, MaMon,HocKy,LanThi FROM Diem WHERE MaSV=N'" + GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() + "'AND MaLop=N'" + cmbMaLop.SelectedValue + "'AND MaMon='" + cmbMonHoc.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and LanThi='" + (Convert.ToInt32(txtLanThi.Text.Trim()) + 1) + "'";
            if (GridViewDiem.CurrentRow.Cells["clmDiem"].Value.ToString().Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập điểm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((Convert.ToDouble(GridViewDiem.CurrentRow.Cells["clmDiem"].Value.ToString().Trim()) > 10))
            {
                MessageBox.Show("Bạn nhập sai điểm.Nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiem.Focus();
                return;
            }
            string str = DAO.GetFieldValues("Select Diem from Diem where MaSV = '" + GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() + "'and MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + (Convert.ToInt32(txtLanThi.Text.Trim()) + 1) + "'");
            if (str != "")
            {
                MessageBox.Show("Bạn không thể sửa vì sinh viên này đã thi lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int x = 0;
                if (Convert.ToDouble(GridViewDiem.CurrentRow.Cells["clmDiem"].Value) < Convert.ToInt32(GridViewDiem.CurrentRow.Cells["clmDiem"].Value))
                {
                    x = Convert.ToInt32(GridViewDiem.CurrentRow.Cells["clmDiem"].Value);
                }
                else if ((Convert.ToDouble(GridViewDiem.CurrentRow.Cells["clmDiem"].Value) - Convert.ToInt32(GridViewDiem.CurrentRow.Cells["clmDiem"].Value)) == 0.5)
                {
                    x = Convert.ToInt32(GridViewDiem.CurrentRow.Cells["clmDiem"].Value) + 1;
                }
                else if ((Convert.ToDouble(GridViewDiem.CurrentRow.Cells["clmDiem"].Value) - Convert.ToInt32(GridViewDiem.CurrentRow.Cells["clmDiem"].Value)) < 0.5)
                {
                    x = Convert.ToInt32(GridViewDiem.CurrentRow.Cells["clmDiem"].Value);
                }
                else if (Convert.ToDouble(GridViewDiem.CurrentRow.Cells["clmDiem"].Value) == Convert.ToInt32(GridViewDiem.CurrentRow.Cells["clmDiem"].Value))
                {
                    x = Convert.ToInt32(GridViewDiem.CurrentRow.Cells["clmDiem"].Value);
                }
                sql = "UPDATE Diem SET Diem = " + x + " WHERE MaSV=N'" + GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() + "'AND MaLop=N'" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "'AND MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + txtLanThi.Text.Trim() + "'";
                DAO.RunSql(sql);
                txtDiem.Text = Convert.ToString(x);
                string str1 = "Select MaSV from Diem where MaSV = '" + GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() + "'and MaLop='" + cmbMaLop.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and MaMon='" + cmbMonHoc.SelectedValue + "' and LanThi='" + (Convert.ToInt32(txtLanThi.Text.Trim()) + 1) + "'";
                if (x >= 5 && DAO.CheckKeyExist(str1) == true)
                {
                    MessageBox.Show("Sinh viên này không phải thi lại lần sau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string s0 = "DELETE Diem WHERE MaSV=N'" + GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() + "'AND MaLop=N'" + cmbMaLop.SelectedValue + "'AND MaMon='" + cmbMonHoc.SelectedValue + "' and HocKy = '" + cmbHocKy.Text + "' and LanThi='" + (Convert.ToInt32(txtLanThi.Text.Trim()) + 1) + "'";
                    DAO.RunSql(s0);
                }
                else if (x < 5 && DAO.CheckKeyExist(s4) == true)
                {
                    MessageBox.Show("Sinh viên này phải thi lại lần " + (Convert.ToInt32(txtLanThi.Text.Trim()) + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (x < 5 && DAO.CheckKeyExist(s4) == false)
                {
                    MessageBox.Show("Sinh viên này phải thi lại lần " + (Convert.ToInt32(txtLanThi.Text.Trim()) + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sql = "INSERT INTO Diem VALUES(N'" + GridViewDiem.CurrentRow.Cells["clmMaSV"].Value.ToString() + "',N'" + cmbMaLop.SelectedValue + "','" + cmbMonHoc.SelectedValue + "',"
                         + cmbHocKy.Text + "," + (Convert.ToInt32(txtLanThi.Text.Trim()) + 1) + ",null)";
                    DAO.RunSql(sql);
                }
                btnDanhSach.Enabled = true;
                cmbMaLop.Enabled = true;
                txtLanThi.Enabled = true;
                cmbHocKy.Enabled = true;
                cmbMonHoc.Enabled = true;
            }
            DAO.CloseConnection();
            Load_DataGridView();
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnDanhSach.Enabled = true;
            btnQuayLai.Enabled = true;
        }
    }
}