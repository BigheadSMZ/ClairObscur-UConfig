using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    partial class Form_YesNoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_YesNoForm));
            this.Button_Yes = new System.Windows.Forms.Button();
            this.Button_No = new System.Windows.Forms.Button();
            this.Label_Message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Button_Yes
            // 
            this.Button_Yes.Location = new System.Drawing.Point(52, 60);
            this.Button_Yes.Name = "Button_Yes";
            this.Button_Yes.Size = new System.Drawing.Size(80, 28);
            this.Button_Yes.TabIndex = 1;
            this.Button_Yes.Text = "Yes";
            this.Button_Yes.UseVisualStyleBackColor = true;
            this.Button_Yes.Click += new System.EventHandler(this.Button_Yes_Click);
            // 
            // Button_No
            // 
            this.Button_No.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_No.Location = new System.Drawing.Point(170, 60);
            this.Button_No.Name = "Button_No";
            this.Button_No.Size = new System.Drawing.Size(80, 28);
            this.Button_No.TabIndex = 2;
            this.Button_No.Text = "No";
            this.Button_No.UseVisualStyleBackColor = true;
            this.Button_No.Click += new System.EventHandler(this.Button_No_Click);
            // 
            // Label_Message
            // 
            this.Label_Message.Location = new System.Drawing.Point(12, 9);
            this.Label_Message.Name = "Label_Message";
            this.Label_Message.Size = new System.Drawing.Size(280, 40);
            this.Label_Message.TabIndex = 3;
            // 
            // Form_YesNoForm
            // 
            this.AcceptButton = this.Button_Yes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_No;
            this.ClientSize = new System.Drawing.Size(304, 96);
            this.Controls.Add(this.Label_Message);
            this.Controls.Add(this.Button_No);
            this.Controls.Add(this.Button_Yes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form_YesNoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yes/No Dialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_YesNoForm_FormClosing);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Button Button_Yes;
        private System.Windows.Forms.Button Button_No;
        private System.Windows.Forms.Label Label_Message;
    }
}