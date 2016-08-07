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

public class NoReSet_160622000059
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
        //CID, Czhuangtai, Cxingming, Cshenfenzheng, Clianxifangshi, Cgongzuodanwei, Cczqx, Cchuangjianren
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_chengzuren");
        param.Add("@CID", guid);
        param.Add("@Czhuangtai", "未承租");
        param.Add("@Cxingming", ht_forUI["Cxingming"].ToString());
        param.Add("@Csqbbh", ht_forUI["Csqbbh"].ToString());
        param.Add("@Cshenfenzheng", ht_forUI["Cshenfenzheng"].ToString());
        param.Add("@Clianxifangshi", ht_forUI["Clianxifangshi"].ToString());
        param.Add("@Cgongzuodanwei", ht_forUI["Cgongzuodanwei"].ToString());
        param.Add("@Cczqx", ht_forUI["Cczqx"].ToString());
        param.Add("@Cchuangjianren", ht_forUI["yhbsp_session_uer_UAid"].ToString());



        alsql.Add("INSERT INTO ZZZ_chengzuren(CID, Czhuangtai, Cxingming,Csqbbh, Cshenfenzheng, Clianxifangshi, Cgongzuodanwei, Cczqx, Cchuangjianren) VALUES(@CID, @Czhuangtai, @Cxingming,@Csqbbh, @Cshenfenzheng, @Clianxifangshi, @Cgongzuodanwei, @Cczqx, @Cchuangjianren)");

        //遍历子表， 插入 
        string zibiao_gts_id = "grid-table-subtable-160706001076";
        DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }

        param.Add("@sub_" + "MainID", guid); //隶属主表id

        for (int i = 0; i < subdt.Rows.Count; i++)
        {
            param.Add("@sub_" + "zgid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_chengzuren_zige"));

            param.Add("@sub_" + "zgnianxian" + "_" + i, subdt.Rows[i]["年限"].ToString());
            param.Add("@sub_" + "zgbegin" + "_" + i, subdt.Rows[i]["资格开始日期"].ToString());
            param.Add("@sub_" + "zgend" + "_" + i, subdt.Rows[i]["资格结束日期"].ToString());
            param.Add("@sub_" + "zgbz" + "_" + i, subdt.Rows[i]["资格备注"].ToString());
 
            string INSERTsql = "INSERT INTO ZZZ_chengzuren_zige ( zgid, zg_CID, zgnianxian, zgbegin, zgend, zgbz ) VALUES(@sub_" + "zgid" + "_" + i + ", @sub_MainID, @sub_" + "zgnianxian" + "_" + i + ", @sub_" + "zgbegin" + "_" + i + ", @sub_" + "zgend" + "_" + i + ", @sub_" + "zgbz" + "_" + i + " )";
            alsql.Add(INSERTsql);
        }
   
        alsql.Add("update ZZZ_chengzuren set Cczqx=(select top 1 zgend from ZZZ_chengzuren_zige where zg_CID=@sub_MainID order by zgend desc  ) where CID=@sub_MainID");

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
        param.Add("@CID", ht_forUI["idforedit"].ToString());
        param.Add("@Cxingming", ht_forUI["Cxingming"].ToString());
        param.Add("@Csqbbh", ht_forUI["Csqbbh"].ToString());
        param.Add("@Cshenfenzheng", ht_forUI["Cshenfenzheng"].ToString());
        param.Add("@Clianxifangshi", ht_forUI["Clianxifangshi"].ToString());
        param.Add("@Cgongzuodanwei", ht_forUI["Cgongzuodanwei"].ToString());
        param.Add("@Cczqx", ht_forUI["Cczqx"].ToString());

        alsql.Add("UPDATE ZZZ_chengzuren SET  Cxingming=@Cxingming,Csqbbh=@Csqbbh, Cshenfenzheng=@Cshenfenzheng, Clianxifangshi=@Clianxifangshi, Cgongzuodanwei=@Cgongzuodanwei, Cczqx=@Cczqx where CID=@CID ");



        //遍历子表，先删除，再插入，已有主键的不重新生成。
        string zibiao_gts_id = "grid-table-subtable-160706001076";
        DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }
        param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
        alsql.Add("delete ZZZ_chengzuren_zige where  zg_CID = @sub_" + "MainID");
        for (int i = 0; i < subdt.Rows.Count; i++)
        {
            if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
            {
                param.Add("@sub_" + "zgid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_chengzuren_zige"));
            }
            else
            {
                param.Add("@sub_" + "zgid" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
            }
            param.Add("@sub_" + "zgnianxian" + "_" + i, subdt.Rows[i]["年限"].ToString());
            param.Add("@sub_" + "zgbegin" + "_" + i, subdt.Rows[i]["资格开始日期"].ToString());
            param.Add("@sub_" + "zgend" + "_" + i, subdt.Rows[i]["资格结束日期"].ToString());
            param.Add("@sub_" + "zgbz" + "_" + i, subdt.Rows[i]["资格备注"].ToString());


            string INSERTsql = "INSERT INTO ZZZ_chengzuren_zige ( zgid, zg_CID, zgnianxian, zgbegin, zgend, zgbz ) VALUES(@sub_" + "zgid" + "_" + i + ", @sub_MainID, @sub_" + "zgnianxian" + "_" + i + ", @sub_" + "zgbegin" + "_" + i + ", @sub_" + "zgend" + "_" + i + ", @sub_" + "zgbz" + "_" + i + " )";
            alsql.Add(INSERTsql);
        }

        alsql.Add("update ZZZ_chengzuren set Cczqx=(select top 1 zgend from ZZZ_chengzuren_zige where zg_CID=@sub_MainID order by zgend desc  ) where CID=@sub_MainID");

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
        param.Add("@CID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_chengzuren_ex where CID=@CID", "数据记录", param);

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
