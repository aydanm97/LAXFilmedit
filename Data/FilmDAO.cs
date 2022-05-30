using LAXFilm.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LAXFilm.Data
{
    public class FilmDAO
    {
        private string connectionString = @"Data Source=DESKTOP-GC15CMR;Initial Catalog=Films;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<FilmModel> FetchAll() 
        {
            List<FilmModel> returnList = new List<FilmModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.movie";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FilmModel film = new FilmModel();
                        film.id = reader.GetInt32(0).ToString();
                        film.MovieName = reader.GetString(1);
                        film.Year = reader.GetInt32(2).ToString();
                        film.Creator = reader.GetString(3);
                        film.Rating = reader.GetInt32(4).ToString();
                        film.Actors = reader.GetString(5);
                        film.Genre = reader.GetString(6);
                        returnList.Add(film);
                    }
                }
            }




                return returnList;
        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = ("DELETE FROM dbo.movie WHERE id = @id");

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int,1000).Value = id;
                


                connection.Open();
                int deletedID = command.ExecuteNonQuery();
   
                return deletedID;
            }
        }

        public FilmModel FetchOne(int id)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.movie WHERE id = @id";
                //
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@id",System.Data.SqlDbType.Int).Value=id;
                connection.Open();
               
                FilmModel film = new FilmModel();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        film.id = reader.GetInt32(0).ToString();
                        film.MovieName = reader.GetString(1);
                        film.Year = reader.GetInt32(2).ToString();
                        film.Creator = reader.GetString(3);
                        film.Rating = reader.GetInt32(4).ToString();
                        film.Actors = reader.GetString(5);
                        film.Genre = reader.GetString(6);
                          
                    }
                }
                return film;
            }




            
        }
        public int Create(FilmModel filmModel)
        {
            
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = ("INSERT INTO movie(id, MovieName, Year, Creator, Rating, Actors, Genre) VALUES(@id, @MovieName, @Year, @Creator, @Rating, @Actors, @Genre)");

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = filmModel.id;
                command.Parameters.Add("@MovieName", System.Data.SqlDbType.VarChar, 1000).Value = filmModel.MovieName;
                command.Parameters.Add("@Year", System.Data.SqlDbType.Int).Value = filmModel.Year;
                command.Parameters.Add("@Creator", System.Data.SqlDbType.VarChar, 1000).Value = filmModel.Creator;
                command.Parameters.Add("@Rating", System.Data.SqlDbType.Int).Value = filmModel.Rating;
                command.Parameters.Add("@Actors", System.Data.SqlDbType.VarChar, 1000).Value = filmModel.Actors;
                command.Parameters.Add("@Genre", System.Data.SqlDbType.VarChar, 1000).Value = filmModel.Genre;

          
                connection.Open();
                int newID = command.ExecuteNonQuery();
                command.ExecuteNonQuery();
                connection.Close();
              
                return newID;
            }
        }

    }

}
