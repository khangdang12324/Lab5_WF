using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_WF_2212387
{
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
		}

	}
}
