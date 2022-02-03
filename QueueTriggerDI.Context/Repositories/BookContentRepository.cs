using Dapper;
using QueueTriggerDI.Context.Entities;
using System;
using System.Data;

namespace QueueTriggerDI.Context.Repositories
{
    public class BookContentRepository : IBookContentRepository
    {
        private readonly IDapperRepository<BookContent> dapperRepository;

        public BookContentRepository(IDapperRepository<BookContent> dapperRepository)
        {
            this.dapperRepository = dapperRepository;
        }

        private const string SELECT_PROCEDURE_NAME = "SelectBookContent";
        private const string INSERT_PROCEDURE_NAME = "InsertBookContent";
        private const string UPDATE_PROCEDURE_NAME = "UpdateBookContent";
        private const string DELETE_PROCEDURE_NAME = "DeleteBookContent";

        public BookContent GetBookContent(Guid bookId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("BookId", bookId, DbType.Guid);

            return dapperRepository.QuerySingle(SELECT_PROCEDURE_NAME, parameters);
        }

        public BookContent AddBookContent(BookContent bookContent)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", Guid.NewGuid(), DbType.Guid);
            parameters.Add("BookId", bookContent.BookId, DbType.Guid);
            parameters.Add("Content", bookContent.Content);

            return dapperRepository.QuerySingle(INSERT_PROCEDURE_NAME, parameters);
        }

        public BookContent UpdateBookContent(BookContent bookContent)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("BookId", bookContent.BookId, DbType.Guid);
            parameters.Add("Content", bookContent.Content);

            return dapperRepository.QuerySingle(UPDATE_PROCEDURE_NAME, parameters);
        }

        public void DeleteBookContent(Guid bookId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("BookId", bookId, DbType.Guid);

            dapperRepository.Execute(DELETE_PROCEDURE_NAME, parameters);
        }
    }
}
