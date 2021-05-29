using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
///DBConnection 的摘要说明
///@author fmm 2015-06-10
/// </summary>
public class DBConnection
{
    public DBConnection()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    protected static void ConnectSQL(SqlConnection conn)
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        else if (conn.State == ConnectionState.Broken)
        {
            conn.Close();
            conn.Open();
        }
    }

    #region 实时水情
    /// <summary>
    /// 获取水位信息
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<WaterInfo> getWaterInfos(string type)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["GXSLSql"]);        //数据库连接对象
        ConnectSQL(conn);
        List<WaterInfo> List_WaterInfos = new List<WaterInfo>();
        string starSql = "";
        switch (type)
        {
            case "Rver":
                {
                    starSql = "select st_rsvr_r.STCD , st_rsvr_r.RZ, st_rsvr_r.INQ,st_rsvr_r.OTQ,st_rsvr_r.W,st_rsvr_r.TM,st_sitinfo_b.站名,st_sitinfo_b.东经,st_sitinfo_b.北纬, st_sitinfo_b.地址 from st_rsvr_r  INNER JOIN st_sitinfo_b on st_rsvr_r.STCD=st_sitinfo_b.站码 where (st_rsvr_r.RZ>0 and st_rsvr_r.TM='2006-05-10 08:00:00.000')";
                    try
                    {
                        SqlCommand cmd = null;
                        SqlDataAdapter da = null;
                        DataSet ds = null;
                        DataTable dt = null;
                        cmd = new SqlCommand(starSql, conn);
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds, "ds");
                        dt = ds.Tables[0];
                        WaterInfo tmp;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            tmp = new WaterInfo();
                            DataRow row = dt.Rows[i];
                            tmp.SiteNum = (Convert.IsDBNull(row["STCD"])) ? 0 : (int)row["STCD"];
                            tmp.SiteName = (Convert.IsDBNull(row["站名"])) ? "" : (string)row["站名"];
                            tmp.SitePntX = (Convert.IsDBNull(row["东经"])) ? "0" : (string)(row["东经"]);// Convert.ToDouble(row["东经"]);
                            tmp.SitePntY = (Convert.IsDBNull(row["北纬"])) ? "0" : (string)(row["北纬"]);
                            tmp.TM = (Convert.IsDBNull(row["TM"])) ? DateTime.Now : (DateTime)row["TM"];
                            tmp.tm = (Convert.IsDBNull(row["TM"])) ? DateTime.Now.ToLongDateString().ToString() : ((DateTime)row["TM"]).ToLongDateString().ToString();
                            tmp.WaterPos = (Convert.IsDBNull(row["RZ"])) ? "0" : (row["RZ"]).ToString();
                            tmp.FlowNum = (Convert.IsDBNull(row["INQ"])) ? "0" : (row["INQ"]).ToString();
                            tmp.NorNum = (Convert.IsDBNull(row["OTQ"])) ? "0" : (row["OTQ"]).ToString();
                            tmp.WarnNum = (Convert.IsDBNull(row["W"])) ? "0" : (row["W"]).ToString();
                            tmp.SiteAddress = (Convert.IsDBNull(row["地址"])) ? "" : (string)row["地址"];
                            List_WaterInfos.Add(tmp);
                        }

                    }
                    catch
                    {
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();

                    }
                    break;
                }
            case "river":
                {
                    starSql = "select st_river_r.STCD , st_river_r.Z, st_river_r.Q,st_river_r.TM,st_sitinfo_b.站名,st_sitinfo_b.东经,st_sitinfo_b.北纬, st_sitinfo_b.地址 from st_river_r  INNER JOIN st_sitinfo_b on st_river_r.STCD=st_sitinfo_b.站码 where (st_river_r.Z>0 and st_river_r.TM='2006-05-08  08:00:00.000')";
                    try
                    {
                        SqlCommand cmd = null;
                        SqlDataAdapter da = null;
                        DataSet ds = null;
                        DataTable dt = null;
                        cmd = new SqlCommand(starSql, conn);
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds, "ds");
                        dt = ds.Tables[0];
                        WaterInfo tmp;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            tmp = new WaterInfo();
                            DataRow row = dt.Rows[i];
                            tmp.SiteNum = (Convert.IsDBNull(row["STCD"])) ? 0 : (int)row["STCD"];
                            tmp.SiteName = (Convert.IsDBNull(row["站名"])) ? "" : (string)row["站名"];
                            tmp.SitePntX = (Convert.IsDBNull(row["东经"])) ? "0" : (string)(row["东经"]);// Convert.ToDouble(row["东经"]);
                            tmp.SitePntY = (Convert.IsDBNull(row["北纬"])) ? "0" : (string)(row["北纬"]);
                            tmp.TM = (Convert.IsDBNull(row["TM"])) ? DateTime.Now : (DateTime)row["TM"];
                            tmp.tm = (Convert.IsDBNull(row["TM"])) ? DateTime.Now.ToLongDateString().ToString() : ((DateTime)row["TM"]).ToLongDateString().ToString();
                            tmp.WaterPos = (Convert.IsDBNull(row["Z"])) ? "0" : (row["Z"]).ToString();
                            //   tmp.FlowNum = (Convert.IsDBNull(row["Q"])) ? "0" : (row["Q"]).ToString();
                            tmp.NorNum = (Convert.IsDBNull(row["Q"])) ? "0" : (row["Q"]).ToString();
                            // tmp.WarnNum = (Convert.IsDBNull(row["W"])) ? "0" : (row["W"]).ToString();
                            tmp.SiteAddress = (Convert.IsDBNull(row["地址"])) ? "" : (string)row["地址"];
                            List_WaterInfos.Add(tmp);
                        }

                    }
                    catch
                    {
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();

                    }
                    break;
                }
        }
        return List_WaterInfos;
    }

    /// <summary>
    /// 获取站点水位信息
    /// </summary>
    /// <param name="SiteNum"></param>
    /// <returns></returns>
    public static List<WaterInfo> getWaterHisInfo(string type, int SiteNum)
    {
        List<WaterInfo> lst_HisInfos = new List<WaterInfo>();
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["GXSLSql"]);        //数据库连接对象
        ConnectSQL(conn);
        string starSql = "";
        switch (type)
        {
            case "river":
                {
                    starSql = "select  st_river_r.STCD , st_river_r.Z, st_river_r.TM,st_sitinfo_b.站名, st_sitinfo_b.地址 from st_river_r  INNER JOIN st_sitinfo_b on st_river_r.STCD=st_sitinfo_b.站码 where (st_river_r.STCD=" + SiteNum + " and st_river_r.TM>='2006-05-08  00:00:00.000' and st_river_r.TM<='2006-05-08  23:00:00.000') ORDER BY st_river_r.TM ";
                    try
                    {
                        SqlCommand cmd = null;
                        SqlDataAdapter da = null;
                        DataSet ds = null;
                        DataTable dt = null;
                        cmd = new SqlCommand(starSql, conn);
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds, "ds");
                        dt = ds.Tables[0];
                        WaterInfo tmp;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            tmp = new WaterInfo();
                            DataRow row = dt.Rows[i];
                            tmp.SiteNum = (Convert.IsDBNull(row["STCD"])) ? 0 : Convert.ToInt32(row["STCD"]);
                            tmp.SiteName = (Convert.IsDBNull(row["站名"])) ? "" : (string)row["站名"];
                            tmp.SiteAddress = (Convert.IsDBNull(row["地址"])) ? "" : (string)row["地址"];
                            tmp.WaterPos = (Convert.IsDBNull(row["Z"])) ? "0" : (row["Z"]).ToString();
                            tmp.TM = (Convert.IsDBNull(row["TM"])) ? DateTime.Now : (DateTime)row["TM"];
                            tmp.tm = (Convert.IsDBNull(row["TM"])) ? DateTime.Now.ToLongDateString().ToString() : ((DateTime)row["TM"]).ToLongTimeString();
                            lst_HisInfos.Add(tmp);
                        }
                    }
                    catch
                    {
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();

                    }
                    break;
                }
            case "Rver":
                {
                    starSql = "select st_rsvr_r.STCD , st_rsvr_r.RZ, st_rsvr_r.TM,st_sitinfo_b.站名, st_sitinfo_b.地址 from st_rsvr_r  INNER JOIN st_sitinfo_b on st_rsvr_r.STCD=st_sitinfo_b.站码 where (st_rsvr_r.STCD=" + SiteNum + " and st_rsvr_r.TM>='2006-05-10 00:00:00.000' and st_rsvr_r.TM<='2006-05-10  23:00:00.000') ORDER BY st_rsvr_r.TM";
                    try
                    {
                        SqlCommand cmd = null;
                        SqlDataAdapter da = null;
                        DataSet ds = null;
                        DataTable dt = null;
                        cmd = new SqlCommand(starSql, conn);
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds, "ds");
                        dt = ds.Tables[0];
                        WaterInfo tmp;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            tmp = new WaterInfo();
                            DataRow row = dt.Rows[i];
                            tmp.SiteNum = (Convert.IsDBNull(row["STCD"])) ? 0 : Convert.ToInt32(row["STCD"]);
                            tmp.SiteName = (Convert.IsDBNull(row["站名"])) ? "" : (string)row["站名"];
                            tmp.SiteAddress = (Convert.IsDBNull(row["地址"])) ? "" : (string)row["地址"];
                            tmp.WaterPos = (Convert.IsDBNull(row["RZ"])) ? "0" : (row["RZ"]).ToString();
                            tmp.TM = (Convert.IsDBNull(row["TM"])) ? DateTime.Now : (DateTime)row["TM"];
                            tmp.tm = (Convert.IsDBNull(row["TM"])) ? DateTime.Now.ToLongDateString().ToString() : ((DateTime)row["TM"]).ToLongTimeString().ToString();
                            lst_HisInfos.Add(tmp);
                        }
                    }
                    catch
                    {
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();

                    }
                    break;
                }
        }
        return lst_HisInfos;
    }
    #endregion

    #region 台风路径
    /// <summary>
    /// 获取台风基本信息
    /// </summary>
    /// <returns></returns>
    public static List<WindInfoDTO> ConnectSQLwind_basicinfo()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["GXSLSql"]);        //数据库连接对象
        ConnectSQL(conn);
        string cmdText = "select * from wind_basicinfo";
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataTable dt = null;
        List<WindInfoDTO> listwindInfoDto = new List<WindInfoDTO>();
        try
        {
            cmd = new SqlCommand(cmdText, conn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "ds");
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                WindInfoDTO windInfoDto = new WindInfoDTO();
                windInfoDto.windid = (Convert.IsDBNull(row["windid"])) ? "" : (string)row["windid"];
                windInfoDto.windname = (Convert.IsDBNull(row["windname"])) ? "" : (string)row["windname"];
                windInfoDto.windeng = (Convert.IsDBNull(row["windeng"])) ? "" : (string)row["windeng"];

                listwindInfoDto.Add(windInfoDto);
            }

        }
        catch
        {
            conn.Close();
        }
        finally
        {
            conn.Close();

        }
        return listwindInfoDto;
    }

    /// <summary>
    /// 获取预测台风详细信息
    /// </summary>
    /// <param name="winID"></param>
    /// <returns></returns>
    public static List<WindForecastDTO> ConnectSQLwindForecastInfo(int winID)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["GXSLSql"]);        //数据库连接对象
        ConnectSQL(conn);
        string cmdText = "select * from wind_forecast where windid= " + winID;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataTable dt = null;
        List<WindForecastDTO> listwindForecastDto = new List<WindForecastDTO>();
        try
        {
            cmd = new SqlCommand(cmdText, conn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "ds");
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                WindForecastDTO windForecastDto = new WindForecastDTO();
                windForecastDto.windid = (Convert.IsDBNull(row["windid"])) ? Convert.ToInt32("") : Convert.ToInt32(row["windid"]);
                windForecastDto.forecast = (Convert.IsDBNull(row["forecast"])) ? "" : (string)row["forecast"];
                windForecastDto.tm = (Convert.IsDBNull(row["tm"])) ? "" : Convert.ToString(Convert.ToDateTime(row["tm"]));
                windForecastDto.jindu = (Convert.IsDBNull(row["jindu"])) ? Convert.ToSingle("") : Convert.ToSingle(row["jindu"]);
                windForecastDto.weidu = (Convert.IsDBNull(row["weidu"])) ? Convert.ToSingle("") : Convert.ToSingle(row["weidu"]);
                windForecastDto.windstrong = (Convert.IsDBNull(row["windstrong"])) ? "" : (string)row["windstrong"];
                windForecastDto.windspeed = (Convert.IsDBNull(row["windspeed"])) ? "" : (string)row["windspeed"];
                windForecastDto.qiya = (Convert.IsDBNull(row["qiya"])) ? "" : (string)row["qiya"];
                windForecastDto.movedirect = (Convert.IsDBNull(row["movedirect"])) ? "" : (string)row["movedirect"];
                windForecastDto.movespeed = (Convert.IsDBNull(row["movespeed"])) ? "" : (string)row["movespeed"];
                listwindForecastDto.Add(windForecastDto);
            }
        }
        catch
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return listwindForecastDto;
    }

    /// <summary>
    /// 获取预测台风详细信息
    /// </summary>
    /// <param name="winID"></param>
    /// <returns></returns>
    public static List<WindDetailInfoDTO> ConnectSQLwindDetailInfo(int winID)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["GXSLSql"]);        //数据库连接对象
        ConnectSQL(conn);
        string cmdText = "select * from wind_info where windid= " + winID;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataTable dt = null;
        List<WindDetailInfoDTO> listwindDetailInfoDto = new List<WindDetailInfoDTO>();
        try
        {
            cmd = new SqlCommand(cmdText, conn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "ds");
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                WindDetailInfoDTO windDetailInfoDto = new WindDetailInfoDTO();
                windDetailInfoDto.windid = (Convert.IsDBNull(row["windid"])) ? Convert.ToInt32("") : Convert.ToInt32(row["windid"]);
                windDetailInfoDto.tm = (Convert.IsDBNull(row["tm"])) ? "" : Convert.ToString(Convert.ToDateTime(row["tm"]));
                windDetailInfoDto.jindu = (Convert.IsDBNull(row["jindu"])) ? Convert.ToSingle("") : Convert.ToSingle(row["jindu"]);
                windDetailInfoDto.weidu = (Convert.IsDBNull(row["weidu"])) ? Convert.ToSingle("") : Convert.ToSingle(row["weidu"]);
                windDetailInfoDto.windstrong = (Convert.IsDBNull(row["windstrong"])) ? "" : (string)row["windstrong"];
                windDetailInfoDto.windspeed = (Convert.IsDBNull(row["windspeed"])) ? "" : (string)row["windspeed"];
                windDetailInfoDto.qiya = (Convert.IsDBNull(row["qiya"])) ? "" : (string)row["qiya"];
                windDetailInfoDto.movedirect = (Convert.IsDBNull(row["movedirect"])) ? "" : (string)row["movedirect"];
                windDetailInfoDto.movespeed = (Convert.IsDBNull(row["movespeed"])) ? "" : (string)row["movespeed"];
                windDetailInfoDto.sevradius = (Convert.IsDBNull(row["sevradius"])) ? 0 : Convert.ToInt32(row["sevradius"]);
                windDetailInfoDto.tenradius = (Convert.IsDBNull(row["tenradius"])) ? 0 : Convert.ToInt32(row["tenradius"]);
                listwindDetailInfoDto.Add(windDetailInfoDto);
            }
        }
        catch
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return listwindDetailInfoDto;
    }
    #endregion

    #region 实时雨情
    /// <summary>
    /// 获取雨量信息
    /// </summary>
    /// <param name="TimeStar"></param>
    /// <param name="TimeEnd"></param>
    /// <returns></returns>
    public static List<RainInfo> getRainInfo(string TimeStar, string TimeEnd, double MixRain, double MarRain)
    {
        //select st_sitinfo_b.站码 , st_soil_r.Col002,st_soil_r.Col007,st_sitinfo_b.站名,st_sitinfo_b.东经,st_sitinfo_b.北纬, st_sitinfo_b.地址 from st_soil_r  INNER JOIN st_sitinfo_b on st_soil_r.Col001=st_sitinfo_b.站码 where (st_soil_r.Col002<='2008-5-16 17:00:00' and st_soil_r.Col002>='2008-5-16 08:00:00' )
        List<RainInfo> lst_RainInfos = new List<RainInfo>();
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["GXSLSql"]);        //数据库连接对象
        ConnectSQL(conn);
        string starSql = "select st_soil_r.Col001, sum(st_soil_r.Col007) as Col007 from st_soil_r where (st_soil_r.Col002<='" + TimeEnd + "' and st_soil_r.Col002>='" + TimeStar + "')" + " group by st_soil_r.Col001";
        try
        {
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;
            DataSet ds1 = null;
            DataTable dt1 = null;
            cmd = new SqlCommand(starSql, conn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "ds");
            dt = ds.Tables[0];
            RainInfo tmp;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmp = new RainInfo();

                DataRow row = dt.Rows[i];

                float j = Convert.ToSingle(row["Col007"]);
                int l = (Convert.IsDBNull(row["Col001"])) ? 0 : Convert.ToInt32(row["Col001"]);

                if (j >= MixRain && j < MarRain)
                {
                    tmp.SiteNum = l;
                    tmp.RainNum = j.ToString();// (Convert.IsDBNull(row["Col007"])) ? 0.0 : Convert.ToDouble(row["Col007"]);
                    string str1 = "select st_sitinfo_b.站名,st_sitinfo_b.东经,st_sitinfo_b.北纬,st_sitinfo_b.地市, st_sitinfo_b.地址 from st_sitinfo_b where 站码= " + tmp.SiteNum;
                    cmd = new SqlCommand(str1, conn);
                    da = new SqlDataAdapter(cmd);
                    ds1 = new DataSet();
                    da.Fill(ds1, "ds1");
                    dt1 = ds1.Tables[0];
                    DataRow row1 = dt1.Rows[0];
                    tmp.SiteName = (Convert.IsDBNull(row1["站名"])) ? "" : (string)row1["站名"];
                    tmp.SitePntX = (Convert.IsDBNull(row1["东经"])) ? "0" : (string)(row1["东经"]);//Convert.ToDouble(row1["东经"]);
                    tmp.SitePntY = (Convert.IsDBNull(row1["北纬"])) ? "0" : (string)(row1["北纬"]);
                    tmp.SiteAddress = (Convert.IsDBNull(row1["地址"])) ? "" : (string)row1["地址"];
                    tmp.Pro = (Convert.IsDBNull(row1["地市"])) ? "" : (string)row1["地市"];
                    lst_RainInfos.Add(tmp);
                }
            }
        }
        catch
        {
        }
        finally
        {
            conn.Close();
        }
        return lst_RainInfos;
    }

    /// <summary>
    /// 获取站点历史雨量信息
    /// </summary>
    /// <param name="SiteNum"></param>
    /// <returns></returns>
    public static List<RainDetailInfo> getSiteHisRainInfos(string TimeStar, string TimeEnd, int SiteNum)
    {
        List<RainDetailInfo> HisInfos = new List<RainDetailInfo>();
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["GXSLSql"]);        //数据库连接对象
        ConnectSQL(conn);
        string starSql = "select st_sitinfo_b.站码 , st_soil_r.Col007,st_soil_r.Col002,st_sitinfo_b.站名,st_sitinfo_b.地市, st_sitinfo_b.地址 from st_soil_r  INNER JOIN st_sitinfo_b on st_soil_r.Col001=st_sitinfo_b.站码 where (st_soil_r.Col001=" + SiteNum + " and " + "st_soil_r.Col002<='" + TimeEnd + "' and st_soil_r.Col002>='" + TimeStar + "') ORDER BY st_soil_r.Col002";//"st_soil_r.Col002>='2008-05-16 00:00:00' and st_soil_r.Col002<='2008-05-16 23:58:00'
        try
        {
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;
            cmd = new SqlCommand(starSql, conn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            dt = new DataTable();
            dt = ds.Tables[0];
            RainDetailInfo tmp;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                tmp = new RainDetailInfo();
                DataRow row = dt.Rows[i];
                tmp.SiteNum = (Convert.IsDBNull(row["站码"])) ? 0 : Convert.ToInt32(row["站码"]);
                tmp.SiteName = (Convert.IsDBNull(row["站名"])) ? "" : (string)row["站名"];
                tmp.RainNum = (Convert.IsDBNull(row["Col007"])) ? "0" : (row["Col007"]).ToString();
                tmp.SiteAddress = (Convert.IsDBNull(row["地址"])) ? "" : (string)row["地址"];
                tmp.Pro = (Convert.IsDBNull(row["地市"])) ? "" : (string)row["地市"];
                tmp.TM = (Convert.IsDBNull(row["Col002"])) ? DateTime.Now : (DateTime)row["Col002"];
                tmp.tm = (Convert.IsDBNull(row["Col002"])) ? DateTime.Now.ToLongDateString().ToString() : ((DateTime)row["Col002"]).ToLongTimeString().ToString();
                HisInfos.Add(tmp);
            }
        }
        catch
        {
            conn.Close();
        }
        finally
        {
            conn.Close();

        }
        return HisInfos;
    }
    #endregion
}