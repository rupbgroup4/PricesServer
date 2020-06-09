using Prices.BLL.Repository_Interfaces;
using Prices.DAL.SQLConnection;
using Prices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prices.BLL.TagEntity
{
    public class SQLTagRepository : ITagRepository
    {
        DBServices db = new DBServices();

        public Item AddTag(Tag tag)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTag(Tag tag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetAllTags()
        {
            //Tag tag = new Tag();
            return (List<Tag>)db.SPGetAll(new Tag());
            //throw new NotImplementedException();
        }

        public Tag GetTagById(string id)
        {
            throw new NotImplementedException();
        }

        public Item UpdateTag(string id, Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}