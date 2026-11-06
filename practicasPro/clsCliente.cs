using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practicasPro
{
    internal class clsCliente
    {

        // Variables de conexión a la base de datos de access
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private string CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Clientes.mdb";
        private string Tabla = "Lista";




        //Declaro las Variables
        private Decimal deuda; 
        private Int32 cantidad;
        private Int32 idCli;
        private String Nom;
        private Decimal deu; 
        private Decimal lim;
        private Int32 idCui;


        //Declaro las PROPIEDADES
        public Decimal TotalDeuda
        {
            get { return deuda; }
        }

        public Int32 CantidadDeudores
        {
            get { return cantidad; }
        }

        public Int32 Cantidad //agg
        {
            get { return cantidad; }
        }

        public Int32 IdCliente
        {
            get { return idCli; }
            set { idCli = value; }
        }

        public String Nombre
        {
            get { return Nom; }
            set { Nom = value; }
        }

        public Decimal Deuda
        {
            get { return deu; }
            set { deu = value; }
        }

        public Decimal limite
        {
            get { return lim; }
            set { lim = value; }
        }

        public Int32 IdCiudad
        {
            get { return idCui; }
            set { idCui = value; }
        }



        public void Listar(DataGridView Grilla)
        {
            try
            {
                //Hacemos la conexión a la Base de Datos
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                //Ejecutamos la conexión, le decimos que timpo de tabla usamos y cual tabla usamos
                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();
                clsCliente ciu = new clsCliente();
                String NomCiu = "";


                Grilla.DataSource = null;
                Grilla.AutoGenerateColumns = false;
                Grilla.Rows.Clear();

                cantidad = 0;
                deuda = 0;

                
                    while (DR.Read())
                    {
                        NomCiu = ciu.Listar(DR.GetInt32(4));

                        Grilla.Rows.Add(
                            DR.GetInt32(0),
                            DR.GetString(1),
                            DR.GetDecimal(2),
                            DR.GetDecimal(3),
                            DR.GetInt32(4)

                        );

                        cantidad++;
                        deuda += DR.GetDecimal(2);
                    }

                

            }
                
            
        }
        






public void Imprimir(PrintPageEventArgs reporte)
        {
            try
            {
                Font LetraTitulo1 = new Font("Arial", 20);
                Font LetraTitulo2 = new Font("Arial", 12);
                Font LetraTexto = new Font("Arial", 8);

                Int32 f = 200;

                reporte.Graphics.DrawString("Listado de Clientes",
                    LetraTitulo1, Brushes.Red, 100, 100);

                reporte.Graphics.DrawString("Código",
                    LetraTitulo2, Brushes.Blue, 100, 180);

                reporte.Graphics.DrawString("Nombre del cliente",
                    LetraTitulo2, Brushes.Blue, 300, 180);

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);

                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow fila in DS.Tables[Tabla].Rows)
                    {
                        reporte.Graphics.DrawString(
                            fila["idCliente"].ToString(),
                            LetraTexto, Brushes.Black, 100, f);

                        reporte.Graphics.DrawString(
                            fila["Nombre"].ToString(),
                            LetraTexto, Brushes.Black, 300, f);

                        f = f + 15;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conexion.Close();
            }
        }




    }
}
