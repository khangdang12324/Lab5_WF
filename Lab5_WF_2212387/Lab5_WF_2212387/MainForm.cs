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
		}

		private void Form1_Load(object sender, EventArgs e)
		{
            qlsv = new QuanLySinhVien();
            qlsv.DocTuFile();
            LoadListView();


        }
        //Lấy thông tin từ controls thông tin SV
        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            bool gt = true;
            List<string> mh = new List<string>();
            sv.MaSo = this.mtxtMSSV.Text;
            sv.HoVaTenLot = this.txtLastName.Text;
            sv.Ten=this.txtFirstName.Text;
            sv.NgaySinh = this.dtpBirth.Value;
            sv.DiaChi = this.txtDiaChi.Text;
            sv.Lop = this.cbClass.Text;
            sv.SoCMND= this.mtxtCMND.Text;
            sv.SoDT=this.mtxtPhoneNumber.Text; 

            if (rdMale.Checked)
                gt = false;
            sv.GioiTinh = gt;
            for (int i = 0; i < this.clbSubjects.Items.Count; i++)
                if (clbSubjects.GetItemChecked(i))
                    mh.Add( clbSubjects.Items[i].ToString());
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
            sv.Lop = lvitem.SubItems[4].Text;
            sv.SoCMND = lvitem.SubItems[5].Text;
            sv.SoDT = lvitem.SubItems[6].Text;
            sv.DiaChi = lvitem.SubItems[8].Text;
            sv.GioiTinh = false;
            if (lvitem.SubItems[7].Text == "Nam")
                sv.GioiTinh = true;
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

            this.txtDiaChi.Text = sv.DiaChi;
            this.cbClass.Text = sv.Lop;
            this.mtxtCMND.Text = sv.SoCMND;
            this.mtxtPhoneNumber.Text = sv.SoDT;

            if (sv.GioiTinh)
                this.rdMale.Checked = true;
            else
                this.rdFemale.Checked = true;
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
        //Thêm sinh viên vào ListView
        private void ThemSV(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MaSo);
            lvitem.SubItems.Add(sv.HoVaTenLot);
            lvitem.SubItems.Add(sv.Ten);

            lvitem.SubItems.Add(sv.NgaySinh.ToShortDateString());
            lvitem.SubItems.Add(sv.Lop);
            lvitem.SubItems.Add(sv.SoCMND);
            lvitem.SubItems.Add(sv.SoDT);
            lvitem.SubItems.Add(sv.DiaChi);

            string gt = "Nữ";
            if (sv.GioiTinh)
                gt = "Nam";
            lvitem.SubItems.Add(gt);
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
        private List<SinhVien> LoadJSON(string path)
        {
            List<SinhVien> list = new List<SinhVien>();

            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    var array = (JObject)JsonConvert.DeserializeObject(json);
                    var students = array["SinhVien"]?.Children();

                    if (students != null)
                    {
                        foreach (var item in students)
                        {
                            string mssv = item["MSSV"]?.Value<string>();
                            string hovatenlot = item["HoVaTenLot"]?.Value<string>();
                            string ten = item["Ten"]?.Value<string>();
                            DateTime ngaysinh = item["NgaySinh"]?.Value<DateTime>() ?? default;
                            string lop = item["Lop"]?.Value<string>();
                            string socmnd = item["SoCMND"]?.Value<string>();
                            string sodt = item["SoDT"]?.Value<string>();
                            bool gioitinh = item["GioiTinh"]?.Value<bool>() ?? false;
                            string diachi = item["DiaChi"]?.Value<string>();
                            List<string> monhoc = item["MonHoc"]?.ToObject<List<string>>() ?? new List<string>();

                            SinhVien info = new SinhVien(mssv, hovatenlot, ten, ngaysinh, lop, socmnd, sodt, gioitinh, diachi, monhoc);
                            list.Add(info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (ví dụ: ghi lại lỗi)
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }

            return list;
        }


        private void label1_Click(object sender, EventArgs e)
		{

		}

        private void btnJSon_Click(object sender, EventArgs e)
        {
            string Str = "";
            string Path = "D:\\Lab5_WF-main\\Lab5_WF-main\\Lab5_WF_2212387\\bin\\Debug\\DanhSach.json";
            List<SinhVien> list = LoadJSON(Path);

            for (int i = 0; i < list.Count; i++)
            {
                SinhVien info = list[i];
            }

            MessageBox.Show(Str);
        }


        private void lvDSSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.lvDSSV.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem lvitem =
                this.lvDSSV.SelectedItems[0];
                SinhVien sv = GetSinhVienLV(lvitem);
                ThietLapThongTin(sv);
            }
        }
    }
}
