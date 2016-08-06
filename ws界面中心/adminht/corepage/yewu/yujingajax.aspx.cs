using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminht_corepage_yewu_yujingajax : System.Web.UI.Page
{
    public string llb_s1 = "0";
    public string llc_s1 = "0";
    public string llb_s2 = "0";
    public string llc_s2 = "0";
    public string llb_s3 = "0";
    public string llc_s3 = "0";
    public string llb_s4 = "0";
    public string llc_s4 = "0";

    private int GetFirstMonday(int year, int month)
    {

        string s = year.ToString() + "-" + month.ToString() + "-" + "1";
        DateTime dt = DateTime.Parse(s);
        DayOfWeek week = dt.DayOfWeek;
        int deffday;
        if (week > DayOfWeek.Monday)
        {
            deffday = DayOfWeek.Saturday - week + 2;
        }
        else
        {
            deffday = DayOfWeek.Monday - week;
        }

        return dt.AddDays(deffday).Day;

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        string xia2 = DateTime.Now.AddMonths(2).Month.ToString();
        string xia1 = DateTime.Now.AddMonths(1).Month.ToString();

        //判断是否每月的第一个周一
        int jihao = GetFirstMonday(DateTime.Now.Year, DateTime.Now.Month);
        if (DateTime.Compare(DateTime.Parse("" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + jihao + ""), DateTime.Now) == 0 || 1==1)
        {

            object[] re_dsi = IPC.Call("全局报警数据获取", new object[] { UserSession.唯一键 });
            if (re_dsi[0].ToString() == "ok")
            {

                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                DataSet dsreturn = (DataSet)re_dsi[1];
                llb_s1 = xia2;
                llc_s1 = dsreturn.Tables["数据记录"].Rows[0]["chengzuzige"].ToString();
                llb_s2 = xia1;
                llc_s2 = dsreturn.Tables["数据记录"].Rows[0]["fangzufukuan"].ToString();
                llb_s3 = xia1;
                llc_s3 = dsreturn.Tables["数据记录"].Rows[0]["hetongdaoqi"].ToString();
                llb_s4 = xia1;
                llc_s4 = dsreturn.Tables["数据记录"].Rows[0]["cheweifei"].ToString();

                if (llc_s1 == "0" && llc_s2 == "0" && llc_s3 == "0" && llc_s4 == "0")
                {
                    ulmain.Visible = false;
                }

            }
            else
            {
                Response.Write(re_dsi[1].ToString());//向客户端输出错误字符串

            }

        }
        else
        {
            ulmain.Visible = false;
        }

        
    }
}