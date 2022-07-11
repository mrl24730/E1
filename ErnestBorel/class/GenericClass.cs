using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ErnestBorel
{

    /* OUTPUT */
    public class BasicOutput
    {
        public int status = 0;
        public string message = "";
        public Object data;
    }

    #region News
    public class newsObj
    {
        public string news_ref = "";
        public string news_title = "";
    }

    public class articleObj
    {
        public int news_id = 0;
        public string news_ref= "";
        public DateTime date;
        public string displayDate = "";
        public string title = "";
        public string meta = "";
        public string breadcrumb = "";
        public List<string> imageURL = new List<string>();
        public List<string> imageCaption = new List<string>();
        public string captionString = "";
        public string videoURL = "";
        public string tag = "";
        public string ambassador = "";
        public string desc = "";
        public newsObj prev = new newsObj();
        public newsObj next = new newsObj();
        public List<relateNewsObj> relatedNews = new List<relateNewsObj>();
        
    }

    public class relateNewsObj
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
    }

    #endregion

    public class pageObj
    {
        public string title = "";
        public string metaKeyword = "";
        public string metaDesc = "";
    }


    #region watch and collection

    public class collectionObj
    {
        public int idx_collection {get;set;}
        public string type_name { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string col_ref { get; set; }
        public string hero_image { get; set; }
    }

    public class watchObj: pageObj
    {
        public string type = "";
        public string type_lang = "";
        public string col_ref = "";
        public string col_name = "";
        public string idx_watch = "";
        public string spec = "";
        public string image;
        public string breadcrumb = "";
        public List<string> listWatch = new List<string>();
        public List<string> listCollection= new List<string>();
    }

    public class searchObj
    {
        public string type = "";
        public string gender = "";
        public string bracelet = "";
        public string shape = "";
        public string material = "";
        public string cover = "";
        public string keyword = "";
    }

    public class selectorObj
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    #endregion

    #region customer service
    public class locationObj
    {
        public string idx = "";
        public string idx_parent = "";
        public int default_cs = 0;
        public string name = "";
        public decimal lng = 0;
        public decimal lat = 0;
        public int zoom = 0;
        public int zoom_baidu = 0;
    }

    public struct locationOutput
    {
        public DataTable data;
        public List<locationObj> region;
        public List<locationObj> country;
        public List<locationObj> city;
    }
    #endregion

    #region Mobile application
    public struct initHomeImg
    {
        public string iphone { get; set; }
        public string android { get; set; }
    }

    public struct initOutput
    {
        public long ts;
        public List<string> banner;
        public Dictionary<string, dynamic> watches;
        public List<dynamic> news;
        public List<dynamic> families;
    }

    public struct initNews
    {
        public string id;
        public string title;
        public string date;
        public string file; 
    }

    public struct initFamily
    {
        public string id;
        public string desc;
        public string img;
        public string name;
    }

    public struct initWatch
    {
        public string id;
        public string name;
        public string img;
        public string family;
    }

    public struct appWatchOutput
    {
        public long ts;
        public int total;
        public List<appWatchItem> watch;
    }

    public struct appWatchItem
    {
        public string model;
        public List<string> spec;
        public string img;
        public string large;

    }

    public struct appNews
    {
        public long ts;
        public string date;
        public string title;
        public string desc;
        public string source;
        public string author;
        public string img;
    }
    #endregion

    #region for Old Mobile App (3 level only)
    public struct appLocation
    {
        public List<appRegion> region{ get; set; }
        public long ts { get; set; }
        
    }

    public struct appRegion
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<appCountry> country { get; set; }
    }

    public struct appCountry
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<appCity> city { get; set; }
    }
    
    public struct appCity
    {
        public string id { get; set; }
        public string name { get; set; }
        public decimal lng { get; set; }
        public decimal lat { get; set; }
        public int zoom { get; set; }
        public int zoom_baidu { get; set; }
    }
    #endregion


    #region for New Mobile App & Web (unlimted level of location)

    public struct LocationList
    {
        public long ts { get; set; }
        public List<LocationItem> list { get; set; }
    }
    public struct WatchList
    {
        public string idx_watch { get; set; }
        public int idx_collection { get; set; }
        public string watch_matching { get; set; }
        public string watch_gender { get; set; }
        public string watch_bracelet { get; set; }
        public string watch_case { get; set; }
        public string watch_shape { get; set; }
        public string watch_surface1 { get; set; }
        public string watch_surface2 { get; set; }
        public string watch_surface3 { get; set; }
        public decimal price { get; set; }
        public string idx_lang { get; set; }
        public string watch_spec { get; set; }
        public string watch_type { get; set; }
        public string watch_type_lang { get; set; }
        public string col_movement { get; set; }
        public string col_name { get; set; }
        public string col_desc { get; set; }
        public string image_s { get; set; }
        public string image_l{ get; set; }
        public string image_t { get; set; }
    }

    public struct WatchDetail
    {
        public string idx_watch { get; set; }
        public string watch_type { get; set; }
        public string watch_oldmodel { get; set; }
        public string watch_type_lang { get; set; }
        public string watch_spec { get; set; }
        public string watch_gender { get; set; }
        public decimal price { get; set; }
        public object watch_matching { get; set; }
        public int idx_collection { get; set; }
        public string col_name { get; set; }

        public string image_s { get; set; }
        public string image_l { get; set; }
        public string image_t { get; set; }

    }


    public struct Customer
    {
        public int idx_customer { get; set; }
        public string company_name { get; set; }
        public string customer_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string idcard { get; set; }
        public DateTime created_at { get; set; }
    }

    public struct LocationItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public object list { get; set; }
    }

    public struct CityItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public decimal lng { get; set; }
        public decimal lat { get; set; }
        public int zoom { get; set; }
        public int zoom_baidu { get; set; }
    }
    #endregion


}
