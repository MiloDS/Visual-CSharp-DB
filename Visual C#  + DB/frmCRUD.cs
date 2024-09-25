using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Visual_C_____DB
{
    public partial class frmCRUD : Form
    {
        SQLiteConnection objConnection;
        public frmCRUD()
        {
            InitializeComponent();
            string lsConnectionString = @"Data Source=|DataDirectory|\miBaseDeDatos.db;";
            objConnection = new SQLiteConnection(lsConnectionString);
        }
        
        private void frmCRUD_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.SuspendLayout();
            form1.Show();            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            try
            {
                objConnection.Open();

                SQLiteCommand objCommand = new SQLiteCommand();
                objCommand.Connection = connection;
                objCommand.CommandText = "INSERT INTO tblEstudiantes (ID,Nombre,Edad,Semestre) VALUES (@ID, @Nombre, @Edad, @Semestre)";
                objCommand.Parameters.AddWithValue("@ID", txtID.Text);
                objCommand.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                objCommand.Parameters.AddWithValue("@Edad", txtEdad.Text);
                objCommand.Parameters.AddWithValue("@Semestre", txtSemestre.Text);
                objCommand.ExecuteNonQuery();
                objConnection.Close();
                MessageBox.Show("Datos insertados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se insertaron los datos. Detalles:" + ex.Message);
            }
            finally
            {
                if(objConnection.State == ConnectionState.Open)
                    objConnection.Close();
            }
            

        }

        private void frmCRUD_Load(object sender, EventArgs e)
        {
            try
            {
                objConnection.Open();
                MessageBox.Show("Conexion Establecida.");
                objConnection.Close();
            }
            catch (Exception) { throw; }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {                       
            try
            {
                objConnection.Open();
                SQLiteCommand objCommand = new SQLiteCommand();
                objCommand.Connection = objConnection;
                objCommand.CommandText = "SELECT * FROM tblEstudiantes";
                SQLiteDataAdapter objAdapter = new SQLiteDataAdapter(objCommand);
                DataTable objDt = new DataTable();
                objAdapter.Fill(objDt);
                DataGridView dgvEstudiantes = new DataGridView() 
                { 
                    DataSource = objDt,
                    Dock = DockStyle.Fill,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };
                
                Form frmGridForm = new Form() 
                {
                    Text = "Datos de los estudiantes",
                    Size = new Size(500,400)
                };

                frmGridForm.Controls.Add(dgvEstudiantes);
                frmGridForm.Show();
                
                objConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: No se encontraron los datos. Detalles:" + ex.Message);
            }            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                objConnection.Open();
                SQLiteCommand objCommand = new SQLiteCommand();
                objCommand.Connection = objConnection;                
                objCommand.CommandText = "UPDATE tblEstudiantes SET Nombre = @Nombre, Edad = @Edad, Semestre = @Semestre WHERE ID = @ID";
                objCommand.Parameters.AddWithValue("@ID", txtID.Text);
                objCommand.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                objCommand.Parameters.AddWithValue("@Edad", txtEdad.Text);
                objCommand.Parameters.AddWithValue("@Semestre", txtSemestre.Text);

                int lnRowsAffected = objCommand.ExecuteNonQuery();
                if (lnRowsAffected > 0)
                {
                    MessageBox.Show("Datos actualizados correctamente.");
                }
                else
                {
                    MessageBox.Show("No se encontró el registro con el ID proporcionado.");
                }
                objConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se actualizaron los datos. Detalles:" + ex.Message);
            }            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                objConnection.Open();
                SQLiteCommand objCommand = new SQLiteCommand();
                objCommand.Connection = objConnection;
                objCommand.CommandText = "DELETE FROM tblEstudiantes WHERE ID = @ID";
                objCommand.Parameters.AddWithValue("@ID",txtID.Text);

                int lnRowAffected = objCommand.ExecuteNonQuery();
                if (lnRowAffected > 0)
                {
                    MessageBox.Show("Datos eliminados correctamente.");
                }
                else
                {
                    MessageBox.Show("No se encontro el registro con el ID proporcionado.");
                }
                objConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: No se eliminaron los datos. Detalles:" + ex.Message);
            }            
        }
    }
}
