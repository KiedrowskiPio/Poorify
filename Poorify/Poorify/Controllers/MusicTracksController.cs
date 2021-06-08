using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Poorify.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Poorify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicTracksController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public MusicTracksController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select TrackId, TrackName, TrackAlbum, TrackArtist, TrackLength, TrackDate, PhotoFileName from dbo.MusicTracks";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MusicTracksConnection");
            SqlDataReader reader;

            using(SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader); ;

                    reader.Close();
                    con.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(MusicTracks mt)
        {
            string query = @"insert into dbo.MusicTracks values ('"+mt.TrackName+@"', '"+mt.TrackAlbum+@"','"+mt.TrackArtist+@"','"+mt.TrackLength+ @"','" + mt.TrackDate + @"','" + mt.PhotoFileName + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MusicTracksConnection");
            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader); ;

                    reader.Close();
                    con.Close();
                }
            }
            return new JsonResult("we did it");
        }

        [HttpPut]
        public JsonResult Put(MusicTracks mt)
        {
            string query = @"update dbo.MusicTracks set TrackName ='" + mt.TrackName + @"', 
                            TrackAlbum = '" + mt.TrackAlbum + @"', 
                            TrackArtist = '" + mt.TrackArtist + @"', 
                            TrackLength = '" + mt.TrackLength + @"', 
                            TrackDate = '" + mt.TrackDate + @"', 
                            PhotoFileName = '" + mt.PhotoFileName + @"' from dbo.MusicTracks where '" + mt.TrackId+@"' = TrackId";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MusicTracksConnection");
            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader); ;

                    reader.Close();
                    con.Close();
                }
            }
            return new JsonResult("updated");
        }

        [HttpDelete("{TrackId}")]
        public JsonResult Delete(int TrackId)
        {
            string query = @"delete from dbo.MusicTracks where '" + TrackId + @"' = TrackId";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MusicTracksConnection");
            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader); ;

                    reader.Close();
                    con.Close();
                }
            }
            return new JsonResult("deleted");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("generic.png");
            }
        }

        [Route("GetTrackNames")]
        public JsonResult GetTrackNames()
        {
            string query = @"select TrackName from dbo.MusicTracks";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MusicTracksConnection");
            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader); ;

                    reader.Close();
                    con.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
