using ASP.NET_Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ASP.NET_Framework.Data
{
    internal class TaskDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ToDoTasks;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<TaskModel> FetchAll()
        {
            List<TaskModel> returnList = new List<TaskModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Tasks]";

                SqlCommand command = new SqlCommand(sqlQuery,connection);

                connection.Open();
                SqlDataReader reader =  command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TaskModel task = new TaskModel();
                        task.Id = reader.GetInt32(0);
                        task.Title = reader.GetString(1);
                        task.Category = reader.GetString(2);
                        task.Description = reader.GetString(3);
                        task.DateTime = reader.GetDateTime(4);
                        task.IsDone = reader.GetBoolean(5);

                        returnList.Add(task);
                    }
                }

                return returnList;
            }

        }

        public TaskModel FetchOne(int Id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Tasks] WHERE Id = @id";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id",System.Data.SqlDbType.Int).Value = Id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                TaskModel task = new TaskModel();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        task.Id = reader.GetInt32(0);
                        task.Title = reader.GetString(1);
                        task.Category = reader.GetString(2);
                        task.Description = reader.GetString(3);
                        task.DateTime = reader.GetDateTime(4);
                        task.IsDone = reader.GetBoolean(5);
                       
                    }
                }
                return task;
            }
        }

        internal int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {              

                string sqlQuery = "DELETE FROM [dbo].[Tasks] WHERE Id = @Id";
                
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.VarChar, 1000).Value = id;
                
                connection.Open();

                int deletedID = command.ExecuteNonQuery();

                return deletedID;
            }
        }
    

        public int CreateOrUpdate(TaskModel taskModel)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "";
                if (taskModel.Id <= 0)
                {
                    sqlQuery = "INSERT INTO [dbo].[Tasks] VALUES (@Title,@Category,@Description,@DateTime,@IsDone)";
                }
                else
                {
                    sqlQuery = "UPDATE [dbo].[Tasks] SET Title = @Title, Category = @Category , Description = @Description , DateTime = @DateTime , IsDone = @IsDone WHERE ID = @Id";
                }


                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.VarChar, 1000).Value = taskModel.Id;
                command.Parameters.Add("@Title",System.Data.SqlDbType.VarChar,1000).Value = taskModel.Title;
                command.Parameters.Add("@Category", System.Data.SqlDbType.VarChar, 1000).Value = taskModel.Category;
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar, 1000).Value = taskModel.Description;
                command.Parameters.Add("@DateTime", System.Data.SqlDbType.DateTime).Value = taskModel.DateTime;
                command.Parameters.Add("@IsDone", System.Data.SqlDbType.Bit, 1).Value = taskModel.IsDone;

                connection.Open();

                int newID = command.ExecuteNonQuery();
             
                return newID;
            }
        }
    }
}