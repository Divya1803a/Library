using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using LibraryAPITests.Models;


namespace LibraryAPITests
{
    public class LibraryAPIHelper
    {
        HttpClient client = new HttpClient();
        private string url = "http://localhost:5000/Library/";

        public void ShowBooks(Books books)
        {
            Console.WriteLine($"Name: {books.BookName}\tBooks Id: " +
                $"{books.BookID}\tBookPrice: {books.BookPrice}");
        }

        public async Task<Books> CreateBooksAsync(Books newBooks)
        {
            var content = new StringContent(newBooks.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{url}",  content);
            var BooksData = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Books>(BooksData);
            return book;
        }

        public async Task<Books> GetBooksAsync(int id)
        {
            Books Books = null;
            HttpResponseMessage response = await client.GetAsync($"{url}{id}");
            if (response.IsSuccessStatusCode)
            {
                //student = await response.Content.ReadAsAsync<Student>();
                var BooksStrData = await response.Content.ReadAsStringAsync();
                Books = JsonConvert.DeserializeObject<Books>(BooksStrData);
            }
            return Books;
        }

        public async Task<List<Books>> GetBooksAsync()
        {
            List<Books> books = null;
            HttpResponseMessage response = await client.GetAsync($"{url}");
            if (response.IsSuccessStatusCode)
            {
                //student = await response.Content.ReadAsAsync<Student>();
                var BooksStrData = await response.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<List<Books>>(BooksStrData);
            }
            return books;
        }

        public async Task<Books> UpdateBooksAsync(Books updBooks)
        {
            var content = new StringContent(updBooks.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{url}", content);
            var BooksStrData = await response.Content.ReadAsStringAsync();
            var BooksData = JsonConvert.DeserializeObject<Books>(BooksStrData);
            return BooksData;
        }

        public async Task<int> DeleteBooksAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{url}{id}");
            var delData = await response.Content.ReadAsStringAsync();
            var numOfRows = int.Parse(delData);
            return numOfRows;
        }
    }

}