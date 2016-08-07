﻿using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminht_corepage_WUC_fordemohome : System.Web.UI.UserControl
{
    public DataTable dtgzt = new DataTable();  //我的工作台数据
    protected void Page_Load(object sender, EventArgs e)
    {
        ////调用执行方法获取数据
        object[] re_dsi = IPC.Call("获取我的工作台数据", new object[] { UserSession.唯一键, "" });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            dtgzt = (DataTable)(re_dsi[1]);
            //


        }
        else
        {
            dtgzt = null;

        }
    }
}