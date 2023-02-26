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
    public partial class InventarioLista : Form
    {
        public InventarioLista()
        {
            InitializeComponent();
            mostrarDatos();
        }
        public void mostrarDatos()
        {
            dgvDatos.DataSource = new DAOInventario().cargarDatos();
            dgvDatos.Columns["areas_id"].Visible = false;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarInventario frm= new AgregarInventario();
            frm.ShowDialog();
            if (frm.Guardado)
            {
                mostrarDatos();
            }
        }

        private void Inventario_Load(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                int idInventario = int.Parse(dgvDatos.SelectedRows[0].Cells[0].Value.ToString());
                AgregarInventario frm = new AgregarInventario(idInventario);
                frm.ShowDialog();
                if (frm.Guardado)
                {
                    mostrarDatos();
                }
            }
            else
            {
                MessageBox.Show("No hay registros para atualizar", "Registros Vacios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("¿Está seguro de que desea eliminar?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    == DialogResult.Yes)
                {
                    int idInventario = int.Parse(dgvDatos.SelectedRows[0].Cells[0].Value.ToString());
                    bool aux = new DAOInventario().eliminar(idInventario);
                    if (aux)
                    {
                        MessageBox.Show("Se eliminó correctamente el registro", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mostrarDatos();
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay registros para atualizar", "Registros Vacios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
