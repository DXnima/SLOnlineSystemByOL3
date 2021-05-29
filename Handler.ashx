<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

using System.IO;
using Newtonsoft.Json;

public class Handler : IHttpHandler
{
    //前台传递过来的json字符对象
    public struct ParaStrObj
    {
        public string paraStr { get; set; }
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string method = System.Web.HttpContext.Current.Request["method"].ToString();
        string res = slOnlineAnalyse(method, context);
        context.Response.Write(res);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public string slOnlineAnalyse(string method, HttpContext context)
    {
        string res = null;
        switch (method)
        {
            case "sssq":    //实时水情
                string sqOper = System.Web.HttpContext.Current.Request["oper"].ToString();
                string sqType = System.Web.HttpContext.Current.Request["type"].ToString();
                switch (sqOper)
                {
                    case "waterInfo":
                        res = Sample.showWaterInfo(sqType);
                        break;
                    case "WaterHisInfo":
                        int siteNum = Convert.ToInt32(System.Web.HttpContext.Current.Request["siteNum"]);
                        res = Sample.showSiteWaterHisInfos(sqType, siteNum);
                        break;
                }
                break;
            case "tflj":    //台风路径
                string tfOper = System.Web.HttpContext.Current.Request["oper"].ToString();
                switch (tfOper)
                {
                    case "tfInfo":
                        res = Sample.showWindbasicInfo();
                        break;
                    case "forcastInfo":
                        int tfID = Convert.ToInt32(System.Web.HttpContext.Current.Request["tfID"]);
                        res = Sample.showWindForcastInfo(tfID);
                        break;
                    case "detailInfo":
                        int tfID2 = Convert.ToInt32(System.Web.HttpContext.Current.Request["tfID"]);
                        res = Sample.showWindDetailInfo(tfID2);
                        break;
                }
                break;
            case "jydzx":       //降雨等值线
                var PostedJsonData = HttpUtility.UrlDecode(new StreamReader(context.Request.InputStream).ReadToEnd());
                ParaStrObj paraStrObj = JavaScriptConvert.DeserializeObject<ParaStrObj>(PostedJsonData);
                string paraStr = paraStrObj.paraStr;
                res = Sample.AnalyseRun(paraStr);
                break;
            case "ssyq":        //实时雨情
                string yqOper = System.Web.HttpContext.Current.Request["oper"].ToString();
                switch (yqOper)
                {
                    case "rainNum":
                        string StarTime = System.Web.HttpContext.Current.Request["s"].ToString();
                        string EndTime = System.Web.HttpContext.Current.Request["e"].ToString();
                        double MixNum = Convert.ToDouble(System.Web.HttpContext.Current.Request["minRain"]);
                        double MaxNum = Convert.ToDouble(System.Web.HttpContext.Current.Request["maxRain"]);
                        res = Sample.GetRainNums(StarTime, EndTime, MixNum, MaxNum);
                        break;
                    case "rainInfo":
                        string StarTime1 = System.Web.HttpContext.Current.Request["s"].ToString();
                        string EndTime1 = System.Web.HttpContext.Current.Request["e"].ToString();
                        int SiteNum = Convert.ToInt32(System.Web.HttpContext.Current.Request["siteNum"]);
                        res = Sample.GetSiteRainInfo(StarTime1, EndTime1, SiteNum);
                        break;
                }
                break;
            default: break;
        }
        return res;
    }
}