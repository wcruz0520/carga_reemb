
namespace CargaReembolso
{
    partial class CargaReemForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLimpUDO = new System.Windows.Forms.Button();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.btnSimular = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDocs = new System.Windows.Forms.ComboBox();
            this.cmbUDOs = new System.Windows.Forms.ComboBox();
            this.btnCUDO = new System.Windows.Forms.Button();
            this.btnCDOC = new System.Windows.Forms.Button();
            this.gridUDO = new System.Windows.Forms.DataGridView();
            this.gridDOC = new System.Windows.Forms.DataGridView();
            this.btnLimpDoc = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridUDO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDOC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLimpUDO
            // 
            this.btnLimpUDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpUDO.Location = new System.Drawing.Point(1285, 45);
            this.btnLimpUDO.Name = "btnLimpUDO";
            this.btnLimpUDO.Size = new System.Drawing.Size(137, 34);
            this.btnLimpUDO.TabIndex = 0;
            this.btnLimpUDO.Text = "LIMPIAR";
            this.btnLimpUDO.UseVisualStyleBackColor = true;
            // 
            // btnProcesar
            // 
            this.btnProcesar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.Location = new System.Drawing.Point(29, 801);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(137, 34);
            this.btnProcesar.TabIndex = 1;
            this.btnProcesar.Text = "PROCESAR";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // btnSimular
            // 
            this.btnSimular.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimular.Location = new System.Drawing.Point(205, 801);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(137, 34);
            this.btnSimular.TabIndex = 2;
            this.btnSimular.Text = "SIMULAR";
            this.btnSimular.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tipo de tabla nativa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tipo de UDO";
            // 
            // cmbDocs
            // 
            this.cmbDocs.FormattingEnabled = true;
            this.cmbDocs.Location = new System.Drawing.Point(205, 428);
            this.cmbDocs.Name = "cmbDocs";
            this.cmbDocs.Size = new System.Drawing.Size(174, 24);
            this.cmbDocs.TabIndex = 5;
            // 
            // cmbUDOs
            // 
            this.cmbUDOs.FormattingEnabled = true;
            this.cmbUDOs.Location = new System.Drawing.Point(205, 55);
            this.cmbUDOs.Name = "cmbUDOs";
            this.cmbUDOs.Size = new System.Drawing.Size(174, 24);
            this.cmbUDOs.TabIndex = 6;
            // 
            // btnCUDO
            // 
            this.btnCUDO.Location = new System.Drawing.Point(437, 56);
            this.btnCUDO.Name = "btnCUDO";
            this.btnCUDO.Size = new System.Drawing.Size(75, 23);
            this.btnCUDO.TabIndex = 7;
            this.btnCUDO.Text = "...";
            this.btnCUDO.UseVisualStyleBackColor = true;
            this.btnCUDO.Click += new System.EventHandler(this.btnCUDO_Click);
            // 
            // btnCDOC
            // 
            this.btnCDOC.Location = new System.Drawing.Point(437, 429);
            this.btnCDOC.Name = "btnCDOC";
            this.btnCDOC.Size = new System.Drawing.Size(75, 23);
            this.btnCDOC.TabIndex = 8;
            this.btnCDOC.Text = "...";
            this.btnCDOC.UseVisualStyleBackColor = true;
            this.btnCDOC.Click += new System.EventHandler(this.btnCDOC_Click);
            // 
            // gridUDO
            // 
            this.gridUDO.BackgroundColor = System.Drawing.Color.White;
            this.gridUDO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUDO.Location = new System.Drawing.Point(29, 101);
            this.gridUDO.Name = "gridUDO";
            this.gridUDO.RowHeadersWidth = 51;
            this.gridUDO.RowTemplate.Height = 24;
            this.gridUDO.Size = new System.Drawing.Size(1393, 302);
            this.gridUDO.TabIndex = 9;
            // 
            // gridDOC
            // 
            this.gridDOC.BackgroundColor = System.Drawing.Color.White;
            this.gridDOC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDOC.Location = new System.Drawing.Point(29, 475);
            this.gridDOC.Name = "gridDOC";
            this.gridDOC.RowHeadersWidth = 51;
            this.gridDOC.RowTemplate.Height = 24;
            this.gridDOC.Size = new System.Drawing.Size(1393, 302);
            this.gridDOC.TabIndex = 10;
            // 
            // btnLimpDoc
            // 
            this.btnLimpDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpDoc.Location = new System.Drawing.Point(1285, 422);
            this.btnLimpDoc.Name = "btnLimpDoc";
            this.btnLimpDoc.Size = new System.Drawing.Size(137, 34);
            this.btnLimpDoc.TabIndex = 11;
            this.btnLimpDoc.Text = "LIMPIAR";
            this.btnLimpDoc.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CargaReembolso.Properties.Resources.LogoSS;
            this.pictureBox1.Location = new System.Drawing.Point(1230, 782);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1235, 828);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Copyright";
            // 
            // CargaReemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1451, 863);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLimpDoc);
            this.Controls.Add(this.gridDOC);
            this.Controls.Add(this.gridUDO);
            this.Controls.Add(this.btnCDOC);
            this.Controls.Add(this.btnCUDO);
            this.Controls.Add(this.cmbUDOs);
            this.Controls.Add(this.cmbDocs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSimular);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.btnLimpUDO);
            this.Name = "CargaReemForm";
            this.Text = "CARGA REEMBOLSOS";
            this.Load += new System.EventHandler(this.CargaReemForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridUDO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDOC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLimpUDO;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDocs;
        private System.Windows.Forms.ComboBox cmbUDOs;
        private System.Windows.Forms.Button btnCUDO;
        private System.Windows.Forms.Button btnCDOC;
        private System.Windows.Forms.DataGridView gridUDO;
        private System.Windows.Forms.DataGridView gridDOC;
        private System.Windows.Forms.Button btnLimpDoc;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
    }
}

