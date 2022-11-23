namespace KarelSeviceFormApp
{
    partial class Form1
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
            this.cb_camp = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_pollob = new System.Windows.Forms.ComboBox();
            this.btn_run = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_camp
            // 
            this.cb_camp.FormattingEnabled = true;
            this.cb_camp.Location = new System.Drawing.Point(6, 31);
            this.cb_camp.Name = "cb_camp";
            this.cb_camp.Size = new System.Drawing.Size(282, 24);
            this.cb_camp.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_camp);
            this.groupBox1.Location = new System.Drawing.Point(12, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 87);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kampanya Alanı";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_pollob);
            this.groupBox2.Location = new System.Drawing.Point(313, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 87);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pollob Alanı";
            // 
            // cb_pollob
            // 
            this.cb_pollob.FormattingEnabled = true;
            this.cb_pollob.Location = new System.Drawing.Point(7, 30);
            this.cb_pollob.Name = "cb_pollob";
            this.cb_pollob.Size = new System.Drawing.Size(276, 24);
            this.cb_pollob.TabIndex = 0;
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(12, 107);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(75, 52);
            this.btn_run.TabIndex = 3;
            this.btn_run.Text = "Çalıştır";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(93, 107);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 52);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "Durdur";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(174, 107);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 52);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "Kapat";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 214);
            this.ControlBox = false;
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "AHL Kampanya Uygulaması";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_camp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.ComboBox cb_pollob;
    }
}

