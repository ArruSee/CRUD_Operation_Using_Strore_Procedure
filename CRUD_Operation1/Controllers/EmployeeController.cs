using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Net.Configuration;
using Newtonsoft.Json;
using CRUD_Operation1.Models;

namespace CRUD_Operation1.Controllers
{
    public class EmployeeController : Controller
    {
        SqlConnection con=new SqlConnection("data source=ARUN;initial catalog=db220823;integrated security=true");
        public ActionResult EmployeeForm()
        {
            return View();
        }
        public void Employee_Insert_Update(EmployeeModel obj)
        {
            if (obj.EmpId > 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_emp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", obj.EmpId);
                cmd.Parameters.AddWithValue("@name", obj.A);
                cmd.Parameters.AddWithValue("@address", obj.B);
                cmd.Parameters.AddWithValue("@age", obj.C);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_emp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", obj.A);
                cmd.Parameters.AddWithValue("@address", obj.B);
                cmd.Parameters.AddWithValue("@age", obj.C);
                cmd.ExecuteNonQuery();
                con.Close();
                
            }
           
        }

        //public void Employee_Update(EmployeeModel obj)
        //{
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("usp_emp_update", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@empid", obj.EmpId);
        //    cmd.Parameters.AddWithValue("@name", obj.A);
        //    cmd.Parameters.AddWithValue("@address", obj.B);
        //    cmd.Parameters.AddWithValue("@age", obj.C);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        public void Employee_Delete(EmployeeModel obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", obj.EmpId);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public JsonResult Employee_Edit(EmployeeModel obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp_edit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", obj.EmpId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string pp = JsonConvert.SerializeObject(dt);
            return Json(pp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Employee_Show()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp_show", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            sda.Fill(dt);   
            con.Close();
            string pp=JsonConvert.SerializeObject(dt);
            return Json(pp,JsonRequestBehavior.AllowGet);
        }
    }
}