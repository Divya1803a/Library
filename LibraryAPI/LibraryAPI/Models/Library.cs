using System;

namespace LibraryAPI.Models
{
    public class Library
    {
        public Library()
        {

        }
        // Constructor
        // public Student(int sId, string name, string gender, DateTime dob, string c)
        // {
        //     Console.WriteLine($"{sId} {name} {gender} {dob}");
        //     StudentId = sId;
        //     Name = name;
        //     Gender = gender;
        //     DOB = dob;
        //     City = c;
        // }

        public int BookID { get; set; }

        public string BookName { get; set; }

        public int BookPrice { get; set; }

        public int CategoryID { get; set; }

        public int AuthorsID { get; set; }

      
        public static bool IsBooksValid(Library ns, out string errMsg)
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

        public static bool IsBookIDValid(string strBookID, out int BookID, out string errMsg)
        {
            // ******************************************************
            // Validation
            // Check StudentId is not string, > 0 && < 999
            // ******************************************************
            bool res = Int32.TryParse(strBookID, out BookID);
            if (!res)  //res == false  //res == true
            {
                errMsg = "Invalid Input. Please input a valid StudentId";
                return false;
            }

            // Check if StudentId > 0
            if (BookID <= 0)
            {
                errMsg = "Book Id should be greater than 0";
                return false;
            }

            if (BookID > 999)
            {
                errMsg = "Book Id should be less than 999";
                return false;
            }
            errMsg = "";
            return true;
        }
    }

}