using System;
using Newtonsoft.Json;

namespace LibraryAPITests.Models
{
    public class Books
    {
        public int BookID { get; set; }

        public string BookName { get; set; }

        public int BookPrice { get; set; }

        public int CategoryID { get; set; }

        public int AuthorsID { get; set; }

        

        public override string ToString()
        {
            return  JsonConvert.SerializeObject(this);
        }

        public static bool IsBooksValid(Books ns, out string errMsg)
        {
            bool res = IsBookNameValid(ns.BookName, out errMsg);
            if(!res)
            {
                return false;
            }

            errMsg = "";
            return true;
        }

        public static bool IsBookNameValid(string BookName, out string errMsg)
        {
            if (BookName.Length <= 1 || BookName.Trim().Length <= 1)
            {
                errMsg = "Name cannot be empty. Please input a name";
                return false;
            }
            errMsg = "";
            return true;
        }

        public static bool IsBookIDValid(string strBookId, out int BookId, out string errMsg)
        {
            // ******************************************************
            // Validation
            // Check StudentId is not string, > 0 && < 999
            // ******************************************************
            bool res = Int32.TryParse(strBookId, out BookId);
            if (!res)  //res == false  //res == true
            {
                errMsg = "Invalid Input. Please input a valid BookID";
                return false;
            }

            // Check if StudentId > 0
            if (BookId <= (int) LibraryValue.MinValue)
            {
                errMsg = $"Book Id should be greater than {(int) LibraryValue.MinValue}";
                return false;
            }

            if (BookId > (int) LibraryValue.MaxValue)
            {
                errMsg = $"Books Id should be less than {(int) LibraryValue.MaxValue}";
                return false;
            }
            errMsg = "";
            return true;
        }
    }

}