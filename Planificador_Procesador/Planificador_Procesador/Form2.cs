using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Planificador_Procesador
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //--------INSTANCIACION

        NuevoProceso procesar; //CREACIÓN DE OBJETO DE LA CLASE "NuevoProceso"
        Cola generar;//OBJETO para acceder a metodos de encolar y recorrer de la clase COLA

        public Form2()
        {
            InitializeComponent();
            generar = new Cola(metroProgressBar1, metroGrid1);
        }
        
        private void metroTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
          (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
       
        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            metroTextBox1.Text = "1";//Inicializa por defecto el numero
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                procesar = new NuevoProceso();
                procesar.nombre = metroTextBox3.Text;
                procesar.ID = Convert.ToInt32(metroTextBox1.Text);
                procesar.tiempo = Convert.ToInt32(metroTextBox2.Text);
                procesar.quantum = Convert.ToInt32(metroComboBox1.Text);
                AñadirProceso(procesar);

                metroComboBox1.Hide();

                htmlLabel2.Text = "Valor Quantum " + metroComboBox1.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("No ha ingresado datos correctos");
            }


            if (metroTextBox3.Text != "")
            {
                metroTextBox1.Text = Convert.ToString(Convert.ToInt16(metroTextBox1.Text) + 1);
            }
            metroTextBox3.Clear(); metroTextBox2.Clear();
        }
        private void AñadirProceso(NuevoProceso temporal)
        {
            procesar = new NuevoProceso();
            //crea un string de cada valor y los agrega al cuadro
            string ID = temporal.ID.ToString();
            string nom = temporal.nombre;
            string tiemp = temporal.tiempo.ToString();
            string quantum = temporal.quantum.ToString();
            string[] arregloproceso = { ID, nom, tiemp, "Cargado" };
            metroGrid1.Rows.Add(arregloproceso);
          
            generar.Encolar1(temporal);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroButton1.Hide();
            generar.Recorrer();
        }

        private void metroProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroGrid1.Rows.Clear();
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
