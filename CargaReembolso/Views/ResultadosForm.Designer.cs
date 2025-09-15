
namespace CargaReembolso.Views
{
    partial class ResultadosForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpReembolsos = new System.Windows.Forms.TabPage();
            this.dgvReembolsos = new System.Windows.Forms.DataGridView();
            this.tpFacturas = new System.Windows.Forms.TabPage();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tpReembolsos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReembolsos)).BeginInit();
            this.tpFacturas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpReembolsos);
            this.tabControl1.Controls.Add(this.tpFacturas);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tpReembolsos
            // 
            this.tpReembolsos.Controls.Add(this.dgvReembolsos);
            this.tpReembolsos.Location = new System.Drawing.Point(4, 29);
            this.tpReembolsos.Name = "tpReembolsos";
            this.tpReembolsos.Padding = new System.Windows.Forms.Padding(3);
            this.tpReembolsos.Size = new System.Drawing.Size(792, 417);
            this.tpReembolsos.TabIndex = 0;
            this.tpReembolsos.Text = "Reembolso";
            this.tpReembolsos.UseVisualStyleBackColor = true;
            // 
            // dgvReembolsos
            // 
            this.dgvReembolsos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReembolsos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvReembolsos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReembolsos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReembolsos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReembolsos.Location = new System.Drawing.Point(3, 3);
            this.dgvReembolsos.Name = "dgvReembolsos";
            this.dgvReembolsos.ReadOnly = true;
            this.dgvReembolsos.RowHeadersVisible = false;
            this.dgvReembolsos.RowHeadersWidth = 51;
            this.dgvReembolsos.RowTemplate.Height = 28;
            this.dgvReembolsos.Size = new System.Drawing.Size(786, 411);
            this.dgvReembolsos.TabIndex = 0;
            // 
            // tpFacturas
            // 
            this.tpFacturas.Controls.Add(this.dgvFacturas);
            this.tpFacturas.Location = new System.Drawing.Point(4, 29);
            this.tpFacturas.Name = "tpFacturas";
            this.tpFacturas.Padding = new System.Windows.Forms.Padding(3);
            this.tpFacturas.Size = new System.Drawing.Size(792, 417);
            this.tpFacturas.TabIndex = 1;
            this.tpFacturas.Text = "Factura";
            this.tpFacturas.UseVisualStyleBackColor = true;
            // 
            // dgvFacturas
            // 
            this.dgvFacturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFacturas.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvFacturas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFacturas.Location = new System.Drawing.Point(3, 3);
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.RowHeadersVisible = false;
            this.dgvFacturas.RowHeadersWidth = 51;
            this.dgvFacturas.RowTemplate.Height = 28;
            this.dgvFacturas.Size = new System.Drawing.Size(786, 411);
            this.dgvFacturas.TabIndex = 0;
            // 
            // ResultadosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "ResultadosForm";
            this.Text = "ResultadosForm";
            this.tabControl1.ResumeLayout(false);
            this.tpReembolsos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReembolsos)).EndInit();
            this.tpFacturas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpReembolsos;
        private System.Windows.Forms.TabPage tpFacturas;
        private System.Windows.Forms.DataGridView dgvReembolsos;
        private System.Windows.Forms.DataGridView dgvFacturas;
    }
}