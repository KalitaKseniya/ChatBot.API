
using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChatBot.Infrastructure.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Permission> Get(bool trackChanges)
                                                => FindAll(trackChanges);

        public Permission GetById(int id, bool trackChanges)
                                       => FindByCondition(p => p.Id == id, trackChanges).FirstOrDefault();
        public Permission GetByName(string name, bool trackChanges)
                                       => FindByCondition(p => p.Name == name, trackChanges).FirstOrDefault();

        public void CreatePermission(Permission permission) => Create(permission);

        public void DeletePermission(Permission permission) => Delete(permission);

        public void UpdatePermission(Permission permission) => Update(permission);


    }
}
