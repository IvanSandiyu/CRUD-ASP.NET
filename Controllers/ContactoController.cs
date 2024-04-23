using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ProyectoASP.Models;
using System.Data.SqlClient;
using System.Data;
namespace ProyectoASP.Controllers
{
    public class ContactoController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString(); //Para realizar la conexion a la BBDD
        static List<Contacto> listaContactos = new List<Contacto>();

        // GET: Contacto
        public ActionResult Inicio()
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                listaContactos = new List<Contacto>();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONTACTO", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader()) //Devuelve los resultados de la bbdd
                {
                    while (dr.Read())
                    {
                        Contacto c = new Contacto();
                        c.idContacto = Convert.ToInt32(dr["IdContacto"]);
                        c.Nombre = dr["Nombre"].ToString();
                        c.Apellido = dr["Apellido"].ToString();
                        c.Telefono = Convert.ToInt32(dr["Telefono"]);
                        c.Correo = dr["Correo"].ToString();

                        listaContactos.Add(c);
                    }
                }
            }
            return View(listaContactos);
        }

        public ActionResult CrearContacto()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CrearContacto(Contacto contacto)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_Registrar", conn);
                //cmd.Parameters.AddWithValue("IdContacto", contacto.idContacto);
                cmd.Parameters.AddWithValue("Nombre", contacto.Nombre);
                cmd.Parameters.AddWithValue("Apellido", contacto.Apellido);
                cmd.Parameters.AddWithValue("Telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", contacto.Correo);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto"); //Vovlemos a inicio
        }

        public ActionResult Eliminar(int? idContacto)
        {
            
            if (idContacto == null) return RedirectToAction("Inicio", "Contacto");
            Contacto c = listaContactos.Where(cont => cont.idContacto == idContacto).FirstOrDefault();
            using (SqlConnection conn = new SqlConnection(conexion)) ;
            return View(c);

        }
        [HttpPost]
        public ActionResult Eliminar(int idContacto)
        {
          
            using (SqlConnection conn = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_Eliminar", conn);
                cmd.Parameters.AddWithValue("IdContacto", idContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto"); //Vovlemos a inicio

        }

        public ActionResult Editar(int? idContacto) //el "?" significa que permite valores null
        {
            if (idContacto == null) return RedirectToAction("Inicio", "Contacto");
            Contacto c = listaContactos.Where(cont => cont.idContacto == idContacto).FirstOrDefault();
            using (SqlConnection conn = new SqlConnection(conexion)) ;
            return View(c);


        }

        [HttpPost]
        public ActionResult Editar(Contacto contacto)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_Editar", conn);
                cmd.Parameters.AddWithValue("IdContacto", contacto.idContacto);
                cmd.Parameters.AddWithValue("Nombre", contacto.Nombre);
                cmd.Parameters.AddWithValue("Apellido", contacto.Apellido);
                cmd.Parameters.AddWithValue("Telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", contacto.Correo);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto"); //Vovlemos a inicio

        }

        
    }
}