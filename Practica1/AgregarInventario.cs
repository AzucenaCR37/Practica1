using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1
{
    public partial class AgregarInventario : Form
    {
        public bool Guardado { get; set; }
        public AgregarInventario()
        {
            InitializeComponent();
            mostrarAreasCombo();
        }

        //si se presionó el botón agregar se queda en 0, si no obtiene el valor del id a editar
        int id;

        public AgregarInventario(int id) : this()
        {
            //Cargar el usuario de la BD
            try
            {
                this.id = id;
                Inventario objInventario = new DAOInventario().CargarUno(id);
                txtID.Text = objInventario.id.ToString();
                txtNombre.Text = objInventario.nombreCorto.ToString();
                txtDescripcion.Text = objInventario.descripcion.ToString();
                txtSerie.Text = objInventario.serie.ToString();
                txtColor.Text = objInventario.color.ToString();
                dtpFecha.Text = objInventario.fechaAdquision.ToString();
                cboTipoAdquision.SelectedItem = objInventario.tipoAdquision.ToString();
                txtObservaciones.Text = objInventario.observaciones.ToString();
                cboArea.SelectedItem = objInventario.nombreArea.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        public void mostrarAreasCombo()
        {
            List<Area> lista = new DAOArea().cargarDatos();
            cboArea.DataSource = lista;
            cboArea.DisplayMember = "nombre";
            cboArea.ValueMember = "id";
        }

        private void btnAgregarArea_Click(object sender, EventArgs e)
        {
            AgregarArea frm = new AgregarArea();
            frm.ShowDialog();
            if (frm.Guardado)
            {
                mostrarAreasCombo();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtID.Text == "" || cboArea.Text=="")
            {
                MessageBox.Show("Hay campos OBLIGATORIOS vacíos (*)", "Campos necesarios vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    Inventario objinventario = new Inventario();
                    objinventario.id = Convert.ToInt32(txtID.Text.Trim());
                    objinventario.nombreCorto= txtNombre.Text.Trim();
                    objinventario.descripcion = txtDescripcion.Text.Trim();
                    objinventario.serie = txtSerie.Text.Trim();
                    objinventario.color = txtColor.Text.Trim();
                    objinventario.fechaAdquision = Convert.ToDateTime(dtpFecha.Text).Date;
                    objinventario.tipoAdquision = cboTipoAdquision.SelectedItem.ToString();
                    objinventario.observaciones = txtObservaciones.Text.Trim();
                    objinventario.areas_id = Convert.ToInt32(cboArea.SelectedValue);

                    bool resultado = false;
                    if (id == 0)
                    {
                        int agregado = new DAOInventario().agregarInventario(objinventario);
                        resultado = (agregado > 0);

                    }
                    else
                    {
                        objinventario.id = id;
                        resultado = new DAOInventario().editar(objinventario);
                    }

                    if (resultado)
                    {
                        Guardado = true;
                        MessageBox.Show("Se registró correctamente el inventario", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un producto con el mismo id en el sistema", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
