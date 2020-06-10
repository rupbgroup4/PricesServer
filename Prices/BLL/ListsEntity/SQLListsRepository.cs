using Prices.BLL.Repository_Interfaces;
using Prices.DAL.SQLConnection;
using Prices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prices.BLL.ListsEntity
{
    public class SQLListsRepository : IListsRepository
    {
        DBServices db = new DBServices();

        public IEnumerable<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategory> GetAllSubCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return (List<Tag>)db.SPGetAll(new Tag());
        }
    }
}