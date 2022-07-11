using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ErnestBorel.admin
{

    public enum IR_status {
        published,
        pending,
        withheld
    }

    public enum IR_lang {
        en,
        tc,
        sc,
        fr,
        jp
    }

    public enum IR_save_status
    {
        success,
        error
    }

    public class IR_masterRecord
    {
        public int rowNum;
        public int rec_idx;
        public DateTime ir_releaseDate;
        public DateTime ir_lastUpdated;
        public string ir_title;
        public int ir_status;
        public int ir_langFlag;
        public string ir_langStr;
        public string ir_statusDisplay;
        public List<IR_detailRecord> list;
    }

    public struct IR_detailRecord
    {
        public int rec_idx;
        public int lang;
        public string langStr;
        public int master_idx;
        public string title;
        public string desc;
        public string file;
        public int filesize;
        public string filesizeStr;
    }

    public static class AdminHelper
    {
        public static IR_save_status SaveIRrecords(HttpContext _context, ref IR_masterRecord _ir_RecMaster, ref int _masterRec_idx)
        {
            var request = _context.Request;
            Dictionary<string, string> kvp = new Dictionary<string, string>();
            List<string> langSuffix = new List<string>();
            int langFlag = 0;

            //DBHelper.save_IRRecords();
            //AdminHelper.SaveIRrecords(HttpContext.Current, ref list_irDetails, ref ir_Rec, ref rec_idx);

            foreach (String key in request.Form.AllKeys)
            {
                kvp.Add(key, request[key]);

                if (key.IndexOf("input_lang_") == 0)
                {
                    int langNum = Convert.ToInt16(request[key]) -1;
                    langFlag += (int)Math.Pow(2, (langNum));
                    langSuffix.Add(((IR_lang)langNum).ToString());
                }
            }

            //No release detail?
            if (langFlag == 0) return IR_save_status.error;


            //assemble master record;
            int hh = 0;
            int mm = 0;
            if (!String.IsNullOrEmpty(kvp["spinnerHH"]))
            {
                int.TryParse(kvp["spinnerHH"], out hh);
            }
            if (!String.IsNullOrEmpty(kvp["spinnerMM"]))
            {
                int.TryParse(kvp["spinnerMM"], out mm);
            }

            string dtString = kvp["input_releaseDate"] + " " + hh + ":" + mm;
            
            _ir_RecMaster.ir_releaseDate = DateTime.ParseExact(dtString, "yyyy-MM-dd H:m", null);
            
            _ir_RecMaster.ir_langFlag = langFlag;
            if (_ir_RecMaster.ir_releaseDate > DateTime.Now)
            {
                _ir_RecMaster.ir_status = (int) IR_status.pending;
            }
            else
            {
                _ir_RecMaster.ir_status = (int) IR_status.published;
            }
            if (_masterRec_idx != 0) _ir_RecMaster.rec_idx = _masterRec_idx;

            


            //build detail list
            foreach (string l in langSuffix)
            {
                IR_detailRecord ir_detailRec = new IR_detailRecord();
                if (!String.IsNullOrEmpty (kvp["rec_idx_" + l]) ) ir_detailRec.rec_idx = Convert.ToInt32( kvp["rec_idx_" + l] );
                ir_detailRec.title = kvp["input_title_" + l];
                ir_detailRec.desc = kvp["input_desc_" + l];
                ir_detailRec.master_idx = _masterRec_idx;
                ir_detailRec.lang = Convert.ToInt16(kvp["input_lang_" + l]) - 1;

                if (request.Files["input_file_" + l].ContentLength != 0)
                {
                    ir_detailRec.file = "Investor_" + _ir_RecMaster.rec_idx + "_" + l + ".pdf";
                    ir_detailRec.filesize = request.Files["input_file_" + l].ContentLength;
                }
                else
                {
                    ir_detailRec.file = kvp["org_filename_" + l];
                    ir_detailRec.filesize = Convert.ToInt32(kvp["org_filesize_" + l]);

                }

                _ir_RecMaster.list.Add(ir_detailRec);
                //_list_irDetailRecords.Add(ir_detailRec);
            }


            //save_IRRecords(ref List<IR_detailRecord> _ir_detailList, ref IR_masterRecord _ir_RecMaster)
            DBHelper.save_IRRecords(ref _ir_RecMaster);

            foreach (string file in request.Files)
            {
                HttpPostedFile uploadFile = request.Files[file] as HttpPostedFile;
                

                if (uploadFile.ContentLength == 0) continue;
                string savedFileName = Path.Combine(
                   AppDomain.CurrentDomain.BaseDirectory,
                   "pdf",
                   Path.GetFileName("Investor_" + _ir_RecMaster.rec_idx + "_" + file.Substring(file.Length - 2) + ".pdf"));
                uploadFile.SaveAs(savedFileName);
            }


            _masterRec_idx = _ir_RecMaster.rec_idx;

            return IR_save_status.success;
        }
    }
}