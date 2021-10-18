using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FloraSoft.Cps.UserManager.DAL
{
    public class DataAccess
    {
        public List<TTarget> ReadList<TTarget>(string sql, CommandType commandType,
                          params SqlParameter[] Parameters) where TTarget : new()
        {
            IDataReader reader = ExecuteReader(sql, commandType, Parameters);
            List<TTarget> list = new List<TTarget>();
            while (reader.Read())
            {
                TTarget obj = new TTarget();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetValue(i) != DBNull.Value)
                    {
                        SetPropertyValue(obj, reader.GetName(i), reader.GetValue(i));
                    }
                }
                list.Add(obj);
            }
            reader.Close();
            return list;
        }
        public TTarget ReadSingle<TTarget>(string sql, CommandType commandType,
                          params SqlParameter[] Parameters) where TTarget : new()
        {
            TTarget obj = new TTarget();
            IDataReader reader = ExecuteReader(sql, commandType, Parameters);
            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetValue(i) != DBNull.Value)
                    {
                        SetPropertyValue(obj, reader.GetName(i), reader.GetValue(i));
                    }
                }
            }
            reader.Close();
            return obj;
        }

        public IDataReader ExecuteReader(string sql, CommandType CommandType, params SqlParameter[] Parameters)
        {
            SqlConnection connection = new SqlConnection(AppVariables.ConStr);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType;
            command.CommandText = sql;
            if (Parameters != null)
                command.Parameters.AddRange(Parameters);

            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int ExecuteNonQuery(string sql, CommandType CommandType, params SqlParameter[] Parameters)
        {
            SqlConnection connection = new SqlConnection(AppVariables.ConStr);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType;
            command.CommandText = sql;
            command.Parameters.AddRange(Parameters);
            int retval = -1;
            try
            {
                connection.Open();
                retval = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch
            {

            }
            return retval;
        }

        public DataTable GetdataTable(string sql, CommandType CommandType, params SqlParameter[] Parameters) //shanchoy changed
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(AppVariables.ConStr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType;
            command.CommandText = sql;
            if (Parameters != null)
            {
                command.Parameters.AddRange(Parameters);
            }
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dataTable);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch
            {

            }
            return dataTable;
        }   

        public SqlParameter CreateParameter(string ParameterName, SqlDbType ParameterType)
        {
            SqlParameter p = new SqlParameter(ParameterName, ParameterType);
            return p;
        }

        public SqlParameter CreateParameter(string ParameterName, SqlDbType ParameterType, int Size)
        {
            SqlParameter p = new SqlParameter(ParameterName, ParameterType, Size);
            return p;
        }

        public SqlParameter CreateOutputParameter(string ParameterName, SqlDbType ParameterType, int Size)
        {
            SqlParameter p = new SqlParameter(ParameterName, ParameterType, Size);
            p.Direction = ParameterDirection.Output;
            return p;
        }

        public SqlParameter CreateParameter(string ParameterName, object ParameterValue)
        {
            SqlParameter p = new SqlParameter(ParameterName, ParameterValue);
            return p;
        }

        public SqlParameter CreateParameter(string ParameterName)
        {
            SqlParameter p = this.CreateParameter(ParameterName);
            return p;
        }

        public static void SetPropertyValue(object target, string propertyName, object value)
        {
            PropertyInfo propertyInfo =
                target.GetType().GetProperty(propertyName.ToString());
            if (value == null)
                propertyInfo.SetValue(target, value, null);
            else
            {
                Type pType =
                GetPropertyType(propertyInfo.PropertyType);
                Type vType =
                    GetPropertyType(value.GetType());
                if (pType.Equals(vType))
                {
                    // types match, just copy value
                    propertyInfo.SetValue(target, value, null);
                }
            }
        }

        private static Type GetPropertyType(Type propertyType)
        {
            Type type = propertyType;
            if (type.IsGenericType &&
                (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                return Nullable.GetUnderlyingType(type);
            return type;
        }

        public static PropertyInfo[] GetSourceProperties(Type sourceType)
        {
            List<PropertyInfo> result = new List<PropertyInfo>();
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(sourceType);
            foreach (PropertyDescriptor item in props)
                if (item.IsBrowsable)
                    result.Add(sourceType.GetProperty(item.Name));
            return result.ToArray();
        }

        internal SqlParameter CreateOutputParameter(string ParameterName, SqlDbType ParameterType)
        {
            SqlParameter p = new SqlParameter(ParameterName, ParameterType);
            p.Direction = ParameterDirection.Output;
            return p;
        }

        internal SqlParameter CreateImageOutputParameter(string ParameterName, SqlDbType ParameterType)
        {
            SqlParameter p = new SqlParameter(ParameterName, ParameterType);
            p.Direction = ParameterDirection.Output;
            p.Value = null;
            p.Size = 15000;
            return p;
        }
    }
}