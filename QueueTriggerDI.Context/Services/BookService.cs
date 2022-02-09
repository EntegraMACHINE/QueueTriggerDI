using AutoMapper;
using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Context.Entities;
using QueueTriggerDI.Context.Repositories;
using QueueTriggerDI.Utils.Checkers;
using System;

namespace QueueTriggerDI.Context.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IBookContentRepository bookContentRepository;

        public BookService(IBookRepository bookRepository, IBookContentRepository bookContentRepository)
        {
            this.bookRepository = bookRepository;
            this.bookContentRepository = bookContentRepository;
        }

        public BookDto AddBook(BookDto bookDto)
        {
            Verify.NotNullOrDefault(nameof(bookDto), bookDto);

            Book book = bookRepository.AddBook(
                new Book
                {
                    Id = bookDto.Id,
                    Name = bookDto.Name,
                    Author = bookDto.Author
                });

            if (book == null)
                return null;

            BookContent bookContent = bookContentRepository.AddBookContent(
                new BookContent
                {
                    Id = Guid.NewGuid(),
                    BookId = book.Id,
                    Content = bookDto.Content
                });

            return new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Content = bookContent.Content
            };
        }

        public BookDto GetBookById(Guid id)
        {
            Verify.NotEmpty(nameof(id), id);

            Book book = bookRepository.GetBook(id);

            if (book == null)
                return null;

            BookContent bookContent = bookContentRepository.GetBookContent(id);

            BookDto bookDto = new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Content = bookContent.Content
            };

            return bookDto;
        }

        public BookDto UpdateBook(Guid id, BookDto newBookData)
        {
            Verify.NotEmpty(nameof(id), id);
            Verify.NotNullOrDefault(nameof(newBookData), newBookData);

            Book book = bookRepository.GetBook(id);

            if (book == null)
                return null;

            Book updatedBook = bookRepository.UpdateBook(
                new Book
                {
                    Id = book.Id,
                    Name = newBookData.Name,
                    Author = newBookData.Author
                });

            BookContent updatedBookContent = bookContentRepository.UpdateBookContent(
                new BookContent
                {
                    BookId = id,
                    Content = newBookData.Content
                });

            return new BookDto
            {
                Id = updatedBook.Id,
                Name = updatedBook.Name,
                Author = updatedBook.Author,
                Content = updatedBookContent.Content
            };
        }

        public bool DeleteBook(Guid id)
        {
            Verify.NotEmpty(nameof(id), id);

            Book book = bookRepository.GetBook(id);

            if (book == null)
                return false;

            BookContent bookContent = bookContentRepository.GetBookContent(id);

            bookRepository.DeleteBook(book.Id);

            if (bookContent != null)
                bookContentRepository.DeleteBookContent(book.Id);

            return true;
        }
    }
}
