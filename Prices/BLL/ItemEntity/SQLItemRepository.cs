﻿using Prices.BLL.ReceiptEntity;
using Prices.BLL.Repository_Interfaces;
using Prices.DAL.SQLConnection;
using Prices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prices.BLL.ItemEntity
{
    public class SQLItemRepository : IItemRepository
    {
        DBServices db = new DBServices();
        public Item AddItem(Item item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Item item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAllItems()
        {
            List<Item> allItams = new List<Item>();
            //allItams = (List<Item>)db.SPGetAll(new Item());
            Search<Item> search = new Search<Item>()
            {
                User = new User() { Lat = 32.342234, Lon = 34.912419 },
                Distance_radius = 100,
                Model = new Item(),
                Max_price = 1000,
                Min_price = 0,
                Statement_Type = "select"
            };
            allItams = (List<Item>)db.SPGetResults(search);

            //allItams = db.SPGetAll(new Item());
            for (int i = 0; i < allItams.Count; i++)
            {
                allItams[i].Tags = (List<Tag>)db.SPGetById(new Tag(), "SelectByItemId", allItams[i].Item_id);//Add tags for each item
            }
            return allItams;
        }
        public IEnumerable<Item> GetResults(Search<Item> search)
        {
            List<Item> results = (List<Item>)db.SPGetResults(search);
            List<Item> filteredResults = new List<Item>();

            List<string> searchTags = new List<string>();
            foreach (Tag tag in search.Tags)
            {
                searchTags.Add(tag.Tag_id);
            }

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Tags = (List<Tag>)db.SPGetById(new Tag(), "SelectByItemId", results[i].Item_id);//Add tags for each item

                if (results[i].Tags.Count >= searchTags.Count)
                {
                    List<string> resultTags = new List<string>();

                    foreach (Tag tag in results[i].Tags)
                    {
                        resultTags.Add(tag.Tag_id);
                    }
                    if (!searchTags.Except(resultTags).Any())//This checks whether there are any elements in searchTags which aren't in resultTags - and then inverts the result.
                    {
                        filteredResults.Add(results[i]);
                    }
                }
            }
            //return results;
            return filteredResults;
        }

        public Item GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public Item UpdateItem(int id, Item item)
        {
            throw new NotImplementedException();
        }

    }
}