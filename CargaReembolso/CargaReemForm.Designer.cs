
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
            this.btnProcesar = new System.Windows.Forms.Button();
            this.btnSimular = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlInferior = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.btnConnect = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnFact = new System.Windows.Forms.Button();
            this.btnReemb = new System.Windows.Forms.Button();
            this.pnlSuperior = new System.Windows.Forms.Panel();
            this.TbLypanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrueba = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlInferior.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlSuperior.SuspendLayout();
            this.TbLypanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProcesar
            // 
            this.btnProcesar.BackColor = System.Drawing.Color.White;
            this.btnProcesar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnProcesar.FlatAppearance.BorderSize = 0;
            this.btnProcesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcesar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnProcesar.Image = global::CargaReembolso.Properties.Resources.execute;
            this.btnProcesar.Location = new System.Drawing.Point(8, 0);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(206, 50);
            this.btnProcesar.TabIndex = 1;
            this.btnProcesar.Text = "PROCESAR";
            this.btnProcesar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnProcesar.UseVisualStyleBackColor = false;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // btnSimular
            // 
            this.btnSimular.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSimular.BackColor = System.Drawing.Color.White;
            this.btnSimular.FlatAppearance.BorderSize = 0;
            this.btnSimular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimular.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimular.Location = new System.Drawing.Point(322, 4);
            this.btnSimular.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(154, 46);
            this.btnSimular.TabIndex = 2;
            this.btnSimular.Text = "SIMULAR";
            this.btnSimular.UseVisualStyleBackColor = false;
            this.btnSimular.Visible = false;
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::CargaReembolso.Properties.Resources.LogoSS;
            this.pictureBox1.Location = new System.Drawing.Point(619, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 41);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pnlInferior
            // 
            this.pnlInferior.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlInferior.Controls.Add(this.tableLayoutPanel2);
            this.pnlInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInferior.Location = new System.Drawing.Point(0, 936);
            this.pnlInferior.Name = "pnlInferior";
            this.pnlInferior.Padding = new System.Windows.Forms.Padding(5);
            this.pnlInferior.Size = new System.Drawing.Size(1632, 119);
            this.pnlInferior.TabIndex = 16;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.11647F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.72088F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.58132F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.58132F));
            this.tableLayoutPanel2.Controls.Add(this.btnSimular, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnProcesar, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.prgBar, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnPrueba, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1622, 109);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // prgBar
            // 
            this.prgBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgBar.Location = new System.Drawing.Point(842, 3);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(777, 48);
            this.prgBar.TabIndex = 13;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.White;
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Image = global::CargaReembolso.Properties.Resources.conexion;
            this.btnConnect.Location = new System.Drawing.Point(3, 0);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(219, 46);
            this.btnConnect.TabIndex = 14;
            this.btnConnect.Text = "Conectar";
            this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 53);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1632, 883);
            this.pnlContent.TabIndex = 18;
            // 
            // btnFact
            // 
            this.btnFact.AutoSize = true;
            this.btnFact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFact.FlatAppearance.BorderSize = 0;
            this.btnFact.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnFact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFact.Location = new System.Drawing.Point(819, 3);
            this.btnFact.Name = "btnFact";
            this.btnFact.Size = new System.Drawing.Size(810, 47);
            this.btnFact.TabIndex = 1;
            this.btnFact.Text = "Factura";
            this.btnFact.UseVisualStyleBackColor = true;
            this.btnFact.Click += new System.EventHandler(this.btnFact_Click);
            // 
            // btnReemb
            // 
            this.btnReemb.AutoSize = true;
            this.btnReemb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReemb.FlatAppearance.BorderSize = 0;
            this.btnReemb.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnReemb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReemb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReemb.Location = new System.Drawing.Point(3, 3);
            this.btnReemb.Name = "btnReemb";
            this.btnReemb.Size = new System.Drawing.Size(810, 47);
            this.btnReemb.TabIndex = 2;
            this.btnReemb.Text = "Reembolso";
            this.btnReemb.UseVisualStyleBackColor = true;
            this.btnReemb.Click += new System.EventHandler(this.btnReemb_Click);
            // 
            // pnlSuperior
            // 
            this.pnlSuperior.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSuperior.Controls.Add(this.TbLypanel);
            this.pnlSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlSuperior.Name = "pnlSuperior";
            this.pnlSuperior.Size = new System.Drawing.Size(1632, 53);
            this.pnlSuperior.TabIndex = 17;
            // 
            // TbLypanel
            // 
            this.TbLypanel.ColumnCount = 2;
            this.TbLypanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TbLypanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TbLypanel.Controls.Add(this.btnFact, 1, 0);
            this.TbLypanel.Controls.Add(this.btnReemb, 0, 0);
            this.TbLypanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbLypanel.Location = new System.Drawing.Point(0, 0);
            this.TbLypanel.Name = "TbLypanel";
            this.TbLypanel.RowCount = 1;
            this.TbLypanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TbLypanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TbLypanel.Size = new System.Drawing.Size(1632, 53);
            this.TbLypanel.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(842, 57);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(777, 49);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // btnPrueba
            // 
            this.btnPrueba.BackColor = System.Drawing.Color.White;
            this.btnPrueba.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrueba.Enabled = false;
            this.btnPrueba.FlatAppearance.BorderSize = 0;
            this.btnPrueba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrueba.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnPrueba.IconColor = System.Drawing.Color.Black;
            this.btnPrueba.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPrueba.Location = new System.Drawing.Point(8, 57);
            this.btnPrueba.Name = "btnPrueba";
            this.btnPrueba.Size = new System.Drawing.Size(119, 49);
            this.btnPrueba.TabIndex = 16;
            this.btnPrueba.Text = "Prueba";
            this.btnPrueba.UseVisualStyleBackColor = false;
            this.btnPrueba.Visible = false;
            // 
            // CargaReemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1632, 1055);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSuperior);
            this.Controls.Add(this.pnlInferior);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CargaReemForm";
            this.Text = "CARGA REEMBOLSOS";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlInferior.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pnlSuperior.ResumeLayout(false);
            this.TbLypanel.ResumeLayout(false);
            this.TbLypanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlInferior;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnFact;
        private System.Windows.Forms.Button btnReemb;
        private System.Windows.Forms.Panel pnlSuperior;
        private System.Windows.Forms.TableLayoutPanel TbLypanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private FontAwesome.Sharp.IconButton btnPrueba;
    }
}

