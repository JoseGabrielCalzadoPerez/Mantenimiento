using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Mantenimiento
{
    public partial class GestionarCatalogos : MaterialSkin.Controls.MaterialForm
    {
        // Listas estáticas para compartir entre formularios
        public static List<string> ListaAreas = new List<string>();
        public static List<string> ListaEquipos = new List<string>();

        private string rutaArchivoCatalogos = Path.Combine(Application.StartupPath, "catalogos.xml");

        public GestionarCatalogos()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(800, 500);
            this.MaximizeBox = false;

            // Cargar datos primero desde archivo
            CargarDatosDesdeArchivo();
            // Solo agregar predeterminados si no hay datos
            CargarDatosPredeterminados();
        }

        private void GestionarCatalogos_Load(object sender, EventArgs e)
        {
            // Configurar pestañas
            tabControl1.SelectedIndex = 0;

            // Cargar datos en las listas
            ActualizarListaAreas();
            ActualizarListaEquipos();
        }

        private void CargarDatosPredeterminados()
        {
            // Solo cargar datos predeterminados si las listas están vacías
            if (ListaAreas.Count == 0)
            {
                ListaAreas.AddRange(new[] { "N/A", "Administración", "Contabilidad", "Ventas", "Soporte", "Recursos Humanos", "IT", "Gerencia" });
            }

            if (ListaEquipos.Count == 0)
            {
                ListaEquipos.AddRange(new[] { "N/A", "CPU", "Monitor", "Impresora", "UPS", "Scanner", "Laptop", "Mouse", "Teclado", "Proyector" });
            }

            // Guardar los datos por primera vez si no existía el archivo
            if (!File.Exists(rutaArchivoCatalogos))
            {
                GuardarDatosEnArchivo();
            }
        } 

        private void CargarDatosDesdeArchivo()
        {
            try
            {
                if (File.Exists(rutaArchivoCatalogos))
                {
                    XDocument doc = XDocument.Load(rutaArchivoCatalogos);

                    // Limpiar las listas antes de cargar
                    ListaAreas.Clear();
                    ListaEquipos.Clear();

                    // Cargar áreas
                    var areasElement = doc.Root.Element("Areas");
                    if (areasElement != null)
                    {
                        foreach (var areaElement in areasElement.Elements("Area"))
                        {
                            string valor = areaElement.Value;
                            if (!string.IsNullOrWhiteSpace(valor) && !ListaAreas.Contains(valor))
                            {
                                ListaAreas.Add(valor);
                            }
                        }
                    }

                    // Cargar equipos
                    var equiposElement = doc.Root.Element("Equipos");
                    if (equiposElement != null)
                    {
                        foreach (var equipoElement in equiposElement.Elements("Equipo"))
                        {
                            string valor = equipoElement.Value;
                            if (!string.IsNullOrWhiteSpace(valor) && !ListaEquipos.Contains(valor))
                            {
                                ListaEquipos.Add(valor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar catálogos: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GuardarDatosEnArchivo()
        {
            try
            {
                XDocument doc = new XDocument(
                    new XElement("Catalogos",
                        new XElement("Areas",
                            ListaAreas.Where(area => !string.IsNullOrWhiteSpace(area))
                                     .Select(area => new XElement("Area", area))
                        ),
                        new XElement("Equipos",
                            ListaEquipos.Where(equipo => !string.IsNullOrWhiteSpace(equipo))
                                       .Select(equipo => new XElement("Equipo", equipo))
                        )
                    )
                );

                doc.Save(rutaArchivoCatalogos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar catálogos: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarListaAreas()
        {
            listBoxAreas.Items.Clear();
            foreach (var area in ListaAreas.OrderBy(x => x == "N/A" ? 0 : 1).ThenBy(x => x))
            {
                listBoxAreas.Items.Add(area);
            }
        }

        private void ActualizarListaEquipos()
        {
            listBoxEquipos.Items.Clear();
            foreach (var equipo in ListaEquipos.OrderBy(x => x == "N/A" ? 0 : 1).ThenBy(x => x))
            {
                listBoxEquipos.Items.Add(equipo);
            }
        }

        // Método público para recargar catálogos desde archivo
        public static void RecargarCatalogos()
        {
            try
            {
                string rutaArchivo = Path.Combine(Application.StartupPath, "catalogos.xml");

                if (File.Exists(rutaArchivo))
                {
                    XDocument doc = XDocument.Load(rutaArchivo);

                    // Limpiar las listas
                    ListaAreas.Clear();
                    ListaEquipos.Clear();

                    // Cargar áreas
                    var areasElement = doc.Root.Element("Areas");
                    if (areasElement != null)
                    {
                        foreach (var areaElement in areasElement.Elements("Area"))
                        {
                            string valor = areaElement.Value;
                            if (!string.IsNullOrWhiteSpace(valor) && !ListaAreas.Contains(valor))
                            {
                                ListaAreas.Add(valor);
                            }
                        }
                    }

                    // Cargar equipos
                    var equiposElement = doc.Root.Element("Equipos");
                    if (equiposElement != null)
                    {
                        foreach (var equipoElement in equiposElement.Elements("Equipo"))
                        {
                            string valor = equipoElement.Value;
                            if (!string.IsNullOrWhiteSpace(valor) && !ListaEquipos.Contains(valor))
                            {
                                ListaEquipos.Add(valor);
                            }
                        }
                    }
                }

                // Si las listas siguen vacías, cargar valores predeterminados
                if (ListaAreas.Count == 0)
                {
                    ListaAreas.AddRange(new[] { "N/A", "Administración", "Contabilidad", "Ventas", "Soporte" });
                }

                if (ListaEquipos.Count == 0)
                {
                    ListaEquipos.AddRange(new[] { "N/A", "CPU", "Monitor", "Impresora", "UPS", "Scanner", "Laptop" });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recargar catálogos: " + ex.Message);
            }
        }

        // ========== EVENTOS PARA ÁREAS ==========
        private void btnAgregarArea_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNuevaArea.Text))
            {
                MessageBox.Show("Por favor, ingresa el nombre del área.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nuevaArea = txtNuevaArea.Text.Trim();

            if (ListaAreas.Any(a => a.Equals(nuevaArea, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Esta área ya existe en la lista.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListaAreas.Add(nuevaArea);
            ActualizarListaAreas();
            GuardarDatosEnArchivo();
            txtNuevaArea.Clear();

            MessageBox.Show("Área agregada exitosamente.", "Éxito",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnModificarArea_Click(object sender, EventArgs e)
        {
            if (listBoxAreas.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecciona un área para modificar.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNuevaArea.Text))
            {
                MessageBox.Show("Por favor, ingresa el nuevo nombre del área.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string areaActual = listBoxAreas.SelectedItem.ToString();
            string nuevaArea = txtNuevaArea.Text.Trim();

            if (areaActual == "N/A")
            {
                MessageBox.Show("No se puede modificar el valor 'N/A'.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ListaAreas.Any(a => a.Equals(nuevaArea, StringComparison.OrdinalIgnoreCase)) &&
                !nuevaArea.Equals(areaActual, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Esta área ya existe en la lista.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = ListaAreas.FindIndex(a => a.Equals(areaActual, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                ListaAreas[index] = nuevaArea;
            }

            ActualizarListaAreas();
            GuardarDatosEnArchivo();
            txtNuevaArea.Clear();

            MessageBox.Show("Área modificada exitosamente.", "Éxito",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminarArea_Click(object sender, EventArgs e)
        {
            if (listBoxAreas.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecciona un área para eliminar.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string areaSeleccionada = listBoxAreas.SelectedItem.ToString();

            if (areaSeleccionada == "N/A")
            {
                MessageBox.Show("No se puede eliminar el valor 'N/A'.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show($"¿Estás seguro de eliminar el área '{areaSeleccionada}'?",
                                                    "Confirmar eliminación", MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                ListaAreas.RemoveAll(a => a.Equals(areaSeleccionada, StringComparison.OrdinalIgnoreCase));
                ActualizarListaAreas();
                GuardarDatosEnArchivo();

                MessageBox.Show("Área eliminada exitosamente.", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ========== EVENTOS PARA EQUIPOS ==========
        private void btnAgregarEquipo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNuevoEquipo.Text))
            {
                MessageBox.Show("Por favor, ingresa el nombre del equipo.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nuevoEquipo = txtNuevoEquipo.Text.Trim();

            if (ListaEquipos.Any(eq => eq.Equals(nuevoEquipo, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Este equipo ya existe en la lista.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListaEquipos.Add(nuevoEquipo);
            ActualizarListaEquipos();
            GuardarDatosEnArchivo();
            txtNuevoEquipo.Clear();

            MessageBox.Show("Equipo agregado exitosamente.", "Éxito",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnModificarEquipo_Click(object sender, EventArgs e)
        {
            if (listBoxEquipos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecciona un equipo para modificar.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNuevoEquipo.Text))
            {
                MessageBox.Show("Por favor, ingresa el nuevo nombre del equipo.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string equipoActual = listBoxEquipos.SelectedItem.ToString();
            string nuevoEquipo = txtNuevoEquipo.Text.Trim();

            if (equipoActual == "N/A")
            {
                MessageBox.Show("No se puede modificar el valor 'N/A'.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ListaEquipos.Any(eq => eq.Equals(nuevoEquipo, StringComparison.OrdinalIgnoreCase)) &&
                !nuevoEquipo.Equals(equipoActual, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Este equipo ya existe en la lista.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = ListaEquipos.FindIndex(eq => eq.Equals(equipoActual, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                ListaEquipos[index] = nuevoEquipo;
            }

            ActualizarListaEquipos();
            GuardarDatosEnArchivo();
            txtNuevoEquipo.Clear();

            MessageBox.Show("Equipo modificado exitosamente.", "Éxito",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminarEquipo_Click(object sender, EventArgs e)
        {
            if (listBoxEquipos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecciona un equipo para eliminar.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string equipoSeleccionado = listBoxEquipos.SelectedItem.ToString();

            if (equipoSeleccionado == "N/A")
            {
                MessageBox.Show("No se puede eliminar el valor 'N/A'.", "Advertencia",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show($"¿Estás seguro de eliminar el equipo '{equipoSeleccionado}'?",
                                                    "Confirmar eliminación", MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                ListaEquipos.RemoveAll(eq => eq.Equals(equipoSeleccionado, StringComparison.OrdinalIgnoreCase));
                ActualizarListaEquipos();
                GuardarDatosEnArchivo();

                MessageBox.Show("Equipo eliminado exitosamente.", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ========== EVENTOS DE LAS LISTAS ==========
        private void listBoxAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAreas.SelectedIndex >= 0)
            {
                txtNuevaArea.Text = listBoxAreas.SelectedItem.ToString();
            }
        }

        private void listBoxEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxEquipos.SelectedIndex >= 0)
            {
                txtNuevoEquipo.Text = listBoxEquipos.SelectedItem.ToString();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            GuardarDatosEnArchivo();
            base.OnFormClosing(e);
        }

        private void guna2CirclePictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GestionarCatalogos formCatalogos = new GestionarCatalogos();
            formCatalogos.FormClosed += (s, args) => this.Close();
            formCatalogos.Show();
        }

        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AgregarProducto formAgregar = new AgregarProducto();
            formAgregar.FormClosed += (s, args) => this.Close();
            formAgregar.Show();
        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistroMantenimiento formRegistro = new RegistroMantenimiento();
            formRegistro.FormClosed += (s, args) => this.Close();
            formRegistro.Show();
        }

        private void guna2CirclePictureBox3_Click_1(object sender, EventArgs e)
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
    }
}