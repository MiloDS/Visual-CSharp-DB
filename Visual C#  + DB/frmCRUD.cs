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
        SQLiteConnection connection;
        public frmCRUD()
        {
            InitializeComponent();
            string connectionString = @"Data Source=|DataDirectory|\miBaseDeDatos.db;";
            connection = new SQLiteConnection(connectionString);
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
                connection.Open();

                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO tblEstudiantes (ID,Nombre,Edad,Semestre) VALUES (@ID, @Nombre, @Edad, @Semestre)";
                command.Parameters.AddWithValue("@ID", txtID.Text);
                command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                command.Parameters.AddWithValue("@Edad", txtEdad.Text);
                command.Parameters.AddWithValue("@Semestre", txtSemestre.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Datos insertados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se insertaron los datos. Detalles:" + ex.Message);
            }
            finally
            {
                if(connection.State == ConnectionState.Open)
                    connection.Close();
            }
            

        }

        private void frmCRUD_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                MessageBox.Show("Conexion Establecida.");
                connection.Close();
            }
            catch (Exception) { throw; }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {                       
            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM tblEstudiantes";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataGridView dgvEstudiantes = new DataGridView() 
                { 
                    DataSource = dt,
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
                
                connection.Close();
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
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;                
                command.CommandText = "UPDATE tblEstudiantes SET Nombre = @Nombre, Edad = @Edad, Semestre = @Semestre WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", txtID.Text);
                command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                command.Parameters.AddWithValue("@Edad", txtEdad.Text);
                command.Parameters.AddWithValue("@Semestre", txtSemestre.Text);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Datos actualizados correctamente.");
                }
                else
                {
                    MessageBox.Show("No se encontró el registro con el ID proporcionado.");
                }
                connection.Close();
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
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM tblEstudiantes WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID",txtID.Text);

                int rowAffected = command.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    MessageBox.Show("Datos eliminados correctamente.");
                }
                else
                {
                    MessageBox.Show("No se encontro el registro con el ID proporcionado.");
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: No se eliminaron los datos. Detalles:" + ex.Message);
            }            
        }
    }
}
