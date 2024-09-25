using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Alexander Vargas Mejia
//18-09-2024
//visual C# + DB

namespace Visual_C_____DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            frmCRUD frmCRUD = new frmCRUD();
            this.SuspendLayout();
            this.Hide();
            frmCRUD.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {            
            Application.Exit();
        }
    }
}
