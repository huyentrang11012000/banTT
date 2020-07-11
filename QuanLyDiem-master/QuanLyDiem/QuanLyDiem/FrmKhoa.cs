using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;


namespace QuanLyDiem
{
    public partial class FrmKhoa : Form
    {
        DataTable tblKhoa;
        public FrmKhoa()
        {
            InitializeComponent();
        }

        private void FrmKhoa_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            LoadDataToGrivew();
            txtMaKhoa.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            DAO.CloseConnection();
        }
        public void LoadDataToGrivew()
        {
            try
            {
                DAO.OpenConnection();
                string sql = "select * from Khoa";
                tblKhoa = DAO.GetDataToTable(sql);
                GridViewKhoa.DataSource = tblKhoa;

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

        private void ResetValues()
        {
            txtMaKhoa.Text = "";
            txtTenKhoa.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            txtWebsite.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            GridViewKhoa.Enabled = false;
            txtMaKhoa.Enabled = true;
            txtMaKhoa.Focus();
            return;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            if (txtMaKhoa.Text == "")
            {
                MessageBox.Show("bạn chưa nhập mã khoa ");
                txtMaKhoa.Focus();
                return;
            }
            if (txtTenKhoa.Text == "")
            {
                MessageBox.Show("bạn chưa nhập tên khoa");
                txtTenKhoa.Focus();
                return;
            }
            if (txtDienThoai.Text == "")
            {
                MessageBox.Show("bạn chưa nhập số điện thoại ");
                txtDienThoai.Focus();
                return;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("bạn chưa nhập địa chỉ ");
                txtDiaChi.Focus();
                return;
            }
            if (txtWebsite.Text == "")
            {
                MessageBox.Show("bạn chưa nhập website ");
                txtWebsite.Focus();
                return;
            }

            string sqlCheckKey = "Select * from Khoa Where MaKhoa = '"
                                + txtMaKhoa.Text.ToString() + "'";

            if (DAO.CheckKeyExist(sqlCheckKey))
            {
                MessageBox.Show("Mã khoa đã tồn tại", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DAO.CloseConnection();
                txtMaKhoa.Focus();
                return;
            }
            string sql = "insert into Khoa values ('" +
                            txtMaKhoa.Text.ToString() + "' , N'" +
                            txtTenKhoa.Text.Trim() + "','" + txtDienThoai.Text.Trim() + "','" + txtDiaChi.Text.Trim() + "','" + txtWebsite.Text.Trim() + "')";
            DAO.RunSql(sql);
            LoadDataToGrivew();
            GridViewKhoa.Enabled = true;
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = true;
            btnLuu.Enabled = false;
            txtMaKhoa.Enabled = false;
            DAO.CloseConnection();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            string sql;          
            if (tblKhoa.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhoa.Text == "")
            {
                MessageBox.Show("Chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenKhoa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhoa.Focus();
                return;
            }
             sql = "Update Khoa set TenKhoa = N'" + txtTenKhoa.Text.Trim() + "', DienThoai = N'" + txtDienThoai.Text.Trim()
                + "',DiaChi = N'" + txtDiaChi.Text.Trim() + "', Website = N'" + txtWebsite.Text.Trim() + "' where MaKhoa = '"
                + txtMaKhoa.Text.ToString() + "'";     
             DAO.RunSql(sql);
             LoadDataToGrivew();
             ResetValues();
             btnHuy.Enabled = false;
             DAO.CloseConnection();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //DAO.OpenConnection();
            string sql;
            if (tblKhoa.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhoa.Text == "")
            {
                MessageBox.Show("Chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "select MaKhoa from ChuyenNganh where MaKhoa='" + txtMaKhoa.Text.Trim() + "'";
            string sql1 = "select MaKhoa from SinhVien where MaKhoa='" + txtMaKhoa.Text.Trim() + "'";
            string sql2 = "select MaKhoa from Lop where MaKhoa='" + txtMaKhoa.Text.Trim() + "'";
            if (DAO.CheckKeyExist(sql) || DAO.CheckKeyExist(sql1) || DAO.CheckKeyExist(sql2))
                MessageBox.Show("Bạn không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    sql = "DELETE Khoa WHERE MaKhoa='" + txtMaKhoa.Text.ToString() + "'";
                    DAO.RunSql(sql);
                    LoadDataToGrivew();
                    ResetValues();
                }
            }
            //DAO.CloseConnection();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            GridViewKhoa.Enabled = true;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaKhoa.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GridViewKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DAO.OpenConnection();
            txtMaKhoa.Text = GridViewKhoa.CurrentRow.Cells["clmMaKhoa"].Value.ToString();
            txtTenKhoa.Text = GridViewKhoa.CurrentRow.Cells["clmTenKhoa"].Value.ToString();
            txtDienThoai.Text = GridViewKhoa.CurrentRow.Cells["clmDienThoai"].Value.ToString();
            txtDiaChi.Text = GridViewKhoa.CurrentRow.Cells["clmDiaChi"].Value.ToString();
            txtWebsite.Text = GridViewKhoa.CurrentRow.Cells["clmWebsite"].Value.ToString();
            txtMaKhoa.Enabled = false;
            btnHuy.Enabled = true;
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}



