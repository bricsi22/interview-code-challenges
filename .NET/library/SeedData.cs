﻿using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi
{
    public class SeedData
    {
        public static void SetInitialData()
        {
            var ernestMonkjack = new Author
            {
                Name = "Ernest Monkjack"
            };
            var sarahKennedy = new Author
            {
                Name = "Sarah Kennedy"
            };
            var margaretJones = new Author
            {
                Name = "Margaret Jones"
            };
            var josephAlbahari = new Author
            {
                Name = "Joseph Albahari"
            };
            var robertCMartin = new Author
            {
                Name = "Robert C. Martin"
            };

            var clayBook = new Book
            {
                Title = "The Importance of Clay",
                Format = BookFormat.Paperback,
                Author = ernestMonkjack,
                ISBN = "1305718181",
                Preserved = false,
                RowVersion = new byte[] { 1 }
            };

            var agileBook = new Book
            {
                Title = "Agile Project Management - A Primer",
                Format = BookFormat.Hardback,
                Author = sarahKennedy,
                ISBN = "1293910102",
                Preserved = false,
                RowVersion = new byte[] { 1 }
            };

            var rustBook = new Book
            {
                Title = "Rust Development Cookbook",
                Format = BookFormat.Paperback,
                Author = margaretJones,
                ISBN = "3134324111",
                Preserved = false,
                RowVersion = new byte[] { 1 }
            };

            var csharpInANutShell = new Book
            {
                Title = "C# in a Nutshell",
                Format = BookFormat.Paperback,
                Author = josephAlbahari,
                ISBN = "1098147448",
                Preserved = false,
                RowVersion = new byte[] { 1 }
            };

            var cleanCode = new Book
            {
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                Format = BookFormat.Paperback,
                Author = robertCMartin,
                ISBN = "9780132350884",
                Preserved = false,
                RowVersion = new byte[] { 1 }
            };
            

            var daveSmith = new Borrower
            {
                Name = "Dave Smith",
                EmailAddress = "dave@smithy.com"
            };

            var lianaJames = new Borrower
            {
                Name = "Liana James",
                EmailAddress = "liana@gmail.com"
            };

            var richardBaldauf = new Borrower
            {
                Name = "Richard Baldauf",
                EmailAddress = "baldauf.richard22@gmail.com"
            };

            var bookOnLoanUntilToday = new BookStock {
                Book = clayBook,
                OnLoanTo = daveSmith,
                LoanEndDate = DateTime.Now.Date
            };

            var bookNotOnLoan = new BookStock
            {
                Book = clayBook,
                OnLoanTo = null,
                LoanEndDate = null
            };

            var bookOnLoanUntilNextWeek = new BookStock
            {
                Book = agileBook,
                OnLoanTo = lianaJames,
                LoanEndDate = DateTime.Now.Date.AddDays(7)
            };

            var rustBookStock = new BookStock
            {
                Book = rustBook,
                OnLoanTo = null,
                LoanEndDate = null
            };

            var csharpInNutshellBookStock = new BookStock
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Book = csharpInANutShell,
                OnLoanTo = richardBaldauf,
                LoanEndDate = DateTime.Now.Date.AddDays(-5)
            };

            var cleanCodeBookStock = new BookStock
            {
                Book = cleanCode,
                OnLoanTo = richardBaldauf,
                LoanEndDate = DateTime.Now.Date.AddDays(10)
            };

            using (var context = new LibraryContext())
            {
                context.Authors.Add(ernestMonkjack);
                context.Authors.Add(sarahKennedy);
                context.Authors.Add(margaretJones);
                context.Authors.Add(josephAlbahari);
                context.Authors.Add(robertCMartin);


                context.Books.Add(clayBook);
                context.Books.Add(agileBook);
                context.Books.Add(rustBook);
                context.Books.Add(csharpInANutShell);
                context.Books.Add(cleanCode);

                context.Borrowers.Add(daveSmith);
                context.Borrowers.Add(lianaJames);
                context.Borrowers.Add(richardBaldauf);

                context.Catalogue.Add(bookOnLoanUntilToday);
                context.Catalogue.Add(bookNotOnLoan);
                context.Catalogue.Add(bookOnLoanUntilNextWeek);
                context.Catalogue.Add(rustBookStock);
                context.Catalogue.Add(csharpInNutshellBookStock);
                context.Catalogue.Add(cleanCodeBookStock);

                context.SaveChanges();

            }
        }
    }
}
