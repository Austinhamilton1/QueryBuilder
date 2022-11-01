using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using QueryBuilder.Models;

namespace QueryBuilder
{
    public class QueryBuilder : IDisposable
    {
        SqliteConnection connection;

        public QueryBuilder(string locationOfDatabase)
        {
            this.connection = new SqliteConnection("DataSource=" + locationOfDatabase);
            connection.Open();
        }

        public T Read<T>(int id) where T : IClassModel, new()
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name} WHERE Id={id}";
            var reader = command.ExecuteReader();
            T data = new T();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    else
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                }
            }

            return data;
        }

        public List<T> ReadAll<T>() where T : IClassModel, new()
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name}";
            var reader = command.ExecuteReader();
            T data;
            var datas = new List<T>();
            while(reader.Read())
            {
                data = new T();
                for(int i = 0; i < reader.FieldCount; i++)
                {
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    else
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                }
                datas.Add(data);
            }
            return datas;
        }

        public void Create<T>(T obj)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            List<string> values = new List<string>();
            List<string> names = new List<string>();

            foreach(PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                    values.Add($"\"{property.GetValue(obj)}\"");
                else
                    values.Add(property.GetValue(obj).ToString());

                names.Add(property.Name);
                
            }

            string sbValues = string.Join(',', values.ToArray());
            string sbNames = string.Join(',', names.ToArray());

            var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO {typeof(T).Name} ({sbNames}) VALUES ({sbValues})";

            var insert = command.ExecuteNonQuery();
        }

        public void Update<T>(T obj) where T : IClassModel
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            List<string> values = new List<string>();
            List<string> names = new List<string>();

            for(int i = 1; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];

                if (property.PropertyType == typeof(string))
                    values.Add($"\"{property.GetValue(obj)}\"");
                else
                    values.Add(property.GetValue(obj).ToString());

                names.Add(property.Name);
            }

            string[] pairs = new string[values.Count];

            for(int i = 0; i < pairs.Length; i++)
                    pairs[i] = $"{names[i]} = {values[i]}";

            var command = connection.CreateCommand();
            command.CommandText = $"UPDATE {typeof(T).Name} SET {string.Join(',', pairs)} WHERE Id = {properties[0].GetValue(obj)}";
            var update = command.ExecuteNonQuery();
        }

        public void Delete<T>(T obj) where T : IClassModel
        {
            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {typeof(T).Name} WHERE Id = {obj.Id}";
            var delete = command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
