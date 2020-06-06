﻿using Prices.BLL.Repository_Interfaces;
using Prices.BLL.UserEntity;
using Prices.DAL.SQLConnection;
using Prices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prices.BLL.ReceiptEntity
{
    public class SQLReceiptRepository : IReceiptRepository
    {
        DBServices db = new DBServices();
        public Receipt AddReceipt(Receipt receipt)
        {
            if (receipt.Store.Store_name != null)
            {
                receipt.Store.Store_id = Guid.NewGuid().ToString("N");
                db.InsertToDB(receipt.Store);//insert new store
            }
            receipt.Receipt_id = Guid.NewGuid().ToString("N");
            db.InsertToDB(receipt);//insert receipt

            for (int i = 0; i < receipt.Items.Count; i++)//insert each item
            {
                receipt.Items[i].Item_id = Guid.NewGuid().ToString("N");
                receipt.Items[i].Receipt_id = receipt.Receipt_id;
                receipt.Items[i].User_id = receipt.User_id;
                receipt.Items[i].Id_type = "UserUser";
                db.InsertToDB(receipt.Items[i]);
                for (int j = 0; j < receipt.Items[i].Tags.Count; j++)//insert each tag for each item
                {
                    if (receipt.Items[i].Tags[j].Tag_title != null)//insert new tag to tags_tbl
                    {
                        receipt.Items[i].Tags[j].Tag_id = Guid.NewGuid().ToString("N");
                        db.InsertToDB(receipt.Items[i].Tags[j]);
                        receipt.Items[i].Tags[j--].Tag_title = null;
                    }
                    else// insert to items_tags_tbl
                    {
                        receipt.Items[i].Tags[j].Item_id = receipt.Items[i].Item_id;
                        db.InsertToDB(receipt.Items[i].Tags[j]);
                    }
                }
            }
            User user = new User();
            user.User_id = receipt.User_id;
            user.UpdateUserRank(receipt);
            db.SPUpdate(user);
            return receipt;
        }

        public bool DeleteReceipt(Receipt receipt)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Receipt> GetAllReceipts()
        {
            Console.WriteLine("hi");
            return new List<Receipt> { };
            //throw new NotImplementedException();
        }

        public Receipt GetReceiptById(string id)
        {
            throw new NotImplementedException();
        }
        public Receipt UpdateReceipt(string id, Item receipt)
        {
            throw new NotImplementedException();
        }
    }
}