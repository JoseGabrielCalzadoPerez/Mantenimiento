using Negocio;
using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.MonthCalendar;

namespace Mantenimiento
{
    public partial class AgregarProducto : MaterialSkin.Controls.MaterialForm
    {
        public AgregarProducto()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(800, 500);
            this.MaximizeBox = false;
            ConfigurarTabIndexBasico();
            // Manejar el tab loop
            txtNota.PreviewKeyDown += TxtNota_PreviewKeyDown;

        }

        private void ConfigurarTabIndexBasico()
        {
            dateTimePickerEntrada.TabIndex = 1;
            txtEquipo.TabIndex = 2;
            txtArea.TabIndex = 3;
            txtModelo.TabIndex = 4;
            txtInventario.TabIndex = 5;
            txtDiagnostico.TabIndex = 6;
            txtServiceTag.TabIndex = 7;
            txtSoporte.TabIndex = 8;
            txtNota.TabIndex = 9;
            foreach (Control control in this.Controls)
            {
                if (control is TextBox || control is ComboBox ||
                    control is DateTimePicker || control.GetType().Name.Contains("Guna2"))
                {
                    control.TabStop = true;
                }
            }
        }   

        private void TxtNota_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && !e.Shift) // Solo Tab normal, no Shift+Tab
            {
                e.IsInputKey = true;
                txtModelo.Focus(); // Vuelve al inicio en Modelo
            }
        }
        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            dateTimePickerEntrada.Value = DateTime.Now;
            // Agregar opción "En Proceso" al ComboBox de estado
            txtEstado.Items.Add("En Proceso");
            txtEstado.SelectedIndex = 0; // Seleccionar "En Proceso" por defecto
            // Recargar catálogos desde archivo antes de configurar ComboBoxes
            GestionarCatalogos.RecargarCatalogos();
            // Configurar ComboBoxes con datos dinámicos
            ConfigurarComboBoxes();
        }

        private void ConfigurarComboBoxes()
        {
            // Configurar ComboBox de Equipos
            ConfigurarComboBoxNA(txtEquipo);
            CargarOpcionesDesdeListaEstatica(txtEquipo, GestionarCatalogos.ListaEquipos);
            // Configurar ComboBox de Áreas
            ConfigurarComboBoxNA(txtArea);
            CargarOpcionesDesdeListaEstatica(txtArea, GestionarCatalogos.ListaAreas);
        }

        private void ConfigurarComboBoxNA(Guna.UI2.WinForms.Guna2ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Evita que el usuario escriba
        }

        private void CargarOpcionesDesdeListaEstatica(Guna.UI2.WinForms.Guna2ComboBox comboBox, List<string> opciones)
        {
            // Cargar opciones desde la lista estática
            if (opciones != null && opciones.Count > 0)
            {
                foreach (string opcion in opciones)
                {
                    if (!comboBox.Items.Contains(opcion))
                    {
                        comboBox.Items.Add(opcion);
                    }
                }
                // Seleccionar "N/A" por defecto si existe
                if (comboBox.Items.Contains("N/A"))
                {
                    comboBox.SelectedItem = "N/A";
                }
                else if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            else
            {
                // Si no hay opciones cargadas, usar valores predeterminados
                comboBox.Items.Add("N/A");
                comboBox.SelectedIndex = 0;
                // Agregar opciones por defecto según el tipo
                if (comboBox == txtEquipo)
                {
                    AgregarOpcionesComunes(comboBox, new[] { "CPU", "Monitor", "Impresora", "UPS", "Scanner", "Laptop" });
                }
                else if (comboBox == txtArea)
                {
                    AgregarOpcionesComunes(comboBox, new[] { "Administración", "Contabilidad", "Ventas", "Soporte" });
                }
            }
        }

        private void AgregarOpcionesComunes(Guna.UI2.WinForms.Guna2ComboBox comboBox, string[] opciones)
        {
            foreach (var opcion in opciones)
            {
                if (!comboBox.Items.Contains(opcion))
                {
                    comboBox.Items.Add(opcion);
                }
            }
        }

        // Método para refrescar los ComboBoxes (útil cuando se regresa del formulario de gestión)
        public void RefrescarComboBoxes()
        {
            // Recargar catálogos desde archivo
            GestionarCatalogos.RecargarCatalogos();

            // Reconfigurar ComboBoxes
            ConfigurarComboBoxes();
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener valores de los ComboBox
                string equipo = txtEquipo.SelectedItem?.ToString() ?? "N/A";
                string area = txtArea.SelectedItem?.ToString() ?? "N/A";
                string modelo = txtModelo.Text?.Trim() ?? "N/A";
                string inventario = txtInventario.Text?.Trim() ?? "N/A";
                string diagnostico = txtDiagnostico.Text?.Trim() ?? "";
                DateTime fechaEntrada = dateTimePickerEntrada.Value;
                // Crear instancia de Tramite
                Negocio.Tramite tramite = new Negocio.Tramite();
                // Llamar al método para agregar
                tramite.Agregar2(
                    fechaEntrada,
                    equipo,
                    area,
                    modelo,
                    inventario,
                    diagnostico,
                    txtServiceTag.Text?.Trim() ?? "",
                    txtSoporte.Text?.Trim() ?? "",
                    txtNota.Text?.Trim() ?? ""
                );
                MessageBox.Show("Registro agregado exitosamente.",
                                  "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Limpiar formulario después de agregar
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el registro: " + ex.Message,
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtModelo.Clear();
            txtInventario.Clear();
            txtDiagnostico.Clear();
            txtServiceTag.Clear();
            txtSoporte.Clear();
            txtNota.Clear();
            dateTimePickerEntrada.Value = DateTime.Now;
            // Resetear ComboBoxes a N/A si existe
            if (txtEquipo.Items.Contains("N/A"))
                txtEquipo.SelectedItem = "N/A";
            else if (txtEquipo.Items.Count > 0)
                txtEquipo.SelectedIndex = 0;
            if (txtArea.Items.Contains("N/A"))
                txtArea.SelectedItem = "N/A";
            else if (txtArea.Items.Count > 0)
                txtArea.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void txtFechaEntrada_Click(object sender, EventArgs e)
        {
        }

        private void txtArea_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.Hide(); // Comentado para evitar que se oculte al seleccionar
        }

        private void guna2CirclePictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AgregarProducto formAgregar = new AgregarProducto();
            formAgregar.FormClosed += (s, args) => this.Close();
            formAgregar.Show();
        }

        private void guna2CirclePictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            RegistroMantenimiento formRegistro = new RegistroMantenimiento();
            formRegistro.FormClosed += (s, args) => this.Close();
            formRegistro.Show();
        }

        // Método mejorado para gestionar catálogos
        private void btnGestionarCatalogos_Click(object sender, EventArgs e)
        {
            try
            {
                GestionarCatalogos formCatalogos = new GestionarCatalogos();
                DialogResult resultado = formCatalogos.ShowDialog();

                // Siempre refrescar los ComboBoxes después de cerrar el formulario de gestión
                // independientemente del DialogResult
                RefrescarComboBoxes();

                formCatalogos.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de gestión: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2CirclePictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GestionarCatalogos formCatalogos = new GestionarCatalogos();
            formCatalogos.FormClosed += (s, args) => this.Close();
            formCatalogos.Show();
        }

        private void txtSoporte_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistroMantenimiento formRegistro = new RegistroMantenimiento();
            formRegistro.FormClosed += (s, args) => this.Close();
            formRegistro.Show();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            GestionarCatalogos formCatalogos = new GestionarCatalogos();
            formCatalogos.FormClosed += (s, args) => this.Close();
            formCatalogos.Show();
        }

        private void dateTimePickerSalida_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNota_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInventario_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerEntrada_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtEstado_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void txtDiagnostico_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtServiceTag_TextChanged(object sender, EventArgs e)
        {

        }
    }
}