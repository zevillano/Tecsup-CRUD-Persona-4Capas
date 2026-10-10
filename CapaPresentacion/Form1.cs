using System;
using System.Drawing;
using System.Windows.Forms;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        private PersonaBLL objBLL = new PersonaBLL();
        private int idPersonaSeleccionada = 0;

        // Declaración de controles
        private Label lblDocumento, lblNombres, lblApellidos, lblBuscar;
        private TextBox txtDocumento, txtNombres, txtApellidos, txtBuscar;
        private Button btnGuardar, btnModificar, btnEliminar, btnLimpiar, btnBuscar;
        private DataGridView dgvPersonas;

        public Form1()
        {
            InitializeComponent();
            ConstruirInterfaz(); // Construye los controles por código
        }

        private void ConstruirInterfaz()
        {
            // Configuración de la Ventana
            this.Text = "Gestión de Personas - CRUD";
            this.Size = new Size(800, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Labels
            lblDocumento = new Label { Text = "Documento:", Location = new Point(20, 20), AutoSize = true };
            lblNombres = new Label { Text = "Nombres:", Location = new Point(20, 60), AutoSize = true };
            lblApellidos = new Label { Text = "Apellidos:", Location = new Point(20, 100), AutoSize = true };
            lblBuscar = new Label { Text = "Buscar:", Location = new Point(280, 20), AutoSize = true };

            // TextBoxes
            txtDocumento = new TextBox { Location = new Point(100, 17), Width = 150 };
            txtNombres = new TextBox { Location = new Point(100, 57), Width = 150 };
            txtApellidos = new TextBox { Location = new Point(100, 97), Width = 150 };
            txtBuscar = new TextBox { Location = new Point(340, 17), Width = 280 };

            // Botones
            btnGuardar = new Button { Text = "Guardar", Location = new Point(20, 140), Width = 110, BackColor = Color.LightGreen };
            btnModificar = new Button { Text = "Modificar", Location = new Point(140, 140), Width = 110 };
            btnEliminar = new Button { Text = "Eliminar", Location = new Point(20, 175), Width = 110, BackColor = Color.LightCoral };
            btnLimpiar = new Button { Text = "Limpiar", Location = new Point(140, 175), Width = 110 };
            btnBuscar = new Button { Text = "Buscar", Location = new Point(630, 15), Width = 120 };

            // DataGridView
            dgvPersonas = new DataGridView
            {
                Location = new Point(280, 55),
                Size = new Size(470, 360),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Agregar controles al formulario
            this.Controls.Add(lblDocumento);
            this.Controls.Add(lblNombres);
            this.Controls.Add(lblApellidos);
            this.Controls.Add(lblBuscar);
            this.Controls.Add(txtDocumento);
            this.Controls.Add(txtNombres);
            this.Controls.Add(txtApellidos);
            this.Controls.Add(txtBuscar);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnModificar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(btnLimpiar);
            this.Controls.Add(btnBuscar);
            this.Controls.Add(dgvPersonas);

            // Asignación de Eventos
            btnGuardar.Click += btnGuardar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnBuscar.Click += btnBuscar_Click;
            dgvPersonas.CellClick += dgvPersonas_CellClick;
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void CargarTabla()
        {
            dgvPersonas.DataSource = objBLL.Listar();
        }

        private void Limpiar()
        {
            idPersonaSeleccionada = 0;
            txtDocumento.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtBuscar.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Persona obj = new Persona
            {
                Documento = txtDocumento.Text.Trim(),
                Nombres = txtNombres.Text.Trim(),
                Apellidos = txtApellidos.Text.Trim()
            };

            string mensaje;
            if (objBLL.Registrar(obj, out mensaje))
            {
                MessageBox.Show("Persona registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarTabla();
                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Persona obj = new Persona
            {
                IdPersona = idPersonaSeleccionada,
                Documento = txtDocumento.Text.Trim(),
                Nombres = txtNombres.Text.Trim(),
                Apellidos = txtApellidos.Text.Trim()
            };

            string mensaje;
            if (objBLL.Modificar(obj, out mensaje))
            {
                MessageBox.Show("Persona modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarTabla();
                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idPersonaSeleccionada <= 0)
            {
                MessageBox.Show("Debe seleccionar una persona de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar lógicamente este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string mensaje;
                if (objBLL.Eliminar(idPersonaSeleccionada, out mensaje))
                {
                    MessageBox.Show("Persona eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarTabla();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvPersonas.DataSource = objBLL.Buscar(txtBuscar.Text.Trim());
        }

        private void dgvPersonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvPersonas.Rows[e.RowIndex];
                idPersonaSeleccionada = Convert.ToInt32(fila.Cells["IdPersona"].Value);
                txtDocumento.Text = fila.Cells["Documento"].Value?.ToString();
                txtNombres.Text = fila.Cells["Nombres"].Value?.ToString();
                txtApellidos.Text = fila.Cells["Apellidos"].Value?.ToString();
            }
        }
    }
}