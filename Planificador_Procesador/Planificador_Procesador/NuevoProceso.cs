using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Planificador_Procesador
{
    class NuevoProceso
    {
        //ATRIBUTOS CON GET Y SET
        public string nombre { get; set; }
        public int ID { get; set; }
        public int tiempo { get; set; }
        public string estado { get; set; }
        public int quantum { get; set; }
        public int tiemporestante { get; set; }
        private NuevoProceso siguiente;
        public NuevoProceso Siguiente { get { return siguiente; } set { siguiente = value; } }
        //CONSTRUCTOR
        public NuevoProceso()
        {
            nombre = "";
            ID = 0;
            tiempo = 0;
            quantum = 0;
            estado = "Listo, espere";
            tiemporestante = tiempo;
        }
    }

    public class Cola
    {
        private NuevoProceso frente;
        private NuevoProceso final;
        private int numEle;

        private  NuevoProceso Frente
        {
            get { return Frente1; }
            set { Frente1 = value; }
        }
        private  NuevoProceso Final
        {
            get { return final; }
            set { final = value; }
        }
        public int N
        {
            get { return numEle; }
            set { numEle = value; }
        }

        internal  NuevoProceso Frente1 { get => Frente2; set => Frente2 = value; }
        internal NuevoProceso Frente2 { get => frente; set => frente = value; }

        //--------DEFINICIÓN DE OBJETOS
        ProgressBar proBar;
        DataGridView dataGridView;
        //--------CONSTRUCTOR CON SOBRECARGA
        public Cola(ProgressBar proBar, DataGridView tempdata)
        {
            Final = null;
            Frente = null;
            numEle = 0;
            this.proBar = proBar;
            dataGridView = tempdata;
        }

        //--------MÉTODO ENCOLAR
        internal void Encolar1(NuevoProceso tarea)
        {

           
            NuevoProceso nuevo = new NuevoProceso();
            if (numEle == 0)
            {

                nuevo.Siguiente = null;
                Frente = tarea;
                Final = tarea;

            }
            else
            {
                Final.Siguiente = tarea;
                Final = tarea;
            }
            numEle++;

        }

        //--------MÉTODO DESENCOLAR
        public void Desencolar()
        {

            if (numEle == 0)
            {
                MessageBox.Show("NO HAY MAS PROCESOS");
            }
            else
            {
                NuevoProceso temp = Frente;
                Frente = temp.Siguiente;
                temp.Siguiente = null;
                numEle--;
            }
        }

        //--------MÉTODO PARA RELLENAR ARREGLO
        public string[] ArregloData(string un, string dos, string tres, string cua)
        {
            string[] arregloproceso = new String[4];
            arregloproceso[0] = un;
            arregloproceso[1] = dos;
            arregloproceso[2] = tres;
            arregloproceso[3] = cua;
            return arregloproceso;
        }

        //--------MÉTODO RECORRER COLA
        public void Recorrer()
        {
           // dataGridView.Rows.Clear();
            dataGridView.Refresh();
            if (Frente1 == null)
            {
                MessageBox.Show("NO HAY PROCESOS");

            }
            else
            {
                while (Frente1 != null && Frente1.tiemporestante != -1)
                {
                    if (Frente1.tiempo > Frente1.quantum)
                    {
                        Frente1.estado = "En ejecución";
                        //Variable para calcular progreso en barra en intervalos
                        double conta = 100 / Frente1.quantum;
                        for (double i = 0; i <= Frente1.quantum; i++)
                        {//ciclo for para mostrar progreso en barra
                         //Envia valor de progreso
                            proBar.Value = Convert.ToInt32(i * conta);
                            //Suspende o manda a dormir al sistema por 500 milisegundos
                            //cada intervalo "conta" para simular ejecucion
                            Thread.Sleep(500);
                        }
                        //Actualiza que el nuevo tiempo restando el ejecutado
                        Frente1.tiempo = Frente1.tiempo - Frente1.quantum;
                        Frente1.tiemporestante = Frente1.tiempo;
                        //Actualiza estado 
                        Frente1.estado = "En espera";
                        dataGridView.Rows.Add(ArregloData(Convert.ToString(Frente1.ID),
                            Frente1.nombre, Convert.ToString(Frente1.tiemporestante), Frente1.estado));
                        MessageBox.Show("Proceso---" + Frente1.nombre + "---enviado a espera");
                        NuevoProceso temporal = Frente1;
                        if (numEle != 1)
                        {

                            Frente = temporal.Siguiente;
                            Frente.Siguiente = temporal.Siguiente.Siguiente;
                            Encolar1(temporal);
                        }
                        else
                        {
                            Encolar1(temporal);
                            Desencolar();
                        }

                    }
                    else
                    {
                        //Variable para calcular progreso en barra en intervalos
                        double conta = 100 / Frente1.quantum;
                        for (double i = 0; i <= Frente1.quantum; i++)
                        {
                            //Rellena barra de progreso en intervalos "conta"
                            proBar.Value = Convert.ToInt32(i * conta);
                            //Manda a dormir al proceso actual
                            Thread.Sleep(500);
                        }
                        Frente1.tiempo = 0;
                        Frente1.tiemporestante = -1;
                        Frente1.estado = "Terminado";
                        dataGridView.Rows.Add(ArregloData(Convert.ToString(Frente1.ID),
                            Frente1.nombre, "0", Frente1.estado));
                        MessageBox.Show("Proceso---" + Frente1.nombre + "---Terminado");
                        //Elimina proceso de la cola
                        Desencolar();

                    }
                    if (Frente1 == null)
                    {
                        MessageBox.Show("PROCESOS TERMINADOS");

                    }


                }

                proBar.Value = 50;
                Thread.Sleep(1000);
                proBar.Value = 100;
                MessageBox.Show("PROCESOS TERMINADOS CON EXITO");
            }

        }


    }
}
