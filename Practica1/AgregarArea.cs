using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1
{
    public partial class AgregarArea : Form
    {
        public bool Guardado { get; set; }
        public AgregarArea()
        {
            InitializeComponent();
        }
        int idArea=0;

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Trim()=="" || txtNombre.Text.Trim() == "")
            {
                MessageBox.Show("Hay campos OBLIGATORIOS vacíos (*)", "Campos necesarios vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    Area objArea = new Area();
                    objArea.id = Convert.ToInt32(txtID.Text);
                    objArea.nombre = txtNombre.Text.Trim();
                    objArea.ubicacion = txtUbicacion.Text.Trim();

                    bool resultado = false;
                    if (idArea == 0)
                    {
                        int agregado = new DAOArea().agregarArea(objArea);
                        resultado = (agregado > 0);
                    }

                    if (resultado)
                    {
                        Guardado = true;
                        MessageBox.Show("Se registró correctamente la área", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe una área con el mismo id en el sistema", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
