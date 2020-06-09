using Prices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prices.BLL.Repository_Interfaces
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(string id);
        Item AddTag(Tag tag);
        Item UpdateTag(string id, Tag tag);
        bool DeleteTag(Tag tag);
    }
}
