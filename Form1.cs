using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace parcial1
{
    public partial class Form1 : Form
    {
        List<alquiler> lista1 = new List<alquiler>();
        
        List<estudante> lista2 = new List<estudante>();
        List<libroo> lista3 = new List<libroo>();
        List<lista> lista4 = new List<lista>();
        DateTime fecha = DateTime.Now;


        public Form1()
        {
            InitializeComponent();
            LeerArquilados();
            LeerLibros();
            LeerAlumnos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            alquiler presTemp = new alquiler();
            presTemp.Carnet_del_Alumno = Int32.Parse(textBox1.Text);
            presTemp.Codigo_Libro = Int32.Parse(textBox2.Text);
            presTemp.Fecha_Prestamo = monthCalendar1.SelectionStart;
            presTemp.Fecha_Devolucion = monthCalendar2.SelectionStart;
           lista1.Add(presTemp);
            FileStream stream = new FileStream("prestamos.txt", FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter writer = new StreamWriter(stream);

            foreach (var p in lista1)
            {
                writer.WriteLine(p.Carnet_del_Alumno);
                writer.WriteLine(p.Codigo_Libro);
                writer.WriteLine(p.Fecha_Prestamo);
                writer.WriteLine(p.Fecha_Devolucion);
            }
            writer.Close();
            

        }
        private void LeerArquilados()
        {
            FileStream stream = new FileStream("arquiler.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                alquiler prestamoTemp = new alquiler();

                prestamoTemp.Carnet_del_Alumno = Int32.Parse(reader.ReadLine());
                prestamoTemp.Codigo_Libro = Int32.Parse(reader.ReadLine());
                prestamoTemp.Fecha_Prestamo = Convert.ToDateTime(reader.ReadLine());
                prestamoTemp.Fecha_Devolucion= Convert.ToDateTime(reader.ReadLine());
                lista1.Add(prestamoTemp);
            }
            reader.Close();
        }
        private void LeerLibros()
           
        {
            FileStream stream = new FileStream("libros.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                libroo libroTemp = new libroo();

              libroTemp.Codigo = Int32.Parse(reader.ReadLine());
                libroTemp.Titulo = reader.ReadLine();
                libroTemp.Autor= reader.ReadLine();
              libroTemp.Año= Int32.Parse(reader.ReadLine());
               
                lista3.Add(libroTemp);
            }
            reader.Close();
        }
        private void LeerAlumnos()
            //Lee y agrega elementos a la lista alumnos
        {
            FileStream stream = new FileStream("alumnos.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                estudante esTemp = new estudante();

                esTemp.Carnet = Int32.Parse(reader.ReadLine());
              esTemp.Nombre= reader.ReadLine();
                esTemp.Direccion = reader.ReadLine();
               

                lista2.Add(esTemp);
            }
            reader.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lista3.Count; i++)
            {
                for (int j = 0; j < lista1.Count; j++)
                {
                    if (lista3[i].Codigo == lista1[j].Codigo_Libro)
                    {
                        lista datos = new lista();
                        datos.Nombre = lista2[i].Nombre;
                        datos.Titulo = lista3[j].Titulo;
                        datos.Fecha_Prestamo = lista1[i].Fecha_Prestamo;
                        datos.Fecha_Devolucion = lista1[i].Fecha_Devolucion;
                    

                        lista4.Add(datos);
                    }
                }


            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista4;
            dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //conteo de los libros no devueltos
            int cuenta = 0;

        
            //Recorre toda la lista de prestamos
            for (int i = 0; i < lista1.Count(); i++)
            {
                //compara la fecha actual a la fecha de devolucion si la es mayor
                //significa que el libro no a sido devuelto
                
                if (lista1[i].Fecha_Devolucion > fecha)
                {
                    cuenta = cuenta + 1;
                }
            }

            label5.Text = "los libros pendientes de devolucion son: " +  cuenta;


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
