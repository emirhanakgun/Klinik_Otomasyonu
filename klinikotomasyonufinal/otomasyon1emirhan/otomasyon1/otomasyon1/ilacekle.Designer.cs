namespace KlinikOtomasyonu1
{
    partial class ilacekle
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ilacekle));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ilacidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ilacadiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ilaclarBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.otomasyon1DataSet14 = new KlinikOtomasyonu1.otomasyon1DataSet14();
            this.ilaclarTableAdapter = new KlinikOtomasyonu1.otomasyon1DataSet14TableAdapters.ilaclarTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ilaclarBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otomasyon1DataSet14)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ilacidDataGridViewTextBoxColumn,
            this.ilacadiDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.ilaclarBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(-1, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(328, 201);
            this.dataGridView1.TabIndex = 0;
            // 
            // ilacidDataGridViewTextBoxColumn
            // 
            this.ilacidDataGridViewTextBoxColumn.DataPropertyName = "ilac_id";
            this.ilacidDataGridViewTextBoxColumn.HeaderText = "İLAÇ İD";
            this.ilacidDataGridViewTextBoxColumn.Name = "ilacidDataGridViewTextBoxColumn";
            this.ilacidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ilacadiDataGridViewTextBoxColumn
            // 
            this.ilacadiDataGridViewTextBoxColumn.DataPropertyName = "ilac_adi";
            this.ilacadiDataGridViewTextBoxColumn.HeaderText = "İLAÇ ADI";
            this.ilacadiDataGridViewTextBoxColumn.Name = "ilacadiDataGridViewTextBoxColumn";
            // 
            // ilaclarBindingSource
            // 
            this.ilaclarBindingSource.DataMember = "ilaclar";
            this.ilaclarBindingSource.DataSource = this.otomasyon1DataSet14;
            // 
            // otomasyon1DataSet14
            // 
            this.otomasyon1DataSet14.DataSetName = "otomasyon1DataSet14";
            this.otomasyon1DataSet14.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ilaclarTableAdapter
            // 
            this.ilaclarTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "İLAÇ EKLE";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(175, 27);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, -6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 3;
            this.button1.Text = "GERİ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(225, 60);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 27);
            this.button2.TabIndex = 4;
            this.button2.Text = "EKLE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ilacekle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 296);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ilacekle";
            this.Text = "İLAÇ EKLE";
            this.Load += new System.EventHandler(this.ilacekle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ilaclarBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otomasyon1DataSet14)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private otomasyon1DataSet14 otomasyon1DataSet14;
        private System.Windows.Forms.BindingSource ilaclarBindingSource;
        private otomasyon1DataSet14TableAdapters.ilaclarTableAdapter ilaclarTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ilacidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ilacadiDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}