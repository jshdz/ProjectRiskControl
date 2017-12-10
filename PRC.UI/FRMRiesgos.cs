using PRC.API.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
