using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

/// <summary>
///Sample 的摘要说明
/// </summary>
public class Sample
{
    public Sample()
    {
    }

    #region 实时水情模块
    /// <summary>
    ///获取水位信息
    /// </summary>
    /// <returns></returns>
    public static string showWaterInfo(string type)
    {
        List<WaterInfo> listSite = DBConnection.getWaterInfos(type);
        string resInfo = ConvertToJson(listSite);
        return resInfo;
    }
    /// <summary>
    /// 站点水位信息
    /// </summary>
    /// <param name="type"></param>
    /// <param name="SiteNum"></param>
    /// <returns></returns>
    public static string showSiteWaterHisInfos(string type, int SiteNum)
    {
        List<WaterInfo> listSite = DBConnection.getWaterHisInfo(type, SiteNum);
        string resInfo = ConvertToJson(listSite);
        return resInfo;
    }
    #endregion

    #region 台风路径模块
    /// <summary>
    /// //获取台风基本信息
    /// </summary>
    /// <returns></returns>
    public static string showWindbasicInfo()
    {
        List<WindInfoDTO> listWindBasicInfo = DBConnection.ConnectSQLwind_basicinfo();
        string resInfo = ConvertToJson(listWindBasicInfo);
        return resInfo;
    }

    /// <summary>
    /// 获取台风预测信息
    /// </summary>
    /// <param name="winid"></param>
    /// <returns></returns>
    public static string showWindForcastInfo(int winid)
    {
        List<WindForecastDTO> listWindForecastInfo = DBConnection.ConnectSQLwindForecastInfo(winid);
        string resInfo = ConvertToJson(listWindForecastInfo);
        return resInfo;
    }

    /// <summary>
    /// 获取台风详细信息
    /// </summary>
    /// <param name="winid"></param>
    /// <returns></returns>
    public static string showWindDetailInfo(int winid)
    {

        List<WindDetailInfoDTO> listWindDetailInfo = DBConnection.ConnectSQLwindDetailInfo(winid);
        string resInfo = ConvertToJson(listWindDetailInfo);
        return resInfo;
    }
    #endregion

    #region 降雨等值线模块
    /// <summary>
    /// 等值线分析功能关系数据库配置
    /// </summary>
    public static string ConnectionString = ConfigurationManager.ConnectionStrings["ContourConnectionString"].ToString();
    public struct rfInfo
    {
        public string STCD;         //站点号
        public double Lon;          //经度
        public double Lat;          //纬度
        public double Rainfall;     //实际雨量
    }

    /// <summary>
    /// 降雨等值线分析入口
    /// </summary>
    /// <param name="paraStr"></param>
    /// <returns></returns>
    public static string AnalyseRun(string paraStr)
    {
        string m_path = null;
        if (!String.IsNullOrEmpty(paraStr))
        {
            List<string> para = new List<string>();
            string[] paraList = paraStr.Split('&');
            if (paraList != null && paraList.Length > 0)
            {
                for (int i = 0; i < paraList.Length; i++)
                {
                    para.Add(paraList[i].Split('=')[1]);
                }

                if (para[para.Count - 1].Equals("0"))  //单日
                {
                    m_path = DayContourService(para[0], para[1], para[2], para[3]);
                }

                if (para[para.Count - 1].Equals("1"))  //累积
                {
                    m_path = AccDayContourService(para[0], para[1], para[2], para[3], para[4]);
                }

            }
        }
        return m_path;
    }

    /// <summary>
    ///  单日等值线分析入口
    /// </summary>
    /// <param name="gap">雨量间隔</param>
    /// <param name="startRf">起始雨量</param>
    /// <param name="endRf">中止雨量</param>
    /// <param name="startDay">日期</param>
    /// <returns></returns>
    public static string DayContourService(string gap, string startRf, string endRf, string startDay)
    {
        float gapVal = (float)Convert.ToDouble(gap);
        float startRfVal = (float)Convert.ToDouble(startRf);
        float endRfVal = (float)Convert.ToDouble(endRf);
        DateTime startDayVal = Convert.ToDateTime(startDay);

        string m_path = DayContour(gapVal, startRfVal, endRfVal, startDayVal);
        return m_path;
    }

