using Datos;
using DocumentFormat.OpenXml.Presentation;
using Guna.UI2.WinForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Image = System.Drawing.Image;

namespace Mantenimiento
{
    public partial class RegistroMantenimiento : MaterialSkin.Controls.MaterialForm
    {
        Procedimientos pr = new Procedimientos();
        private DataTable datosOriginales;
        private Image iconoVerdePequeno;
        private Image iconoRojoPequeno;
        private const string PLACEHOLDER_BUSCADOR = "Buscar por cualquier campo...";

        public RegistroMantenimiento()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.KeyPreview = true;
            this.AutoScroll = true;

            // AGREGAR: Suscribirse al evento CellValueChanged para sincronización en tiempo real
            guna2DataGridView1.CellValueChanged += guna2DataGridView1_CellValueChanged;
        }

        private void RegistroMantenimiento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void RegistroMantenimiento_Load(object sender, EventArgs e)
        {
            MostrarMantenimiento();
            ConfigurarBuscador();

            // Configurar el evento para que siempre oculte el ID cuando se cargen datos
            guna2DataGridView1.DataBindingComplete += (s, ev) => ConfigurarDataGridView();
        }

        // Método para configurar el DataGridView y ocultar la columna ID
        private void ConfigurarDataGridView()
        {
            try
            {
                // Ocultar la columna ID si existe
                if (guna2DataGridView1.Columns.Contains("Id"))
                {
                    guna2DataGridView1.Columns["Id"].Visible = false;
                }

                // DESHABILITAR ORDENAMIENTO POR COLUMNAS
                foreach (DataGridViewColumn column in guna2DataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // Personalizar headers
                if (guna2DataGridView1.Columns.Contains("FECHA_DE_ENTRADA"))
                    guna2DataGridView1.Columns["FECHA_DE_ENTRADA"].HeaderText = "Fecha Entrada";

                if (guna2DataGridView1.Columns.Contains("Equipo"))
                    guna2DataGridView1.Columns["Equipo"].HeaderText = "Equipo";

                if (guna2DataGridView1.Columns.Contains("AREA"))
                    guna2DataGridView1.Columns["AREA"].HeaderText = "Área";

                if (guna2DataGridView1.Columns.Contains("MODELO_DE_EQUIPO"))
                    guna2DataGridView1.Columns["MODELO_DE_EQUIPO"].HeaderText = "Modelo";

                if (guna2DataGridView1.Columns.Contains("INVENTARIO"))
                    guna2DataGridView1.Columns["INVENTARIO"].HeaderText = "Inventario";

                if (guna2DataGridView1.Columns.Contains("DIAGNOSTICO"))
                    guna2DataGridView1.Columns["DIAGNOSTICO"].HeaderText = "Diagnóstico";

                if (guna2DataGridView1.Columns.Contains("FECHA_DE_SALIDA"))
                    guna2DataGridView1.Columns["FECHA_DE_SALIDA"].HeaderText = "Fecha Salida";

                if (guna2DataGridView1.Columns.Contains("Service_tag"))
                    guna2DataGridView1.Columns["Service_tag"].HeaderText = "Service Tag";

                if (guna2DataGridView1.Columns.Contains("SOPORTE"))
                    guna2DataGridView1.Columns["SOPORTE"].HeaderText = "Soporte";

                if (guna2DataGridView1.Columns.Contains("NOTA"))
                    guna2DataGridView1.Columns["NOTA"].HeaderText = "Nota";

                // CONFIGURACIÓN DE ALTURA PARA ICONOS PEQUEÑOS
                guna2DataGridView1.RowTemplate.Height = 35; // Altura más pequeña
                guna2DataGridView1.ScrollBars = ScrollBars.Both;
                guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                guna2DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                guna2DataGridView1.AllowUserToAddRows = false;
                guna2DataGridView1.ReadOnly = true;
                guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // AJUSTAR ALTURA DE FILAS EXISTENTES
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                        row.Height = 35;
                }

                // DESHABILITAR ORDENAMIENTO TAMBIÉN A NIVEL DE CONTROL
                guna2DataGridView1.AllowUserToOrderColumns = false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al configurar DataGridView: " + ex.Message);
            }
        }
        // Método principal para mostrar datos - MEJORADO
        public void MostrarMantenimiento()
        {
            try
            {
                Tramite tramite = new Tramite();
                datosOriginales = tramite.mostrar();

                if (datosOriginales == null)
                    datosOriginales = new DataTable();

                // Limpiar el DataSource antes de asignar uno nuevo
                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.DataSource = datosOriginales;

                // CONFIGURAR PRIMERO EL TAMAÑO DE COLUMNAS
                guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Configurar ANTES de agregar columna imagen
                ConfigurarDataGridView();

                // Agregar columna imagen pequeña
                AgregarColumnaImagen();

                // Asignar iconos
                AsignarIconosConRecursos();

                // Forzar actualización visual
                guna2DataGridView1.Refresh();
                guna2DataGridView1.Invalidate();

                Console.WriteLine($"Datos actualizados: {datosOriginales.Rows.Count} registros");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar datos: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarColumnaImagen()
        {
            // Eliminar columna si ya existe
            if (guna2DataGridView1.Columns.Contains("Imagen"))
            {
                guna2DataGridView1.Columns.Remove("Imagen");
            }

            // Crear nueva columna imagen con tamaño pequeño
            DataGridViewImageColumn columnaImagen = new DataGridViewImageColumn();
            columnaImagen.Name = "Imagen";
            columnaImagen.HeaderText = "Estado";
            columnaImagen.ImageLayout = DataGridViewImageCellLayout.Zoom;

            // TAMAÑO PEQUEÑO Y FIJO
            columnaImagen.Width = 40;  // Más pequeño
            columnaImagen.MinimumWidth = 40;
            columnaImagen.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            columnaImagen.Resizable = DataGridViewTriState.False;
            columnaImagen.Frozen = false;

            // Insertar al inicio
            guna2DataGridView1.Columns.Insert(0, columnaImagen);
        }

        // Método mejorado para asignar iconos con mejor manejo de actualizaciones
        private void AsignarIconosConRecursos()
        {
            try
            {
                // Verificar que las imágenes existan
                if (Properties.Resources.icono_verde2 == null || Properties.Resources.icono_rojo == null)
                {
                    MessageBox.Show("No se encontraron los iconos en Resources", "Error");
                    return;
                }

                // Crear versiones pequeñas de las imágenes UNA SOLA VEZ
                Image iconoVerdePequeno = RedimensionarImagen(Properties.Resources.icono_verde2, 24, 24);
                Image iconoRojoPequeno = RedimensionarImagen(Properties.Resources.icono_rojo, 24, 24);

                // Buscar columna de fecha con debug mejorado
                string nombreColumnaFecha = null;
                if (guna2DataGridView1.Columns.Contains("FECHA_DE_SALIDA"))
                    nombreColumnaFecha = "FECHA_DE_SALIDA";
                else if (guna2DataGridView1.Columns.Contains("FechaSalida"))
                    nombreColumnaFecha = "FechaSalida";

                if (string.IsNullOrEmpty(nombreColumnaFecha))
                {
                    Console.WriteLine("No se encontró columna de fecha de salida");
                    return;
                }

                // Verificar que existe la columna Imagen
                if (!guna2DataGridView1.Columns.Contains("Imagen"))
                {
                    Console.WriteLine("No se encontró columna Imagen");
                    return;
                }

                int totalFilas = 0;
                int iconosVerdes = 0;
                int iconosRojos = 0;

                Console.WriteLine($"=== INICIANDO ASIGNACIÓN DE ICONOS ===");
                Console.WriteLine($"Columna fecha: {nombreColumnaFecha}");
                Console.WriteLine($"Total filas en grid: {guna2DataGridView1.Rows.Count}");

                foreach (DataGridViewRow fila in guna2DataGridView1.Rows)
                {
                    if (fila.IsNewRow) continue;
                    totalFilas++;

                    try
                    {
                        var celdaFecha = fila.Cells[nombreColumnaFecha];
                        var valorFecha = celdaFecha?.Value;

                        // LÓGICA CORREGIDA: 
                        // Verde = Tiene fecha de salida válida
                        // Rojo = NO tiene fecha de salida o es inválida

                        bool tieneFechaSalida = false;

                        if (valorFecha != null && valorFecha != DBNull.Value)
                        {
                            DateTime fechaSalida;

                            // Intentos múltiples de parseo de fecha
                            if (valorFecha is DateTime)
                            {
                                fechaSalida = (DateTime)valorFecha;
                                // Verificar que no sea fecha mínima (fecha por defecto)
                                if (fechaSalida != DateTime.MinValue && fechaSalida.Year > 1900)
                                {
                                    tieneFechaSalida = true;
                                }
                            }
                            else if (DateTime.TryParse(valorFecha.ToString(), out fechaSalida))
                            {
                                // Verificar que no sea fecha mínima (fecha por defecto)
                                if (fechaSalida != DateTime.MinValue && fechaSalida.Year > 1900)
                                {
                                    tieneFechaSalida = true;
                                }
                            }
                        }

                        // Asignar icono basándose en la presencia de fecha válida
                        if (tieneFechaSalida)
                        {
                            // COMPLETADO - Tiene fecha de salida - Icono Verde
                            fila.Cells["Imagen"].Value = new Bitmap(iconoVerdePequeno);
                            iconosVerdes++;
                            Console.WriteLine($"Fila {fila.Index}: VERDE (tiene fecha de salida) - Fecha: {valorFecha}");
                        }
                        else
                        {
                            // PENDIENTE - Sin fecha de salida válida - Icono Rojo  
                            fila.Cells["Imagen"].Value = new Bitmap(iconoRojoPequeno);
                            iconosRojos++;
                            Console.WriteLine($"Fila {fila.Index}: ROJO (sin fecha de salida) - Valor: {valorFecha ?? "NULL"}");
                        }
                    }
                    catch (Exception exFila)
                    {
                        Console.WriteLine($"Error en fila {fila.Index}: {exFila.Message}");
                        if (fila.Cells["Imagen"] != null)
                        {
                            fila.Cells["Imagen"].Value = new Bitmap(iconoRojoPequeno);
                            iconosRojos++;
                        }
                    }
                }

                // Información de debug
                Console.WriteLine($"=== RESUMEN FINAL ===");
                Console.WriteLine($"Total filas procesadas: {totalFilas}");
                Console.WriteLine($"Iconos verdes (con fecha de salida): {iconosVerdes}");
                Console.WriteLine($"Iconos rojos (sin fecha de salida): {iconosRojos}");

                // Forzar actualización visual del DataGridView
                guna2DataGridView1.Refresh();
                guna2DataGridView1.Invalidate();

                // Limpiar imágenes temporales
                iconoVerdePequeno?.Dispose();
                iconoRojoPequeno?.Dispose();

                Console.WriteLine("Iconos asignados y grid actualizado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al asignar iconos: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error en AsignarIconosConRecursos: {ex.Message}");
            }
        }
        private Image RedimensionarImagen(Image imagenOriginal, int ancho, int alto)
        {
            try
            {
                Bitmap imagenRedimensionada = new Bitmap(ancho, alto);
                using (Graphics g = Graphics.FromImage(imagenRedimensionada))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawImage(imagenOriginal, 0, 0, ancho, alto);
                }
                return imagenRedimensionada;
            }
            catch
            {
                return imagenOriginal; // Devolver original si hay error
            }
        }

        // Método mejorado para refrescar después de modificar
        private void RefrescarDatosCompleto()
        {
            try
            {
                Console.WriteLine("Iniciando refresco completo de datos...");

                // Simplemente recargar datos sin intentar mantener selección
                MostrarMantenimiento(); // Este método ya maneja la configuración completa

                // Limpiar búsqueda si existe
                if (txtBuscador != null)
                {
                    txtBuscador.Text = string.Empty;
                }

                Console.WriteLine("Datos refrescados completamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al refrescar: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error en RefrescarDatosCompleto: {ex.Message}");
            }
        }
        // Método adicional para refrescar cuando se actualiza un registro específico
        public void RefrescarDespuesDeModificar(int idModificado)
        {
            try
            {
                Console.WriteLine($"Refrescando después de modificar registro ID: {idModificado}");

                // Recargar datos desde la base de datos
                Tramite tramite = new Tramite();
                datosOriginales = tramite.mostrar();

                if (datosOriginales == null)
                {
                    datosOriginales = new DataTable();
                    return;
                }

                // Limpiar DataSource y reasignar
                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.DataSource = datosOriginales;

                // Reconfigurar todo
                ConfigurarDataGridView();
                AgregarColumnaImagen();
                AsignarIconosConRecursos();

                // CORRECCIÓN: Intentar mantener selección de manera más segura
                try
                {
                    // Buscar fila por ID de manera más robusta
                    DataRow filaEncontrada = null;
                    int indiceFilaEncontrada = -1;

                    for (int i = 0; i < datosOriginales.Rows.Count; i++)
                    {
                        if (datosOriginales.Columns.Contains("Id"))
                        {
                            var idValue = datosOriginales.Rows[i]["Id"];
                            if (idValue != null && int.TryParse(idValue.ToString(), out int rowId) && rowId == idModificado)
                            {
                                filaEncontrada = datosOriginales.Rows[i];
                                indiceFilaEncontrada = i;
                                break;
                            }
                        }
                    }

                    // Establecer selección de manera segura
                    if (indiceFilaEncontrada >= 0 && indiceFilaEncontrada < guna2DataGridView1.Rows.Count)
                    {
                        // Buscar la primera columna visible para establecer CurrentCell
                        DataGridViewColumn primeraColumnaVisible = null;
                        foreach (DataGridViewColumn col in guna2DataGridView1.Columns)
                        {
                            if (col.Visible)
                            {
                                primeraColumnaVisible = col;
                                break;
                            }
                        }

                        if (primeraColumnaVisible != null)
                        {
                            var fila = guna2DataGridView1.Rows[indiceFilaEncontrada];
                            if (!fila.IsNewRow)
                            {
                                // Establecer CurrentCell de manera segura
                                guna2DataGridView1.CurrentCell = fila.Cells[primeraColumnaVisible.Index];
                                fila.Selected = true;

                                // Hacer scroll hacia la fila si es necesario
                                guna2DataGridView1.FirstDisplayedScrollingRowIndex = Math.Max(0, indiceFilaEncontrada - 5);
                            }
                        }
                    }
                    else if (guna2DataGridView1.Rows.Count > 0)
                    {
                        // Si no se encuentra la fila, seleccionar la primera
                        var primeraFila = guna2DataGridView1.Rows[0];
                        if (!primeraFila.IsNewRow)
                        {
                            DataGridViewColumn primeraColumnaVisible = guna2DataGridView1.Columns
                                .Cast<DataGridViewColumn>()
                                .FirstOrDefault(col => col.Visible);

                            if (primeraColumnaVisible != null)
                            {
                                guna2DataGridView1.CurrentCell = primeraFila.Cells[primeraColumnaVisible.Index];
                                primeraFila.Selected = true;
                            }
                        }
                    }
                }
                catch (Exception exSeleccion)
                {
                    Console.WriteLine($"Error al establecer selección: {exSeleccion.Message}");
                    // Si hay error con la selección, al menos asegurar que no cause crash
                }

                Console.WriteLine("Datos refrescados correctamente después de modificar");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al refrescar después de modificar: {ex.Message}", "Error");
                Console.WriteLine($"Error RefrescarDespuesDeModificar: {ex.Message}");
            }
        }
        private bool TieneFechaSalidaValida(object valorFecha)
        {
            if (valorFecha == null || valorFecha == DBNull.Value)
                return false;

            DateTime fechaSalida;

            // Si ya es DateTime
            if (valorFecha is DateTime)
            {
                fechaSalida = (DateTime)valorFecha;
                return fechaSalida != DateTime.MinValue && fechaSalida.Year > 1900;
            }

            // Intentar parsear string
            if (DateTime.TryParse(valorFecha.ToString(), out fechaSalida))
            {
                return fechaSalida != DateTime.MinValue && fechaSalida.Year > 1900;
            }

            return false;
        }
        private void ActualizarIconoEnFila(int indiceFila)
        {
            try
            {
                if (indiceFila < 0 || indiceFila >= guna2DataGridView1.Rows.Count)
                    return;

                var fila = guna2DataGridView1.Rows[indiceFila];
                if (fila.IsNewRow) return;

                // Buscar columna de fecha
                string nombreColumnaFecha = guna2DataGridView1.Columns.Contains("FECHA_DE_SALIDA")
                    ? "FECHA_DE_SALIDA" : "FechaSalida";

                if (string.IsNullOrEmpty(nombreColumnaFecha) || !guna2DataGridView1.Columns.Contains(nombreColumnaFecha))
                    return;

                var valorFecha = fila.Cells[nombreColumnaFecha].Value;
                bool tieneFechaSalida = TieneFechaSalidaValida(valorFecha);

                // Crear imágenes pequeñas
                Image icono = tieneFechaSalida
                    ? RedimensionarImagen(Properties.Resources.icono_verde2, 24, 24)
                    : RedimensionarImagen(Properties.Resources.icono_rojo, 24, 24);

                if (guna2DataGridView1.Columns.Contains("Imagen"))
                {
                    fila.Cells["Imagen"].Value = new Bitmap(icono);
                }

                icono?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar icono en fila {indiceFila}: {ex.Message}");
            }
        }
        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Solo procesar si es la columna de fecha de salida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var nombreColumna = guna2DataGridView1.Columns[e.ColumnIndex].Name;
                if (nombreColumna == "FECHA_DE_SALIDA" || nombreColumna == "FechaSalida")
                {
                    ActualizarIconoEnFila(e.RowIndex);
                }
            }
        }

        private void txtBuscador_TextChanged_1(object sender, EventArgs e)
        {
            string texto = string.Empty;
            bool esPlaceholder = false;

            if (sender is Guna2TextBox g) // En Guna2, cuando hay placeholder, Text queda vacío
            {
                texto = (g.Text ?? string.Empty).Trim();
                esPlaceholder = string.IsNullOrEmpty(texto);
            }
            else if (sender is TextBox tb) // En TextBox normal usamos tu esquema de placeholder gris
            {
                texto = (tb.Text ?? string.Empty).Trim();
                esPlaceholder = (tb.ForeColor == Color.Gray && tb.Text == PLACEHOLDER_BUSCADOR);
            }

            if (string.IsNullOrWhiteSpace(texto) || esPlaceholder)
            {
                if (datosOriginales != null)
                {
                    guna2DataGridView1.DataSource = datosOriginales;
                    ConfigurarDataGridView();
                    AgregarColumnaImagen();
                    AsignarIconosConRecursos();
                }
                return;
            }

            FiltrarDatos(texto);
        }

        private void ConfigurarBuscador()
        {
            var ctrl = this.Controls.Find("txtBuscador", true).FirstOrDefault();
            if (ctrl == null) return;

            if (ctrl is Guna2TextBox guna)
            {
                guna.TextChanged += txtBuscador_TextChanged_1;
                if (string.IsNullOrEmpty(guna.PlaceholderText))
                    guna.PlaceholderText = PLACEHOLDER_BUSCADOR;
            }
            else if (ctrl is TextBox tb)
            {
                tb.TextChanged += txtBuscador_TextChanged_1;
                ConfigurarPlaceholder(tb, PLACEHOLDER_BUSCADOR);
            }
        }

        private void ConfigurarPlaceholder(TextBox textBox, string placeholderText)
        {
            textBox.Text = placeholderText;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        private void FiltrarDatos(string texto)
        {
            if (datosOriginales == null) return;

            DataTable datosFiltrados = datosOriginales.Clone();

            foreach (DataRow row in datosOriginales.Rows)
            {
                foreach (DataColumn column in datosOriginales.Columns)
                {
                    if (row[column].ToString().IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        datosFiltrados.ImportRow(row);
                        break;
                    }
                }
            }

            guna2DataGridView1.DataSource = datosFiltrados;

            // Siempre volver a configurar y asignar iconos
            ConfigurarDataGridView();
            AgregarColumnaImagen();
            AsignarIconosConRecursos();
        }

        private void LimpiarBusqueda()
        {
            txtBuscador.Text = string.Empty;

            if (datosOriginales != null)
            {
                guna2DataGridView1.DataSource = datosOriginales;
                ConfigurarDataGridView();
                AgregarColumnaImagen();       // Reagregar columna Imagen
                AsignarIconosConRecursos();   // Reasignar iconos
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            LimpiarBusqueda();
        }

        private void RefrescarDatos()
        {
            try
            {
                Procedimientos proc = new Procedimientos();
                datosOriginales = proc.Mostrar();
                if (datosOriginales == null)
                    datosOriginales = new DataTable();

                guna2DataGridView1.DataSource = datosOriginales;
                ConfigurarDataGridView();
                AgregarColumnaImagen();
                AsignarIconosConRecursos();

                LimpiarBusqueda();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al refrescar los datos: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // EVENTOS DE BOTONES Y CONTROLES
        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Dock = DockStyle.Fill;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                AgregarProducto formAgregar = new AgregarProducto();

                DialogResult resultado = formAgregar.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    RefrescarDatos();
                }

                formAgregar.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botón MODIFICAR actualizado
        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (guna2DataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, selecciona un registro para modificar.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                T GetCellValue<T>(string columnName, T defaultValue = default(T))
                {
                    try
                    {
                        var cellValue = guna2DataGridView1.CurrentRow.Cells[columnName] == null
                            ? null
                            : guna2DataGridView1.CurrentRow.Cells[columnName].Value;

                        if (cellValue == null || cellValue == DBNull.Value)
                            return defaultValue;

                        if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
                        {
                            DateTime dateResult;
                            if (DateTime.TryParse(cellValue.ToString(), out dateResult))
                                return (T)(object)dateResult;
                            else
                                return defaultValue;
                        }

                        return (T)Convert.ChangeType(cellValue, typeof(T));
                    }
                    catch
                    {
                        return defaultValue;
                    }
                }

                // Obtener el ID aunque no esté visible
                int id = GetIdDelRegistroSeleccionado();
                DateTime fechaEntrada = GetCellValue<DateTime>("FECHA_DE_ENTRADA", DateTime.Now);
                string equipo = GetCellValue<string>("Equipo", "");
                string area = GetCellValue<string>("AREA", "");
                string modelo = GetCellValue<string>("MODELO_DE_EQUIPO", "");
                string inventario = GetCellValue<string>("INVENTARIO", "");
                string diagnostico = GetCellValue<string>("DIAGNOSTICO", "");
                DateTime fechaSalida = GetCellValue<DateTime>("FECHA_DE_SALIDA", DateTime.Now);
                string serviceTag = GetCellValue<string>("Service_tag", "");
                string soporte = GetCellValue<string>("SOPORTE", "");
                string nota = GetCellValue<string>("NOTA", "");

                if (id <= 0)
                {
                    MessageBox.Show("No se pudo obtener un ID válido del registro seleccionado.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Modificar formModificar = new Modificar(
                    id, fechaEntrada, equipo, area, modelo,
                    inventario, diagnostico, fechaSalida,
                    serviceTag, soporte, nota
                );

                DialogResult resultado = formModificar.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    // Usar el nuevo método de refresco completo
                    RefrescarDatosCompleto();

                    MessageBox.Show("Registro modificado y actualizado correctamente.",
                                   "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                formModificar.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de modificación: " + ex.Message + "\n\nDetalles: " + ex.StackTrace,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método mejorado para obtener el ID del registro seleccionado
        private int GetIdDelRegistroSeleccionado()
        {
            if (guna2DataGridView1.CurrentRow == null) return 0;

            // Primero intentar obtener el ID de los datos originales usando el índice de la fila
            try
            {
                int rowIndex = guna2DataGridView1.CurrentRow.Index;

                // Si estamos mostrando datos filtrados, necesitamos encontrar la fila correspondiente en los datos originales
                DataTable tablaActual = guna2DataGridView1.DataSource as DataTable;
                if (tablaActual != null && tablaActual != datosOriginales)
                {
                    // Buscar en datos originales por coincidencia de varios campos
                    DataRow filaSeleccionada = tablaActual.Rows[rowIndex];

                    foreach (DataRow filaOriginal in datosOriginales.Rows)
                    {
                        // Comparar por varios campos para encontrar la fila correcta
                        bool coincide = true;
                        string[] camposComparar = { "FECHA_DE_ENTRADA", "Equipo", "AREA", "MODELO_DE_EQUIPO", "INVENTARIO" };

                        foreach (string campo in camposComparar)
                        {
                            if (tablaActual.Columns.Contains(campo) && datosOriginales.Columns.Contains(campo))
                            {
                                var valorFiltrada = filaSeleccionada[campo];
                                var valorOriginal = filaOriginal[campo];

                                if (!object.Equals(valorFiltrada, valorOriginal))
                                {
                                    coincide = false;
                                    break;
                                }
                            }
                        }

                        if (coincide && datosOriginales.Columns.Contains("Id"))
                        {
                            var idValue = filaOriginal["Id"];
                            if (idValue != null && int.TryParse(idValue.ToString(), out int id))
                                return id;
                        }
                    }
                }
                else
                {
                    // Trabajar directamente con los datos originales
                    if (rowIndex < datosOriginales.Rows.Count && datosOriginales.Columns.Contains("Id"))
                    {
                        var idValue = datosOriginales.Rows[rowIndex]["Id"];
                        if (idValue != null && int.TryParse(idValue.ToString(), out int id))
                            return id;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener ID: " + ex.Message);
            }

            // Método alternativo: buscar en la fila actual aunque la columna esté oculta
            string[] posiblesNombresId = { "Id", "ID", "id", "Id_Mantenimiento", "ID_MANTENIMIENTO" };

            foreach (var nombreCol in posiblesNombresId)
            {
                if (guna2DataGridView1.Columns.Contains(nombreCol))
                {
                    var cellValue = guna2DataGridView1.CurrentRow.Cells[nombreCol].Value;
                    if (cellValue != null && int.TryParse(cellValue.ToString(), out int idTmp))
                        return idTmp;
                }
            }

            return 0;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2DataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona un registro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int filaActual = guna2DataGridView1.CurrentRow.Index; // Guardar fila actual
                int id = GetIdDelRegistroSeleccionado();
                if (id <= 0)
                {
                    MessageBox.Show("No se pudo determinar el ID del registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var confirmar = MessageBox.Show($"¿Seguro que deseas eliminar el registro?",
                                                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmar != DialogResult.Yes) return;

                var tramite = new Tramite();
                tramite.EliminarData(id);

                RefrescarDatos();

                // Ajustar selección después de refrescar
                if (guna2DataGridView1.Rows.Count > 0)
                {
                    int nuevaFila = Math.Min(filaActual, guna2DataGridView1.Rows.Count - 1);
                    guna2DataGridView1.CurrentCell = guna2DataGridView1.Rows[nuevaFila].Cells[0];
                    guna2DataGridView1.Rows[nuevaFila].Selected = true;
                }

                MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "PDF (*.pdf)|*.pdf";
                saveFile.FileName = "Reporte.pdf";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
                    PdfWriter.GetInstance(doc, new FileStream(saveFile.FileName, FileMode.Create));
                    doc.Open();

                    Paragraph titulo = new Paragraph("Reporte de Datos", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(titulo);
                    doc.Add(new Paragraph("\n"));

                    // Contar solo las columnas visibles para el PDF
                    int columnasVisibles = 0;
                    foreach (DataGridViewColumn col in guna2DataGridView1.Columns)
                    {
                        if (col.Visible)
                            columnasVisibles++;
                    }

                    PdfPTable tabla = new PdfPTable(columnasVisibles);

                    // Agregar headers solo de columnas visibles
                    foreach (DataGridViewColumn col in guna2DataGridView1.Columns)
                    {
                        if (col.Visible)
                        {
                            tabla.AddCell(new Phrase(col.HeaderText));
                        }
                    }

                    // Agregar datos solo de columnas visibles
                    foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (guna2DataGridView1.Columns[cell.ColumnIndex].Visible)
                                {
                                    tabla.AddCell(new Phrase(cell.Value?.ToString()));
                                }
                            }
                        }
                    }

                    doc.Add(tabla);
                    doc.Close();

                    MessageBox.Show("Exportado a PDF correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a PDF: " + ex.Message);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Workbooks.Add(Type.Missing);

                int columna = 0;
                // Solo exportar columnas visibles
                foreach (DataGridViewColumn col in guna2DataGridView1.Columns)
                {
                    if (col.Visible)
                    {
                        columna++;
                        excelApp.Cells[1, columna] = col.HeaderText;
                    }
                }

                int fila = 0;
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        fila++;
                        columna = 0;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (guna2DataGridView1.Columns[cell.ColumnIndex].Visible)
                            {
                                columna++;
                                excelApp.Cells[fila + 1, columna] = cell.Value?.ToString();
                            }
                        }
                    }
                }

                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a Excel: " + ex.Message);
            }
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

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            GestionarCatalogos formCatalogos = new GestionarCatalogos();
            formCatalogos.FormClosed += (s, args) => this.Close();
            formCatalogos.Show();
        }
    }
}