using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///WindDetailInfoDTO 的摘要说明
/// </summary>
public class WindDetailInfoDTO
{
    public int windid { get; set; }             //台风编号
    public string tm { get; set; }              //时间
    public float jindu { get; set; }            //经度
    public float weidu { get; set; }            //纬度
    public string windstrong { get; set; }      //风力
    public string windspeed { get; set; }       //风速
    public string qiya { get; set; }            //气压
    public string movespeed { get; set; }       //移动速度
    public string movedirect { get; set; }      //方向
    public int sevradius { get; set; }          //半径1
    public int tenradius { get; set; }          //半径2
}