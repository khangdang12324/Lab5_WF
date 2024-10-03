using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_WF_2212387
{
	public class SinhVien
	{
		//Thuoc tinh
		public string MaSo { get; set; }
		public string HoVaTenLot { get; set; }
		public string Ten { get; set; }
		public DateTime NgaySinh { get; set; }
		public string Lop { get; set; }
		public string SoCMND { get; set; }
		public	string SoDT { get; set; }
		public bool  GioiTinh { get; set; }
		public string DiaChi { get; set; }
		public List<string> MonHoc { get; set; }
		
		//Phuoc thuc tao lap
		public SinhVien()
		{
			MonHoc = new List<string>();
		}

		public SinhVien(string ms,string ht, string t,DateTime ngay, string lop, string cmnd, string dt,bool gt,string dc,List<string>mh)
		{
			this.MaSo = ms;
			this.HoVaTenLot = ht;
			this.Ten = t;
			this.NgaySinh = ngay;
			this.Lop = lop;
			this.SoCMND = cmnd;
			this.SoDT = dt;
			this.GioiTinh = gt;
			this.DiaChi = dc;
			this.MonHoc = mh;

		}
	}
	
}
