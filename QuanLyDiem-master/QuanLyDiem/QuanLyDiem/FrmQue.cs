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

    public partial class FrmQue : Form
    {
        DataTable tblQue;

        public FrmQue()
        {
            InitializeComponent();
        }
        private void FrmQue_Load(object sender, EventArgs e)
        {
            LoadDataToGrivew();
            txtMaQue.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        public void LoadDataToGrivew()
        {
            try
            {
                DAO.OpenConnection();
                string sql = "select * from Que";
                tblQue = DAO.GetDataToTable(sql);
                GridViewQue.DataSource = tblQue;
                // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
                GridViewQue.AllowUserToAddRows = false;
                // Không cho phép sửa dữ liệu trực tiếp trên lưới
                GridViewQue.EditMode = DataGridViewEditMode.EditProgrammatically;
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

        private void GridViewQue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DAO.OpenConnection();
            txtMaQue.Text = GridViewQue.CurrentRow.Cells["clmMaQue"].Value.ToString();
            txtTenQue.Text = GridViewQue.CurrentRow.Cells["clmTenQue"].Value.ToString();
            txtMaQue.Enabled = false;
            btnHuy.Enabled = true;
        }
        private void ResetValues()
        {
            txtMaQue.Text = "";
            txtTenQue.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            GridViewQue.Enabled = false;
            txtMaQue.Enabled = true;
            txtMaQue.Focus();
            return;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            string sql;
            if (tblQue.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaQue.Text == "")
            {
                MessageBox.Show("Chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenQue.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên quê", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenQue.Focus();
                return;
            }
            sql = "Update Que set TenQue = N'" + txtTenQue.Text.Trim() + "' where MaQue = '" + txtMaQue.Text.Trim() + "'";  
            DAO.RunSql(sql);
            LoadDataToGrivew();
            ResetValues();
            btnHuy.Enabled = false;
            DAO.CloseConnection();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            if (txtMaQue.Text == "")
            {
                MessageBox.Show("bạn chưa nhập mã quê ");
                txtMaQue.Focus();
                return;
            }
            if (txtTenQue.Text == "")
            {
                MessageBox.Show("bạn chưa nhập tên quê");
                txtTenQue.Focus();
                return;
            }
            string sqlCheckKey = "Select * from Que Where MaQue = '"
                                + txtMaQue.Text.Trim() + "'";
            DAO.OpenConnection();


            if (DAO.CheckKeyExist(sqlCheckKey))
            {
                MessageBox.Show("Mã quê đã tồn tại", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DAO.CloseConnection();
                txtMaQue.Focus();
                return;
            }
            string sql = "insert into Que values ('" + txtMaQue.Text.Trim() + "' , N'" + txtTenQue.Text.Trim() + "')";
            DAO.RunSql(sql);
            LoadDataToGrivew();
            GridViewQue.Enabled = true;
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaQue.Enabled = false;
            DAO.CloseConnection();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblQue.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaQue.Text == "")
            {
                MessageBox.Show("Chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "select MaQue from SinhVien where MaQue='" + txtMaQue.Text.Trim() + "'";
            if (DAO.CheckKeyExist(sql))
                MessageBox.Show("Bạn không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    sql = "DELETE Que WHERE MaQue= N'" + txtMaQue.Text.Trim() + "'";
                    DAO.RunSql(sql);
                    LoadDataToGrivew();
                    ResetValues();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            GridViewQue.Enabled = true;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaQue.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}



