﻿using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;
using System.Web.Script.Serialization;

public class NoReSet_160622000061
{
 

    /// <summary>
    /// 初始化返回值数据集,执行结果只有两种ok和err(大多数情况是这个标准)
    /// </summary>
    /// <returns></returns>
    private DataSet initReturnDataSet()
    {
        DataSet ds = new DataSet();
        DataTable auto2 = new DataTable();
        auto2.TableName = "返回值单条";
        auto2.Columns.Add("执行结果");
        auto2.Columns.Add("提示文本");
        auto2.Columns.Add("附件信息1");
        auto2.Columns.Add("附件信息2");
        auto2.Columns.Add("附件信息3");
        auto2.Columns.Add("附件信息4");
        auto2.Columns.Add("附件信息5");
        ds.Tables.Add(auto2);
        return ds;
    }

    /// <summary>
    /// 增加数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_ADD(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }
        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });
        //参数合法性各种验证，这里要根据具体业务逻辑处理

        //开始真正的处理，根据业务逻辑操作数据库
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        //TZID, TZriqi, TZ_HID, TZbeizhu, TZchuangjianren
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_tuizujilu");
        param.Add("@TZID", guid);
        param.Add("@TZriqi", ht_forUI["TZriqi"].ToString());
        param.Add("@TZ_HID", ht_forUI["TZ_HID"].ToString());
        param.Add("@TZbeizhu", ht_forUI["TZbeizhu"].ToString());
        param.Add("@TZchuangjianren", ht_forUI["yhbsp_session_uer_UAid"].ToString());



        alsql.Add("INSERT INTO ZZZ_tuizujilu(TZID, TZriqi, TZ_HID, TZbeizhu, TZchuangjianren) VALUES(@TZID, @TZriqi, @TZ_HID, @TZbeizhu, @TZchuangjianren)");
        alsql.Add("UPDATE ZZZ_RZHT SET  Hzhuangtai='已退租'  where HID =@TZ_HID");
        alsql.Add("UPDATE ZZZ_fangzi SET  Fzhuangtai='空置'  where FID = (select top 1 H_FID from ZZZ_RZHT where HID=@TZ_HID)");
        alsql.Add("UPDATE ZZZ_chengzuren SET  Czhuangtai='未承租'  where CID =(select top 1 H_CID from ZZZ_RZHT where HID=@TZ_HID)");

        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "退租成功，相关状态已更改！";
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，保存失败：" + return_ht["return_errmsg"].ToString();
        }
        return dsreturn;
    }

    /// <summary>
    /// 编辑数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_EDIT(DataTable parameter_forUI)
    {
        

        return null;
    }

    /// <summary>
    /// 编辑数据前获取数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_EDIT_INFO(DataTable parameter_forUI)
    {
    

        return null;
    }


}
