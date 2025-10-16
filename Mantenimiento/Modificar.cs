using Guna.UI2.WinForms;
using Negocio;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mantenimiento
{
    public partial class Modificar : MaterialSkin.Controls.MaterialForm
    {
        private Tramite tramite = new Tramite();
        private int idRegistro; // Para almacenar el ID del registro a modificar

        public Modificar()
        {
            InitializeComponent();
        }

        // Constructor que recibe los datos del registro seleccionado
        public Modificar(int id, DateTime fechaEntrada, string equipo, string area, string modelo,
                        string inventario, string diagnostico, DateTime fechaSalida,
                        string serviceTag, string soporte, string nota)
        {
            InitializeComponent();

            // Guardar el ID
            idRegistro = id;

            // Recargar catálogos desde archivo antes de cargar datos
            GestionarCatalogos.RecargarCatalogos();

            // Llenar los campos con los datos recibidos
            CargarDatos(fechaEntrada, equipo, area, modelo, inventario, diagnostico,
                       fechaSalida, serviceTag, soporte, nota);

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
            dateTimePickerSalida.TabIndex = 6;
            txtDiagnostico.TabIndex = 7;
            txtServiceTag.TabIndex = 8;
            txtSoporte.TabIndex = 9;
            txtNota.TabIndex = 10;

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

        // Método mejorado para cargar datos con mejor manejo de fechas
        private void CargarDatos(DateTime fechaEntrada, string equipo, string area, string modelo,
                       string inventario, string diagnostico, DateTime fechaSalida,
                       string serviceTag, string soporte, string nota)
        {
            // Primero configurar los ComboBoxes
            ConfigurarComboBoxes();

            // Cargar las fechas
            dateTimePickerEntrada.Value = fechaEntrada;

            // Manejar fecha de salida - si es fecha mínima o muy antigua, usar fecha actual
            if (fechaSalida == DateTime.MinValue || fechaSalida.Year < 1900)
            {
                dateTimePickerSalida.Value = DateTime.Now;
                Console.WriteLine("Fecha de salida era inválida, usando fecha actual");
            }
            else
            {
                dateTimePickerSalida.Value = fechaSalida;
                Console.WriteLine($"Fecha de salida cargada: {fechaSalida:dd/MM/yyyy}");
            }

            // Cargar ComboBoxes - si el valor está vacío o es null, usar "N/A"
            CargarComboBox(txtEquipo, equipo);
            CargarComboBox(txtArea, area);

            // Cargar campos de texto normales
            txtModelo.Text = string.IsNullOrWhiteSpace(modelo) ? "" : modelo;
            txtInventario.Text = string.IsNullOrWhiteSpace(inventario) ? "N/A" : inventario;
            txtDiagnostico.Text = diagnostico ?? "";
            txtServiceTag.Text = serviceTag ?? "";
            txtSoporte.Text = soporte ?? "";
            txtNota.Text = nota ?? "";
        }

        private void CargarComboBox(Guna.UI2.WinForms.Guna2ComboBox comboBox, string valor)
        {
            // Si el valor está vacío o es null, seleccionar "N/A"
            if (string.IsNullOrWhiteSpace(valor))
            {
                if (comboBox.Items.Contains("N/A"))
                {
                    comboBox.SelectedItem = "N/A";
                }
                else if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
                return;
            }

            // Buscar el valor en los items del ComboBox (case insensitive)
            var itemEncontrado = comboBox.Items.Cast<string>()
                .FirstOrDefault(item => item.Equals(valor, StringComparison.OrdinalIgnoreCase));

            if (itemEncontrado != null)
            {
                comboBox.SelectedItem = itemEncontrado;
            }
            else
            {
                // Si el valor no existe en la lista, agregarlo y seleccionarlo
                comboBox.Items.Add(valor);
                comboBox.SelectedItem = valor;

                // También agregar a la lista estática correspondiente
                if (comboBox == txtEquipo && !GestionarCatalogos.ListaEquipos.Contains(valor))
                {
                    GestionarCatalogos.ListaEquipos.Add(valor);
                }
                else if (comboBox == txtArea && !GestionarCatalogos.ListaAreas.Contains(valor))
                {
                    GestionarCatalogos.ListaAreas.Add(valor);
                }
            }
        }

        private void Modificar_Load(object sender, EventArgs e)
        {
            // Configurar fechas por defecto si están vacías
            if (dateTimePickerEntrada.Value == DateTime.MinValue)
                dateTimePickerEntrada.Value = DateTime.Now;
            if (dateTimePickerSalida.Value == DateTime.MinValue)
                dateTimePickerSalida.Value = DateTime.Now;

            // Solo configurar ComboBoxes si no se han configurado ya (para evitar duplicados)
            if (txtEquipo.Items.Count == 0)
            {
                // Recargar catálogos desde archivo
                GestionarCatalogos.RecargarCatalogos();
                ConfigurarComboBoxes();
            }
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
            // Limpiar items existentes para evitar duplicados
            comboBox.Items.Clear();
            // Configurar como DropDownList para evitar que el usuario escriba
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarOpcionesDesdeListaEstatica(Guna.UI2.WinForms.Guna2ComboBox comboBox, System.Collections.Generic.List<string> opciones)
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
            }
            else
            {
                // Si no hay opciones cargadas, usar valores predeterminados
                comboBox.Items.Add("N/A");

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
                // Solo agregar si no existe ya
                if (!comboBox.Items.Contains(opcion))
                {
                    comboBox.Items.Add(opcion);
                }
            }
        }

        // Método para refrescar los ComboBoxes
        public void RefrescarComboBoxes()
        {
            // Recargar catálogos desde archivo
            GestionarCatalogos.RecargarCatalogos();

            // Guardar selecciones actuales
            string equipoSeleccionado = txtEquipo.SelectedItem?.ToString();
            string areaSeleccionada = txtArea.SelectedItem?.ToString();

            // Reconfigurar ComboBoxes
            ConfigurarComboBoxes();

            // Restaurar selecciones si existen
            if (!string.IsNullOrEmpty(equipoSeleccionado))
            {
                CargarComboBox(txtEquipo, equipoSeleccionado);
            }
            if (!string.IsNullOrEmpty(areaSeleccionada))
            {
                CargarComboBox(txtArea, areaSeleccionada);
            }
        }

        // BOTÓN GUARDAR MODIFICACIÓN - CORREGIDO
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtEquipo.Text) || txtEquipo.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecciona un equipo.", "Campo requerido",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtArea.Text) || txtArea.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecciona un área.", "Campo requerido",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Recopilar datos del formulario
                DateTime fechaEntrada = dateTimePickerEntrada.Value;
                string equipo = txtEquipo.SelectedItem?.ToString() ?? "N/A";
                string area = txtArea.SelectedItem?.ToString() ?? "N/A";
                string modelo = txtModelo.Text.Trim();
                string inventario = string.IsNullOrWhiteSpace(txtInventario.Text) ? "N/A" : txtInventario.Text.Trim();
                string diagnostico = txtDiagnostico.Text.Trim();
                DateTime fechaSalida = dateTimePickerSalida.Value;
                string serviceTag = txtServiceTag.Text.Trim();
                string soporte = txtSoporte.Text.Trim();
                string nota = txtNota.Text.Trim();

                // Llamar al método de modificación
                tramite.ModificarData(idRegistro, fechaEntrada, equipo, area, modelo,
                                    inventario, diagnostico, fechaSalida, serviceTag, soporte, nota);

                MessageBox.Show("Registro modificado correctamente.", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Establecer el resultado del diálogo como OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el registro: " + ex.Message,
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
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
            dateTimePickerSalida.Value = DateTime.Now;

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

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {
            // Evento del campo modelo
        }

        // Evento para navegación entre formularios
        private void btnIrARegistros_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistroMantenimiento formRegistro = new RegistroMantenimiento();
            formRegistro.FormClosed += (s, args) => this.Close();
            formRegistro.Show();
        }

        private void btnIrAAgregar_Click(object sender, EventArgs e)
        {
            this.Hide();
            AgregarProducto formAgregar = new AgregarProducto();
            formAgregar.FormClosed += (s, args) => this.Close();
            formAgregar.Show();
        }

        private void btnIrAGestionar_Click(object sender, EventArgs e)
        {
            this.Hide();
            GestionarCatalogos formCatalogos = new GestionarCatalogos();
            formCatalogos.FormClosed += (s, args) => this.Close();
            formCatalogos.Show();
        }
    }
}