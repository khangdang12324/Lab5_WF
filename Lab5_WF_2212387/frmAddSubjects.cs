using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_WF_2212387
{
	public partial class frmAddSubjects : Form
	{
		public string SubjectName { get; private set; }
		public frmAddSubjects()
		{
			InitializeComponent();
		}

		private void btnAddSubjects_Click(object sender, EventArgs e)
		{
			SubjectName = txtSubjectName.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancle_Click(object sender, EventArgs e)
		{
			this.Close();	
		}
	}
}
