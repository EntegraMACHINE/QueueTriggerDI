using AutoMapper;
using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Context.Entities;
using QueueTriggerDI.Context.Repositories;
using System;
using System.Collections.Generic;

namespace QueueTriggerDI.Context.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IBookContentRepository bookContentRepository;
        private readonly IMapper mapper;

        public BookService(IBookRepository bookRepository, IBookContentRepository bookContentRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.bookContentRepository = bookContentRepository;
            this.mapper = mapper;
        }

        public BookDto AddBook(BookDto bookDto)
        {
            Book book = bookRepository.AddBook(mapper.Map<Book>(bookDto));

            BookContent bookContent = mapper.Map<BookContent>(bookDto);
            bookContent.BookId = book.Id;
            bookContentRepository.AddBookContent(bookContent);

            return bookDto;
        }

        public BookDto GetBook(Guid id)
        {
            BookDto bookDto = mapper.Map<BookDto>(bookRepository.GetBook(id));
            return mapper.Map(bookContentRepository.GetBookContent(id), bookDto);
        }

        public IList<BookDto> GetBooks(string author)
        {
            throw new NotImplementedException();
        }

        public BookDto UpdateBook(BookDto bookDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
