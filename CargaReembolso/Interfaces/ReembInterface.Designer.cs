
namespace CargaReembolso.Interfaces
{
    partial class ReembInterface
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
            this.pnl2 = new System.Windows.Forms.Panel();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.grdFact = new System.Windows.Forms.DataGridView();
            this.pnl3 = new System.Windows.Forms.Panel();
            this.grdReemb = new System.Windows.Forms.DataGridView();
            this.pnl2.SuspendLayout();
            this.pnl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFact)).BeginInit();
            this.pnl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReemb)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIntro
            // 
            this.lblIntro.AutoSize = true;
            this.lblIntro.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblIntro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntro.Location = new System.Drawing.Point(10, 10);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(297, 20);
            this.lblIntro.TabIndex = 1;
            this.lblIntro.Text = "Sección para registros de reembolsos.";
            // 
            // pnl2
            // 
            this.pnl2.Controls.Add(this.btnCargar);
            this.pnl2.Controls.Add(this.btnLimpiar);
            this.pnl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl2.Location = new System.Drawing.Point(0, 44);
            this.pnl2.Name = "pnl2";
            this.pnl2.Padding = new System.Windows.Forms.Padding(10);
            this.pnl2.Size = new System.Drawing.Size(1632, 56);
            this.pnl2.TabIndex = 8;
            // 
            // btnCargar
            // 
            this.btnCargar.BackColor = System.Drawing.Color.White;
            this.btnCargar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCargar.FlatAppearance.BorderSize = 0;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargar.Location = new System.Drawing.Point(10, 10);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(86, 36);
            this.btnCargar.TabIndex = 3;
            this.btnCargar.Text = "...";
            this.btnCargar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.White;
            this.btnLimpiar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(1463, 10);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(159, 36);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // pnl1
            // 
            this.pnl1.Controls.Add(this.lblIntro);
            this.pnl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl1.Location = new System.Drawing.Point(0, 0);
            this.pnl1.Name = "pnl1";
            this.pnl1.Padding = new System.Windows.Forms.Padding(10);
            this.pnl1.Size = new System.Drawing.Size(1632, 44);
            this.pnl1.TabIndex = 7;
            // 
            // grdFact
            // 
            this.grdFact.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grdFact.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdFact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFact.Location = new System.Drawing.Point(0, 0);
            this.grdFact.Name = "grdFact";
            this.grdFact.RowHeadersWidth = 51;
            this.grdFact.RowTemplate.Height = 28;
            this.grdFact.Size = new System.Drawing.Size(1632, 873);
            this.grdFact.TabIndex = 6;
            // 
            // pnl3
            // 
            this.pnl3.Controls.Add(this.grdReemb);
            this.pnl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl3.Location = new System.Drawing.Point(0, 100);
            this.pnl3.Name = "pnl3";
            this.pnl3.Padding = new System.Windows.Forms.Padding(10);
            this.pnl3.Size = new System.Drawing.Size(1632, 773);
            this.pnl3.TabIndex = 9;
            // 
            // grdReemb
            // 
            this.grdReemb.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grdReemb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdReemb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReemb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReemb.Location = new System.Drawing.Point(10, 10);
            this.grdReemb.Name = "grdReemb";
            this.grdReemb.RowHeadersWidth = 51;
            this.grdReemb.RowTemplate.Height = 28;
            this.grdReemb.Size = new System.Drawing.Size(1612, 753);
            this.grdReemb.TabIndex = 3;
            // 
            // ReembInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.pnl3);
            this.Controls.Add(this.pnl2);
            this.Controls.Add(this.pnl1);
            this.Controls.Add(this.grdFact);
            this.Name = "ReembInterface";
            this.Size = new System.Drawing.Size(1632, 873);
            this.pnl2.ResumeLayout(false);
            this.pnl1.ResumeLayout(false);
            this.pnl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFact)).EndInit();
            this.pnl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReemb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.DataGridView grdFact;
        private System.Windows.Forms.Panel pnl3;
        private System.Windows.Forms.DataGridView grdReemb;
    }
}
