using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planificador_Procesador
{
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {

        public int contador;
        public int tam_Proceso;
        public Form3()
        {
            InitializeComponent();
            contador = 1;
            tam_Proceso = 0;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            


        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            tam_Proceso = Convert.ToInt32(metroTextBox2.Text);
            MessageBox.Show("El proceso fue ejecutado con exito" );
            contador++;
            metroGrid1.Rows.Add(contador,tam_Proceso);
            metroTextBox2.Text = " "; 
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            metroGrid1.Rows.Clear();
        }
    }
}
