using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using WebApplication3;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        SQLiteConnection con = new SQLiteConnection("Data Source=C:\\Users\\richa\\source\\repos\\WebApplication3\\test.db");


        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }




        [HttpGet]
        public IEnumerable<User> GetData()
        {
            SQLiteCommand cmd = new SQLiteCommand(con);
            con.Open();
            cmd.CommandText = "SELECT * FROM Student";
            SQLiteDataReader dr = cmd.ExecuteReader();
            List<User> studentData = new List<User>();
            int a = dr.FieldCount;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    User student = new User();
                    student.id = dr["id"].ToString();
                    student.name = dr["name"].ToString();
                    student.email = dr["email"].ToString();
                    student.contact = (decimal)(long)dr["contact"];
                    student.address = dr["address"].ToString();
                    studentData.Add(student);
                }
            }
            dr.Close();
            con.Close();
            return studentData;
        }


        [HttpPost]
        //    [Route("Post")]
        public IActionResult Create([FromBody] User Student)
        {
            SQLiteCommand cmd = new SQLiteCommand(con);
            con.Open();
            cmd.CommandText = "INSERT INTO Student(name,id,email,contact,address) VALUES('" + Student.name + "','" + Student.id + "','" + Student.email + "','" + Student.contact + "','" + Student.address + "')";
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return CreatedAtAction(nameof(Create), Student);
        }


        [HttpGet]
        [Route("id")]
        public IEnumerable<string> GetData([FromQuery] string id)
        {
            SQLiteCommand cmd = new SQLiteCommand(con);
            con.Open();
            cmd.CommandText = "SELECT * FROM Student where id='" + id + "'";
            SQLiteDataReader dr = cmd.ExecuteReader();
            List<string> studentData = new List<string>();
            int a = dr.FieldCount;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    studentData.Add("Id: " + dr["id"].ToString());
                    studentData.Add("Name: " + dr["name"].ToString());
                    studentData.Add("Contact: " + dr["email"].ToString());
                    studentData.Add("Account No: " + dr["contact"].ToString());
                    studentData.Add("Current Balance: " + dr["address"].ToString());
                }
            }
            dr.Close();
            con.Close();
            return studentData;
        }

        [HttpPut]
        //  [Route("Put")]
        public IActionResult Put([FromBody] User Student, [FromQuery] string id)
        {
            SQLiteCommand cmd = new SQLiteCommand(con);
            con.Open();
            cmd.CommandText = "update Student set name ='" + Student.name + "',contact='" + Student.contact + "', email='" + Student.email + "', address='" + Student.address + "' WHERE ID='" + id + "'";
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return CreatedAtAction(nameof(Put), Student);
        }


        [HttpDelete]
        // [Route("Delete")]
        public IActionResult Delete(User Student, [FromQuery] string id)
        {
            SQLiteCommand cmd = new SQLiteCommand(con);
            con.Open();
            cmd.CommandText = "DELETE FROM Student WHERE Id=" + id + "";
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return CreatedAtAction(nameof(Delete), Student);
        }
    }
}