using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyDiem
{
    public partial class FrmSinhVien : Form
    {
        string sql;
        DataTable tblSinhVien;
        string GioiTinh;
        public FrmSinhVien()
        {
            InitializeComponent();
        }

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            LoadDatatogriview();

            cmbMaKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaQue.DropDownStyle = ComboBoxStyle.DropDownList; ;
            cmbMaLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaDanToc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaChuyenNganh.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaHDT.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaChucVu.DropDownStyle = ComboBoxStyle.DropDownList;
            sql = "select MaLop, TenLop from Lop";
            DAO.FillDataToCombo(sql, cmbMaLop, "MaLop", "TenLop");
            sql = "select MaKhoa, TenKhoa from Khoa";
            DAO.FillDataToCombo(sql, cmbMaKhoa, "MaKhoa", "TenKhoa");
            sql = "select MaQue, TenQue from Que";
            DAO.FillDataToCombo(sql, cmbMaQue, "MaQue", "TenQue");
            sql = "select MaDanToc, TenDanToc from DanToc";
            DAO.FillDataToCombo(sql, cmbMaDanToc, "MaDanToc", "TenDanToc");
            sql = "select MaChuyenNganh, TenChuyenNganh from ChuyenNganh";
            DAO.FillDataToCombo(sql, cmbMaChuyenNganh, "MaChuyenNganh", "TenChuyenNganh");
            sql = "select MaChucVu, TenChucVu from ChucVu";
            DAO.FillDataToCombo(sql, cmbMaChucVu, "MaChucVu", "TenChucVu");
            sql = "select MaHDT, TenHDT from HeDaoTao";
            DAO.FillDataToCombo(sql, cmbMaHDT, "MaHDT", "TenHDT");
            btnLuu.Enabled = false;
            tat();
            btnSua.Enabled = false;
            btnHuy.Enabled = false;
            btnXoa.Enabled = false;
            btnQuayLai.Enabled = false;
            btnThem.Enabled = false;
            ResetValues();
            cmbMaKhoa.SelectedIndex = -1;
            cmbMaLop.SelectedIndex = -1;
            cmbMaChuyenNganh.SelectedIndex = -1;
            GridViewSinhVien.AllowUserToAddRows = false;
            txtMaSV.MaxLength = 15;
            txtTenSV.MaxLength = 50;
            GridViewSinhVien.ReadOnly = true;
        }
        private void LoadDatatogriview()
        {
            try
            {
                DAO.OpenConnection();
                sql = " select * from SinhVien";
                tblSinhVien = DAO.GetDataToTable(sql);
                GridViewSinhVien.DataSource = tblSinhVien;
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
        private void LoadDatatogriview1()
        {
            try
            {
                DAO.OpenConnection();
                sql = " select * from SinhVien where MaLop='" + cmbMaLop.SelectedValue + "'";
                tblSinhVien = DAO.GetDataToTable(sql);
                GridViewSinhVien.DataSource = tblSinhVien;
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
        public void tat()
        {
            txtMaSV.Enabled = false;
            txtTenSV.Enabled = false;

            mtbNgaySinh.Enabled = false;
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            cmbMaQue.Enabled = false;
            cmbMaDanToc.Enabled = false;
            //cmbMaChuyenNganh.Enabled = false;
            cmbMaHDT.Enabled = false;
            cmbMaChucVu.Enabled = false;
        }
        private void ResetValues()
        {
            txtMaSV.Text = "";
            txtTenSV.Text = "";
            cmbMaQue.SelectedIndex = -1;
            cmbMaDanToc.SelectedIndex = -1;
            cmbMaHDT.SelectedIndex = -1;

            cmbMaChucVu.SelectedIndex = -1;
            rdNam.Checked = false;
            rdNu.Checked = false;
            mtbNgaySinh.Text = "";
        }

        private void cmbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

            string str = "select MaLop,TenLop from Lop where MaKhoa=N'" + cmbMaKhoa.SelectedValue + "'";
            DAO.FillDataToCombo(str, cmbMaLop, "MaLop", "TenLop");
            str = "select MaChuyenNganh,TenChuyenNganh from ChuyenNganh where MaKhoa=N'" + cmbMaKhoa.SelectedValue + "'";
            DAO.FillDataToCombo(str, cmbMaChuyenNganh, "MaChuyenNganh", "TenChuyenNganh");
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();

            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            cmbMaKhoa.Enabled = false;
            cmbMaLop.Enabled = false;
            GridViewSinhVien.Enabled = false;
            bat();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnDanhSach.Enabled = false;
            txtMaSV.Focus();
        }
        public void bat()
        {
            txtMaSV.Enabled = true;
            txtTenSV.Enabled = true;
            mtbNgaySinh.Enabled = true;
            rdNam.Enabled = true;
            rdNu.Enabled = true;
            cmbMaQue.Enabled = true;
            cmbMaDanToc.Enabled = true;
            cmbMaChuyenNganh.Enabled = true;
            cmbMaHDT.Enabled = true;
            cmbMaChucVu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "")
            {
                MessageBox.Show("Bạn không được để trống mã sinh viên");
                txtMaSV.Focus();

                return;
            }
            if (txtTenSV.Text == "")

            {
                MessageBox.Show("Bạn không được để trống tên sinh viên");
                txtTenSV.Focus();
                return;
            }
            if (cmbMaKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn khoa");
                cmbMaKhoa.Focus();
                return;
            }
            if (cmbMaLop.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn  lớp");
                cmbMaLop.Focus();
                return;
            }
            if (cmbMaQue.SelectedIndex == -1)

            {
                MessageBox.Show("Bạn chưa chọn quê");
                cmbMaQue.Focus();
                return;
            }
            if (cmbMaDanToc.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn  dân tộc");
                cmbMaDanToc.Focus();
                return;
            }
            if (cmbMaHDT.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn hệ đào tạo");
                cmbMaHDT.Focus();
                return;
            }
            if (cmbMaChuyenNganh.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn chuyên ngành");
                cmbMaChuyenNganh.Focus();
                return;
            }
            if (cmbMaChucVu.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn chức vụ");
                cmbMaChucVu.Focus();
                return;
            }

            if (mtbNgaySinh.Text == "  /  /")
            {
                MessageBox.Show("Bạn không để trống ngày sinh");
                mtbNgaySinh.Focus();
                return;
            }
            if (rdNam.Checked == false && rdNu.Checked == false)
            {
                MessageBox.Show("Bạn chưa chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DAO.IsDate(mtbNgaySinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbNgaySinh.Text = "";
                mtbNgaySinh.Focus();
                return;
            }

            if (rdNam.Checked == true)
            {
                GioiTinh = "Nam";
            }
            if (rdNu.Checked == true)
            {
                GioiTinh = "Nữ";
            }

            string s1 = " select MaSV from SinhVien where MaSV ='" + txtMaSV.Text.Trim() + "'";
            DAO.OpenConnection();
            if (DAO.CheckKeyExist(s1))
            {
                MessageBox.Show(" Mã sinh viên đã tồn tại", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DAO.CloseConnection();
                txtMaSV.Focus();
                return;
            }
            sql = " insert into SinhVien  values('" + txtMaSV.Text.Trim() + "',N'" + txtTenSV.Text.Trim() + "','" + cmbMaKhoa.SelectedValue.ToString() + "','" + cmbMaLop.SelectedValue.ToString() + "','"
           + DAO.ConvertDateTime(mtbNgaySinh.Text) + "',N'" + GioiTinh + "','" + cmbMaQue.SelectedValue.ToString() + "','" + cmbMaDanToc.SelectedValue.ToString() + "','" + cmbMaChuyenNganh.SelectedValue.ToString()
           + "','" + cmbMaHDT.SelectedValue.ToString() + "','" + cmbMaChucVu.SelectedValue.ToString() + "')";
            string sql1 = " update Lop set SiSo = SiSo +1 WHERE MaLop = '" + cmbMaLop.SelectedValue + "'";
            DAO.RunSql(sql1);
            int s2 = Convert.ToInt32(DAO.GetFieldValues(" select SiSo from Lop where MaLop = '" + cmbMaLop.SelectedValue + "'"));
            MessageBox.Show(" Bạn đã thêm mới thành công, sĩ số sau khi cập nhật của phòng '" + cmbMaLop.Text + "' là " + s2, " thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DAO.RunSql(sql);
            DAO.CloseConnection();
            LoadDatatogriview1();
            DAO.CloseConnection();
            GridViewSinhVien.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaSV.Enabled = false;
            btnDanhSach.Enabled = true;
            cmbMaLop.Enabled = true;
            cmbMaKhoa.Enabled = true;
        }

        private void GridViewSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSV.Text = GridViewSinhVien.CurrentRow.Cells["clmMaSV"].Value.ToString();
            txtTenSV.Text = GridViewSinhVien.CurrentRow.Cells["clmTenSV"].Value.ToString();
            sql = "SELECT TenKhoa FROM Khoa WHERE MaKhoa='" + GridViewSinhVien.CurrentRow.Cells["clmMaKhoa"].Value.ToString() + "'";
            string s1 = "SELECT TenLop FROM Lop WHERE MaLop='" + GridViewSinhVien.CurrentRow.Cells["clmMaLop"].Value.ToString() + "'";
            string s2 = "SELECT TenQue FROM Que WHERE MaQue='" + GridViewSinhVien.CurrentRow.Cells["clmMaQue"].Value.ToString() + "'";
            string s3 = "SELECT TenDanToc FROM DanToc WHERE MaDanToc='" + GridViewSinhVien.CurrentRow.Cells["clmMaDanToc"].Value.ToString() + "'";
            string s4 = "SELECT TenChuyenNganh FROM ChuyenNganh WHERE MaChuyenNganh='" + GridViewSinhVien.CurrentRow.Cells["clmMaChuyenNganh"].Value.ToString() + "'";
            string s5 = "SELECT TenHDT FROM HeDaoTao WHERE MaHDT='" + GridViewSinhVien.CurrentRow.Cells["clmMaHDT"].Value.ToString() + "'";
            string s6 = "SELECT TenChucVu FROM ChucVu WHERE MaChucVu='" + GridViewSinhVien.CurrentRow.Cells["clmMaChucVu"].Value.ToString() + "'";
            cmbMaKhoa.Text = DAO.GetFieldValues(sql);
            cmbMaLop.Text = DAO.GetFieldValues(s1);
            mtbNgaySinh.Text = GridViewSinhVien.CurrentRow.Cells["clmNgaySinh"].Value.ToString();
            cmbMaQue.Text = DAO.GetFieldValues(s2);
            cmbMaDanToc.Text = DAO.GetFieldValues(s3);
            cmbMaChuyenNganh.Text = DAO.GetFieldValues(s4);
            cmbMaHDT.Text = DAO.GetFieldValues(s5);
            cmbMaChucVu.Text = DAO.GetFieldValues(s6);

            if (GridViewSinhVien.CurrentRow.Cells["clmGioitinh"].Value.ToString() == "Nam")
                rdNam.Checked = true;
            else
                rdNu.Checked = true;
            if (btnQuayLai.Enabled == true)
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnHuy.Enabled = true;
                btnThem.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnHuy.Enabled = true;
                btnThem.Enabled = false;
            }

            txtMaSV.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int s1 = Convert.ToInt32(DAO.GetFieldValues("select SiSo from Lop where MaLop='" + cmbMaLop.SelectedValue + "'"));
            if (tblSinhVien.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSV.Text == "")
            {
                MessageBox.Show("Chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string s11 = "select MaSV from Diem where MaSV='" + txtMaSV.Text.Trim() + "'";
            if (DAO.CheckKeyExist(s11) == true)
                MessageBox.Show("Bạn không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                if (MessageBox.Show("bạn có muốn xóa không?", "thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    sql = " delete from SinhVien where MaSV='" + txtMaSV.Text + "'";
                    String sql1 = " update Lop set SiSo = SiSo -1 WHERE MaLop = '" + cmbMaLop.SelectedValue + "'";
                    DAO.RunSql(sql1);
                    s1 = Convert.ToInt32(DAO.GetFieldValues(" select SiSo from Lop where MaLop = '" + cmbMaLop.SelectedValue + "'"));
                    MessageBox.Show(" Sĩ số sau khi cập nhật của lớp '" + cmbMaLop.Text + "' là " + s1, " thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DAO.OpenConnection();
                    DAO.RunSql(sql);
                    ResetValues();
                    LoadDatatogriview1();
                }
            }
            cmbMaLop.Enabled = true;
            cmbMaKhoa.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (tblSinhVien.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSV.Text == "")
            {
                MessageBox.Show("Chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtTenSV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSV.Focus();
                return;
            }
            if (cmbMaKhoa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbMaKhoa.Focus();
                return;
            }
            if (cmbMaLop.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbMaLop.Focus();
                return;
            }
            if (cmbMaQue.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập quê", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbMaQue.Focus();
                return;
            }
            if (cmbMaDanToc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập dân tôc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbMaDanToc.Focus();
                return;
            }
            if (cmbMaChucVu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chức vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbMaChucVu.Focus();
                return;
            }
            if (cmbMaChuyenNganh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chuyên ngành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbMaChuyenNganh.Focus();
                return;
            }
            if (mtbNgaySinh.Text == "  /  /")

            {
                MessageBox.Show("Bạn không để trống ngày sinh");
                mtbNgaySinh.Focus();
                return;
            }
            if (!DAO.IsDate(mtbNgaySinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbNgaySinh.Text = "";
                mtbNgaySinh.Focus();
                return;
            }
            if (rdNam.Checked == true)
                GioiTinh = "Nam";
            if (rdNu.Checked == true)
                GioiTinh = "Nữ";

            sql = "UPDATE SinhVien SET  TenSV=N'" + txtTenSV.Text.ToString() +
                  "',MaKhoa='" + cmbMaKhoa.SelectedValue.ToString() + "',MaLop='" + cmbMaLop.SelectedValue.ToString() +
                    "',NgaySinh='" + DAO.ConvertDateTime(mtbNgaySinh.Text) + "',GioiTinh=N'" + GioiTinh
                   + "',MaQue='" + cmbMaQue.SelectedValue.ToString() +
                   "',MaDanToc='" + cmbMaDanToc.SelectedValue.ToString() + "',MaChuyenNganh='" + cmbMaChuyenNganh.SelectedValue.ToString() + "',MaHDT='" + cmbMaHDT.SelectedValue.ToString() + "',MaChucVu='" + cmbMaChucVu.SelectedValue.ToString() +
                   "' WHERE MaSV='" + txtMaSV.Text + "'";
            string A = cmbMaLop.SelectedValue.ToString();
            string B = Convert.ToString(DAO.GetFieldValues("select MaLop from SinhVien where MaSV='" + txtMaSV.Text + "'"));
            if (A != B)
            {
                int s1 = Convert.ToInt32(DAO.GetFieldValues("select SiSo from Lop where MaLop='" + A + "'"));
                String sql1 = " update Lop set SiSo = SiSo +1 WHERE MaLop = '" + A + "'";
                DAO.RunSql(sql1);
                s1 = Convert.ToInt32(DAO.GetFieldValues(" select SiSo from Lop where MaLop = '" + A + "'"));
                MessageBox.Show(" Sĩ số sau khi cập nhật của lớp '" + A + "' là " + s1, " thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int s2 = Convert.ToInt32(DAO.GetFieldValues("select SiSo from Lop where MaLop='" + B + "'"));
                String sql2 = " update Lop set SiSo = SiSo -1 WHERE MaLop = '" + B + "'";
                DAO.RunSql(sql2);
                s2 = Convert.ToInt32(DAO.GetFieldValues("select SiSo from Lop where MaLop='" + B + "'"));
                MessageBox.Show(" Sĩ số sau khi cập nhật của lớp '" + B + "' là " + s2, " thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                


            }
            DAO.OpenConnection();
            DAO.RunSql(sql);
            LoadDatatogriview1();
            DAO.CloseConnection();
            cmbMaLop.Enabled = true;
            cmbMaKhoa.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (btnQuayLai.Enabled == true)
            {
                ResetValues();
                btnHuy.Enabled = false;
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                txtMaSV.Enabled = false;
                GridViewSinhVien.Enabled = true;
                btnDanhSach.Enabled = true;
                cmbMaLop.Enabled = true;
                cmbMaKhoa.Enabled = true;
            }
            else
            {
                ResetValues();
                btnHuy.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                txtMaSV.Enabled = false;
                GridViewSinhVien.Enabled = true;
                btnDanhSach.Enabled = true;
                cmbMaLop.Enabled = true;
                cmbMaKhoa.Enabled = true;
                cmbMaKhoa.SelectedIndex = -1;
                cmbMaLop.SelectedIndex = -1;
                cmbMaChuyenNganh.SelectedIndex = -1;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDanhSach_Click(object sender, EventArgs e)
        {

            if (cmbMaKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn khoa khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbMaLop.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn mã lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ResetValues();
            //cmbMaChuyenNganh.SelectedIndex = -1;
            DataTable tblSinhVien = DAO.GetDataToTable("select * from SinhVien where MaLop='" + cmbMaLop.SelectedValue + "'");
            GridViewSinhVien.DataSource = tblSinhVien;
            btnThem.Enabled = true;
            btnQuayLai.Enabled = true;
            bat();
            txtMaSV.Enabled = false;

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadDatatogriview();
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnXoa.Enabled = false;
            btnQuayLai.Enabled = false;
            btnSua.Enabled = false;
            tat();
            cmbMaKhoa.SelectedIndex = -1;
            cmbMaLop.SelectedIndex = -1;
            cmbMaChuyenNganh.SelectedIndex = -1;
            btnThem.Enabled = false;
            GridViewSinhVien.Enabled = true;
            btnDanhSach.Enabled = true;
            cmbMaLop.Enabled = true;
            cmbMaKhoa.Enabled = true;
        }
    }
}
