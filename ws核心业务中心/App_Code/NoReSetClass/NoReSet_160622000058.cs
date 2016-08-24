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

public class NoReSet_160622000058
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
        //FID, Fzhuangtai, Fdiqu, Fzuoluo, Fjcsj, Fjzmj, Fhxjg, Fshineisheshi, Fchewei, Fsssbpz, Fchuangjianren, Faddtime
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_fangzi");
        param.Add("@FID", guid);
        param.Add("@Fzhuangtai", "空置");
        param.Add("@Fdiqu", ht_forUI["Fdiqu"].ToString());
        param.Add("@Fzuoluo", ht_forUI["Fzuoluo"].ToString());
        param.Add("@Fjcsj", ht_forUI["Fjcsj"].ToString());
        param.Add("@Fjzmj", ht_forUI["Fjzmj"].ToString());
        param.Add("@Fhxjg", ht_forUI["Fhxjg"].ToString());
        param.Add("@Fshineisheshi", ht_forUI["Fshineisheshi"].ToString());
        //param.Add("@Fchewei", ht_forUI["Fchewei"].ToString());

        param.Add("@Fjdzj", ht_forUI["Fjdzj"].ToString());
        param.Add("@Fjdwyf", ht_forUI["Fjdwyf"].ToString());
        param.Add("@Fjddtf", ht_forUI["Fjddtf"].ToString());
        param.Add("@Fjdqtfy", ht_forUI["Fjdqtfy"].ToString());

        if (ht_forUI.Contains("allpath_file1"))
        { param.Add("@Fsssbpz", ht_forUI["allpath_file1"].ToString()); }
        else
        {
            param.Add("@Fsssbpz", "");
        }

        param.Add("@Fchuangjianren", ht_forUI["yhbsp_session_uer_UAid"].ToString());



        alsql.Add("INSERT INTO ZZZ_fangzi(FID, Fzhuangtai, Fdiqu, Fzuoluo, Fjcsj, Fjzmj, Fhxjg, Fshineisheshi, Fsssbpz, Fchuangjianren, Fjdzj,Fjdwyf,Fjddtf,Fjdqtfy ) VALUES(@FID, @Fzhuangtai, @Fdiqu, @Fzuoluo, @Fjcsj, @Fjzmj, @Fhxjg, @Fshineisheshi, @Fsssbpz, @Fchuangjianren , @Fjdzj,@Fjdwyf,@Fjddtf,@Fjdqtfy)");
 

        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "新增成功！{" + guid + "}";
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
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }


        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略
        if (ht_forUI["idforedit"].ToString().Trim() == "")
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有明确的修改目标！";
            return dsreturn;
        }
        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        param.Add("@FID", ht_forUI["idforedit"].ToString());
        param.Add("@Fdiqu", ht_forUI["Fdiqu"].ToString());
        param.Add("@Fzuoluo", ht_forUI["Fzuoluo"].ToString());
        param.Add("@Fjcsj", ht_forUI["Fjcsj"].ToString());
        param.Add("@Fjzmj", ht_forUI["Fjzmj"].ToString());
        param.Add("@Fhxjg", ht_forUI["Fhxjg"].ToString());
        param.Add("@Fshineisheshi", ht_forUI["Fshineisheshi"].ToString());
        //param.Add("@Fchewei", ht_forUI["Fchewei"].ToString());

        param.Add("@Fjdzj", ht_forUI["Fjdzj"].ToString());
        param.Add("@Fjdwyf", ht_forUI["Fjdwyf"].ToString());
        param.Add("@Fjddtf", ht_forUI["Fjddtf"].ToString());
        param.Add("@Fjdqtfy", ht_forUI["Fjdqtfy"].ToString());


        if (ht_forUI.Contains("allpath_file1"))
        { param.Add("@Fsssbpz", ht_forUI["allpath_file1"].ToString()); }
        else
        {
            param.Add("@Fsssbpz", "");
        }

        alsql.Add("UPDATE ZZZ_fangzi SET  Fdiqu=@Fdiqu, Fzuoluo=@Fzuoluo, Fjcsj=@Fjcsj, Fjzmj=@Fjzmj, Fhxjg=@Fhxjg, Fshineisheshi=@Fshineisheshi, Fsssbpz=@Fsssbpz,  Fjdzj=@Fjdzj,Fjdwyf=@Fjdwyf,Fjddtf=@Fjddtf,Fjdqtfy=@Fjdqtfy where FID=@FID ");

         

        return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "修改成功！{" + ht_forUI["idforedit"].ToString() + "}";
        }
        else
        {
            //其实要记录日志，而不是输出，这里只是演示
            //dsreturn.Tables.Add(parameter_forUI.Copy());
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，修改失败：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }

    /// <summary>
    /// 编辑数据前获取数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_EDIT_INFO(DataTable parameter_forUI)
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

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_fangzi_ex where FID=@FID", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有找到指定数据!";
                return dsreturn;
            }

            dsreturn.Tables.Add(redb);
 


            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误，获取失败：" + return_ht["return_errmsg"].ToString();
        }


        return dsreturn;
    }


}
