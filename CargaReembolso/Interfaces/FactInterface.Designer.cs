
namespace CargaReembolso.Interfaces
{
    partial class FactInterface
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblIntro = new System.Windows.Forms.Label();
            this.grdFact = new System.Windows.Forms.DataGridView();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.pnl3 = new System.Windows.Forms.Panel();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdFact)).BeginInit();
            this.pnl1.SuspendLayout();
            this.pnl2.SuspendLayout();
            this.pnl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIntro
            // 
            this.lblIntro.AutoSize = true;
            this.lblIntro.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblIntro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntro.Location = new System.Drawing.Point(10, 10);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(230, 20);
            this.lblIntro.TabIndex = 1;
            this.lblIntro.Text = "Sección para cargar facturas.";
            // 
            // grdFact
            // 
            this.grdFact.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grdFact.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdFact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFact.Location = new System.Drawing.Point(10, 10);
            this.grdFact.Name = "grdFact";
            this.grdFact.RowHeadersVisible = false;
            this.grdFact.RowHeadersWidth = 51;
            this.grdFact.RowTemplate.Height = 28;
            this.grdFact.Size = new System.Drawing.Size(1612, 839);
            this.grdFact.TabIndex = 3;
            // 
            // pnl1
            // 
            this.pnl1.Controls.Add(this.lblIntro);
            this.pnl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl1.Location = new System.Drawing.Point(0, 0);
            this.pnl1.Name = "pnl1";
            this.pnl1.Padding = new System.Windows.Forms.Padding(10);
            this.pnl1.Size = new System.Drawing.Size(1632, 44);
            this.pnl1.TabIndex = 4;
            // 
            // pnl2
            // 
            this.pnl2.Controls.Add(this.btnCargar);
            this.pnl2.Controls.Add(this.btnLimpiar);
            this.pnl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl2.Location = new System.Drawing.Point(0, 44);
            this.pnl2.Name = "pnl2";
            this.pnl2.Padding = new System.Windows.Forms.Padding(10);
            this.pnl2.Size = new System.Drawing.Size(1632, 70);
            this.pnl2.TabIndex = 5;
            // 
            // pnl3
            // 
            this.pnl3.Controls.Add(this.grdFact);
            this.pnl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl3.Location = new System.Drawing.Point(0, 114);
            this.pnl3.Name = "pnl3";
            this.pnl3.Padding = new System.Windows.Forms.Padding(10);
            this.pnl3.Size = new System.Drawing.Size(1632, 859);
            this.pnl3.TabIndex = 6;
            // 
            // btnCargar
            // 
            this.btnCargar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCargar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCargar.FlatAppearance.BorderSize = 0;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargar.Image = global::CargaReembolso.Properties.Resources.upload_icon;
            this.btnCargar.Location = new System.Drawing.Point(10, 10);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(86, 50);
            this.btnCargar.TabIndex = 3;
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLimpiar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::CargaReembolso.Properties.Resources.refresh;
            this.btnLimpiar.Location = new System.Drawing.Point(1535, 10);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(87, 50);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // FactInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.pnl3);
            this.Controls.Add(this.pnl2);
            this.Controls.Add(this.pnl1);
            this.Name = "FactInterface";
            this.Size = new System.Drawing.Size(1632, 973);
            ((System.ComponentModel.ISupportInitialize)(this.grdFact)).EndInit();
            this.pnl1.ResumeLayout(false);
            this.pnl1.PerformLayout();
            this.pnl2.ResumeLayout(false);
            this.pnl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView grdFact;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.Panel pnl3;
        private System.Windows.Forms.Button btnCargar;
    }
}