    /// <summary>
    ///  累积等值线分析入口
    /// </summary>
    /// <param name="gap">雨量间隔</param>
    /// <param name="startRf">起始雨量</param>
    /// <param name="endRf">中止雨量</param>
    /// <param name="startDay">起始日期</param>
    /// <param name="endDay">中止日期</param>
    /// <returns></returns>
    public static string AccDayContourService(string gap, string startRf, string endRf, string startDay, string endDay)
    {
        float gapVal = (float)Convert.ToDouble(gap);
        float startRfVal = (float)Convert.ToDouble(startRf);
        float endRfVal = (float)Convert.ToDouble(endRf);
        DateTime startDayVal = Convert.ToDateTime(startDay);
        DateTime endDayVal = Convert.ToDateTime(endDay);

        string m_path = AccDayContour(gapVal, startRfVal, endRfVal, startDayVal, endDayVal);
        return m_path;
    }

    /// <summary>
    /// 累计雨量等值线分析
    /// </summary>
    /// <param name="gap">雨量间隔</param>
    /// <param name="startRf">起始雨量</param>
    /// <param name="endRf">中止雨量</param>
    /// <param name="startDay">起始日期</param>
    /// <param name="endDay">中止日期</param>
    /// <returns></returns>
    public static string AccDayContour(float gap, float startRf, float endRf, DateTime startDay, DateTime endDay)
    {
        List<rfInfo> data = GetAccRadarRf(startDay, endDay);
        return ConvertToJson(data);
    }

    /// <summary>
    /// 查询累积雨量，获取相关观测点信息
    /// </summary>
    /// <param name="startDate">起始时间</param>
    /// <param name="endDate">中止时间</param>
    /// <returns>站点相关信息列表，包括经纬度以及累积雨量值</returns>
    public static List<rfInfo> GetAccRadarRf(DateTime startDate, DateTime endDate)
    {
        var rfList = new List<rfInfo>();
        var rfoldList = new List<rfInfo>();
        List<DataTable> dsList = GetRfByDates(startDate, endDate);
        if (dsList == null || dsList.Count == 0)
        {
            return null;
        }

        List<decimal> temRainfall = new List<decimal>();
        List<string> stcdlist = new List<string>();
        for (int i = 0; i < dsList.Count; i++)
        {
            for (int j = 0; j < dsList[i].Rows.Count; j++)
            {
                string stcdID = dsList[i].Rows[j].ItemArray[0].ToString();
                if (!stcdlist.Contains(stcdID))
                {
                    stcdlist.Add(stcdID);
                    var temInfo = new rfInfo();
                    temInfo.STCD = stcdID;
                    temInfo.Lon = Convert.ToDouble(dsList[i].Rows[j].ItemArray[1]);
                    temInfo.Lat = Convert.ToDouble(dsList[i].Rows[j].ItemArray[2]);
                    if (Convert.IsDBNull(dsList[i].Rows[j].ItemArray[3]))
                    {
                        temRainfall.Add(0);
                    }
                    else
                    {
                        temRainfall.Add(Math.Round(Convert.ToDecimal(dsList[i].Rows[j].ItemArray[3]), 2));
                    }
                    rfoldList.Add(temInfo);
                }
                else
                {
                    var indexSTCD = stcdlist.IndexOf(stcdID);
                    var oldRainFall = temRainfall[indexSTCD];
                    decimal nowRainFall = 0;
                    if (Convert.IsDBNull(dsList[i].Rows[j].ItemArray[3]))
                    {
                        nowRainFall = 0;
                    }
                    else
                    {
                        nowRainFall = Math.Round(Convert.ToDecimal(dsList[i].Rows[j].ItemArray[3]), 2);
                    }

                    temRainfall[indexSTCD] = oldRainFall + nowRainFall;
                }
            }
        }

        for (int j = 0; j < stcdlist.Count; j++)
        {
            var temInfo = new rfInfo();
            temInfo.STCD = Convert.ToString(rfoldList[j].STCD);
            temInfo.Lon = Convert.ToDouble(rfoldList[j].Lon);
            temInfo.Lat = Convert.ToDouble(rfoldList[j].Lat);
            temInfo.Rainfall = Convert.ToDouble(temRainfall[j]);
            rfList.Add(temInfo);

        }
        return rfList;
    }

