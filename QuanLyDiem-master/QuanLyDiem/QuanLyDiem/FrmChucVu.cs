using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Windows;

namespace QuanLyDiem
{
    public partial class FrmChucVu : Form
    {
        DataTable tblChucVu;
        public FrmChucVu()
        {
            InitializeComponent();
        }

        private void FrmChucVu_Load(object sender, EventArgs e)
        {
            LoadDataToGrivew();
            txtMaChucVu.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        public void LoadDataToGrivew()
        {
            try
            {
                DAO.OpenConnection();
                string sql = "select * from ChucVu";
                tblChucVu = DAO.GetDataToTable(sql);
                GridViewChucVu.DataSource = tblChucVu;
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
        private void GridViewChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DAO.OpenConnection();
            txtMaChucVu.Text = GridViewChucVu.CurrentRow.Cells["clmMaChucVu"].Value.ToString();
            txtTenChucVu.Text = GridViewChucVu.CurrentRow.Cells["clmTenChucVu"].Value.ToString();
            txtMaChucVu.Enabled = false;
            btnHuy.Enabled = true;
        }
        private void ResetValues()
        {
            txtMaChucVu.Text = "";
            txtTenChucVu.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            GridViewChucVu.Enabled = false;
            txtMaChucVu.Enabled = true;
            txtMaChucVu.Focus();
            return;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            if (txtMaChucVu.Text == "")
            {
                MessageBox.Show("bạn chưa nhập mã chức vụ ");
                txtMaChucVu.Focus();
                return;
            }
            if (txtTenChucVu.Text == "")
            {
                MessageBox.Show("bạn chưa nhập tên chức vụ");
                txtTenChucVu.Focus();
                return;
            }
            string sqlCheckKey = "Select * from ChucVu Where MaChucVu = '"
                                + txtMaChucVu.Text.Trim() + "'";
            if (DAO.CheckKeyExist(sqlCheckKey))
            {
                MessageBox.Show("Mã chức vụ đã tồn tại", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DAO.CloseConnection();
                txtMaChucVu.Focus();
                return;
            }
            string sql = "insert into ChucVu values ('" + txtMaChucVu.Text.Trim() + "' , N'" + txtTenChucVu.Text.Trim() + "')";
            DAO.RunSql(sql);
            LoadDataToGrivew();
            GridViewChucVu.Enabled = true;
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaChucVu.Enabled = false;
            DAO.CloseConnection();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            string sql;
            if (tblChucVu.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaChucVu.Text == "")
            {
                MessageBox.Show("Chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenChucVu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên chức vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenChucVu.Focus();
                return;
            }
            sql = "Update ChucVu set TenChucVu = N'" + txtTenChucVu.Text.Trim() + "' where MaChucVu = '" + txtMaChucVu.Text.Trim() + "'";
            DAO.RunSql(sql);
            LoadDataToGrivew();
            ResetValues();
            btnHuy.Enabled = false;
            DAO.CloseConnection();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblChucVu.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaChucVu.Text == "")
            {
                MessageBox.Show("Chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "select MaChucVu from SinhVien where MaChucVu='" + txtMaChucVu.Text.Trim() + "'";
            if (DAO.CheckKeyExist(sql))
                MessageBox.Show("Bạn không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    sql = "DELETE ChucVu WHERE MaChucVu= N'" + txtMaChucVu.Text.Trim() + "'";
                    DAO.RunSql(sql);
                    LoadDataToGrivew();
                    ResetValues();
                }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            GridViewChucVu.Enabled = true;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaChucVu.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
