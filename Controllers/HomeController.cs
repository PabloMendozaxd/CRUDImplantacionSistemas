using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using ProyectoFinal.Models;
using System.Data.SqlClient;


namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private string connection = "Data Source=imsnor.database.windows.net;Initial Catalog=db;User ID=dbUserN;Password=Npas$w0rd";
        public IActionResult Index()
        {
            var data = new List<Carreras>();
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("SELECT [Id], [Titulo], [Creditos], [Campus] FROM [Carreras]", con);

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Add(new Carreras
                    {
                        Id = (Guid)dr["Id"],
                        Titulo = (string)dr["Titulo"],
                        Creditos = (int)dr["Creditos"],
                        Campus = (string)dr["Campus"]
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            return View(data);
        }
        public IActionResult Details(Guid id)
        {
            var data = new Carreras();
            var con = new SqlConnection(connection);
            var cmd = new SqlCommand("SELECT [Id], [Titulo], [Creditos], [Campus] FROM [Carreras] WHERE [Id] = @i", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["Id"];
                    data.Titulo = (string)dr["Titulo"];
                    data.Creditos = (int)dr["Creditos"];
                    data.Campus = (string)dr["Campus"];
                }
                return PartialView(data);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Carreras data)
        {
            var con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [Carreras] ([Id],[Titulo],[Creditos],[Campus]) VALUES (NEWID(), @t, @cr,@cm);", con);


            cmd.Parameters.Add("@t", SqlDbType.NVarChar, 50).Value = data.Titulo;
            cmd.Parameters.Add("@cr", SqlDbType.Int).Value = data.Creditos;
            cmd.Parameters.Add("@cm", SqlDbType.NVarChar, 100).Value = data.Campus;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public IActionResult Edit(Guid id)
        {
            var data = new Carreras();
            var con = new SqlConnection(connection);
            var cmd = new SqlCommand("SELECT [Id], [Titulo], [Creditos], [Campus] FROM [Carreras] WHERE [Id] = @i", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["Id"];
                    data.Titulo = (string)dr["Titulo"];
                    data.Creditos = (int)dr["Creditos"];
                    data.Campus = (string)dr["Campus"];

                }
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Carreras data)
        {
            var con = new SqlConnection(connection);
            var cmd = new SqlCommand(@"UPDATE [Carreras] SET [Titulo] = @t, [Creditos] = @cr, [Campus] = @cm	WHERE [Id] = @i;", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("@t", SqlDbType.NVarChar, 100).Value = data.Titulo;
            cmd.Parameters.Add("@cr", SqlDbType.Int).Value = data.Creditos;
            cmd.Parameters.Add("@cm", SqlDbType.NVarChar, 100).Value = data.Campus;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public IActionResult Delete(Guid id)
        {
            var con = new SqlConnection(connection);
            var cmd = new SqlCommand("DELETE FROM [Carreras] WHERE [Id] = @i", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}