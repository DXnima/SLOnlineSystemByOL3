using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///RainInfo 的摘要说明
/// </summary>
public class RainInfo
{
	public RainInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public int SiteNum { get; set; }            //站码
    public string SiteName { get; set; }        //站名
    public string SitePntX { get; set; }        //东经
    public string SitePntY { get; set; }        //北纬
    public string SiteAddress { get; set; }     //地址
    public string Pro { get; set; }
    public string RainNum { get; set; }
}