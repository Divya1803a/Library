using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using LibraryAPI.Services;


namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                // ******************************************************
                // 2. Run SQL statement
                // ******************************************************
                LibraryService Books = new LibraryService();
                List<Library> books = Books.GetAllBooks();
                if(books.Count <= 0)
                {
                    return NotFound("No books exist");
                }

                // ******************************************************
                // 3. Return Data
                // ******************************************************
                return Ok(books);
            }
            catch (Exception ex)
            {
                // ******************************************************
                // 4. If Exception return 500
                // ******************************************************
                LogError(ex);
                return StatusCode(500, "Unknown Error");
            }
        }

        [HttpGet("{id}")]
        //[HttpGet]
        public IActionResult Get([FromRoute] string id)
        {
            try
            {
                // ******************************************************
                // 1. Validation
                // ******************************************************
                bool res = Library.IsBookIDValid(id, out int bookid, out string errMsg);
                if (!res)  //res == false  //res == true
                {
                    BadRequest(errMsg);
                }

                // ******************************************************
                // 2. Run SQL statement
                // ******************************************************
                LibraryService LibraryService = new LibraryService();
                Library books = LibraryService.GetBooks(bookid);
                if(books == null)
                {
                    return NotFound("Books with Book ID - " + bookid + " does not exist.");
                }

                // ******************************************************
                // 3. Return Data
                // ******************************************************
                return Ok(books);
            }
            catch (Exception ex)
            {
                // ******************************************************
                // 4. If Exception return 500
                // ******************************************************
                LogError(ex);
                return StatusCode(500, "Unknown Error");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Library updBooks)
        {
            try
            {
                //1. Validation
                bool res = Library.IsBooksValid(updBooks, out string errMsg);
                if (res == false)  //!res
                {
                    return BadRequest(errMsg);
                }

                //2. Execute DB
                LibraryService sda = new LibraryService();
                int numRows = sda.UpdateBook(updBooks);
                if (numRows == 0)
                {
                    return BadRequest("Invalid Books. Cannot Insert.");
                }
                //3. Return Data
                return Ok(updBooks);
            }
            catch(Exception ex)
            {
                // 4. If Exception return 500
                LogError(ex);
                return StatusCode(500, "Unknown Error - " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Library newBooks)
        {
            try
            {
                //1. Validation
                bool res = Library.IsBooksValid(newBooks, out string errMsg);
                if (res == false)  //!res
                {
                    return BadRequest(errMsg);
                }

                //2. Execute DB
                LibraryService sda = new LibraryService();
                // int numOfRows = sda.InsertStudent(newStudent);
                int newBookID = sda.InsertBook(newBooks);
                if (newBookID <= 0)
                {
                    return BadRequest("Invalid Books. Cannot Insert.");
                }
                //3. Return Data
                newBooks.BookID = newBookID;
                return Ok(newBooks);
            }
            catch(Exception ex)
            {
                // 4. If Exception return 500
                LogError(ex);
                return StatusCode(500, "Unknown Error - " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]string id)
        {
            try
            {
                // ******************************************************
                // 1. Validation
                // ******************************************************
                bool res = Library.IsBookIDValid(id, out int bookId, out string errMsg);
                if (!res)  //res == false  //res == true
                {
                    BadRequest(errMsg);
                }

                // ******************************************************
                // 2. Run SQL statement
                // ******************************************************
                LibraryService LibraryService = new LibraryService();
                int numOfRows = LibraryService.DeleteBooks(bookId);
                if(numOfRows <= 0)
                {
                    return NotFound("Boook with Book Id - " + bookId + " does not exist.");
                }

                // ******************************************************
                // 3. Return Data
                // ******************************************************
                return Ok(bookId);
            }
            catch (Exception ex)
            {
                // ******************************************************
                // 4. If Exception return 500
                // ******************************************************
                LogError(ex);
                return StatusCode(500, "Unknown Error");
            }
        }

        private void LogError(Exception ex)
        {
            //Do Something to Log an Error
        }
    }
}
