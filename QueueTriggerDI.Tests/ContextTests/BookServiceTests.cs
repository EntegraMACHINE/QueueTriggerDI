using Moq;
using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Context.Entities;
using QueueTriggerDI.Context.Repositories;
using QueueTriggerDI.Context.Services;
using System;
using Xunit;

namespace QueueTriggerDI.Tests.ContextTests
{
    public class BookServiceTests
    {
        private readonly BookService _systemUnderTest;

        private readonly Mock<IBookRepository> _bookRepositoryMock = new Mock<IBookRepository>();
        private readonly Mock<IBookContentRepository> _bookContentRepositoryMock = new Mock<IBookContentRepository>();

        public BookServiceTests()
        {
            _systemUnderTest = new BookService(_bookRepositoryMock.Object, _bookContentRepositoryMock.Object);
        }

        [Fact]
        public void AddBook_ShouldReturnNewBook_WhenBookWasAdded()
        {
            //Arrange
            BookDto bookDto = new BookDto
            {
                Id = Guid.NewGuid(),
                Name = "TestBookName",
                Author = "TestBookAuthor",
                Content = "TestBookContent"
            };

            _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>())).Returns(
                new Book
                {
                    Id = bookDto.Id,
                    Name = bookDto.Name,
                    Author = bookDto.Author
                });
            _bookContentRepositoryMock.Setup(x => x.AddBookContent(It.IsAny<BookContent>())).Returns(
                new BookContent
                {
                    Id = Guid.NewGuid(),
                    BookId = bookDto.Id,
                    Content = bookDto.Content
                });

            //Act
            BookDto addedBook = _systemUnderTest.AddBook(bookDto);

            //Assert
            Assert.Equal(bookDto.Name, addedBook.Name);
            Assert.Equal(bookDto.Author, addedBook.Author);
            Assert.Equal(bookDto.Content, addedBook.Content);
        }

        [Fact]
        public void AddBook_ShouldReturnNull_WhenBookWasNotAdded()
        {
            //Arrange
            BookDto bookDto = new BookDto
            {
                Id = Guid.NewGuid(),
                Name = "TestBookName",
                Author = "TestBookAuthor",
                Content = "TestBookContent"
            };

            _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>())).Returns(() => null);

            //Act
            BookDto updatedBook = _systemUnderTest.AddBook(bookDto);

            //Assert
            Assert.Null(updatedBook);
        }

        [Fact]
        public void GetBookById_ShouldReturnBook_WhenBookExists()
        {
            //Arrange
            Guid bookId = Guid.NewGuid();
            Book book = new Book
            {
                Id = bookId,
                Name = "TestBookName",
                Author = "TestBookAuthor"
            };
            BookContent bookContent = new BookContent
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                Content = "TestBookContent"
            };

            _bookRepositoryMock.Setup(x => x.GetBook(bookId)).Returns(book);
            _bookContentRepositoryMock.Setup(x => x.GetBookContent(bookId)).Returns(bookContent);

            //Act
            BookDto bookDto = _systemUnderTest.GetBookById(bookId);

            //Assert
            Assert.Equal(bookId, bookDto.Id);
            Assert.Equal(bookContent.Content, bookDto.Content);
        }

        [Fact]
        public void GetBookById_ShouldReturnNull_WhenBookDoesNotExists()
        {
            //Arrange
            _bookRepositoryMock.Setup(x => x.GetBook(It.IsAny<Guid>())).Returns(() => null);

            //Act
            BookDto bookDto = _systemUnderTest.GetBookById(Guid.NewGuid());

            //Assert
            Assert.Null(bookDto);
        }

        [Fact]
        public void UpdateBook_ShouldReturnUpdatedBookDto_WhenBookSuccessfullyUpdated()
        {
            //Arrange
            BookDto bookDto = new BookDto
            {
                Id = Guid.NewGuid(),
                Name = "UpdatedTestBookName",
                Author = "UpdatedTestBookAuthor",
                Content = "UpdatedTestBookContent"
            };

            _bookRepositoryMock.Setup(x => x.GetBook(bookDto.Id)).Returns(
                new Book
                {
                    Id = bookDto.Id,
                    Name = "NameBeforeUpdate",
                    Author = "AuthorBeforeUpdate"
                });

            Book bookAfterUpdate = new Book
            {
                Id = bookDto.Id,
                Name = bookDto.Name,
                Author = bookDto.Author
            };

            BookContent contentAfterUpdate = new BookContent
            {
                BookId = bookDto.Id,
                Content = bookDto.Content
            };

            _bookRepositoryMock.Setup(x => x.UpdateBook(It.IsAny<Book>())).Returns(bookAfterUpdate);
            _bookContentRepositoryMock.Setup(x => x.UpdateBookContent(It.IsAny<BookContent>())).Returns(contentAfterUpdate);

            //Act
            BookDto updatedBook = _systemUnderTest.UpdateBook(bookDto.Id, bookDto);

            //Assert
            Assert.Equal(bookDto.Name, updatedBook.Name);
            Assert.Equal(bookDto.Author, updatedBook.Author);
            Assert.Equal(bookDto.Content, updatedBook.Content);
        }

        [Fact]
        public void UpdateBook_ShouldReturnNull_WhenBookDoesNotExists()
        {
            //Arrange
            BookDto bookDto = new BookDto
            {
                Id = Guid.NewGuid(),
                Name = "TestBookName",
                Author = "TestBookAuthor",
                Content = "TestBookContent"
            };

            _bookRepositoryMock.Setup(x => x.GetBook(bookDto.Id)).Returns(() => null);

            //Act
            BookDto updatedBook = _systemUnderTest.UpdateBook(bookDto.Id, bookDto);

            //Assert
            Assert.Null(updatedBook);
        }

        [Fact]
        public void DeleteBook_ShouldReturnTrue_WgenBookDeleted()
        {
            //Arrange
            Guid bookId = Guid.NewGuid();
            Book book = new Book
            {
                Id = bookId,
                Name = "TestBookName",
                Author = "TestBookAuthor"
            };
            BookContent bookContent = new BookContent
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                Content = "TestBookContent"
            };

            _bookRepositoryMock.Setup(x => x.GetBook(bookId)).Returns(book);
            _bookContentRepositoryMock.Setup(x => x.GetBookContent(bookId)).Returns(bookContent);

            _bookRepositoryMock.Setup(x => x.DeleteBook(bookId)).Returns(true);

            //Act
            bool result = _systemUnderTest.DeleteBook(bookId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteBook_ShouldReturnFalse_WhenBookDoesNotExists()
        {
            //Arrange
            _bookRepositoryMock.Setup(x => x.GetBook(It.IsAny<Guid>())).Returns(() => null);

            //Act
            bool result = _systemUnderTest.DeleteBook(Guid.NewGuid());

            //Assert
            Assert.False(result);
        }
    }
}
