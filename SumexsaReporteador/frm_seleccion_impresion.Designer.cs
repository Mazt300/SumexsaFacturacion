namespace SumexsaFacturacion
{
    partial class frm_seleccion_impresion
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_original = new System.Windows.Forms.Button();
            this.btn_copia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(121, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "¿QUE REPORTE DESEA IMPRIMIR?";
            // 
            // btn_original
            // 
            this.btn_original.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_original.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_original.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_original.ForeColor = System.Drawing.Color.White;
            this.btn_original.Location = new System.Drawing.Point(158, 49);
            this.btn_original.Name = "btn_original";
            this.btn_original.Size = new System.Drawing.Size(92, 36);
            this.btn_original.TabIndex = 29;
            this.btn_original.Text = "Original";
            this.btn_original.UseVisualStyleBackColor = false;
            this.btn_original.Click += new System.EventHandler(this.btn_original_Click);
            // 
            // btn_copia
            // 
            this.btn_copia.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_copia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_copia.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_copia.ForeColor = System.Drawing.Color.White;
            this.btn_copia.Location = new System.Drawing.Point(287, 49);
            this.btn_copia.Name = "btn_copia";
            this.btn_copia.Size = new System.Drawing.Size(92, 36);
            this.btn_copia.TabIndex = 30;
            this.btn_copia.Text = "Copia";
            this.btn_copia.UseVisualStyleBackColor = false;
            this.btn_copia.Click += new System.EventHandler(this.btn_copia_Click);
            // 
            // frm_seleccion_impresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(506, 107);
            this.Controls.Add(this.btn_copia);
            this.Controls.Add(this.btn_original);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_seleccion_impresion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_original;
        private System.Windows.Forms.Button btn_copia;
    }
}