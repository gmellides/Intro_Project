using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    public interface IRepository<T> where T : class
    {
        // TODO xml documentation missing
        // TODO this can be used as your base repository which the other repository interfaces can extend

        /// <summary>
        /// Get all entities from database
        /// </summary>
        /// <returns>List of models from Database</returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Returns an entity with
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>A single entity</returns>
        Task<T> GetEntityByID(int id);

        /// <summary>
        /// Save Entity in database
        /// </summary>
        /// <param name="input">Entity to be saved in DB</param>
        Task SaveEntity(T input);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="input">The entity that needs to be updated</param>
        Task UpdateEntity(T input);
    }
}