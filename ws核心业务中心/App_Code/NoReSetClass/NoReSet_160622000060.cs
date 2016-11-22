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

public class NoReSet_160622000060
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
    /// 检查合同期限，不能超过资格期限
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    private string check_htqx(string Hdaoqiriqi,string H_CID)
    {

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@CID", H_CID);
        param.Add("@Hdaoqiriqi", Hdaoqiriqi);

        return_ht = I_DBL.RunParam_SQL(" select  Datediff(day,cast((select  top 1 Cczqx from ZZZ_chengzuren where CID=@CID) as datetime),cast(@Hdaoqiriqi as datetime))  as chazhi", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count > 0)
            {
                if (Convert.ToInt32(redb.Rows[0]["chazhi"]) > 0)
                {
                    return "提交失败，合同期限不能超过资格期限！已超"+ redb.Rows[0]["chazhi"].ToString() + "天。";
                }
                else
                {
                    return "ok";
                }
                
            }
            else
            {
                return "检查合同期限出错";
            }

        }
        else
        {
            return "检查合同期限出错";
        }



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
        string ck_qx = check_htqx(ht_forUI["Hdaoqiriqi"].ToString(), ht_forUI["H_CID"].ToString());
        if (ck_qx != "ok")
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = ck_qx;
            return dsreturn;
        }

        //开始真正的处理，根据业务逻辑操作数据库
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        //HID, Hzhuangtai, H_CID, H_FID, Hdaoqiriqi, Hfkfs, Hje, Hyjrq, Hqiandingriqi, Hbeizhu, Hchuangjianren
        //string guid = CombGuid.GetMewIdFormSequence("ZZZ_RZHT");
        param.Add("@HID", ht_forUI["HID"].ToString());
        param.Add("@Hzhuangtai", "草稿");
        param.Add("@H_CID", ht_forUI["H_CID"].ToString());
        param.Add("@H_FID", ht_forUI["H_FID"].ToString());
        param.Add("@Hshengxiaoriqi", ht_forUI["Hshengxiaoriqi"].ToString());
        param.Add("@Hdaoqiriqi", ht_forUI["Hdaoqiriqi"].ToString());
        param.Add("@Hfkfs", ht_forUI["Hfkfs"].ToString());
        param.Add("@Hje", ht_forUI["Hje"].ToString());



        param.Add("@Hzlqx", ht_forUI["Hzlqx"].ToString());
        param.Add("@Hjdzj", ht_forUI["Hjdzj"].ToString());
        param.Add("@Hjdwyf", ht_forUI["Hjdwyf"].ToString());
        param.Add("@Hjddtf", ht_forUI["Hjddtf"].ToString());
        param.Add("@Hjdqtfy", ht_forUI["Hjdqtfy"].ToString());
       
        param.Add("@Hqiandingriqi", ht_forUI["Hqiandingriqi"].ToString());
        param.Add("@Hbeizhu", ht_forUI["Hbeizhu"].ToString());
        param.Add("@Hchuangjianren", ht_forUI["yhbsp_session_uer_UAid"].ToString());



        alsql.Add("INSERT INTO ZZZ_RZHT(HID, Hzhuangtai, H_CID, H_FID, Hdaoqiriqi, Hfkfs, Hje, Hshengxiaoriqi, Hqiandingriqi, Hbeizhu, Hchuangjianren,  Hzlqx,Hjdzj,Hjdwyf,Hjddtf,Hjdqtfy ) VALUES(@HID, @Hzhuangtai, @H_CID, @H_FID, @Hdaoqiriqi, @Hfkfs, @Hje, @Hshengxiaoriqi, @Hqiandingriqi, @Hbeizhu, @Hchuangjianren,  @Hzlqx,@Hjdzj,@Hjdwyf,@Hjddtf,@Hjdqtfy)");
 

        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "新增成功！{" + ht_forUI["HID"].ToString() + "}";
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


        string ck_qx = check_htqx(ht_forUI["Hdaoqiriqi"].ToString(), ht_forUI["H_CID"].ToString());
        if (ck_qx != "ok")
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = ck_qx;
            return dsreturn;
        }
        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        param.Add("@HID", ht_forUI["idforedit"].ToString());
        param.Add("@H_CID", ht_forUI["H_CID"].ToString());
        param.Add("@H_FID", ht_forUI["H_FID"].ToString());
        param.Add("@Hshengxiaoriqi", ht_forUI["Hshengxiaoriqi"].ToString());
        param.Add("@Hdaoqiriqi", ht_forUI["Hdaoqiriqi"].ToString());
        param.Add("@Hfkfs", ht_forUI["Hfkfs"].ToString());
        param.Add("@Hje", ht_forUI["Hje"].ToString());

        param.Add("@Hzlqx", ht_forUI["Hzlqx"].ToString());
        param.Add("@Hjdzj", ht_forUI["Hjdzj"].ToString());
        param.Add("@Hjdwyf", ht_forUI["Hjdwyf"].ToString());
        param.Add("@Hjddtf", ht_forUI["Hjddtf"].ToString());
        param.Add("@Hjdqtfy", ht_forUI["Hjdqtfy"].ToString());

        param.Add("@Hqiandingriqi", ht_forUI["Hqiandingriqi"].ToString());
        param.Add("@Hbeizhu", ht_forUI["Hbeizhu"].ToString());

        alsql.Add("UPDATE ZZZ_RZHT SET  H_CID=@H_CID, H_FID=@H_FID, Hdaoqiriqi=@Hdaoqiriqi, Hfkfs=@Hfkfs, Hje=@Hje, Hshengxiaoriqi=@Hshengxiaoriqi, Hqiandingriqi=@Hqiandingriqi, Hbeizhu=@Hbeizhu ,   Hzlqx=@Hzlqx,Hjdzj=@Hjdzj,Hjdwyf=@Hjdwyf,Hjddtf=@Hjddtf,Hjdqtfy=@Hjdqtfy where HID=@HID ");
 

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
        param.Add("@HID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_RZHT_ex where HID=@HID", "数据记录", param);

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
