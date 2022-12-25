namespace Zip.Installments.DAL.Interfaces
{
    /// <summary>
    ///     The repository wrapper for the facade interface.
    /// </summary>
    public interface IRepositoryWrapper
    {
        /// <summary>
        ///     An instance of order repository
        /// </summary>
        IOrdersRepository OrdersRepository { get; }

        /// <summary>
        ///     To save the db call
        /// </summary>
        /// <returns>Returns a task</returns>
        Task Save();
    }
}
