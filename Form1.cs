using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace COLEGIO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        clsDatos datos = new clsDatos();
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Instruccion sql.
            String SQL1 = "SELECT * FROM materias";

            //Lleno las tablas necesarias.
            DataTable tbVende = datos.getData(SQL1);

            //Recorro la tabla 
            foreach (DataRow fila in tbVende.Rows)
            {
                //Cargo las casillas con los vendedores y su id en la propiedad tag.
                ListViewItem casillas = listView1.Items.Add(fila["nombre"].ToString());
                casillas.Tag = fila["materia"].ToString();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();

            Int32 Aprobados = 0;
            Int32 Aplazados = 0;
            Int32 Ausentes = 0;
            Int32 CantidadAprobados = 0;
            Int32 CantidadAplazados = 0;
            Int32 CantidadAusentes = 0;

            String SQL2 = "SELECT materia, dni, nota FROM notas";
            DataTable tbVenta = datos.getData(SQL2);

            
            chart1.Titles.Add("FINALES IES 21");
            


            Series serie = chart1.Series.Add("Alumnos".ToString());

                //Recorro la listview para obtener el tag del checkbox
                foreach (ListViewItem casilla in listView1.CheckedItems)
                {
                    Int32 tagMateria = Convert.ToInt32(casilla.Tag);

                    //Recorro los registros de la tabla
                    foreach (DataRow filaVenta in tbVenta.Rows)
                    {
                        Int32 nota = Convert.ToInt32(filaVenta["nota"].ToString());
                        Int32 idmateria = Convert.ToInt32(filaVenta["materia"].ToString());


                        if (tagMateria == idmateria)
                        {
                            if (nota >= 4)
                            {
                                Aprobados = Aprobados + CantidadAprobados + 1; 
                            }
                            if (nota < 4 && nota >= 1)
                            {
                                Aplazados = Aplazados + CantidadAplazados + 1;
                            }
                            if (nota == -1)
                            {
                                Ausentes = Ausentes + CantidadAusentes + 1;
                            }
                        }
                    }
                    
                    
                }
            serie.Points.AddXY("Aprobados", Aprobados);
            serie.Points.AddXY("Aplazados", Aplazados);
            serie.Points.AddXY("Ausentes", Ausentes);
            


        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
