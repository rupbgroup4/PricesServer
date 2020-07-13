using Newtonsoft.Json;
using Prices.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Prices.DAL.SQLConnection
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        #region Constractors
        public DBServices()
        {
            //
            // TODO: Add constructor logic here
            //

        }
        #endregion

        #region This method inserts a Receipt to receipts_tbl table 
        //public int InsertReceiptToDB(Receipt receipt)
        //{
        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        //con = connect("DBConnectionString"); // create the connection
        //        con = new DBConnectionBuilder().Connect("DBConnectionString"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    String cStr = BuildInsertCommand(receipt);      // helper method to build the insert string

        //    //cmd = CreateCommand(cStr, con);             // create the command
        //    cmd = new DBCommandBuilder().CreateCommand(cStr, con); // create the command

        //    try
        //    {
        //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
        //        return numEffected;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //        // write to log
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }

        //}
        public int InsertToDB<T>(T type)
        {
            SqlConnection con;
            SqlCommand cmd;

            #region DBConnectionString

            try
            {
                con = new DBConnectionBuilder().Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            #endregion

            string spName = new DBCommandBuilder().SPBuildInsertCommand(type, out Dictionary<string, string> parameters);      // helper method to build the SP Insert

            cmd = new DBCommandBuilder().SPCreateCommand(spName, con, parameters); // create the SP command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        #endregion

        #region Build the Insert command String
        //private String BuildInsertCommand(Receipt receipt)
        //{
        //    String command;
        //    //////
        //    //StringBuilder sb = new StringBuilder();
        //    // use a string builder to create the dynamic string
        //    //sb.AppendFormat("Values('{0}', '{1}' ,{2}, {3}, {4})", airport.Int_id, airport.Code, airport.Lat, airport.Lon,airport.Name);
        //    //String prefix = "INSERT INTO Airports_2020 " + "(int_id, code, lat, lon, name) ";
        //    //command = prefix + sb.ToString();
        //    /////
        //    command = "";
        //    //command = "SET IDENTITY_INSERT Airports_2020 ON ";
        //    //command += $"INSERT INTO Airports_2020 (int_id, code, lat, lon,name) " +
        //    //           $"values ({airport.Int_id}, '{airport.Code}', {airport.Lat}, {airport.Lon}, '{airport.Name}')";
        //    //command += "SET IDENTITY_INSERT Airports_2020 OFF ";

        //    return command;
        //}
        //private String BuildInsertCommand<T>(T type)
        //{
        //    String command;
        //    //////
        //    //StringBuilder sb = new StringBuilder();
        //    // use a string builder to create the dynamic string
        //    //sb.AppendFormat("Values('{0}', '{1}' ,{2}, {3}, {4})", airport.Int_id, airport.Code, airport.Lat, airport.Lon,airport.Name);
        //    //String prefix = "INSERT INTO Airports_2020 " + "(int_id, code, lat, lon, name) ";
        //    //command = prefix + sb.ToString();
        //    /////
        //    if (type is Receipt)
        //    {
        //        command = "";
        //        //command = "SET IDENTITY_INSERT Airports_2020 ON ";
        //        //command += $"INSERT INTO Airports_2020 (int_id, code, lat, lon,name) " +
        //        //           $"values ({airport.Int_id}, '{airport.Code}', {airport.Lat}, {airport.Lon}, '{airport.Name}')";
        //        //command += "SET IDENTITY_INSERT Airports_2020 OFF ";
        //    }
        //    else
        //    {
        //        command = "";
        //    }



        //    return command;
        //}

        #endregion

        #region Read Items using a DataSet --Not In Use!--
        public DBServices GetItemsDataSet()
        {
            SqlConnection con = null;
            try
            {
                //con = connect("DBConnectionString");
                con = new DBConnectionBuilder().Connect("DBConnectionString");
                da = new SqlDataAdapter("SELECT... FROM...", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                // write errors to log file
                // try to handle the error
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }


            return this;

        }

        #endregion

        #region Get items using data reader
        //public List<Item> GetAllItems()
        //{
        //    List<Item> itemsList = new List<Item>();
        //    SqlConnection con = null;

        //    try
        //    {
        //        //con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        //        con = new DBConnectionBuilder().Connect("DBConnectionString"); //create a connection to the database using the connection String defined in the web config file
        //        String selectSTR = "SELECT * FROM items_tbl";

        //        SqlCommand cmd = new SqlCommand(selectSTR, con);

        //        // get a reader
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

        //        while (dr.Read())
        //        {
        //            // Read till the end of the data into a row
        //            Item item = new Item();
        //            item.Item_id = (string)dr["item_id"];
        //            item.Receipt_id = (string)dr["receipt_id"];
        //            item.Item_title = (string)dr["item_title"];
        //            item.Price = (double)dr["price"];
        //            item.Discount_dollar = (double)dr["discount_dollar"];
        //            item.Discount_percent = (double)dr["discount_percent"];
        //            item.Item_Description = (string)dr["item_description"];
        //            item.User_id = (string)dr["user_id"];
        //            item.Item_image = (string)dr["item_image"];
        //            item.Id_type = (string)dr["id_type"];
        //            //if ((bool)dr["item tags"])
        //            //{
        //            //    item.Tag_id.Add((int)dr["item tags"]);
        //            //}
        //            itemsList.Add(item);

        //        }
        //        return itemsList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }
        //}
        //public List<Item> SPGetAllItems()
        //{
        //    List<Item> itemsList = new List<Item>();
        //    SqlConnection con = null;

        //    try
        //    {
        //        con = new DBConnectionBuilder().Connect("DBConnectionString"); //create a connection to the database using the connection String defined in the web config file
        //        string selectSTR = "SPItems";
        //        Dictionary<string, string> parameters = new Dictionary<string, string> { { "@StatementType", "select" } };
        //        SqlCommand cmd = new DBCommandBuilder().SPCreateCommand(selectSTR, con, parameters);

        //        // get a reader
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

        //        while (dr.Read())
        //        {
        //            // Read till the end of the data into a row
        //            Item item = new Item();
        //            item.Item_id = (string)dr["item_id"];
        //            item.Receipt_id = (string)dr["receipt_id"];
        //            item.Item_title = (string)dr["item_title"];
        //            item.Price = (double)dr["price"];
        //            item.Discount_dollar = (double)dr["discount_dollar"];
        //            item.Discount_percent = (double)dr["discount_percent"];
        //            item.Item_Description = (string)dr["item_description"];
        //            item.User_id = (string)dr["user_id"];
        //            item.Item_image = (string)dr["item_image"];
        //            item.Id_type = (string)dr["id_type"];

        //            itemsList.Add(item);
        //        }
        //        return itemsList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }
        //}

        public IList SPGetAll<T>(T type)
        {
            //IList<T> list;
            SqlConnection con = null;

            try
            {
                con = new DBConnectionBuilder().Connect("DBConnectionString"); //create a connection to the database using the connection String defined in the web config file
                string spName = "NUN";
                //if (type is Item)
                //{
                //    spName = "SPItems";
                //}
                //else 
                if (type is Receipt)
                {
                    spName = "SPReceipts";
                }
                else if (type is Tag)
                {
                    spName = "SPTags";
                }
                else if (type is Store)
                {
                    spName = "SPStores";
                }
                else if (type is Category)
                {
                    spName = "SPCategory";
                }
                else if (type is SubCategory)
                {
                    spName = "SPSubCategory";
                }

                Dictionary<string, string> parameters = new Dictionary<string, string> { { "@StatementType", "select" } };
                SqlCommand cmd = new DBCommandBuilder().SPCreateCommand(spName, con, parameters);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                //if (type is Item)
                //{

                //    List<Item> list = new List<Item>();
                //    while (dr.Read())
                //    {
                //        Item item = new Item();
                //        // Read till the end of the data into a row
                //        item.Item_id = (string)dr["item_id"];
                //        item.Receipt_id = (string)dr["receipt_id"];
                //        item.Item_title = (string)dr["item_title"];
                //        item.Price = (double)dr["price"];
                //        item.Discount_dollar = (double)dr["discount_dollar"];
                //        item.Discount_percent = (double)dr["discount_percent"];
                //        item.Item_Description = (string)dr["item_description"];
                //        item.User_id = (string)dr["user_id"];
                //        item.Item_image = (string)dr["item_image"];
                //        item.Id_type = (string)dr["id_type"];

                //        list.Add(item);
                //    }
                //    return list;
                //}
                //else 
                if (type is Receipt)
                {

                    List<Receipt> list = new List<Receipt>();
                    while (dr.Read())
                    {
                        Receipt receipt = new Receipt();
                        // Read till the end of the data into a row
                        receipt.Receipt_id = (string)dr["receipt_id"];
                        receipt.User_id = (string)dr["user_id"];
                        receipt.Date = (DateTime)dr["date"];
                        receipt.Receipt_Description = (string)dr["receipt_description"];
                        receipt.Discount_dollar = (double)dr["discount_dollar"];
                        receipt.Discount_percent = (double)dr["discount_percent"];
                        receipt.Receipt_image = (string)dr["receipt_image"];
                        receipt.Store.Store_id = (string)dr["store_id"];

                        list.Add(receipt);
                    }
                    return list;
                }
                else if (type is Tag)
                {
                    List<Tag> list = new List<Tag>();
                    while (dr.Read())
                    {
                        Tag tag = new Tag();
                        // Read till the end of the data into a row
                        tag.Tag_id = (string)dr["tag_id"];
                        tag.Tag_title = (string)dr["tag_title"];

                        list.Add(tag);
                    }
                    return list;
                }
                else if (type is Store)
                {
                    List<Store> list = new List<Store>();
                    while (dr.Read())
                    {
                        Store store = new Store();
                        // Read till the end of the data into a row
                        store.Store_id = (string)dr["store_id"];
                        store.Store_name = (string)dr["store_name"];
                        store.Lat = (double)dr["lat"];
                        store.Lon = (double)dr["lon"];

                        list.Add(store);
                    }
                    return list;
                }
                else if (type is Category)
                {
                    List<Category> list = new List<Category>();
                    while (dr.Read())
                    {
                        Category category = new Category();
                        // Read till the end of the data into a row
                        category.Category_id = (string)dr["category_id"];
                        category.Category_title = (string)dr["category_title"];

                        list.Add(category);
                    }
                    return list;
                }
                else if (type is SubCategory)
                {
                    List<SubCategory> list = new List<SubCategory>();
                    while (dr.Read())
                    {
                        SubCategory subCategory = new SubCategory();
                        // Read till the end of the data into a row
                        subCategory.Sub_category_id = (string)dr["sub_category_id"];
                        subCategory.Sub_category_title = (string)dr["sub_category_title"];

                        list.Add(subCategory);
                    }
                    return list;
                }

                return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //SPGetItemById() { 


        //}

        //SPGetStoreById() {

        //}

        public IList SPGetById<T>(T type, string selectType, string id)
        {
            //IList<T> list;
            SqlConnection con = null;

            try
            {
                con = new DBConnectionBuilder().Connect("DBConnectionString"); //create a connection to the database using the connection String defined in the web config file
                string spName = "NUN";
                Dictionary<string, string> parameters = new Dictionary<string, string> { { "@StatementType", selectType } };
                if (type is Item)
                {
                    spName = "SPItems";
                    parameters.Add("@item_id", id);
                }
                else if (type is Tag)
                {
                    spName = "SPItemsTags";
                    if (selectType == "SelectByItemId")
                    {
                        parameters.Add("@item_id", id);
                    }
                    else if (selectType == "SelectByTagId")
                    {
                        parameters.Add("@tag_id", id);
                    }
                }
                else if (type is Receipt)
                {
                    spName = "SPReceipts";
                    parameters.Add("@receipt_id", id);
                }
                else if (type is Store)
                {
                    spName = "SPStores";
                    parameters.Add("@store_id", id);
                }
                else if (type is User)
                {
                    spName = "SPUsers";
                    parameters.Add("@user_id", id);
                }

                SqlCommand cmd = new DBCommandBuilder().SPCreateCommand(spName, con, parameters);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (type is Item)
                {
                    Item item = type as Item;
                    List<Item> list = new List<Item>();
                    while (dr.Read())
                    {
                        // Read till the end of the data into a row
                        item.Item_id = (string)dr["item_id"];
                        item.Receipt_id = (string)dr["receipt_id"];
                        item.Receipt_image = (string)dr["receipt_image"];
                        item.Item_title = (string)dr["item_title"];
                        item.Price = (double)dr["price"];
                        item.Discount_dollar = (double)dr["discount_dollar"];
                        item.Discount_percent = (double)dr["discount_percent"];
                        item.Item_Description = (string)dr["item_description"];
                        item.User_id = (string)dr["user_id"];
                        item.Item_image = (string)dr["item_image"];
                        item.Id_type = (string)dr["id_type"];

                        list.Add(item);
                    }
                    return list;
                }
                else if (type is Tag)
                {

                    List<Tag> list = new List<Tag>();
                    while (dr.Read())
                    {
                        Tag tag = new Tag();
                        // Read till the end of the data into a row
                        tag.Tag_id = (string)dr["tag_id"];
                        tag.Tag_title = (string)dr["tag_title"];
                        list.Add(tag);
                    }
                    return list;
                }
                else if (type is Receipt)
                {

                    List<Receipt> list = new List<Receipt>();
                    while (dr.Read())
                    {
                        Receipt receipt = new Receipt();
                        // Read till the end of the data into a row
                        receipt.Receipt_id = (string)dr["receipt_id"];
                        receipt.User_id = (string)dr["user_id"];
                        receipt.Date = (DateTime)dr["date"];
                        receipt.Receipt_Description = (string)dr["receipt_description"];
                        receipt.Discount_dollar = (double)dr["discount_dollar"];
                        receipt.Discount_percent = (double)dr["discount_percent"];
                        receipt.Receipt_image = (string)dr["receipt_image"];
                        receipt.Store.Store_id = (string)dr["store_id"];

                        list.Add(receipt);
                    }
                    return list;
                }
                else if (type is Store)
                {

                    List<Store> list = new List<Store>();
                    while (dr.Read())
                    {
                        Store store = new Store();
                        // Read till the end of the data into a row
                        store.Store_id = (string)dr["store_id"];
                        store.Store_name = (string)dr["store_name"];
                        store.Lat = (double)dr["lat"];
                        store.Lon = (double)dr["lon"];
                        list.Add(store);
                    }
                    return list;
                }
                else if (type is User)
                {

                    List<User> list = new List<User>();
                    while (dr.Read())
                    {
                        User user = new User();
                        // Read till the end of the data into a row
                        user.User_id = (string)dr["user_id"];
                        user.First_name = (string)dr["first_name"];
                        user.Last_name = (string)dr["last_name"];
                        user.Password = (string)dr["password"];
                        user.Birthdate = (DateTime)dr["birthdate"];
                        user.Gender = (bool)dr["gender"];
                        user.State = (string)dr["state"];
                        user.City = (string)dr["city"];
                        user.User_rank = (int)dr["user_rank"];
                        list.Add(user);
                    }
                    return list;
                }
                return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public IList SPGetResults<T>(Search<T> search)
        {
            //IList<T> list;
            SqlConnection con = null;

            try
            {
                con = new DBConnectionBuilder().Connect("DBConnectionString"); //create a connection to the database using the connection String defined in the web config file
                string spName = "NUN";

                Dictionary<string, string> parameters = new Dictionary<string, string> { { "@StatementType", search.Statement_Type } };

                if (search.Model is Item)
                {
                    int user_rank = (10 + ((search.User.User_rank - 1000) / 15));//how many results to show
                    user_rank = user_rank > 10 ? user_rank : 10;
                    //user_rank >= 1000 ? user_rank= user_rank:user_rank = 1000;
                    spName = "SPItems";
                    parameters.Add("@user_lat", search.User.Lat.ToString());
                    parameters.Add("@user_lon", search.User.Lon.ToString());
                    parameters.Add("@max_distance", search.Distance_radius.ToString());
                    parameters.Add("@max_price", search.Max_price.ToString());
                    parameters.Add("@min_price", search.Min_price.ToString());
                    parameters.Add("@user_rank", user_rank.ToString());
                }
                #region for later

                //else if (search.Model is Tag)
                //{
                //    spName = "SPItemsTags";
                //    if (selectType == "SelectByItemId")
                //    {
                //        parameters.Add("@item_id", id);
                //    }
                //    else if (selectType == "SelectByTagId")
                //    {
                //        parameters.Add("@tag_id", id);
                //    }
                //}
                //else if (search.Model is Receipt)
                //{
                //    spName = "SPReceipts";
                //    parameters.Add("@receipt_id", id);
                //}
                //else if (search.Model is Store)
                //{
                //    spName = "SPStores";
                //    parameters.Add("@store_id", id);
                //}
                #endregion
                SqlCommand cmd = new DBCommandBuilder().SPCreateCommand(spName, con, parameters);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (search.Model is Item)
                {

                    List<Item> list = new List<Item>();
                    while (dr.Read())
                    {
                        Item item = new Item();
                        // Read till the end of the data into a row
                        item.Item_id = (string)dr["item_id"];
                        item.Receipt_id = (string)dr["receipt_id"];
                        item.Receipt_image = (string)dr["receipt_image"];
                        item.Receipt_description = (string)dr["receipt_description"];
                        item.Receipt_discount_dollar = (double)dr["receipts_discount_dollar"];
                        item.Receipt_discount_percent = (double)dr["receipts_discount_percent"];
                        item.Item_title = (string)dr["item_title"];
                        item.Price = (double)dr["price"];
                        item.Discount_dollar = (double)dr["discount_dollar"];
                        item.Discount_percent = (double)dr["discount_percent"];
                        item.Item_Description = (string)dr["item_description"];
                        item.User_id = (string)dr["user_id"];
                        item.Item_image = (string)dr["item_image"];
                        item.Id_type = (string)dr["id_type"];
                        item.Store_name = (string)dr["store_name"];
                        item.Store_lat = Convert.ToString(dr["lat"]);
                        item.Store_lon = Convert.ToString(dr["lon"]);
                        item.Distance = (double)dr["distance"];
                        item.User_rank = Convert.ToString(dr["user_rank"]);
                        list.Add(item);
                    }
                    return list;
                }

                #region for later


                //else if (search.Model is Tag)
                //{

                //    List<Tag> list = new List<Tag>();
                //    while (dr.Read())
                //    {
                //        Tag tag = new Tag();
                //        // Read till the end of the data into a row
                //        tag.Tag_id = (int)dr["tag_id"];

                //        list.Add(tag);
                //    }
                //    return list;
                //}
                //else if (search.Model is Receipt)
                //{

                //    List<Receipt> list = new List<Receipt>();
                //    while (dr.Read())
                //    {
                //        Receipt receipt = new Receipt();
                //        // Read till the end of the data into a row
                //        receipt.Receipt_id = (string)dr["receipt_id"];
                //        receipt.User_id = (string)dr["user_id"];
                //        receipt.Date = (DateTime)dr["date"];
                //        receipt.Receipt_Description = (string)dr["receipt_description"];
                //        receipt.Discount_dollar = (double)dr["discount_dollar"];
                //        receipt.Discount_percent = (double)dr["discount_percent"];
                //        receipt.Receipt_image = (string)dr["receipt_image"];
                //        receipt.Store.Store_id = (string)dr["store_id"];

                //        list.Add(receipt);
                //    }
                //    return list;
                //}
                //else if (search.Model is Store)
                //{

                //    List<Store> list = new List<Store>();
                //    while (dr.Read())
                //    {
                //        Store store = new Store();
                //        // Read till the end of the data into a row
                //        store.Store_id = (string)dr["store_id"];
                //        store.Store_name = (string)dr["store_name"];
                //        store.Lat = (double)dr["lat"];
                //        store.Lon = (double)dr["lon"];
                //        list.Add(store);
                //    }
                //    return list;
                //}
                #endregion

                return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region Update
        public void SPUpdate<T>(T type)
        {
            SqlConnection con = null;

            try
            {
                con = new DBConnectionBuilder().Connect("DBConnectionString"); //create a connection to the database using the connection String defined in the web config file
                string spName = "NUN";

                Dictionary<string, string> parameters = new Dictionary<string, string> { { "@StatementType", "Update" } };
                if (type is User)
                {
                    User u = type as User;
                    spName = "SPUsers";
                    parameters.Add("@user_id", u.User_id);
                    parameters.Add("@user_rank", u.User_rank.ToString());
                }
                #region for later
                //if (type is Item)
                //{
                //    int user_rank = (10 + ((search.User.User_rank - 1000) / 15));
                //    user_rank = user_rank > 10 ? user_rank : 10;
                //    //user_rank >= 1000 ? user_rank= user_rank:user_rank = 1000;
                //    spName = "SPItems";
                //    parameters.Add("@user_lat", search.User.Lat.ToString());
                //    parameters.Add("@user_lon", search.User.Lon.ToString());
                //    parameters.Add("@max_distance", search.Distance_radius.ToString());
                //    parameters.Add("@max_price", search.Max_price.ToString());
                //    parameters.Add("@min_price", search.Min_price.ToString());
                //    parameters.Add("@user_rank", user_rank.ToString());
                //}
                //else if (search.Model is Tag)
                //{
                //    spName = "SPItemsTags";
                //    if (selectType == "SelectByItemId")
                //    {
                //        parameters.Add("@item_id", id);
                //    }
                //    else if (selectType == "SelectByTagId")
                //    {
                //        parameters.Add("@tag_id", id);
                //    }
                //}
                //else if (search.Model is Receipt)
                //{
                //    spName = "SPReceipts";
                //    parameters.Add("@receipt_id", id);
                //}
                //else if (search.Model is Store)
                //{
                //    spName = "SPStores";
                //    parameters.Add("@store_id", id);
                //}
                #endregion
                SqlCommand cmd = new DBCommandBuilder().SPCreateCommand(spName, con, parameters);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void SPUpdateUserProfile(User user2Update)
        {
            SqlConnection con = null;

            try
            {
                con = new DBConnectionBuilder().Connect("DBConnectionString"); //create a connection to the database using the connection String defined in the web config file
                string spName = "SPUsers";

                Dictionary<string, string> parameters = new Dictionary<string, string> {
                    { "@StatementType", "UpdateProfile" },
                    { "@user_id", user2Update.User_id},
                    { "@field2Update",user2Update.Field2update}
                };
                switch (user2Update.Field2update)
                {
                    case "name":
                        parameters.Add("@first_name", user2Update.First_name.ToString());
                        parameters.Add("@last_name", user2Update.Last_name.ToString());
                        break;
                    case "password":
                        parameters.Add("@password", user2Update.Password.ToString());
                        break;
                    case "birthdate":
                        parameters.Add("@birthdate", user2Update.Birthdate.ToString());
                      //parameters.Add("@birthdate", user2Update.Birthdate.ToString());

                        break;
                    case "gender":
                        parameters.Add("@gender", user2Update.Gender.ToString());
                        break;
                    case "state":
                        parameters.Add("@state", user2Update.State.ToString());
                        break;
                    case "city":
                        parameters.Add("@city", user2Update.City.ToString());
                        break;
                    default:
                        break;
                }
                SqlCommand cmd = new DBCommandBuilder().SPCreateCommand(spName, con, parameters);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        //public void update()
        //{
        //    da.Update(dt);
        //}

        #endregion

    }
}