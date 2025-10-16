namespace Mantenimiento
{
    partial class GestionarCatalogos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAreas = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxAreas = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNuevaArea = new System.Windows.Forms.TextBox();
            this.btnAgregarArea = new System.Windows.Forms.Button();
            this.btnModificarArea = new System.Windows.Forms.Button();
            this.btnEliminarArea = new System.Windows.Forms.Button();
            this.tabEquipos = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxEquipos = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNuevoEquipo = new System.Windows.Forms.TextBox();
            this.btnAgregarEquipo = new System.Windows.Forms.Button();
            this.btnModificarEquipo = new System.Windows.Forms.Button();
            this.btnEliminarEquipo = new System.Windows.Forms.Button();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.guna2CirclePictureBox2 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2CirclePictureBox3 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.tabControl1.SuspendLayout();
            this.tabAreas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabEquipos.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabAreas);
            this.tabControl1.Controls.Add(this.tabEquipos);
            this.tabControl1.Location = new System.Drawing.Point(126, 92);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(855, 514);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAreas
            // 
            this.tabAreas.Controls.Add(this.groupBox1);
            this.tabAreas.Controls.Add(this.groupBox2);
            this.tabAreas.Location = new System.Drawing.Point(4, 25);
            this.tabAreas.Margin = new System.Windows.Forms.Padding(4);
            this.tabAreas.Name = "tabAreas";
            this.tabAreas.Padding = new System.Windows.Forms.Padding(4);
            this.tabAreas.Size = new System.Drawing.Size(847, 485);
            this.tabAreas.TabIndex = 0;
            this.tabAreas.Text = "Gestionar Áreas";
            this.tabAreas.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxAreas);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(333, 414);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Áreas Existentes";
            // 
            // listBoxAreas
            // 
            this.listBoxAreas.FormattingEnabled = true;
            this.listBoxAreas.ItemHeight = 16;
            this.listBoxAreas.Location = new System.Drawing.Point(8, 23);
            this.listBoxAreas.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxAreas.Name = "listBoxAreas";
            this.listBoxAreas.Size = new System.Drawing.Size(316, 372);
            this.listBoxAreas.TabIndex = 0;
            this.listBoxAreas.SelectedIndexChanged += new System.EventHandler(this.listBoxAreas_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNuevaArea);
            this.groupBox2.Controls.Add(this.btnAgregarArea);
            this.groupBox2.Controls.Add(this.btnModificarArea);
            this.groupBox2.Controls.Add(this.btnEliminarArea);
            this.groupBox2.Location = new System.Drawing.Point(349, 7);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(429, 418);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Acciones";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre del Área:";
            // 
            // txtNuevaArea
            // 
            this.txtNuevaArea.Location = new System.Drawing.Point(32, 56);
            this.txtNuevaArea.Margin = new System.Windows.Forms.Padding(4);
            this.txtNuevaArea.Name = "txtNuevaArea";
            this.txtNuevaArea.Size = new System.Drawing.Size(357, 22);
            this.txtNuevaArea.TabIndex = 1;
            // 
            // btnAgregarArea
            // 
            this.btnAgregarArea.BackColor = System.Drawing.Color.Green;
            this.btnAgregarArea.ForeColor = System.Drawing.Color.White;
            this.btnAgregarArea.Location = new System.Drawing.Point(32, 99);
            this.btnAgregarArea.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregarArea.Name = "btnAgregarArea";
            this.btnAgregarArea.Size = new System.Drawing.Size(359, 43);
            this.btnAgregarArea.TabIndex = 2;
            this.btnAgregarArea.Text = "Agregar Área";
            this.btnAgregarArea.UseVisualStyleBackColor = false;
            this.btnAgregarArea.Click += new System.EventHandler(this.btnAgregarArea_Click);
            // 
            // btnModificarArea
            // 
            this.btnModificarArea.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnModificarArea.ForeColor = System.Drawing.Color.White;
            this.btnModificarArea.Location = new System.Drawing.Point(32, 161);
            this.btnModificarArea.Margin = new System.Windows.Forms.Padding(4);
            this.btnModificarArea.Name = "btnModificarArea";
            this.btnModificarArea.Size = new System.Drawing.Size(359, 43);
            this.btnModificarArea.TabIndex = 3;
            this.btnModificarArea.Text = "Modificar Área Seleccionada";
            this.btnModificarArea.UseVisualStyleBackColor = false;
            this.btnModificarArea.Click += new System.EventHandler(this.btnModificarArea_Click);
            // 
            // btnEliminarArea
            // 
            this.btnEliminarArea.BackColor = System.Drawing.Color.Maroon;
            this.btnEliminarArea.ForeColor = System.Drawing.Color.White;
            this.btnEliminarArea.Location = new System.Drawing.Point(32, 223);
            this.btnEliminarArea.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminarArea.Name = "btnEliminarArea";
            this.btnEliminarArea.Size = new System.Drawing.Size(359, 43);
            this.btnEliminarArea.TabIndex = 4;
            this.btnEliminarArea.Text = "Eliminar Área Seleccionada";
            this.btnEliminarArea.UseVisualStyleBackColor = false;
            this.btnEliminarArea.Click += new System.EventHandler(this.btnEliminarArea_Click);
            // 
            // tabEquipos
            // 
            this.tabEquipos.Controls.Add(this.groupBox3);
            this.tabEquipos.Controls.Add(this.groupBox4);
            this.tabEquipos.Location = new System.Drawing.Point(4, 25);
            this.tabEquipos.Margin = new System.Windows.Forms.Padding(4);
            this.tabEquipos.Name = "tabEquipos";
            this.tabEquipos.Padding = new System.Windows.Forms.Padding(4);
            this.tabEquipos.Size = new System.Drawing.Size(847, 485);
            this.tabEquipos.TabIndex = 1;
            this.tabEquipos.Text = "Gestionar Equipos";
            this.tabEquipos.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBoxEquipos);
            this.groupBox3.Location = new System.Drawing.Point(8, 7);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(333, 384);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Equipos Existentes";
            // 
            // listBoxEquipos
            // 
            this.listBoxEquipos.FormattingEnabled = true;
            this.listBoxEquipos.ItemHeight = 16;
            this.listBoxEquipos.Location = new System.Drawing.Point(8, 23);
            this.listBoxEquipos.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxEquipos.Name = "listBoxEquipos";
            this.listBoxEquipos.Size = new System.Drawing.Size(316, 340);
            this.listBoxEquipos.TabIndex = 0;
            this.listBoxEquipos.SelectedIndexChanged += new System.EventHandler(this.listBoxEquipos_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtNuevoEquipo);
            this.groupBox4.Controls.Add(this.btnAgregarEquipo);
            this.groupBox4.Controls.Add(this.btnModificarEquipo);
            this.groupBox4.Controls.Add(this.btnEliminarEquipo);
            this.groupBox4.Location = new System.Drawing.Point(349, 7);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(379, 384);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Acciones";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nombre del Equipo:";
            // 
            // txtNuevoEquipo
            // 
            this.txtNuevoEquipo.Location = new System.Drawing.Point(12, 55);
            this.txtNuevoEquipo.Margin = new System.Windows.Forms.Padding(4);
            this.txtNuevoEquipo.Name = "txtNuevoEquipo";
            this.txtNuevoEquipo.Size = new System.Drawing.Size(357, 22);
            this.txtNuevoEquipo.TabIndex = 1;
            // 
            // btnAgregarEquipo
            // 
            this.btnAgregarEquipo.BackColor = System.Drawing.Color.Green;
            this.btnAgregarEquipo.ForeColor = System.Drawing.Color.White;
            this.btnAgregarEquipo.Location = new System.Drawing.Point(12, 98);
            this.btnAgregarEquipo.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregarEquipo.Name = "btnAgregarEquipo";
            this.btnAgregarEquipo.Size = new System.Drawing.Size(359, 43);
            this.btnAgregarEquipo.TabIndex = 2;
            this.btnAgregarEquipo.Text = "Agregar Equipo";
            this.btnAgregarEquipo.UseVisualStyleBackColor = false;
            this.btnAgregarEquipo.Click += new System.EventHandler(this.btnAgregarEquipo_Click);
            // 
            // btnModificarEquipo
            // 
            this.btnModificarEquipo.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnModificarEquipo.ForeColor = System.Drawing.Color.White;
            this.btnModificarEquipo.Location = new System.Drawing.Point(12, 160);
            this.btnModificarEquipo.Margin = new System.Windows.Forms.Padding(4);
            this.btnModificarEquipo.Name = "btnModificarEquipo";
            this.btnModificarEquipo.Size = new System.Drawing.Size(359, 43);
            this.btnModificarEquipo.TabIndex = 3;
            this.btnModificarEquipo.Text = "Modificar Equipo Seleccionado";
            this.btnModificarEquipo.UseVisualStyleBackColor = false;
            this.btnModificarEquipo.Click += new System.EventHandler(this.btnModificarEquipo_Click);
            // 
            // btnEliminarEquipo
            // 
            this.btnEliminarEquipo.BackColor = System.Drawing.Color.Maroon;
            this.btnEliminarEquipo.ForeColor = System.Drawing.Color.White;
            this.btnEliminarEquipo.Location = new System.Drawing.Point(12, 222);
            this.btnEliminarEquipo.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminarEquipo.Name = "btnEliminarEquipo";
            this.btnEliminarEquipo.Size = new System.Drawing.Size(359, 43);
            this.btnEliminarEquipo.TabIndex = 4;
            this.btnEliminarEquipo.Text = "Eliminar Equipo Seleccionado";
            this.btnEliminarEquipo.UseVisualStyleBackColor = false;
            this.btnEliminarEquipo.Click += new System.EventHandler(this.btnEliminarEquipo_Click);
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2CirclePictureBox1.Image = global::Mantenimiento.Properties.Resources.onamet;
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(24, 92);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(64, 64);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2CirclePictureBox1.TabIndex = 16;
            this.guna2CirclePictureBox1.TabStop = false;
            this.guna2CirclePictureBox1.UseTransparentBackground = true;
            // 
            // guna2CirclePictureBox2
            // 
            this.guna2CirclePictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.guna2CirclePictureBox2.Image = global::Mantenimiento.Properties.Resources.hogar__1_;
            this.guna2CirclePictureBox2.ImageRotate = 0F;
            this.guna2CirclePictureBox2.Location = new System.Drawing.Point(24, 193);
            this.guna2CirclePictureBox2.Name = "guna2CirclePictureBox2";
            this.guna2CirclePictureBox2.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox2.Size = new System.Drawing.Size(64, 64);
            this.guna2CirclePictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2CirclePictureBox2.TabIndex = 17;
            this.guna2CirclePictureBox2.TabStop = false;
            this.guna2CirclePictureBox2.UseTransparentBackground = true;
            this.guna2CirclePictureBox2.Click += new System.EventHandler(this.guna2CirclePictureBox2_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = global::Mantenimiento.Properties.Resources.informe;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(24, 380);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(64, 59);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 24;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // guna2CirclePictureBox3
            // 
            this.guna2CirclePictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.guna2CirclePictureBox3.Image = global::Mantenimiento.Properties.Resources.lista;
            this.guna2CirclePictureBox3.ImageRotate = 0F;
            this.guna2CirclePictureBox3.Location = new System.Drawing.Point(24, 285);
            this.guna2CirclePictureBox3.Name = "guna2CirclePictureBox3";
            this.guna2CirclePictureBox3.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox3.Size = new System.Drawing.Size(64, 64);
            this.guna2CirclePictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2CirclePictureBox3.TabIndex = 23;
            this.guna2CirclePictureBox3.TabStop = false;
            this.guna2CirclePictureBox3.UseTransparentBackground = true;
            this.guna2CirclePictureBox3.Click += new System.EventHandler(this.guna2CirclePictureBox3_Click_1);
            // 
            // GestionarCatalogos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 619);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.guna2CirclePictureBox3);
            this.Controls.Add(this.guna2CirclePictureBox1);
            this.Controls.Add(this.guna2CirclePictureBox2);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GestionarCatalogos";
            this.Text = "Gestionar Catálogos";
            this.Load += new System.EventHandler(this.GestionarCatalogos_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabAreas.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabEquipos.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAreas;
        private System.Windows.Forms.TabPage tabEquipos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxAreas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNuevaArea;
        private System.Windows.Forms.Button btnAgregarArea;
        private System.Windows.Forms.Button btnModificarArea;
        private System.Windows.Forms.Button btnEliminarArea;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxEquipos;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNuevoEquipo;
        private System.Windows.Forms.Button btnAgregarEquipo;
        private System.Windows.Forms.Button btnModificarEquipo;
        private System.Windows.Forms.Button btnEliminarEquipo;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox2;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox3;
    }
}