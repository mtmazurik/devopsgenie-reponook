using DevopsGenie.Reponook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevopsGenie.Reponook.Services
{
    public interface IRepositoryService
    {
        Task<List<string>> GetDatabases();
        Task<List<string>> GetCollections(string database);
        Task<Repository> Create(string repository, string collection, Repository repoObject);
        Task<Repository> Read(string _id, string repository, string collection);
        Task<List<Repository>> ReadAll(string repository, string collection);
        Task<List<Repository>> QueryByKeyAndTag(string repository, string collection, string key, string tag);
        Task<List<Repository>> QueryByKey(string repository, string collection, string key);
        Task<List<Repository>> QueryByTag(string repository, string collection, string tag);
        Task Update(string repository, string collection, Repository repoObject);
        Task Delete(string repository, string collection, string _id);
    }
}