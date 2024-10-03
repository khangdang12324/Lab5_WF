namespace Lab5_WF_2212387
{
	partial class frmAddSubjects
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel12 = new System.Windows.Forms.Panel();
			this.txtSubjectName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnAddSubjects = new System.Windows.Forms.Button();
			this.btnCancle = new System.Windows.Forms.Button();
			this.panel12.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel12
			// 
			this.panel12.Controls.Add(this.txtSubjectName);
			this.panel12.Controls.Add(this.label5);
			this.panel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel12.Location = new System.Drawing.Point(74, 58);
			this.panel12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.panel12.Name = "panel12";
			this.panel12.Size = new System.Drawing.Size(517, 40);
			this.panel12.TabIndex = 4;
			// 
			// txtSubjectName
			// 
			this.txtSubjectName.Location = new System.Drawing.Point(157, 7);
			this.txtSubjectName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtSubjectName.Name = "txtSubjectName";
			this.txtSubjectName.Size = new System.Drawing.Size(347, 22);
			this.txtSubjectName.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 7);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(74, 16);
			this.label5.TabIndex = 1;
			this.label5.Text = "Thêm môn:";
			// 
			// btnAddSubjects
			// 
			this.btnAddSubjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddSubjects.Location = new System.Drawing.Point(333, 128);
			this.btnAddSubjects.Name = "btnAddSubjects";
			this.btnAddSubjects.Size = new System.Drawing.Size(98, 34);
			this.btnAddSubjects.TabIndex = 5;
			this.btnAddSubjects.Text = "Thêm";
			this.btnAddSubjects.UseVisualStyleBackColor = true;
			this.btnAddSubjects.Click += new System.EventHandler(this.btnAddSubjects_Click);
			// 
			// btnCancle
			// 
			this.btnCancle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancle.Location = new System.Drawing.Point(480, 128);
			this.btnCancle.Name = "btnCancle";
			this.btnCancle.Size = new System.Drawing.Size(98, 34);
			this.btnCancle.TabIndex = 6;
			this.btnCancle.Text = "Thoát";
			this.btnCancle.UseVisualStyleBackColor = true;
			this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
			// 
			// frmAddSubjects
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(694, 206);
			this.Controls.Add(this.btnCancle);
			this.Controls.Add(this.btnAddSubjects);
			this.Controls.Add(this.panel12);
			this.Name = "frmAddSubjects";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Thêm môn học";
			this.panel12.ResumeLayout(false);
			this.panel12.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel12;
		private System.Windows.Forms.TextBox txtSubjectName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnAddSubjects;
		private System.Windows.Forms.Button btnCancle;
	}
}