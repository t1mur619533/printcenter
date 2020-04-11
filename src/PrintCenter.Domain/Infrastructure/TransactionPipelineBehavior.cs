using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PrintCenter.Data;

namespace PrintCenter.Domain.Infrastructure
{
    /// <summary>
    /// Adds transaction to the processing pipeline
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ITransaction transaction;

        public TransactionPipelineBehavior(ITransaction transaction)
        {
            this.transaction = transaction;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse result;

            try
            {
                transaction.Begin();

                result = await next();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

            return result;
        }
    }
}