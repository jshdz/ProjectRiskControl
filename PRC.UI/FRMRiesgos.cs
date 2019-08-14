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
            reloadListRiesgos();
            listImpactos.ClearSelected();
            listProbabilidades.ClearSelected();
            listRiesgos.ClearSelected();
            calcValsRiesgos();
        }

        private void loadImpactosDefaults()
        {
            txtImpactoValor.Maximum = 4;
            txtImpactoValor.Minimum = 1;
        }

        private void loadProbabilidadesDefaults()
        {
            txtProbabilidadValor.Maximum = 3;
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

        private void reloadListRiesgos()
        {
            riesgoBindingSource.DataSource = RiesgoServices.getAll();
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

        //BOTONES DEL MENU
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

        //MÉTODOS DEL TAB Configuración de Riesgos.
        private void btnRiesgoAgregar_Click(object sender, EventArgs e)
        {
            Impacto impactoSeleccionado = ImpactoServices.getByCategoria(cbxRiesgoValorImpacto.GetItemText(cbxRiesgoValorImpacto.SelectedItem));
            Probabilidad probabilidadSeleccionada = ProbabilidadServices.getByCategoria(cbxRiesgoValorProbabilidad.GetItemText(cbxRiesgoValorProbabilidad.SelectedItem));

            Riesgo riesgoNuevo = new Riesgo {
                categoria = txtRiesgoCategoria.Text,
                descripcion = txtRiesgoDescripcion.Text,
                encargadoMonitoreo = txtRiesgoEncargadoMonitoreo.Text,
                encargadoRespuesta = txtRiesgoEncargadoRespuesta.Text,
                disparador = txtRiesgoDisparador.Text,
                resultadoEsperado = txtRiesgoResultadoEsperado.Text,
                tipoRespuesta = txtRiesgoTipoRespuesta.Text,
                descripcionRespuesta = txtRiesgoDescRespuesta.Text,
                accionarDisparador = txtRiesgoDisparadorRespuesta.Text,
                resultadoRespuesta = txtRiesgoResultadoRespuesta.Text,
                idProbabilidad = probabilidadSeleccionada.idProbabilidad,
                idImpacto = impactoSeleccionado.idImpacto,
                puntuajePI = 0,
                nivel = "",
                color = ""
            };

            if (RiesgoServices.validarCamposRiesgo(riesgoNuevo)) {
                RiesgoServices.insert(riesgoNuevo);
                reloadListRiesgos();
                clearFieldsRiesgo();
                calcValsRiesgos();
            } else {
                throwException("Rellene los campos requeridos");
            }
        }

        private void btnRiesgoEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar este riesgo?", "Eliminar Riesgo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                RiesgoServices.delete(riesgoBindingSource.Current as Riesgo);
                riesgoBindingSource.RemoveCurrent();
                calcValsRiesgos();
            }
        }

        private void clearFieldsRiesgo()
        {
            txtRiesgoDescripcion.Clear();
            txtRiesgoDescRespuesta.Clear();
            txtRiesgoDisparador.Clear();
            txtRiesgoDisparadorRespuesta.Clear();
            txtRiesgoEncargadoMonitoreo.Clear();
            txtRiesgoEncargadoRespuesta.Clear();
            txtRiesgoResultadoEsperado.Clear();
            txtRiesgoResultadoRespuesta.Clear();
            txtRiesgoTipoRespuesta.Clear();
            txtRiesgoCategoria.Clear();
            cbxRiesgoValorProbabilidad.ResetText();
            cbxRiesgoValorImpacto.ResetText();
        }

        //MÉTODOS DEL TAB MPI.
        private void btnRiesgoCancelar_Click(object sender, EventArgs e)
        {
            clearFieldsRiesgo();
        }

        private void btnRiesgoCalcularMPI_Click(object sender, EventArgs e)
        {
            loadInfoMPI();
        }

        private void loadInfoMPI()
        {
            string[,] infoMPI = RiesgoServices.calcularMPI();

            //Valores predefinidos impactos
            lblPMIImpactoVal1.Text = infoMPI[0,2];
            lblPMIImpactoVal2.Text = infoMPI[0, 3];
            lblPMIImpactoVal3.Text = infoMPI[0, 4];
            lblPMIImpactoVal4.Text = infoMPI[0, 5];
            lblPMIImpactoCateg1.Text = infoMPI[1, 2];
            lblPMIImpactoCateg2.Text = infoMPI[1, 3];
            lblPMIImpactoCateg3.Text = infoMPI[1, 4];
            lblPMIImpactoCateg4.Text = infoMPI[1, 5];

            //Valores predefinidos probabilidades
            lblPMIProbabilidadVal1.Text = infoMPI[4, 0];
            lblPMIProbabilidadVal2.Text = infoMPI[3, 0];
            lblPMIProbabilidadVal3.Text = infoMPI[2, 0];
            lblPMIProbabilidadCateg1.Text = infoMPI[4, 1];
            lblPMIProbabilidadCateg2.Text = infoMPI[3, 1];
            lblPMIProbabilidadCateg3.Text = infoMPI[2, 1];

            //Valores calculados MPI
            lblPMIProbImpVal3_1.Text = infoMPI[2, 2];
            lblPMIProbImpVal3_2.Text = infoMPI[2, 3];
            lblPMIProbImpVal3_3.Text = infoMPI[2, 4];
            lblPMIProbImpVal3_4.Text = infoMPI[2, 5];
            lblPMIProbImpVal2_1.Text = infoMPI[3, 2];
            lblPMIProbImpVal2_2.Text = infoMPI[3, 3];
            lblPMIProbImpVal2_3.Text = infoMPI[3, 4];
            lblPMIProbImpVal2_4.Text = infoMPI[3, 5];
            lblPMIProbImpVal1_1.Text = infoMPI[4, 2];
            lblPMIProbImpVal1_2.Text = infoMPI[4, 3];
            lblPMIProbImpVal1_3.Text = infoMPI[4, 4];
            lblPMIProbImpVal1_4.Text = infoMPI[4, 5];

            //Colores valores calculados MPI
            string[,] coloresValsMPI = RiesgoServices.definirColoresValoresMPI(infoMPI);
            pnlProbImpVal3_1.BackColor = ColorTranslator.FromHtml(coloresValsMPI[2, 2]);
            pnlProbImpVal3_2.BackColor = ColorTranslator.FromHtml(coloresValsMPI[2, 3]);
            pnlProbImpVal3_3.BackColor = ColorTranslator.FromHtml(coloresValsMPI[2, 4]);
            pnlProbImpVal3_4.BackColor = ColorTranslator.FromHtml(coloresValsMPI[2, 5]);
            pnlProbImpVal2_1.BackColor = ColorTranslator.FromHtml(coloresValsMPI[3, 2]);
            pnlProbImpVal2_2.BackColor = ColorTranslator.FromHtml(coloresValsMPI[3, 3]);
            pnlProbImpVal2_3.BackColor = ColorTranslator.FromHtml(coloresValsMPI[3, 4]);
            pnlProbImpVal2_4.BackColor = ColorTranslator.FromHtml(coloresValsMPI[3, 5]);
            pnlProbImpVal1_1.BackColor = ColorTranslator.FromHtml(coloresValsMPI[4, 2]);
            pnlProbImpVal1_2.BackColor = ColorTranslator.FromHtml(coloresValsMPI[4, 3]);
            pnlProbImpVal1_3.BackColor = ColorTranslator.FromHtml(coloresValsMPI[4, 4]);
            pnlProbImpVal1_4.BackColor = ColorTranslator.FromHtml(coloresValsMPI[4, 5]);

            //Visibibilidad labels MPI
            lblPMIImpactoVal1.Visible = true;
            lblPMIImpactoVal2.Visible = true;
            lblPMIImpactoVal3.Visible = true;
            lblPMIImpactoVal4.Visible = true;
            lblPMIImpactoCateg1.Visible = true;
            lblPMIImpactoCateg2.Visible = true;
            lblPMIImpactoCateg3.Visible = true;
            lblPMIImpactoCateg4.Visible = true;
            lblPMIProbabilidadVal1.Visible = true;
            lblPMIProbabilidadVal2.Visible = true;
            lblPMIProbabilidadVal3.Visible = true;
            lblPMIProbabilidadCateg1.Visible = true;
            lblPMIProbabilidadCateg2.Visible = true;
            lblPMIProbabilidadCateg3.Visible = true;
            lblPMIProbImpVal3_1.Visible = true;
            lblPMIProbImpVal3_2.Visible = true;
            lblPMIProbImpVal3_3.Visible = true;
            lblPMIProbImpVal3_4.Visible = true;
            lblPMIProbImpVal2_1.Visible = true;
            lblPMIProbImpVal2_2.Visible = true;
            lblPMIProbImpVal2_3.Visible = true;
            lblPMIProbImpVal2_4.Visible = true;
            lblPMIProbImpVal1_1.Visible = true;
            lblPMIProbImpVal1_2.Visible = true;
            lblPMIProbImpVal1_3.Visible = true;
            lblPMIProbImpVal1_4.Visible = true;
        }

        //MÉTODOS DEL TAB Administracion de riesgos.
        private void btnCalcularRiesgos_Click(object sender, EventArgs e)
        {
            calcValsRiesgos();
        }

        private void calcValsRiesgos()
        {
            List<Riesgo> listaRiesgos = RiesgoServices.getAll();
            List<RiesgoTabla> listaRiesgosTabla = new List<RiesgoTabla>();

            listaRiesgos.ForEach(c => {
                var riesgoTabla = new RiesgoTabla {
                    idRiesgo = c.idRiesgo,
                    probabilidad = Convert.ToInt32(ProbabilidadServices.getById(c.idProbabilidad).valor),
                    impacto = Convert.ToInt32(ImpactoServices.getById(c.idImpacto).valor),
                    color = string.Empty,
                    nivel = string.Empty
                };

                riesgoTabla.valorPI = riesgoTabla.impacto * riesgoTabla.probabilidad;
                listaRiesgosTabla.Add(riesgoTabla);
            });

            dgCalcValsRiesgos.DataSource = listaRiesgosTabla;

            foreach (DataGridViewRow row in dgCalcValsRiesgos.Rows) {
                row.Cells["color"].Style.BackColor = ColorTranslator.FromHtml(RiesgoServices.calcularColorRiesgo(Convert.ToInt32(row.Cells[1].Value), Convert.ToInt32(row.Cells[2].Value)));
                row.Cells["nivel"].Value = RiesgoServices.calcularNivelRiesgo(Convert.ToString(ColorTranslator.ToHtml(row.Cells[5].Style.BackColor)));
            }
        }
    }
}