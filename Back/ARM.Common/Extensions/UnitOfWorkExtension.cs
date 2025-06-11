using ARM.Core.Abstractions.UOW;

namespace ARM.Common.Extensions;


public static class UnitOfWorkExtension
{
    public static async Task<TResult> StartTransactionAsync<TResult>(this IUnitOfWork unitOfWork, Func<Task<TResult>> action)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var result = await action();
            await unitOfWork.CommitTransactionAsync();
            
            return result;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}