using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaonomi.Net.Core.Data
{
    using Models;
    /// <summary>
    /// Marqueur de dépôt Elasticsearch pour entités dérivées de <see cref="EntityBase"/>.
    /// </summary>
    public interface iElasticRepository<TEntity>
        where TEntity : EntityBase
    {
    }
    /// <summary>
    /// Spécialisation pour la persistance des <see cref="Log"/>.
    /// </summary>
    public interface IElasticLogRepository : iElasticRepository<Log>
    {
    }
}
