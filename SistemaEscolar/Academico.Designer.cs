namespace SistemaEscolar
{
    partial class Academico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Academico));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAgregar = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.dgvAcademico = new System.Windows.Forms.DataGridView();
            this.txtNombreAcademicos = new System.Windows.Forms.TextBox();
            this.txtApellidosAcademicos = new System.Windows.Forms.TextBox();
            this.txtGradoAcademico = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcademico)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAgregar,
            this.btnEditar,
            this.btnEliminar,
            this.btnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(654, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(87, 24);
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(72, 24);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(87, 24);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(62, 24);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // dgvAcademico
            // 
            this.dgvAcademico.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvAcademico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAcademico.Location = new System.Drawing.Point(28, 44);
            this.dgvAcademico.Name = "dgvAcademico";
            this.dgvAcademico.RowHeadersWidth = 51;
            this.dgvAcademico.RowTemplate.Height = 24;
            this.dgvAcademico.Size = new System.Drawing.Size(592, 272);
            this.dgvAcademico.TabIndex = 1;
            this.dgvAcademico.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAcademico_CellContentClick);
            this.dgvAcademico.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAcademico_CellContentClick);
            // 
            // txtNombreAcademicos
            // 
            this.txtNombreAcademicos.Location = new System.Drawing.Point(58, 372);
            this.txtNombreAcademicos.Name = "txtNombreAcademicos";
            this.txtNombreAcademicos.Size = new System.Drawing.Size(100, 22);
            this.txtNombreAcademicos.TabIndex = 2;
            this.txtNombreAcademicos.Text = "Nombre";
            // 
            // txtApellidosAcademicos
            // 
            this.txtApellidosAcademicos.Location = new System.Drawing.Point(261, 372);
            this.txtApellidosAcademicos.Name = "txtApellidosAcademicos";
            this.txtApellidosAcademicos.Size = new System.Drawing.Size(100, 22);
            this.txtApellidosAcademicos.TabIndex = 3;
            this.txtApellidosAcademicos.Text = "Apellidos";
            // 
            // txtGradoAcademico
            // 
            this.txtGradoAcademico.Location = new System.Drawing.Point(455, 372);
            this.txtGradoAcademico.Name = "txtGradoAcademico";
            this.txtGradoAcademico.Size = new System.Drawing.Size(100, 22);
            this.txtGradoAcademico.TabIndex = 4;
            this.txtGradoAcademico.Text = "Grado";
            // 
            // Academico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(654, 450);
            this.Controls.Add(this.txtGradoAcademico);
            this.Controls.Add(this.txtApellidosAcademicos);
            this.Controls.Add(this.txtNombreAcademicos);
            this.Controls.Add(this.dgvAcademico);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Academico";
            this.Text = "Academico";
            this.Load += new System.EventHandler(this.Academico_Load_1);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcademico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAgregar;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.DataGridView dgvAcademico;
        private System.Windows.Forms.TextBox txtNombreAcademicos;
        private System.Windows.Forms.TextBox txtApellidosAcademicos;
        private System.Windows.Forms.TextBox txtGradoAcademico;
    }
}