using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab5_WF_2212387
{
	public partial class MainForm : Form
	{
	

		QuanLySinhVien qlsv;

		public MainForm()
		{
			InitializeComponent();
			// Add event handlers for context menu items
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			qlsv = new QuanLySinhVien();
			qlsv.DocTuFile();
			LoadListView();
		}
		#region Phuong thuc
		private string TaoMSSV(string lop)
		{
			// Giả sử năm nhập học là 2 số cuối của năm hiện tại
			string year = DateTime.Now.Year.ToString().Substring(2, 2);
			string bb = "10";

			// Tạo số ngẫu nhiên từ 000 đến 999
			Random random = new Random();
			int ccc = random.Next(0, 1000);

			// Định dạng CCC thành chuỗi 3 chữ số
			string cccString = ccc.ToString("D3");

			// Kết hợp để tạo MSSV
			string mssv = year + bb + cccString;

			return mssv;
		}

		//Lấy thông tin từ controls thông tin SV
		private SinhVien GetSinhVien()
		{
			SinhVien sv = new SinhVien();
			bool gt = true;
			List<string> mh = new List<string>();
			sv.MaSo = this.mtxtMSSV.Text;
			sv.HoVaTenLot = this.txtLastName.Text;
			sv.Ten = this.txtFirstName.Text;
			sv.NgaySinh = this.dtpBirth.Value;
			sv.DiaChi = this.txtDiaChi.Text;	
			sv.Lop = this.cbClass.Text;
			sv.SoCMND = this.mtxtCMND.Text;
			sv.SoDT = this.mtxtPhoneNumber.Text;

			if (rdMale.Checked)
				gt = true;
			sv.GioiTinh = gt;
			for (int i = 0; i < this.clbSubjects.Items.Count; i++)
				if (clbSubjects.GetItemChecked(i))
					mh.Add(clbSubjects.Items[i].ToString());
			sv.MonHoc = mh;
			return sv;
		
		}
		//Lấy thông tin sinh viên từ dòng item của ListView
		private SinhVien GetSinhVienLV(ListViewItem lvitem)
		{
		
			SinhVien sv = new SinhVien();
			sv.MaSo = lvitem.SubItems[0].Text;
			sv.HoVaTenLot = lvitem.SubItems[1].Text;
			sv.Ten = lvitem.SubItems[2].Text;
			sv.NgaySinh = DateTime.Parse(lvitem.SubItems[3].Text);

			string gt = lvitem.SubItems[4].Text;
			sv.GioiTinh = gt == "Nam";

			sv.Lop = lvitem.SubItems[5].Text;
			sv.SoCMND = lvitem.SubItems[6].Text;
			sv.SoDT = lvitem.SubItems[7].Text;
			sv.DiaChi = lvitem.SubItems[8].Text;

			List<string> mh = new List<string>();
			string[] s = lvitem.SubItems[9].Text.Split(',');
			foreach (string t in s)
				mh.Add(t);
			sv.MonHoc = mh;

			return sv;
		}

		//Thiết lập các thông tin lên controls sinh viên
		private void ThietLapThongTin(SinhVien sv)
		{
			this.mtxtMSSV.Text = sv.MaSo;
			this.txtLastName.Text = sv.HoVaTenLot;
			this.txtFirstName.Text = sv.Ten;
			this.dtpBirth.Value = sv.NgaySinh;
			if (sv.GioiTinh)
				this.rdMale.Checked = true;
			else
				this.rdFemale.Checked = true;
			this.txtDiaChi.Text = sv.DiaChi;
			this.cbClass.Text = sv.Lop;
			this.mtxtCMND.Text = sv.SoCMND;
			this.mtxtPhoneNumber.Text = sv.SoDT;


			for (int i = 0; i < this.clbSubjects.Items.Count; i++)
				this.clbSubjects.SetItemChecked(i, false);
			foreach (string s in sv.MonHoc)
			{
				for (int i = 0; i < this.clbSubjects.Items.Count;
				i++)
					if
					(s.CompareTo(this.clbSubjects.Items[i]) == 0)
						this.clbSubjects.SetItemChecked(i,
						true);
			}
			
		}
		private bool KiemTraDuLieu(SinhVien sv)
		{
			if (string.IsNullOrWhiteSpace(sv.MaSo) ||
				string.IsNullOrWhiteSpace(sv.HoVaTenLot) ||
				string.IsNullOrWhiteSpace(sv.Ten) ||
				sv.NgaySinh == null ||
				string.IsNullOrWhiteSpace(sv.Lop) ||
				string.IsNullOrWhiteSpace(sv.SoCMND) ||
				string.IsNullOrWhiteSpace(sv.SoDT) ||
				string.IsNullOrWhiteSpace(sv.DiaChi) ||
				sv.MonHoc == null || sv.MonHoc.Count == 0)
			{
				MessageBox.Show("Vui lòng nhập đầy đủ thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			return true;
		}
		//Thêm sinh viên vào ListView
		private bool KiemTraTrungLapMSSV(string mssv)
		{
			foreach (ListViewItem item in lvDSSV.Items)
			{
				if (item.SubItems[0].Text == mssv)
				{
					return true;
				}
			}
			return false;
		}

		private void ThemSV(SinhVien sv)
			{
			string mssv;
			do
			{
				mssv = TaoMSSV(sv.Lop);
			} while (KiemTraTrungLapMSSV(mssv));
			sv.MaSo = TaoMSSV(sv.Lop);

			ListViewItem lvitem = new ListViewItem(sv.MaSo);
				lvitem.SubItems.Add(sv.HoVaTenLot);
				lvitem.SubItems.Add(sv.Ten);
				lvitem.SubItems.Add(sv.NgaySinh.ToShortDateString());

				string gt = "Nữ";
				if (sv.GioiTinh)
					gt = "Nam";
				lvitem.SubItems.Add(gt);

				lvitem.SubItems.Add(sv.Lop);
				lvitem.SubItems.Add(sv.SoCMND);
				lvitem.SubItems.Add(sv.SoDT);
				lvitem.SubItems.Add(sv.DiaChi);
				string mh = "";
				foreach (string s in sv.MonHoc)
					mh += s + ",";
				mh = mh.Substring(0, mh.Length - 1);
				lvitem.SubItems.Add(mh);
				this.lvDSSV.Items.Add(lvitem);


			}
		//Hiển thị các sinh viên trong qlsv lên ListView
		private void LoadListView()
		{
			this.lvDSSV.Items.Clear();
			foreach (SinhVien sv in qlsv.DanhSach)
			{
				ThemSV(sv);
			}
		}
		#endregion
		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void panel7_Paint(object sender, PaintEventArgs e)
		{

		}
		#region Add button
		private void btnAdd_Click(object sender, EventArgs e)
		{
			SinhVien sv = GetSinhVien();
			if (KiemTraDuLieu(sv))
			{
				ThemSV(sv);
				MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			qlsv.Them(sv);

		}

		private void lvDSSV_SelectedIndexChanged(object sender, EventArgs e)
		{
			lvDSSV.ContextMenuStrip = contextMenuStrip2;
			int count = this.lvDSSV.SelectedItems.Count;
			if (count > 0)
			{
				ListViewItem lvitem =
				this.lvDSSV.SelectedItems[0];
				SinhVien sv = GetSinhVienLV(lvitem);
				ThietLapThongTin(sv);

			}
		}
		private void ClearInputFields()
		{
			this.mtxtMSSV.Clear();
			this.txtLastName.Clear();
			this.txtFirstName.Clear();
			this.dtpBirth.Value = DateTime.Now;
			this.txtDiaChi.Clear();
			this.cbClass.SelectedIndex = -1;
			this.mtxtCMND.Clear();
			this.mtxtPhoneNumber.Clear();
			this.rdMale.Checked = false;
			this.rdFemale.Checked = false;
			for (int i = 0; i < this.clbSubjects.Items.Count; i++)
			{
				this.clbSubjects.SetItemChecked(i, false);
			}
		}
		#endregion
		#region Update button
		private void btnUpdate_Click(object sender, EventArgs e)
		{
			
			// Retrieve updated student information from the form controls
			string maSo = mtxtMSSV.Text;
			string hoVaTenLot = txtLastName.Text;
			string ten = txtFirstName.Text;
			DateTime ngaySinh = dtpBirth.Value;
			bool gioiTinh = rdMale.Checked;
			string lop = cbClass.Text;
			string soCMND = mtxtCMND.Text;
			string soDT = mtxtPhoneNumber.Text;
			string diaChi = txtDiaChi.Text;
			List<string> monHoc = clbSubjects.CheckedItems.Cast<string>().ToList();

			// Create a new SinhVien object with the updated information
			SinhVien sv = new SinhVien
			{
				MaSo = maSo,
				HoVaTenLot = hoVaTenLot,
				Ten = ten,
				NgaySinh = ngaySinh,
				GioiTinh = gioiTinh,
				Lop = lop,
				SoCMND = soCMND,
				SoDT = soDT,
				DiaChi = diaChi,
				MonHoc = monHoc
			};

			// Update the student information in the list
			qlsv.CapNhatSinhVien(sv);
			UpdateListView();
			// Optionally, clear the input fields and refresh the UI
			ClearInputFields();
			MessageBox.Show("Cập nhật thành công!");
			KiemTraDuLieu(sv);
		}
		private void UpdateListView()
		{
			lvDSSV.Items.Clear(); // Clear existing items

			foreach (var sv in qlsv.DanhSach)
			{
				ListViewItem item = new ListViewItem(sv.MaSo);
				item.SubItems.Add(sv.HoVaTenLot);
				item.SubItems.Add(sv.Ten);
				item.SubItems.Add(sv.NgaySinh.ToShortDateString());
				item.SubItems.Add(sv.GioiTinh ? "Nam" : "Nữ");
				item.SubItems.Add(sv.Lop);
				item.SubItems.Add(sv.SoCMND);
				item.SubItems.Add(sv.SoDT);
				item.SubItems.Add(sv.DiaChi);
				item.SubItems.Add(string.Join("*", sv.MonHoc));
				lvDSSV.Items.Add(item);
			}
		}
		#endregion

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if(result == DialogResult.Yes)
			{
				Application.Exit();
			}	
			
		}
		#region Search button
		private List<SinhVien> TimKiemSinhVien(string keyword, string criteria)
		{
			List<SinhVien> ketQua = new List<SinhVien>();

			switch (criteria)
			{
				case "Tên":
					ketQua = qlsv.DanhSach.Where(sv => sv.Ten.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
					break;
				case "Lớp":
					ketQua = qlsv.DanhSach.Where(sv => sv.Lop.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
					break;
				case "MSSV":
					ketQua = qlsv.DanhSach.Where(sv => sv.MaSo.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
					break;
				default:
					MessageBox.Show("Tiêu chí tìm kiếm không hợp lệ.");
					break;
			}

			return ketQua;
		}


		private void btnFind_Click(object sender, EventArgs e)
		{

			string keyword = txtSearch.Text;
			string criteria = cbCriteria.SelectedItem?.ToString();

			if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(criteria))
			{
				MessageBox.Show("Vui lòng nhập từ khóa và chọn tiêu chí tìm kiếm.");
				return;
			}

			List<SinhVien> ketQua = TimKiemSinhVien(keyword, criteria);

			if (ketQua.Count > 0)
			{
				lvDSSV.Items.Clear();
				foreach (var sv in ketQua)
				{
					ListViewItem item = new ListViewItem(sv.MaSo);
					item.SubItems.Add(sv.HoVaTenLot);
					item.SubItems.Add(sv.Ten);
					item.SubItems.Add(sv.NgaySinh.ToShortDateString());
					item.SubItems.Add(sv.GioiTinh ? "Nam" : "Nữ");
					item.SubItems.Add(sv.Lop);
					item.SubItems.Add(sv.SoCMND);
					item.SubItems.Add(sv.SoDT);
					item.SubItems.Add(sv.DiaChi);
					item.SubItems.Add(string.Join("*", sv.MonHoc));
					lvDSSV.Items.Add(item);
				}
			}
			else
			{
				MessageBox.Show("Không tìm thấy sinh viên nào.");
			}
		}
		private void cbCriteria_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnFind_Click(sender, e);
				e.SuppressKeyPress = true; // Ngăn chặn âm thanh "ding" khi nhấn Enter

			}
		}

		#endregion

		private void btnRefesh_Click(object sender, EventArgs e)
		{
			UpdateListView();
		}
		#region Delete button
		private void btnDelete_Click(object sender, EventArgs e)
		{
			
				// Retrieve updated student information from the form controls
				string maSo = mtxtMSSV.Text;
				string hoVaTenLot = txtLastName.Text;
				string ten = txtFirstName.Text;
				DateTime ngaySinh = dtpBirth.Value;
				bool gioiTinh = rdMale.Checked;
				string lop = cbClass.Text;
				string soCMND = mtxtCMND.Text;
				string soDT = mtxtPhoneNumber.Text;
				string diaChi = txtDiaChi.Text;
				List<string> monHoc = clbSubjects.CheckedItems.Cast<string>().ToList();

				// Create a new SinhVien object with the delete information
				SinhVien sv = new SinhVien
				{
					MaSo = maSo,
					HoVaTenLot = hoVaTenLot,
					Ten = ten,
					NgaySinh = ngaySinh,
					GioiTinh = gioiTinh,
					Lop = lop,
					SoCMND = soCMND,
					SoDT = soDT,
					DiaChi = diaChi,
					MonHoc = monHoc
				};

				// Update the student information in the list
				qlsv.XoaSinhVien(sv); // Assuming XoaSinhVien is the method to delete a student
				UpdateListView();
				// Optionally, clear the input fields and refresh the UI
				ClearInputFields();
			qlsv.LuuDanhSachSinhVien();
			MessageBox.Show("Xóa thành công!");

	

		}


		#endregion
	
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{

		}
	
		private void clbSubjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			clbSubjects.ContextMenuStrip = contextMenuStrip1;
			qlsv.LuuDanhSachSinhVien();
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			
			if (clbSubjects.SelectedItem != null)
			{
			
				clbSubjects.Items.Remove(clbSubjects.SelectedItem);
				qlsv.LuuDanhSachSinhVien();
			}
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			using (frmAddSubjects addSubjectForm = new frmAddSubjects())
			{
				if (addSubjectForm.ShowDialog() == DialogResult.OK)
				{
					string newSubject = addSubjectForm.SubjectName;
					if (!string.IsNullOrEmpty(newSubject))
					{
						clbSubjects.Items.Add(newSubject);

						qlsv.LuuDanhSachSinhVien();
					}
				}
			}

		}

		private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
		{
			
		}

		private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (lvDSSV.CheckedItems.Count > 0)
			{
				DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa các sinh viên đã chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (result == DialogResult.Yes)
				{
					foreach (ListViewItem item in lvDSSV.CheckedItems)
					{
						SinhVien sv = GetSinhVienLV(item);
						qlsv.XoaSinhVien(sv);
						lvDSSV.Items.Remove(item);
					

					}
					qlsv.LuuDanhSachSinhVien();
					MessageBox.Show("Đã xóa các sinh viên đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else
			{
				MessageBox.Show("Vui lòng chọn ít nhất một sinh viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			
		}

	}

}
