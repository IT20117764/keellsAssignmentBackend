using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using Employee.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select EmployeetId, FirtName, LastName, Email, convert(varchar(10),DateOfBirth,120) as DateOfBirth, Age, Salary, Department from dbo.Employee";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }



          [HttpPost]
          public JsonResult Post(Employees emp)
          {
            
              string query = @"insert into dbo.Employee (FirtName,LastName,Email,DateOfBirth,Age,Salary,Department) values ('" + emp.FirstName+@"', '" + emp.LastName + @"' ,'" + emp.Email + @"','" + emp.DateOfBirth + @"', '" + emp.Age + @"', '" + emp.Salary + @"' , '" + emp.Department + @"')";
             
            //string query = @"insert into dbo.Employee values('teharuffka','fDilewshan','fthawwru@gmail.com','1990-09-8','24','29300','2')";

             DataTable table = new DataTable();
              string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
              SqlDataReader myReader;
              using (SqlConnection myCon = new SqlConnection(sqlDataSource))
              {
                  myCon.Open();
                  using (SqlCommand myCommand = new SqlCommand(query, myCon))
                  {
                      myReader = myCommand.ExecuteReader();
                      table.Load(myReader); ;

                      myReader.Close();
                      myCon.Close();
                  }
              }

              return new JsonResult("Added Successfully");
          }



        [HttpPut]
        public JsonResult Put(Employees emp)
        {
            string query = @"update dbo.Employee set FirtName = '" + emp.FirstName + @"'
                    ,LastName = '" + emp.LastName + @"'
                    ,Email = '" + emp.Email + @"'
                    ,DateOfBirth = '" + emp.DateOfBirth + @"'
                    ,Age = '" + emp.Age + @"'
                    ,Salary = '" + emp.Salary + @"'
                    ,Department = '" + emp.Department + @"'
                    where EmployeetId = '" + emp.EmployeeId + @"' 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");

        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Employee
                    where EmployeetId = " + id + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }








    }
}
