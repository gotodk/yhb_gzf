using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;
using System.Web.Script.Serialization;

public class NoReSet_160622000062
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
        //MID, M_HID, Mpz, Mje, Mbeizhu, Mzt, Mchuangjianren
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_moneyZJSKD");
        param.Add("@MID", guid);
        param.Add("@Mzt", "草稿");
        param.Add("@M_HID", ht_forUI["M_HID"].ToString());
        param.Add("@Mpz", ht_forUI["Mpz"].ToString());
        param.Add("@Mje", ht_forUI["Mje"].ToString());
        if (ht_forUI["Mxiaciriqi"].ToString() == "")
        {
            param.Add("@Mxiaciriqi", DBNull.Value);
        }
        else
        {
            param.Add("@Mxiaciriqi", ht_forUI["Mxiaciriqi"].ToString());
        }
        //
        param.Add("@Mbeizhu", ht_forUI["Mbeizhu"].ToString());
        param.Add("@Mchuangjianren", ht_forUI["yhbsp_session_uer_UAid"].ToString());

        param.Add("@MHzlqx", ht_forUI["MHzlqx"].ToString());
        param.Add("@MHjdzj", ht_forUI["MHjdzj"].ToString());
        param.Add("@MHjdwyf", ht_forUI["MHjdwyf"].ToString());
        param.Add("@MHjddtf", ht_forUI["MHjddtf"].ToString());
        param.Add("@MHjdqtfy", ht_forUI["MHjdqtfy"].ToString());
        param.Add("@MZ_zj_xmje", ht_forUI["MZ_zj_xmje"].ToString());
        param.Add("@MZ_zj_yjn", ht_forUI["MZ_zj_yjn"].ToString());
        param.Add("@MZ_zj_pzh", ht_forUI["MZ_zj_pzh"].ToString());
        param.Add("@MZ_zj_bcsk", ht_forUI["MZ_zj_bcsk"].ToString());
        param.Add("@MZ_zj_qiankuan", ht_forUI["MZ_zj_qiankuan"].ToString());
        param.Add("@MZ_wy_xmje", ht_forUI["MZ_wy_xmje"].ToString());
        param.Add("@MZ_wy_yjn", ht_forUI["MZ_wy_yjn"].ToString());
        param.Add("@MZ_wy_pzh", ht_forUI["MZ_wy_pzh"].ToString());
        param.Add("@MZ_wy_bcsk", ht_forUI["MZ_wy_bcsk"].ToString());
        param.Add("@MZ_wy_qiankuan", ht_forUI["MZ_wy_qiankuan"].ToString());
        param.Add("@MZ_dt_xmje", ht_forUI["MZ_dt_xmje"].ToString());
        param.Add("@MZ_dt_yjn", ht_forUI["MZ_dt_yjn"].ToString());
        param.Add("@MZ_dt_pzh", ht_forUI["MZ_dt_pzh"].ToString());
        param.Add("@MZ_dt_bcsk", ht_forUI["MZ_dt_bcsk"].ToString());
        param.Add("@MZ_dt_qiankuan", ht_forUI["MZ_dt_qiankuan"].ToString());
        param.Add("@MZ_q_xmje", ht_forUI["MZ_q_xmje"].ToString());
        param.Add("@MZ_q_yjn", ht_forUI["MZ_q_yjn"].ToString());
        param.Add("@MZ_q_pzh", ht_forUI["MZ_q_pzh"].ToString());
        param.Add("@MZ_q_bcsk", ht_forUI["MZ_q_bcsk"].ToString());
        param.Add("@MZ_q_qiankuan", ht_forUI["MZ_q_qiankuan"].ToString());
        param.Add("@MZ_all_xmje", ht_forUI["MZ_all_xmje"].ToString());
        param.Add("@MZ_all_yjn", ht_forUI["MZ_all_yjn"].ToString());
        param.Add("@MZ_all_qiankuan", ht_forUI["MZ_all_qiankuan"].ToString());
        

        alsql.Add("INSERT INTO ZZZ_moneyZJSKD(MID, M_HID, Mpz, Mje,Mxiaciriqi, Mbeizhu, Mzt, Mchuangjianren,MHzlqx, MHjdzj, MHjdwyf,   MHjddtf, MHjdqtfy, MZ_zj_xmje, MZ_zj_yjn, MZ_zj_pzh, MZ_zj_bcsk, MZ_zj_qiankuan, MZ_wy_xmje, MZ_wy_yjn,   MZ_wy_pzh, MZ_wy_bcsk, MZ_wy_qiankuan, MZ_dt_xmje, MZ_dt_yjn, MZ_dt_pzh, MZ_dt_bcsk, MZ_dt_qiankuan,   MZ_q_xmje, MZ_q_yjn, MZ_q_pzh, MZ_q_bcsk, MZ_q_qiankuan, MZ_all_xmje, MZ_all_yjn, MZ_all_qiankuan) VALUES(@MID, @M_HID, @Mpz, @Mje,@Mxiaciriqi, @Mbeizhu, @Mzt, @Mchuangjianren,@MHzlqx, @MHjdzj, @MHjdwyf,   @MHjddtf, @MHjdqtfy, @MZ_zj_xmje, @MZ_zj_yjn, @MZ_zj_pzh, @MZ_zj_bcsk, @MZ_zj_qiankuan, @MZ_wy_xmje, @MZ_wy_yjn,   @MZ_wy_pzh, @MZ_wy_bcsk, @MZ_wy_qiankuan, @MZ_dt_xmje, @MZ_dt_yjn, @MZ_dt_pzh, @MZ_dt_bcsk, @MZ_dt_qiankuan,   @MZ_q_xmje, @MZ_q_yjn, @MZ_q_pzh, @MZ_q_bcsk, @MZ_q_qiankuan, @MZ_all_xmje, @MZ_all_yjn, @MZ_all_qiankuan)");
 

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
        param.Add("@MID", ht_forUI["idforedit"].ToString());
        //param.Add("@M_HID", ht_forUI["M_HID"].ToString());
        param.Add("@Mpz", ht_forUI["Mpz"].ToString());
        param.Add("@Mje", ht_forUI["Mje"].ToString());
        if (ht_forUI["Mxiaciriqi"].ToString() == "")
        {
            param.Add("@Mxiaciriqi", DBNull.Value);
        }
        else
        {
            param.Add("@Mxiaciriqi", ht_forUI["Mxiaciriqi"].ToString());
        }
        param.Add("@Mbeizhu", ht_forUI["Mbeizhu"].ToString());

        param.Add("@MHzlqx", ht_forUI["MHzlqx"].ToString());
        param.Add("@MHjdzj", ht_forUI["MHjdzj"].ToString());
        param.Add("@MHjdwyf", ht_forUI["MHjdwyf"].ToString());
        param.Add("@MHjddtf", ht_forUI["MHjddtf"].ToString());
        param.Add("@MHjdqtfy", ht_forUI["MHjdqtfy"].ToString());
        param.Add("@MZ_zj_xmje", ht_forUI["MZ_zj_xmje"].ToString());
        param.Add("@MZ_zj_yjn", ht_forUI["MZ_zj_yjn"].ToString());
        param.Add("@MZ_zj_pzh", ht_forUI["MZ_zj_pzh"].ToString());
        param.Add("@MZ_zj_bcsk", ht_forUI["MZ_zj_bcsk"].ToString());
        param.Add("@MZ_zj_qiankuan", ht_forUI["MZ_zj_qiankuan"].ToString());
        param.Add("@MZ_wy_xmje", ht_forUI["MZ_wy_xmje"].ToString());
        param.Add("@MZ_wy_yjn", ht_forUI["MZ_wy_yjn"].ToString());
        param.Add("@MZ_wy_pzh", ht_forUI["MZ_wy_pzh"].ToString());
        param.Add("@MZ_wy_bcsk", ht_forUI["MZ_wy_bcsk"].ToString());
        param.Add("@MZ_wy_qiankuan", ht_forUI["MZ_wy_qiankuan"].ToString());
        param.Add("@MZ_dt_xmje", ht_forUI["MZ_dt_xmje"].ToString());
        param.Add("@MZ_dt_yjn", ht_forUI["MZ_dt_yjn"].ToString());
        param.Add("@MZ_dt_pzh", ht_forUI["MZ_dt_pzh"].ToString());
        param.Add("@MZ_dt_bcsk", ht_forUI["MZ_dt_bcsk"].ToString());
        param.Add("@MZ_dt_qiankuan", ht_forUI["MZ_dt_qiankuan"].ToString());
        param.Add("@MZ_q_xmje", ht_forUI["MZ_q_xmje"].ToString());
        param.Add("@MZ_q_yjn", ht_forUI["MZ_q_yjn"].ToString());
        param.Add("@MZ_q_pzh", ht_forUI["MZ_q_pzh"].ToString());
        param.Add("@MZ_q_bcsk", ht_forUI["MZ_q_bcsk"].ToString());
        param.Add("@MZ_q_qiankuan", ht_forUI["MZ_q_qiankuan"].ToString());
        param.Add("@MZ_all_xmje", ht_forUI["MZ_all_xmje"].ToString());
        param.Add("@MZ_all_yjn", ht_forUI["MZ_all_yjn"].ToString());
        param.Add("@MZ_all_qiankuan", ht_forUI["MZ_all_qiankuan"].ToString());

        alsql.Add("UPDATE ZZZ_moneyZJSKD SET  Mpz=@Mpz,Mje=@Mje,Mxiaciriqi=@Mxiaciriqi,Mbeizhu=@Mbeizhu, MHzlqx=@MHzlqx, MHjdzj=@MHjdzj, MHjdwyf=@MHjdwyf,   MHjddtf=@MHjddtf, MHjdqtfy=@MHjdqtfy, MZ_zj_xmje=@MZ_zj_xmje, MZ_zj_yjn=@MZ_zj_yjn, MZ_zj_pzh=@MZ_zj_pzh, MZ_zj_bcsk=@MZ_zj_bcsk, MZ_zj_qiankuan=@MZ_zj_qiankuan, MZ_wy_xmje=@MZ_wy_xmje, MZ_wy_yjn=@MZ_wy_yjn,   MZ_wy_pzh=@MZ_wy_pzh, MZ_wy_bcsk=@MZ_wy_bcsk, MZ_wy_qiankuan=@MZ_wy_qiankuan, MZ_dt_xmje=@MZ_dt_xmje, MZ_dt_yjn=@MZ_dt_yjn, MZ_dt_pzh=@MZ_dt_pzh, MZ_dt_bcsk=@MZ_dt_bcsk, MZ_dt_qiankuan=@MZ_dt_qiankuan,   MZ_q_xmje=@MZ_q_xmje, MZ_q_yjn=@MZ_q_yjn, MZ_q_pzh=@MZ_q_pzh, MZ_q_bcsk=@MZ_q_bcsk, MZ_q_qiankuan=@MZ_q_qiankuan, MZ_all_xmje=@MZ_all_xmje, MZ_all_yjn=@MZ_all_yjn, MZ_all_qiankuan=@MZ_all_qiankuan where MID=@MID and Mzt='草稿' ");
 

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
        param.Add("@MID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_moneyZJSKD_ex where MID=@MID", "数据记录", param);

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
