using Dapper;
using QueueTriggerDI.Context.Entities;
using System;
using System.Data;

namespace QueueTriggerDI.Context.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IDapperRepository<Book> dapperRepository;

        public BookRepository(IDapperRepository<Book> dapperRepository)
        {
            this.dapperRepository = dapperRepository;
        }

        private const string SELECT_PROCEDURE_NAME = "SelectBook";
        private const string INSERT_PROCEDURE_NAME = "InsertBook";
        private const string UPDATE_PROCEDURE_NAME = "UpdateBook";
        private const string DELETE_PROCEDURE_NAME = "DeleteBook";

        public Book GetBook(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);

            return dapperRepository.QuerySingle(SELECT_PROCEDURE_NAME, parameters);
        }

        public Book AddBook(Book book)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", Guid.NewGuid(), DbType.Guid);
            parameters.Add("Name", book.Name, DbType.String);
            parameters.Add("Author", book.Author, DbType.String);

            return dapperRepository.QuerySingle(INSERT_PROCEDURE_NAME, parameters);
        }

        public Book UpdateBook(Book book)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", book.Id, DbType.Guid);
            parameters.Add("Name", book.Name, DbType.String);
            parameters.Add("Author", book.Author, DbType.String);

            return dapperRepository.QuerySingle(UPDATE_PROCEDURE_NAME, parameters);
        }

        public Guid DeleteBook(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);

            dapperRepository.Execute(DELETE_PROCEDURE_NAME, parameters);

            return id;
        }
    }
}
