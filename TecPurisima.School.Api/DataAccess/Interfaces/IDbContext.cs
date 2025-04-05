using System.Data.Common;
namespace TecPurisima.School.Api.DataAccess.Interfaces;

public interface IDbContext
{
    DbConnection Connection { get; }
    
}