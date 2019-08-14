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
    public partial class FRMEarnedValue : MetroFramework.Forms.MetroForm
    {

        /// <summary>
        /// Variables globales utilizadas para mover el frame
        /// </summary>
        static bool drag;
        static int mousex;
        static int mousey;

        public FRMEarnedValue()
        {
            InitializeComponent();
        }

        private void FRMRiesgos_Load(object sender, EventArgs e)
        {

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

        private void btnMenuEV_Click(object sender, EventArgs e)
        {
            using (FRMEarnedValue frm = new FRMEarnedValue()) {
                this.Hide();
                frm.ShowDialog();
            }
        }

        private void btnMenuContRiesgos_Click(object sender, EventArgs e)
        {
            using (FRMRiesgos frm = new FRMRiesgos()) {
                this.Hide();
                frm.ShowDialog();
            }
        }


        //MÉTODOS DEL TAB Configuración de Probabilidad e Impacto.
        private void btnImpactoAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarImpacto_Click(object sender, EventArgs e)
        {

        }

        private void btnImpactoCancelar_Click(object sender, EventArgs e)
        {
            clearFieldsImpacto();
        }

        private void btnProbabilidadAgregar_Click(object sender, EventArgs e)
        {
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
        }

        private void clearFieldsProbabilidad()
        {
        }
    }
}