using Microsoft.EntityFrameworkCore;

namespace Zip.Installments.DAL.Interfaces
{
    public interface IDbContext
    {
        /// <summary>
        ///     Creates a <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <typeparamref name="TEntity" />.
        /// </summary>
        /// <typeparam name="TEntity"> The type of entity for which a set should be returned. </typeparam>
        /// <returns> A set for the given entity type. </returns>
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;
    }
}
