using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_WF_2212387
{
	public delegate int SoSanh(object sv1, object sv2);

	class QuanLySinhVien
	{
		public List<SinhVien> DanhSach;
		public QuanLySinhVien()
		{
			DanhSach = new List<SinhVien>();
		}
		public void Them(SinhVien sv)
		{
			this.DanhSach.Add(sv);
			LuuDanhSachSinhVien();
		}

		public SinhVien this[int index]
		{
			get { return DanhSach[index]; }
			set { DanhSach[index] = value; }
		}
		public void Xoa(object obj, SoSanh ss)
		{
			int i = DanhSach.Count - 1;
			for (; i >= 0; i--)
				if (ss(obj, this[i]) == 0)
					this.DanhSach.RemoveAt(i);
		}
		public void XoaSinhVien(SinhVien sv)
		{
			// Define the comparison logic
			SoSanh ss = (object obj1, object obj2) =>
			{
				SinhVien sv1 = obj1 as SinhVien;
				SinhVien sv2 = obj2 as SinhVien;
				return sv1.MaSo.CompareTo(sv2.MaSo);
			};

			// Call the Xoa method with the student object and comparison logic
			Xoa(sv, ss);
		}

		public SinhVien Tim(object obj, SoSanh ss)
		{
			SinhVien svresult = null;
			foreach (SinhVien sv in DanhSach)
				if (ss(obj, sv) == 0)
				{
					svresult = sv;
					break;
				}
			return svresult;
		}
		public bool Sua(SinhVien svsua, object obj, SoSanh ss)
		{
			int i, count;
			bool kq = false;
			count = this.DanhSach.Count - 1;
			for (i = 0; i < count; i++)
				if (ss(obj, this[i]) == 0)
				{
					this[i] = svsua;
					kq = true;
					break;
				}
			return kq;
		}
		public void DocTuFile()
		{
			string filename = "DanhSachSV.txt";
			string t;
			string[] s;
			SinhVien sv;

			try
			{
				using (StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)))
				{
					while ((t = sr.ReadLine()) != null)
					{
						s = t.Split('*');
						sv = new SinhVien
						{
							MaSo = s[0],
							HoVaTenLot = s[1],
							Ten = s[2],
							NgaySinh = DateTime.Parse(s[3]),
							GioiTinh = s[4] == "Nam",
							Lop = s[5],
							SoCMND = s[6],
							SoDT = s[7],
							DiaChi = s[8],
							MonHoc = new List<string>(s[9].Split('*'))
						};
						this.Them(sv);
					}
				}
			}
			catch (IOException ex)
			{
			}
		}

		// Cập nhật thông tin sinh viên
		public void CapNhatSinhVien(SinhVien sv)
		{
			var sinhVien = DanhSach.FirstOrDefault(s => s.MaSo == sv.MaSo);
			if (sinhVien != null)
			{
				sinhVien.HoVaTenLot = sv.HoVaTenLot;
				sinhVien.Ten = sv.Ten;
				sinhVien.NgaySinh = sv.NgaySinh;
				sinhVien.GioiTinh = sv.GioiTinh;
				sinhVien.Lop = sv.Lop;
				sinhVien.SoCMND = sv.SoCMND;
				sinhVien.SoDT = sv.SoDT;
				sinhVien.DiaChi = sv.DiaChi;
				sinhVien.MonHoc = sv.MonHoc;
				LuuDanhSachSinhVien();
			}
		}

		// Lưu danh sách sinh viên vào file
		public void LuuDanhSachSinhVien()
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(new FileStream("DanhSachSV.txt", FileMode.Create, FileAccess.Write, FileShare.None)))
				{
					foreach (var sv in DanhSach)
					{
						string gioiTinh = sv.GioiTinh ? "Nam" : "Nữ";
						string monHoc = string.Join("*", sv.MonHoc);
						sw.WriteLine($"{sv.MaSo}*{sv.HoVaTenLot}*{sv.Ten}*{sv.NgaySinh.ToShortDateString()}*{gioiTinh}*{sv.Lop}*{sv.SoCMND}*{sv.SoDT}*{sv.DiaChi}*{monHoc}");
					}
				}
			}
			catch (IOException ex)
			{
			
			}
		}
		public class JsonFileHandler
		{
			private string filePath = "DanhSachSV.json";

			public List<SinhVien> ReadFromFile()
			{
				string json = File.ReadAllText(filePath);
				return JsonConvert.DeserializeObject<List<SinhVien>>(json);
			}

			public void WriteToFile(List<SinhVien> students)
			{
				string json = JsonConvert.SerializeObject(students, Formatting.Indented);
				File.WriteAllText(filePath, json);
			}
		}
	}
}
