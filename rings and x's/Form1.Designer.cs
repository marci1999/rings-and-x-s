
namespace rings_and_x_s
{
    partial class Fkepernyo
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
            this.BKezdes = new System.Windows.Forms.Button();
            this.PPalya = new System.Windows.Forms.Panel();
            this.PKezelo = new System.Windows.Forms.Panel();
            this.CBPalyaMerete = new System.Windows.Forms.ComboBox();
            this.LPalyaMerete = new System.Windows.Forms.Label();
            this.PPalya.SuspendLayout();
            this.PKezelo.SuspendLayout();
            this.SuspendLayout();
            // 
            // BKezdes
            // 
            this.BKezdes.Location = new System.Drawing.Point(223, 44);
            this.BKezdes.Name = "BKezdes";
            this.BKezdes.Size = new System.Drawing.Size(208, 64);
            this.BKezdes.TabIndex = 0;
            this.BKezdes.Text = "Kezdés";
            this.BKezdes.UseVisualStyleBackColor = true;
            this.BKezdes.Click += new System.EventHandler(this.BKezdes_Click);
            // 
            // PPalya
            // 
            this.PPalya.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PPalya.Controls.Add(this.BKezdes);
            this.PPalya.Location = new System.Drawing.Point(12, 12);
            this.PPalya.Name = "PPalya";
            this.PPalya.Size = new System.Drawing.Size(627, 627);
            this.PPalya.TabIndex = 1;
            // 
            // PKezelo
            // 
            this.PKezelo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PKezelo.Controls.Add(this.CBPalyaMerete);
            this.PKezelo.Controls.Add(this.LPalyaMerete);
            this.PKezelo.Location = new System.Drawing.Point(638, 12);
            this.PKezelo.Name = "PKezelo";
            this.PKezelo.Size = new System.Drawing.Size(225, 627);
            this.PKezelo.TabIndex = 2;
            // 
            // CBPalyaMerete
            // 
            this.CBPalyaMerete.FormattingEnabled = true;
            this.CBPalyaMerete.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.CBPalyaMerete.Location = new System.Drawing.Point(38, 75);
            this.CBPalyaMerete.Name = "CBPalyaMerete";
            this.CBPalyaMerete.Size = new System.Drawing.Size(121, 21);
            this.CBPalyaMerete.TabIndex = 3;
            // 
            // LPalyaMerete
            // 
            this.LPalyaMerete.AutoSize = true;
            this.LPalyaMerete.Location = new System.Drawing.Point(66, 44);
            this.LPalyaMerete.Name = "LPalyaMerete";
            this.LPalyaMerete.Size = new System.Drawing.Size(69, 13);
            this.LPalyaMerete.TabIndex = 2;
            this.LPalyaMerete.Text = "Pálya Mérete";
            // 
            // Fkepernyo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 649);
            this.Controls.Add(this.PKezelo);
            this.Controls.Add(this.PPalya);
            this.Name = "Fkepernyo";
            this.Text = "Amolba";
            this.Load += new System.EventHandler(this.Fkepernyo_Load);
            this.PPalya.ResumeLayout(false);
            this.PKezelo.ResumeLayout(false);
            this.PKezelo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BKezdes;
        private System.Windows.Forms.Panel PPalya;
        private System.Windows.Forms.Panel PKezelo;
        private System.Windows.Forms.Label LPalyaMerete;
        private System.Windows.Forms.ComboBox CBPalyaMerete;
    }
}

