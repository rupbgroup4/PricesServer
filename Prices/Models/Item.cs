using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Prices.Models
{
    public class Item
    {
        #region Private fields
        private string item_id;
        private string receipt_id;
        private string item_title;
        private double price;
        private double discount_dollar;
        private double discount_percent;
        private string item_description;
        private string user_id;
        private string user_rank;
        private string barcode;
        private string id_type;
        private string store_name;
        private string store_lat;
        private string store_lon;
        private double distance;
        private List<Tag> tags;
        private string item_image;
        private Image temp_item_image;

        #endregion

        #region Properties
        public string Item_id { get => item_id; set => item_id = value; }
        public string Receipt_id { get => receipt_id; set => receipt_id = value; }
        public string Item_title { get => item_title; set => item_title = value; }
        public double Price { get => price; set => price = value; }
        public double Discount_dollar { get => discount_dollar; set => discount_dollar = value; }
        public double Discount_percent { get => discount_percent; set => discount_percent = value; }
        public string Item_Description { get => item_description; set => item_description = value; }
        public string User_id { get => user_id; set => user_id = value; }
        public string Id_type { get => id_type; set => id_type = value; }
        public List<Tag> Tags { get => tags; set => tags = value; }
        public string Item_image { get => item_image; set => item_image = value; }
        public string Store_name { get => store_name; set => store_name = value; }
        public string Store_lat { get => store_lat; set => store_lat = value; }
        public string Store_lon { get => store_lon; set => store_lon = value; }
        public string User_rank { get => user_rank; set => user_rank = value; }
        public double Distance { get => distance; set => distance = value; }
        public string Barcode { get => barcode; set => barcode = value; }
        public Image Temp_item_image { get => temp_item_image; set => temp_item_image = value; }

        #endregion

    }
}