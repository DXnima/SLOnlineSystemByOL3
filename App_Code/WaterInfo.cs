using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///WaterInfo 的摘要说明
/// </summary>
public class WaterInfo
{
    public WaterInfo()
    {

    }
    public int SiteNum { get; set; }            //站码
    public string SiteName { get; set; }        //站名
    public string SitePntX { get; set; }        //站点x坐标，东经
    public string SitePntY { get; set; }        //站点y坐标，北纬
    public string SiteAddress { get; set; }     //地址
    public string WaterPos { get; set; }        //水位
    public string FlowNum { get; set; }         //流量
    public string WarnNum { get; set; }         //警戒
    public string NorNum { get; set; }          //保证/正常
    public DateTime TM { get; set; }
    public string tm { get; set; }
}