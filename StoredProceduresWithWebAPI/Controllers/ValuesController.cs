using StoredProceduresWithWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StoredProceduresWithWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);
        Employee emp = new Employee();
        // GET api/values
        public List<Employee> Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("usp_GetAllEmployees", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Employee> lstEmployee = new List<Employee>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.Name = dt.Rows[i]["Name"].ToString();
                    emp.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    emp.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
                    emp.Active = Convert.ToInt32(dt.Rows[i]["Active"]);
                    lstEmployee.Add(emp);
                }
            }
            if(lstEmployee.Count > 0)
            {
                return lstEmployee;
            }
            else
            {
                return null;
            }
        }

        // GET api/values/5
        public Employee Get(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("usp_GetAEmployeeByID", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Employee emp = new Employee();

            if (dt.Rows.Count > 0)
            {             
            
                    emp.Name = dt.Rows[0]["Name"].ToString();
                    emp.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    emp.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                    emp.Active = Convert.ToInt32(dt.Rows[0]["Active"]);
                

            }
            if (emp != null)
            {
                return emp;
            }
            else
            {
                return null;
            }
        }

        // POST api/values
        public string Post(Employee employee)  
        {
            string msg = ""; 
            if(employee != null)
            {
                SqlCommand cmd = new SqlCommand("usp_AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Active", employee.Active);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg =  "Data has been inserted";
                }
                else
                {
                    msg =  "Error";
                }

            }
            return msg;

        }

        // PUT api/values/5
        public string Put(int id, Employee employee)
        {
            string msg = "";
            if (employee != null)
            {
                SqlCommand cmd = new SqlCommand("usp_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Active", employee.Active);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Data has been updated";
                }
                else
                {
                    msg = "Error";
                }

            }
            return msg;
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            string msg = "";
            
                SqlCommand cmd = new SqlCommand("usp_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Data has been delete";
                }
                else
                {
                    msg = "Error";
                }
            return msg;
        }
    }
}






 