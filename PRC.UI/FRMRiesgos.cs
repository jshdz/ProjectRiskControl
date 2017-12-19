using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRC.API.Core;
using PRC.Model;

namespace PRC.UI
{
    public partial class FRMRiesgos : MetroFramework.Forms.MetroForm
    {

        /// <summary>
        /// Variables globales utilizadas para mover el frame
        /// </summary>
        static bool drag;
        static int mousex;
        static int mousey;

        public FRMRiesgos()
        {
            InitializeComponent();
        }

        private void FRMRiesgos_Load(object sender, EventArgs e)
        {
            loadImpactosDefaults();
            loadProbabilidadesDefaults();
            reloadListImpactos();
            reloadListProbabilidades();
            listImpactos.ClearSelected();
            listProbabilidades.ClearSelected();
        }

        private void loadImpactosDefaults()
        {
            txtImpactoValor.Maximum = 4;
            txtImpactoValor.Minimum = 1;
        }

        private void loadProbabilidadesDefaults()
        {
            txtProbabilidadValor.Maximum = 4;
            txtProbabilidadValor.Minimum = 1;
            txtProbabilidadRangoInicio.Minimum = 1;
            txtProbabilidadRangoFin.Maximum = 99;
        }

        private void reloadListImpactos()
        {
            impactoBindingSource.DataSource = ImpactoServices.getAll();
        }

        private void reloadListProbabilidades()
        {
            probabilidadBindingSource.DataSource = ProbabilidadServices.getAll();
        }

        private void throwException(string pexMsg)
        {
            pnlExceptionMsg.Show();
            lblExceptionMsg.Text = pexMsg;

        }

        private void btnExceptionMsgClose_Click(object sender, EventArgs e)
        {
            pnlExceptionMsg.Hide();
            lblExceptionMsg.Text = "";
        }

        /// <summary>
        /// Metódo para mover el frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlFrameDrag_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            mousex = Cursor.Position.X - this.Left;
            mousey = Cursor.Position.Y - this.Top;
        }

        /// <summary>
        /// Metódo para mover el frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlFrameDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag) {
                this.Top = Cursor.Position.Y - mousey;
                this.Left = Cursor.Position.X - mousex;
            }
        }

        /// <summary>
        /// Metódo para mover el frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlFrameDrag_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnImpactoAgregar_Click(object sender, EventArgs e)
        {
            Impacto impactoNuevo = new Impacto {
                categoria = txtImpactoCategoria.Text,
                descripcion = txtImpactoDesc.Text,
                valor = Convert.ToInt32(txtImpactoValor.Value)
            };

            if (ImpactoServices.validarImpacto(impactoNuevo)) {
                ImpactoServices.insert(impactoNuevo);
                reloadListImpactos();
                clearFieldsImpacto();
            } else {
                throwException("Este impacto no puede ser agregado");
            }
        }

        private void btnImpactoEditar_Click(object sender, EventArgs e)
        {
            //Impacto impactoEditado = new Impacto {
            //    categoria = txtImpactoCategoria.Text,
            //    descripcion = txtImpactoDesc.Text,
            //    valor = Convert.ToInt32(txtImpactoValor.Value)
            //};
            Impacto impactoEditado = ImpactoServices.getByCategoria(txtImpactoCategoria.Text);

            impactoEditado.descripcion = txtImpactoDesc.Text;

            try {
                ImpactoServices.update(impactoBindingSourceForm.Current as Impacto);
            } catch (Exception ex) {
                throwException("Este impacto no puede ser modificado");
            }
        }

        private void btnEliminarImpacto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar este impacto?", "Eliminar Impacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                ImpactoServices.delete(impactoBindingSource.Current as Impacto);
                impactoBindingSource.RemoveCurrent();
            }
        }

        private void btnImpactoCancelar_Click(object sender, EventArgs e)
        {
            clearFieldsImpacto();
        }

        private void btnProbabilidadAgregar_Click(object sender, EventArgs e)
        {
            Probabilidad probabilidadNueva = new Probabilidad {
                categoria = txtProbabilidadCategoria.Text,
                descripcion = txtProbabilidadDesc.Text,
                rangoInicio = Convert.ToInt32(txtProbabilidadRangoInicio.Value),
                rangoFin = Convert.ToInt32(txtProbabilidadRangoFin.Value),
                valor = Convert.ToInt32(txtProbabilidadValor.Value)
            };

            if (ProbabilidadServices.validarProbabilidad(probabilidadNueva)) {
                ProbabilidadServices.insert(probabilidadNueva);
                reloadListProbabilidades();
                clearFieldsProbabilidad();
            } else {
                throwException("Está probababilidad no puede ser agregada");
            }
        }

        private void btnEliminarProbabilidad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar está probabilidad?", "Eliminar Probabilidad", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                ProbabilidadServices.delete(probabilidadBindingSource.Current as Probabilidad);
                probabilidadBindingSource.RemoveCurrent();
            }
        }

        private void btnProbabilidadCancelar_Click(object sender, EventArgs e)
        {
            clearFieldsProbabilidad();
        }

        private void clearFieldsImpacto()
        {
            txtImpactoCategoria.Clear();
            txtImpactoDesc.Clear();
            txtImpactoValor.Value = 1;
        }

        private void clearFieldsProbabilidad()
        {
            txtProbabilidadCategoria.Clear();
            txtProbabilidadDesc.Clear();
            txtProbabilidadRangoInicio.Value = 1;
            txtProbabilidadRangoFin.Value = 99;
            txtProbabilidadValor.Value = 1;
        }

        //private void listImpactos_MouseClick(object sender, MouseEventArgs e)
        //{
        //    //impactoBindingSourceForm.DataSource = ImpactoServices.getByCategoria(listImpactos.GetItemText(listImpactos.SelectedItem));
        //}
    }
}