    /// <summary>
    /// 单日雨量等值线分析
    /// </summary>
    /// <param name="gap">雨量间隔</param>
    /// <param name="startRf">起始雨量</param>
    /// <param name="endRf">中止雨量</param>
    /// <param name="startDay">日期</param>
    /// <returns></returns>
    public static string DayContour(float gap, float startRf, float endRf, DateTime startDay)
    {

        List<rfInfo> data = GetRadarRf(startDay, startDay);
        return ConvertToJson(data);
    }

    /// <summary>
    /// 获得雷达反演每日雨量数据
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static List<rfInfo> GetRadarRf(DateTime startDate, DateTime endDate)
    {
        var rfList = new List<rfInfo>();
        List<DataTable> dsList = GetRfByDates(startDate, endDate);
        if (dsList != null && dsList.Count > 0)
        {
            foreach (DataRow row in dsList[0].Rows)
            {
                var temInfo = new rfInfo();
                temInfo.STCD = Convert.ToString(row.ItemArray[0]);
                temInfo.Lon = Convert.ToDouble(row.ItemArray[1]);
                temInfo.Lat = Convert.ToDouble(row.ItemArray[2]);

                if (Convert.IsDBNull(row.ItemArray[3]))
                {
                    temInfo.Rainfall = 0;
                }
                else
                {
                    temInfo.Rainfall = Convert.ToDouble(row.ItemArray[3]);
                }

                rfList.Add(temInfo);
            }
        }
        return rfList;
    }

    /// <summary>
    /// 查询某段时间的降雨量,每一天存储一个DataTable
    /// </summary>
    /// <param name="startDate">起始时间</param>
    /// <param name="endDate">中止时间</param>
    /// <returns></returns>
    public static List<DataTable> GetRfByDates(DateTime startDate, DateTime endDate)
    {
        List<DataTable> rfList = new List<DataTable>();
        int days = (endDate.Date - startDate.Date).Days + 1;
        try
        {
            OleDbConnection con = new OleDbConnection(ConnectionString);
            for (int i = 0; i < days; i++)
            {
                string temTime = startDate.AddDays(i).ToString("yyyy/M/d HH:mm:ss");
                string maxTime = startDate.AddDays(i + 1).ToString("yyyy/M/d HH:mm:ss");
                string sqlStr = "SELECT STCD,lgtd  as x,lttd as y,DYP as z FROM st_rain where TM between '" + temTime + "' and '" + maxTime + "'order by STCD";

                OleDbDataAdapter adapter = new OleDbDataAdapter(sqlStr, con);
                DataTable dt = new DataTable();
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rfList.Add(dt);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("RadarRf.GetRfByDates:" + e.Message);
                }
            }
            con.Close();
        }
        catch (Exception exp)
        {
            throw new Exception("RadarRf.GetRfByDates:" + exp.Message);
        }
        return rfList;
    }
    #endregion

    #region 实时雨情模块
    /// <summary>
    /// 雨量信息
    /// </summary>
    /// <param name="StarTime"></param>
    /// <param name="EndTime"></param>
    /// <param name="MixNum"></param>
    /// <param name="MaxNum"></param>
    /// <returns></returns>
    public static string GetRainNums(string StarTime, string EndTime, double MixNum, double MaxNum)
    {
        List<RainInfo> listSite = DBConnection.getRainInfo(StarTime, EndTime, MixNum, MaxNum);
        string resInfo = ConvertToJson(listSite);
        return resInfo;
    }

    /// <summary>
    /// 站点雨量历史信息
    /// </summary>
    /// <param name="SiteNum"></param>
    /// <returns></returns>
    public static string GetSiteRainInfo(string StarTime, string EndTime, int SiteNum)
    {
        List<RainDetailInfo> listSite = DBConnection.getSiteHisRainInfos(StarTime, EndTime, SiteNum);
        string resInfo = ConvertToJson(listSite);
        return resInfo;
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 将对象转换成json返回给前台
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static string ConvertToJson(object obj)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
        string resJson = "";
        using (MemoryStream stream = new MemoryStream())
        {
            json.WriteObject(stream, obj);
            resJson = Encoding.UTF8.GetString(stream.ToArray());
        }
        return resJson;
    }
    #endregion
}