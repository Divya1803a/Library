using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using LibraryAPI.Models;


namespace LibraryAPI.Services
{
    public class LibraryService
    {
        string connectionString = "Data Source=.\\SQLExpress;Initial Catalog=Library;Integrated Security=True;";
        public Library GetBooks(int BookID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                string sqlStmt = String.Format("Select BookID, BookName, BookPrice, CategoryID, AuthorsID from Books Where BookID ={0}", BookID);
                //string sqlStmt1 = "Select StudentId, Name, Gender, DOB from Student Where StudentId = " + studentId;
                //string sqlStmt2 = $"Select StudentId, Name, Gender, DOB from Student Where StudentId = {studentId}";
                Console.WriteLine(sqlStmt);
                //Console.WriteLine(sqlStmt1);
                //Console.WriteLine(sqlStmt2);

                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Library s = new Library();
                            s.BookID = reader.GetInt32(0);
                            s.BookName = reader.GetString(1);
                            s.BookPrice = reader.GetInt32(2);
                            s.CategoryID = reader.GetInt32(3);
                            s.AuthorsID = reader.GetInt32(4);

                            // Console.WriteLine("before new");
                            // Student s = new Student(reader.GetInt32(0),
                            //                 reader.GetString(1),
                            //                 reader.GetChar(2),
                            //                 reader.GetDateTime(3));

                            Console.WriteLine(s.BookName);
                            return s;
                        }
                    }
                }
                return null;
            }
        }

        public List<Library> GetAllBooks()
        {
            List<Library> books = new List<Library>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string sqlStmt = String.Format("Select BookID, BookName, BookPrice, CategoryID, AuthorsID from Books");

                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Library s = new Library();
                            s.BookID = reader.GetInt32(0);
                            s.BookName = reader.GetString(1);
                            s.BookPrice = reader.GetInt32(2);
                            s.CategoryID = reader.GetInt32(3);
                            s.AuthorsID = reader.GetInt32(4);

                            books.Add(s);
                        }
                    }
                }
                return books;
            }
        }

        public int InsertBook(Library newBooks)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open Connection
                con.Open();

                // Get SQL Statement
                // string sqlStmt = String.Format(
                // "INSERT INTO [dbo].[Student] ([StudentId],[Name],[DOB],[Gender], [City], [State]) VALUES ({0},'{1}','{2}', '{3}', '{4}', '{5}')",
                //  newStudent.StudentId, newStudent.Name, newStudent.DOB, newStudent.Gender,
                //  newStudent.City, newStudent.State);

                //string sqlStmt = $"INSERT INTO [dbo].[Student] ([StudentId],[Name],[DOB],[Gender], [City], [State]) VALUES ({newStudent.StudentId},'{newStudent.Name}','{newStudent.DOB}', '{newStudent.Gender}', '{newStudent.City}', '{newStudent.State}')";

                string sqlStmt = $"INSERT INTO [dbo].[Books] ([BookName],[BookPrice], [CategoryID], [AuthorsID]) OUTPUT INSERTED.BookID VALUES ('{newBooks.BookName}', {newBooks.BookPrice}, {newBooks.CategoryID}, {newBooks.AuthorsID})";

                Console.WriteLine(sqlStmt);

                // Execute Statement
                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    int newBookID = (int) command.ExecuteScalar();
                    //int numOfRows = command.ExecuteNonQuery();
                    return newBookID;
                }
            }
        }

        public int UpdateBook(Library updBooks)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open Connection
                con.Open();

                // Get SQL Statement
                string sqlStmt = @$"UPDATE [dbo].[Books] SET
                    BookName = '{updBooks.BookName}',
                    BookPrice = {updBooks.BookPrice},
                    CategoryID = {updBooks.CategoryID},
                    AuthorsID = {updBooks.AuthorsID}
                    Where BookID = {updBooks.BookID}";

                Console.WriteLine(sqlStmt);

                // Execute Statement
                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    int numOfRows = command.ExecuteNonQuery();
                    return numOfRows;
                }
            }
        }

        public int DeleteBooks(int bookID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open Connection
                con.Open();

                // Get SQL Statement
                string sqlStmt = $"DELETE FROM [dbo].[Books] WHERE BookID = {bookID}";

                Console.WriteLine(sqlStmt);

                // Execute Statement
                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    int numOfRows = command.ExecuteNonQuery();
                    return numOfRows;
                }
            }
        }
    }
}