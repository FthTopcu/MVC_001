using MVC_001.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_001.DataAccess
{
    public class TeacherDAL
    {
        private static TeacherDAL _Methods { get; set; }
        public static TeacherDAL Methods
        {
            get
            {
                if (_Methods == null)
                    _Methods = new TeacherDAL();
                return _Methods;
            }
        }
        public List<Teacher> List()
        {
            string query = "Select * from Teacher";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            return GetTeacherList(cmd);
        }
        public Teacher GetById(int id)
        {
            string query = "Select * from Teacher where Id = @id;";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                return GetTeacherList(cmd)[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static List<Teacher> GetTeacherList(SqlCommand cmd)
        {
            List<Teacher> teacherList = new List<Teacher>();
            IDataReader reader;
            try
            {
                DbTools.Methods.ConnectDB();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teacherList.Add(
                        new Teacher()
                        {
                            ID = reader.GetInt32(0),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString()
                        });
                }
                DbTools.Methods.DisconnectDB();
            }
            catch
            {
                throw;
            }
            return teacherList;
        }

        internal int Update(Teacher teacher)
        {
            string query = "Update Teacher set Name=@name,Surname=@surname where ID=@id;";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            cmd.Parameters.AddWithValue("@name", teacher.Name);
            cmd.Parameters.AddWithValue("@surname", teacher.Surname);
            cmd.Parameters.AddWithValue("@id", teacher.ID);
            return DbTools.Methods.Execute(cmd);
            //            UPDATE table_name
            //SET column1 = value1, column2 = value2, ...
            //WHERE condition;
        }

        public object Add(Teacher teacher)
        {
            string query = "Insert INTO Teacher (Name,Surname) Values (@name,@surname); Select SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            cmd.Parameters.AddWithValue("@name", teacher.Name);
            cmd.Parameters.AddWithValue("@surname", teacher.Surname);

            return DbTools.Methods.Add(cmd);
        }
        public int Delete(int id)
        {
            string query = "Delete from Teacher where ID = @id;";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            cmd.Parameters.AddWithValue("@id", id);
            return DbTools.Methods.Execute(cmd);

        }
    }
}