using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;

namespace FMDBHelperClass
{
    /// <summary>
    /// ���ڻ����й���վ�������κ�ϵͳͨ��
    /// MS SQL���ݿ�������,�����Ʒ��ɫ(����Ϊ��Ѷϵͳ�趨������ϵͳ��������Ľ�ɫ)
    /// �����б�:
    /// 1.��ʼ�����ݿ�����
    /// 2.���Ͳ�ͬ������ִ��sql��䣬ͨ����ϣ����ִ�н������
    /// return_ds�����ص����ݼ�,ִ��ʧ�ܻ᷵��null��ִ�гɹ����ؽ�����ݼ��� 
    /// return_float�����Ƿ�ִ�гɹ��� ����true/false,�������͡�
    /// return_errmsg����ִ�д���ĵ�����Ϣ���޴��󽫷���''.
    /// return_other�����ķ���ֵ��ͨ����������Ӱ�����������ǲ�׼ȷ�� 
    /// </summary>
    public class Dblink_Sql_Main : I_Dblink
    {

        #region �� �� �� �� �� �� �� �� �� ʼ ��

        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        private string ConnStr = "";

        /// <summary>
        /// ���ڷ���ֵ���ϵĹ�ϣ��
        /// ��ϣ����ֵ˵��:
        /// return_ht["return_ds"] = null; ���ص�ִ�н�����ݼ���DataSet����
        /// return_ht["return_float"] = null; ���ص�ִ�н����־��ִ�гɹ����ҷ��ص����ݴ���0������Ϊtrue
        /// return_ht["return_errmsg"] = null; ���صĴ��󲶻���Ϣ��string����
        /// return_ht["return_other"] = null; ���ص�����ֵ���ͣ���;���������Ͳ���
        /// </summary>
        private Hashtable return_ht = new Hashtable();

        #endregion

        /// <summary>
        /// MS SQL���ݿ������๹�캯��
        /// </summary>
        /// <param name="ConnConfig">web.config�����ļ���connectionStrings�¹������ݿ������ֶε�KEY</param>
        public Dblink_Sql_Main(string ConnConfig)
        {

            if (ConnConfig == "")
            {
                ConnStr = get_info("mainsqlserver"); //Ĭ�ϵ�����
            }
            else
            {
                ConnStr = get_info(ConnConfig);
            }
 



            //��ʼ����ϣ����ֵ
            return_ht.Add("return_ds", null);//��Ҫ���ص����ݼ���
            return_ht.Add("return_float", null);//����ִ�н��������ɹ�����ʧ�ܡ�(true/false)
            return_ht.Add("return_errmsg", null);//���󲶻���Ϣ
            return_ht.Add("return_other", null);//�������ⷵ��ֵ
        }

        ///// <summary>
        ///// ��ע��������ݿ��������Ϣ
        ///// </summary>
        ///// <param name="dblink">�����ַ���</param>
        //public void setregedit(string dblink)
        //{
        //    RegistryKey hklm;
        //    hklm = Registry.CurrentUser;
        //    RegistryKey software;
        //    software = hklm.OpenSubKey("Software", true);
        //    RegistryKey mykey;
        //    mykey = software.CreateSubKey("���Ӳɼ�ϵͳ");
        //    mykey.SetValue("dblink", dblink);
        //    hklm.Close();
        //}
        ///// <summary>
        ///// ��ע����ȡ���ݿ��������Ϣ
        ///// </summary>
        ///// <returns>�����ַ�������</returns>
        //public string[] getregedit()
        //{
        //    //
        //    string[] temp_key = { "", "", "", "" };
        //    RegistryKey pregkey;
        //    pregkey = Registry.CurrentUser.OpenSubKey("Software\\���Ӳɼ�ϵͳ", true);
        //    if (pregkey == null)
        //    {
        //        setregedit(@"user id=sa;password=wwwcbc365comcn;initial catalog=GalaxyAutoCollect;Server=ZRSM1;Connect Timeout=30");
        //        pregkey = Registry.CurrentUser.OpenSubKey("Software\\���Ӳɼ�ϵͳ", true);
        //    }
        //    temp_key.SetValue(pregkey.GetValue("dblink").ToString(), 0);
        //    return temp_key;
        //}

        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        private string get_info(string ConnConfig)
        {
            string connString = ConfigurationManager.ConnectionStrings[ConnConfig].ToString();
            return connString;
        }


        /// <summary>
        /// ����������,����SqlConnection����
        /// </summary>
        /// <param name="Conn">���ݿ����Ӷ���</param>
        private void Dispose(SqlConnection Conn)
        {
            try
            {
                if (Conn != null)
                {
                    Conn.Close();
                }
                //GC.Collect();
            }
            catch { }
        }



        /// <summary>
        /// �����ִ�У���֧������
        /// ִ�о�̬��䡣 ���������ݼ��� �᷵����Ӱ��������
        /// ������ִ�о�̬�����и��¡�����������������ڲ�ѯ������
        /// </summary>
        /// <param name="SQL">��Ҫִ�е�sql���</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��(return_ds��return_float��return_errmsg��return_other)</returns>
        Hashtable I_Dblink.RunProc(string SQL)
        {
            SqlConnection Conn = new SqlConnection(ConnStr);
            try
            {
                int sf = 0;
                //Conn = new SqlConnection(ConnStr);
                Conn.Open();
                SqlCommand Cmd = new SqlCommand(SQL, Conn);
                sf = Cmd.ExecuteNonQuery();
                Dispose(Conn);
                return_ht["return_ds"] = new DataSet();
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = sf;

                return return_ht;
            }
            catch (Exception exp)
            {
                Dispose(Conn);
                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = exp.ToString();
                return_ht["return_other"] = 0;

                return return_ht;
            }
        }


        /// <summary>
        /// �����ִ�У���֧������
        /// ִ�о�̬��䡣 �᷵�����ݼ��� ��������Ӱ��������
        /// ������ִ�о�̬�����в�ѯ�������������ڲ��롢���²�����
        /// </summary>
        /// <param name="SQL">SQL���</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�(return_ds��return_float��return_errmsg��return_other)</returns>
        Hashtable I_Dblink.RunProc(string SQL, string DTname)
        {
            SqlConnection Conn = new SqlConnection(ConnStr);
            try
            {
                DataSet dtreturn = new DataSet();
                int kp = 0;
                Conn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(SQL, Conn);
                kp = Da.Fill(dtreturn);
                Dispose(Conn);
                if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                {
                    dtreturn.Tables[0].TableName = DTname;
                }
                return_ht["return_ds"] = dtreturn;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = -1;



                return return_ht;
            }
            //ִ��ʧ��
            catch (Exception Err)
            {
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = Err.ToString();
                return_ht["return_other"] = -1;

                return return_ht;
            }
        }

        /// <summary>
        /// �����ִ�У���֧������(���ȶ�ȡ����)
        /// ִ�о�̬��䡣 �᷵�����ݼ��� ��������Ӱ��������
        /// ������ִ�о�̬�����в�ѯ�������������ڲ��롢���²�����
        /// </summary>
        /// <param name="SQL">SQL���</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="RedisKey">��</param>
        /// <param name="RedisPZ">�����ַ���</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�(return_ds��return_float��return_errmsg��return_other)</returns>
        Hashtable I_Dblink.RunProc(string SQL, string DTname, string RedisKey, string RedisPZ)
        {
            DataSet formRedis = RedisClass.RedisTryOnlyForDBhelper(RedisKey, RedisPZ);
            if (formRedis != null)
            {
                return_ht["return_ds"] = formRedis;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = -2;
                return return_ht;
            }
            else
            {
                SqlConnection Conn = new SqlConnection(ConnStr);
                try
                {
                    DataSet dtreturn = new DataSet();
                    int kp = 0;
                    Conn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(SQL, Conn);
                    kp = Da.Fill(dtreturn);
                    Dispose(Conn);
                    if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                    {
                        dtreturn.Tables[0].TableName = DTname;
                    }
                    return_ht["return_ds"] = dtreturn;
                    return_ht["return_float"] = true;
                    return_ht["return_errmsg"] = "";
                    return_ht["return_other"] = -1;



                    return return_ht;
                }
                //ִ��ʧ��
                catch (Exception Err)
                {
                    Dispose(Conn);

                    return_ht["return_ds"] = null;
                    return_ht["return_float"] = false;
                    return_ht["return_errmsg"] = Err.ToString();
                    return_ht["return_other"] = -1;

                    return return_ht;
                }
            }
        }

        /// <summary>
        /// �����ִ�У���֧������
        /// ִ�ж�̬��䡣 �᷵�����ݼ��� ��������Ӱ��������
        /// ������ִ�ж�̬�����в�ѯ�������������ڲ��롢���²�����
        /// </summary>
        /// <param name="P_cmd">�����������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="P_ht_in">������ϣ��</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�(return_ds��return_float��return_errmsg��return_other)</returns>
        Hashtable I_Dblink.RunParam_SQL(string P_cmd, string DTname, Hashtable P_ht_in)
        {
            SqlConnection Conn = new SqlConnection(ConnStr);
            try
            {
                DataSet dtreturn = new DataSet();
                int kp = 0;
                Conn.Open();
                SqlDataAdapter Da = new SqlDataAdapter();
                Da.SelectCommand = new SqlCommand();
                Da.SelectCommand.Connection = Conn;
                Da.SelectCommand.CommandText = P_cmd;
                Da.SelectCommand.CommandType = CommandType.Text;

                //ѭ����Ӵ洢���̲���
                IDictionaryEnumerator myEnumerator = P_ht_in.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    SqlParameter param = new SqlParameter();
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = myEnumerator.Key.ToString();
                    if (myEnumerator.Value == null)
                    {
                        // myEnumerator.Value = DBNull.Value;
                        param.Value = DBNull.Value;
                    }
                    else
                    {
                        param.Value = myEnumerator.Value;
                    }
                    Da.SelectCommand.Parameters.Add(param);
                }


                kp = Da.Fill(dtreturn);
                Dispose(Conn);
                if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                {
                    dtreturn.Tables[0].TableName = DTname;
                }
                return_ht["return_ds"] = dtreturn;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = -1;

                return return_ht;
            }
            catch (Exception Ex)
            {
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = Ex.ToString();
                return_ht["return_other"] = -1;

                return return_ht;
            }
        }


        /// <summary>
        /// �����ִ�У���֧������
        /// ִ�ж�̬��䡣 �᷵�����ݼ��� ��������Ӱ��������
        /// ������ִ�ж�̬�����в�ѯ�������������ڲ��롢���²�����
        /// </summary>
        /// <param name="P_cmd">�����������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="P_ht_in">������ϣ��</param>
        /// <param name="RedisKey">��</param>
        /// <param name="RedisPZ">�����ַ���</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�(return_ds��return_float��return_errmsg��return_other)</returns>
        Hashtable I_Dblink.RunParam_SQL(string P_cmd, string DTname, Hashtable P_ht_in, string RedisKey, string RedisPZ)
        {
             DataSet formRedis = RedisClass.RedisTryOnlyForDBhelper(RedisKey, RedisPZ);
             if (formRedis != null)
             {
                 return_ht["return_ds"] = formRedis;
                 return_ht["return_float"] = true;
                 return_ht["return_errmsg"] = "";
                 return_ht["return_other"] = -2;
                 return return_ht;
             }
             else
             {
                 SqlConnection Conn = new SqlConnection(ConnStr);
                 try
                 {
                     DataSet dtreturn = new DataSet();
                     int kp = 0;
                     Conn.Open();
                     SqlDataAdapter Da = new SqlDataAdapter();
                     Da.SelectCommand = new SqlCommand();
                     Da.SelectCommand.Connection = Conn;
                     Da.SelectCommand.CommandText = P_cmd;
                     Da.SelectCommand.CommandType = CommandType.Text;

                     //ѭ����Ӵ洢���̲���
                     IDictionaryEnumerator myEnumerator = P_ht_in.GetEnumerator();
                     while (myEnumerator.MoveNext())
                     {
                         SqlParameter param = new SqlParameter();
                         param.Direction = ParameterDirection.Input;
                         param.ParameterName = myEnumerator.Key.ToString();
                         if (myEnumerator.Value == null)
                         {
                             // myEnumerator.Value = DBNull.Value;
                             param.Value = DBNull.Value;
                         }
                         else
                         {
                             param.Value = myEnumerator.Value;
                         }
                         Da.SelectCommand.Parameters.Add(param);
                     }


                     kp = Da.Fill(dtreturn);
                     Dispose(Conn);
                     if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                     {
                         dtreturn.Tables[0].TableName = DTname;
                     }
                     return_ht["return_ds"] = dtreturn;
                     return_ht["return_float"] = true;
                     return_ht["return_errmsg"] = "";
                     return_ht["return_other"] = -1;

                     return return_ht;
                 }
                 catch (Exception Ex)
                 {
                     Dispose(Conn);

                     return_ht["return_ds"] = null;
                     return_ht["return_float"] = false;
                     return_ht["return_errmsg"] = Ex.ToString();
                     return_ht["return_other"] = -1;

                     return return_ht;
                 }
             }
        }




        /// <summary>
        /// �����ִ�У���֧������
        /// ִ�ж�̬��䡣 ���������ݼ��� �᷵����Ӱ��������
        /// ������ִ�ж�̬�����в��롢���²������������ڲ�ѯ������
        /// </summary>
        /// <param name="P_cmd">�����������</param>
        /// <param name="P_ht_in">������ϣ��</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�(return_ds��return_float��return_errmsg��return_other)</returns>
        Hashtable I_Dblink.RunParam_SQL(string P_cmd,  Hashtable P_ht_in)
        {
            SqlConnection Conn = new SqlConnection(ConnStr);
            try
            {
                int kp = 0;
                Conn.Open();
                SqlDataAdapter Da = new SqlDataAdapter();
                Da.SelectCommand = new SqlCommand();
                Da.SelectCommand.Connection = Conn;
                Da.SelectCommand.CommandText = P_cmd;
                Da.SelectCommand.CommandType = CommandType.Text;

                //ѭ����Ӵ洢���̲���
                IDictionaryEnumerator myEnumerator = P_ht_in.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    SqlParameter param = new SqlParameter();
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = myEnumerator.Key.ToString();
                    if (myEnumerator.Value == null)
                    {
                        // myEnumerator.Value = DBNull.Value;
                        param.Value = DBNull.Value;
                    }
                    else
                    {
                        param.Value = myEnumerator.Value;
                    }
                    Da.SelectCommand.Parameters.Add(param);
                }

              
                kp = kp + Da.SelectCommand.ExecuteNonQuery();


               // kp = Da.Fill(Ds);
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = kp;

                //===============================================by galaxy=====================
                //����һ���̣߳�ִ����־���� 
                ThreadStart gogo = delegate { StartSaveRedis(P_cmd, P_ht_in, kp); };
                Thread thread = new Thread(gogo);
                thread.Name = "StartSaveRedis";
                thread.IsBackground = true;
                //Thread.Sleep(20);
                thread.Start();
 
 
                
            


    

                return return_ht;
            }
            catch (Exception Ex)
            {
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = Ex.ToString();
                return_ht["return_other"] = 0;

                return return_ht;
            }
        }




        /// <summary>
        /// �����ִ�У�֧������
        /// ִ�ж�̬��䡣 ���������ݼ��� �᷵����Ӱ��������
        /// ������ִ�ж�̬�����в��롢���²������������ڲ�ѯ������
        /// </summary>
        /// <param name="SQLStringList">����������</param>
        /// <param name="P_ht_in">������ϣ��</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ������᷵�ؽ����</returns>
        Hashtable I_Dblink.RunParam_SQL(ArrayList SQLStringList, Hashtable P_ht_in)
        {
            if (SQLStringList == null || SQLStringList.Count < 1)
            {
                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = "û��Ҫִ�е����";
                return_ht["return_other"] = 0;

                return return_ht;
            }

            SqlConnection Conn = null;
            SqlTransaction tx = null;
            try
            {

                Conn = new SqlConnection(ConnStr);
                Conn.Open();
                tx = Conn.BeginTransaction();
                int kp = 0;
                ArrayList kpkp = new ArrayList();


                SqlDataAdapter Da = new SqlDataAdapter();
                Da.SelectCommand = new SqlCommand();
                Da.SelectCommand.Connection = Conn;
                Da.SelectCommand.Transaction = tx;

                //ѭ����Ӵ洢���̲���
                IDictionaryEnumerator myEnumerator = P_ht_in.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    SqlParameter param = new SqlParameter();
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = myEnumerator.Key.ToString();
                    if (myEnumerator.Value == null)
                    {
                        // myEnumerator.Value = DBNull.Value;
                        param.Value = DBNull.Value;
                    }
                    else
                    {
                        param.Value = myEnumerator.Value;
                    }
                    Da.SelectCommand.Parameters.Add(param);
                }

     
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        Da.SelectCommand.CommandText = strsql;
                        Da.SelectCommand.CommandType = CommandType.Text;
                        int kptemp = Da.SelectCommand.ExecuteNonQuery();
                        kp = kp + kptemp;
                        kpkp.Add(kptemp);
                    }
                }
                tx.Commit();
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = kp;


                //===============================================by galaxy=====================
                //����һ���̣߳�ִ����־������������ 
                ThreadStart gogo = delegate { StartSaveRedis_al(SQLStringList, P_ht_in, kpkp); };
                Thread thread = new Thread(gogo);
                thread.Name = "StartSaveRedis_al";
                thread.IsBackground = true;
                //Thread.Sleep(20);
                thread.Start();

                return return_ht;
            }
            catch (Exception Ex)
            {
                if (tx != null)
                {
                    tx.Rollback();
                }
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = Ex.ToString();
                return_ht["return_other"] = 0;

                return return_ht;
            }
        }
        /// <summary>
        /// �����ִ�У�֧������
        /// ִ�о�̬��䡣 ���������ݼ��� �᷵����Ӱ��������
        /// ������ִ�о�̬�����в��롢���²������������ڲ�ѯ������
        /// </summary>
        /// <param name="SQLStringList">����������</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ������᷵�ؽ������������Ӱ�������</returns>
        Hashtable I_Dblink.RunParam_SQL(ArrayList SQLStringList)
        {
            if (SQLStringList == null || SQLStringList.Count < 1)
            {
                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = "û��Ҫִ�е����";
                return_ht["return_other"] = 0;

                return return_ht;
            }

            SqlConnection Conn = null;
            SqlTransaction tx = null;

          

            try
            {

                Conn = new SqlConnection(ConnStr);
                Conn.Open();
                tx = Conn.BeginTransaction();
                int kp = 0;
                ArrayList kpkp = new ArrayList();

                SqlCommand cmd = new SqlCommand();
               
                cmd.Transaction = tx;
                cmd.Connection = Conn;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            int kptemp = cmd.ExecuteNonQuery();
                            kp = kp + kptemp;
                            kpkp.Add(kptemp);
                        }
                    }

                    tx.Commit();
                    Dispose(Conn);

                    return_ht["return_ds"] = null;
                    return_ht["return_float"] = true;
                    return_ht["return_errmsg"] = "";
                    return_ht["return_other"] = kp;

                    //===============================================by galaxy=====================
                    //����һ���̣߳�ִ����־������������ 
                    Hashtable P_ht_in = new Hashtable();
                    ThreadStart gogo = delegate { StartSaveRedis_al(SQLStringList, P_ht_in, kpkp); };
                    Thread thread = new Thread(gogo);
                    thread.Name = "StartSaveRedis_al";
                    thread.IsBackground = true;
                    //Thread.Sleep(20);
                    thread.Start();
 
                    return return_ht;
            }
            catch (Exception Ex)
            {
                if (tx != null)
                {
                    tx.Rollback();
                }
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = Ex.ToString();
                return_ht["return_other"] = 0;

                return return_ht;
            }
        }




        /// <summary>
        /// �޲����洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �᷵�����ݼ��� ��������Ӱ��������
        /// </summary>
        /// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�</returns>
        Hashtable I_Dblink.RunProc_CMD(string P_cmd, string DTname)
        {
            SqlConnection Conn = new SqlConnection(ConnStr);
            try
            {
                int kp = 0;
                DataSet dtreturn = new DataSet();
                Conn.Open();
                SqlDataAdapter Da = new SqlDataAdapter();
                Da.SelectCommand = new SqlCommand();
                Da.SelectCommand.Connection = Conn;
                Da.SelectCommand.CommandText = P_cmd;
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                kp = Da.Fill(dtreturn);
                Dispose(Conn);
                if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                {
                    dtreturn.Tables[0].TableName = DTname;
                }
                return_ht["return_ds"] = dtreturn;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = -1;

                return return_ht;
            }
            catch (Exception Ex)
            {
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = Ex.ToString();
                return_ht["return_other"] = -1;

                return return_ht;
            }

        }



         /// <summary>
        /// �޲����洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �᷵�����ݼ��� ��������Ӱ��������
        /// </summary>
        /// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="RedisKey">��</param>
        /// <param name="RedisPZ">�����ַ���</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�</returns>
        Hashtable I_Dblink.RunProc_CMD(string P_cmd, string DTname, string RedisKey, string RedisPZ)
        {
            DataSet formRedis = RedisClass.RedisTryOnlyForDBhelper(RedisKey, RedisPZ);
            if (formRedis != null)
            {
                return_ht["return_ds"] = formRedis;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = -2;
                return return_ht;
            }
            else
            {
                SqlConnection Conn = new SqlConnection(ConnStr);
                try
                {
                    int kp = 0;
                    DataSet dtreturn = new DataSet();
                    Conn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter();
                    Da.SelectCommand = new SqlCommand();
                    Da.SelectCommand.Connection = Conn;
                    Da.SelectCommand.CommandText = P_cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    kp = Da.Fill(dtreturn);
                    Dispose(Conn);
                    if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                    {
                        dtreturn.Tables[0].TableName = DTname;
                    }
                    return_ht["return_ds"] = dtreturn;
                    return_ht["return_float"] = true;
                    return_ht["return_errmsg"] = "";
                    return_ht["return_other"] = -1;

                    return return_ht;
                }
                catch (Exception Ex)
                {
                    Dispose(Conn);

                    return_ht["return_ds"] = null;
                    return_ht["return_float"] = false;
                    return_ht["return_errmsg"] = Ex.ToString();
                    return_ht["return_other"] = -1;

                    return return_ht;
                }
            }
        }

        /// <summary>
        /// �������洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �����������������������᷵�����ݼ��� ��������Ӱ��������
        /// </summary>
        /// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="P_ht_in">��ϣ����Ӧ�洢���̴������,keysΪ������ǣ�valuesΪ����ֵ</param>
        Hashtable I_Dblink.RunProc_CMD(string P_cmd, string DTname, Hashtable P_ht_in)
        {
            SqlConnection Conn = new SqlConnection(ConnStr);
            try
            {
                int kp = 0;
                DataSet dtreturn = new DataSet();
                Conn.Open();
                SqlDataAdapter Da = new SqlDataAdapter();
                Da.SelectCommand = new SqlCommand();
                Da.SelectCommand.Connection = Conn;
                Da.SelectCommand.CommandText = P_cmd;
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                //ѭ����Ӵ洢���̲���
                IDictionaryEnumerator myEnumerator = P_ht_in.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    SqlParameter param = new SqlParameter();
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = myEnumerator.Key.ToString();
                   // param.Value = myEnumerator.Value;
                    if (myEnumerator.Value == null)
                    {
                        // myEnumerator.Value = DBNull.Value;
                        param.Value = DBNull.Value;
                    }
                    else
                    {
                        param.Value = myEnumerator.Value;
                    }
                    Da.SelectCommand.Parameters.Add(param);
                }


                kp = Da.Fill(dtreturn);
                Dispose(Conn);
                if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                {
                    dtreturn.Tables[0].TableName = DTname;
                }
                return_ht["return_ds"] = dtreturn;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = -1;

                return return_ht;
            }
            catch (Exception Ex)
            {
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = Ex.ToString();
                return_ht["return_other"] = -1;

                return return_ht;
            }
        }


        /// <summary>
        /// �������洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �����������������������᷵�����ݼ��� ��������Ӱ��������
        /// </summary>
        /// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="P_ht_in">��ϣ����Ӧ�洢���̴������,keysΪ������ǣ�valuesΪ����ֵ</param>
        /// <param name="RedisKey">��</param>
        /// <param name="RedisPZ">�����ַ���</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�</returns>
        Hashtable I_Dblink.RunProc_CMD(string P_cmd, string DTname, Hashtable P_ht_in, string RedisKey, string RedisPZ)
        {
            DataSet formRedis = RedisClass.RedisTryOnlyForDBhelper(RedisKey, RedisPZ);
            if (formRedis != null)
            {
                return_ht["return_ds"] = formRedis;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = -2;
                return return_ht;
            }
            else
            {
                SqlConnection Conn = new SqlConnection(ConnStr);
                try
                {
                    int kp = 0;
                    DataSet dtreturn = new DataSet();
                    Conn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter();
                    Da.SelectCommand = new SqlCommand();
                    Da.SelectCommand.Connection = Conn;
                    Da.SelectCommand.CommandText = P_cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    //ѭ����Ӵ洢���̲���
                    IDictionaryEnumerator myEnumerator = P_ht_in.GetEnumerator();
                    while (myEnumerator.MoveNext())
                    {
                        SqlParameter param = new SqlParameter();
                        param.Direction = ParameterDirection.Input;
                        param.ParameterName = myEnumerator.Key.ToString();
                        // param.Value = myEnumerator.Value;
                        if (myEnumerator.Value == null)
                        {
                            // myEnumerator.Value = DBNull.Value;
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            param.Value = myEnumerator.Value;
                        }
                        Da.SelectCommand.Parameters.Add(param);
                    }


                    kp = Da.Fill(dtreturn);
                    Dispose(Conn);
                    if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                    {
                        dtreturn.Tables[0].TableName = DTname;
                    }
                    return_ht["return_ds"] = dtreturn;
                    return_ht["return_float"] = true;
                    return_ht["return_errmsg"] = "";
                    return_ht["return_other"] = -1;

                    return return_ht;
                }
                catch (Exception Ex)
                {
                    Dispose(Conn);

                    return_ht["return_ds"] = null;
                    return_ht["return_float"] = false;
                    return_ht["return_errmsg"] = Ex.ToString();
                    return_ht["return_other"] = -1;

                    return return_ht;
                }
            }
        }

        /// <summary>
        /// �������洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �����������������������᷵�����ݼ��� ��������Ӱ��������
        /// </summary>
        /// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="P_ht_in">��ϣ����Ӧ�洢���̴������</param>
        /// <param name="P_ht_out">��ϣ����Ӧ�洢���̴�������,��ַ</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�</returns>
        Hashtable I_Dblink.RunProc_CMD(string P_cmd, string DTname, Hashtable P_ht_in, ref Hashtable P_ht_out)
        {
            SqlConnection Conn = new SqlConnection(ConnStr);
            try
            {
                int kp = 0;
                DataSet dtreturn = new DataSet();
                Conn.Open();
                SqlDataAdapter Da = new SqlDataAdapter();
                Da.SelectCommand = new SqlCommand();
                Da.SelectCommand.Connection = Conn;
                Da.SelectCommand.CommandText = P_cmd;
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                //ѭ������������
                IDictionaryEnumerator myEnumerator1 = P_ht_in.GetEnumerator();
                myEnumerator1.Reset();
                while (myEnumerator1.MoveNext())
                {
                    SqlParameter param1 = new SqlParameter();
                    param1.Direction = ParameterDirection.Input;
                    param1.ParameterName = myEnumerator1.Key.ToString();
                    //param1.Value = myEnumerator1.Value;
                    if (myEnumerator1.Value == null)
                    {
                        // myEnumerator.Value = DBNull.Value;
                        param1.Value = DBNull.Value;
                    }
                    else
                    {
                        param1.Value = myEnumerator1.Value;
                    }
                    Da.SelectCommand.Parameters.Add(param1);
                }


                //ѭ������������
                IDictionaryEnumerator myEnumerator2 = P_ht_out.GetEnumerator();
                myEnumerator2.Reset();
                while (myEnumerator2.MoveNext())
                {
                    SqlParameter param2 = new SqlParameter();
                    param2.Direction = ParameterDirection.Output;
                    param2.ParameterName = myEnumerator2.Key.ToString();
                    param2.Value = myEnumerator2.Value;
                    Da.SelectCommand.Parameters.Add(param2);
                }

                kp = Da.Fill(dtreturn);




                //ѭ����ֵ�µ��������
                Hashtable P_ht_out_temp = new Hashtable();
                IDictionaryEnumerator myEnumerator3 = P_ht_out.GetEnumerator();
                myEnumerator3.Reset();
                while (myEnumerator3.MoveNext())
                {
                    P_ht_out_temp.Add(myEnumerator3.Key.ToString(), Da.SelectCommand.Parameters[myEnumerator3.Key.ToString()].Value.ToString());
                }
                P_ht_out = P_ht_out_temp;


                Dispose(Conn);
                if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                {
                    dtreturn.Tables[0].TableName = DTname;
                }
                return_ht["return_ds"] = dtreturn;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = -1;

                return return_ht;
            }
            catch (Exception Ex)
            {
                Dispose(Conn);

                return_ht["return_ds"] = null;
                return_ht["return_float"] = false;
                return_ht["return_errmsg"] = Ex.ToString();
                return_ht["return_other"] = -1;

                return return_ht;
            }
        }



                /// <summary>
        /// �������洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �����������������������᷵�����ݼ��� ��������Ӱ��������
        /// </summary>
        /// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="P_ht_in">��ϣ����Ӧ�洢���̴������</param>
        /// <param name="P_ht_out">��ϣ����Ӧ�洢���̴�������,��ַ</param>
        /// <param name="RedisKey">��</param>
        /// <param name="RedisPZ">�����ַ���</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�</returns>
        Hashtable I_Dblink.RunProc_CMD(string P_cmd, string DTname, Hashtable P_ht_in, ref Hashtable P_ht_out, string RedisKey, string RedisPZ)
        {
            DataSet formRedis = RedisClass.RedisTryOnlyForDBhelper(RedisKey, RedisPZ);
            if (formRedis != null)
            {
                return_ht["return_ds"] = formRedis;
                return_ht["return_float"] = true;
                return_ht["return_errmsg"] = "";
                return_ht["return_other"] = -2;
                return return_ht;
            }
            else
            {
                SqlConnection Conn = new SqlConnection(ConnStr);
                try
                {
                    int kp = 0;
                    DataSet dtreturn = new DataSet();
                    Conn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter();
                    Da.SelectCommand = new SqlCommand();
                    Da.SelectCommand.Connection = Conn;
                    Da.SelectCommand.CommandText = P_cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    //ѭ������������
                    IDictionaryEnumerator myEnumerator1 = P_ht_in.GetEnumerator();
                    myEnumerator1.Reset();
                    while (myEnumerator1.MoveNext())
                    {
                        SqlParameter param1 = new SqlParameter();
                        param1.Direction = ParameterDirection.Input;
                        param1.ParameterName = myEnumerator1.Key.ToString();
                        //param1.Value = myEnumerator1.Value;
                        if (myEnumerator1.Value == null)
                        {
                            // myEnumerator.Value = DBNull.Value;
                            param1.Value = DBNull.Value;
                        }
                        else
                        {
                            param1.Value = myEnumerator1.Value;
                        }
                        Da.SelectCommand.Parameters.Add(param1);
                    }


                    //ѭ������������
                    IDictionaryEnumerator myEnumerator2 = P_ht_out.GetEnumerator();
                    myEnumerator2.Reset();
                    while (myEnumerator2.MoveNext())
                    {
                        SqlParameter param2 = new SqlParameter();
                        param2.Direction = ParameterDirection.Output;
                        param2.ParameterName = myEnumerator2.Key.ToString();
                        param2.Value = myEnumerator2.Value;
                        Da.SelectCommand.Parameters.Add(param2);
                    }

                    kp = Da.Fill(dtreturn);




                    //ѭ����ֵ�µ��������
                    Hashtable P_ht_out_temp = new Hashtable();
                    IDictionaryEnumerator myEnumerator3 = P_ht_out.GetEnumerator();
                    myEnumerator3.Reset();
                    while (myEnumerator3.MoveNext())
                    {
                        P_ht_out_temp.Add(myEnumerator3.Key.ToString(), Da.SelectCommand.Parameters[myEnumerator3.Key.ToString()].Value.ToString());
                    }
                    P_ht_out = P_ht_out_temp;


                    Dispose(Conn);
                    if (dtreturn.Tables.Count > 0 && DTname != null && DTname.Trim() != "")
                    {
                        dtreturn.Tables[0].TableName = DTname;
                    }
                    return_ht["return_ds"] = dtreturn;
                    return_ht["return_float"] = true;
                    return_ht["return_errmsg"] = "";
                    return_ht["return_other"] = -1;

                    return return_ht;
                }
                catch (Exception Ex)
                {
                    Dispose(Conn);

                    return_ht["return_ds"] = null;
                    return_ht["return_float"] = false;
                    return_ht["return_errmsg"] = Ex.ToString();
                    return_ht["return_other"] = -1;

                    return return_ht;
                }
            }
        }



        private void StartSaveRedis(string P_cmd, Hashtable P_ht_in, int kp)
        {
            try
            {
                //����ǰ����������Ӱ������������Redis������SQLredis�� Ŀǰֻ֧����ͨ�ı�׼��ʽ�����ϸ�ʵ��ʹ�ã�Ҫ������֤һ�¡�
                //����ֻ֧��and,��or�Ļ����ġ�����Ҳֻ����=�ţ� ���Ⱥţ����ں�֮��Ķ����С�
                if (kp > 0)
                {
                    string typeS = "";
                    //��ȡ����ͬʱȷ���������
                    Regex r = new Regex(" update " + "(.*?)" + " set ", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    Match m = r.Match("    " + P_cmd);
                    string tablename = "";
                    tablename = m.Success ? m.Groups[1].Value.Replace("[", "").Replace("]", "").Trim() : "";
                    if (tablename != "") //���Ǹ���
                    {
                        typeS = "����";
                    }
                    else
                    {
                        Regex r2 = new Regex(" insert " + "(.*?)" + "\\(", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        Match m2 = r2.Match("    " + P_cmd);
                        tablename = m2.Success ? Regex.Replace(m2.Groups[1].Value, "into ", "", RegexOptions.IgnoreCase).Replace("[", "").Replace("]", "").Trim() : "";
                        if (tablename != "") //���ǲ���
                        {
                            typeS = "����";
                        }
                    }
                    if (typeS == "����" || typeS == "����")
                    {
                        string dllfile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Bin\\SqlRedisForFMDBHelper_" + tablename + ".dll";
                        if (File.Exists(dllfile))
                        {

                            //���»�����ֶκ�ֵ
                            Dictionary<string, string> filed = new Dictionary<string, string>();
                            //���µ�������ֵ
                            Dictionary<string, string> where = new Dictionary<string, string>();

                            switch (typeS)
                            {
                                case "����":
                                    Regex r_g = new Regex(" set " + "(.*?)" + " where ", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                                    string[] temp = r_g.Match(P_cmd + " where ").Groups[1].Value.Trim().Split(',');
                                    foreach (string OneZDtemp in temp)
                                    {
                                        string[] ZD = OneZDtemp.Split('=');
                                        filed.Add(ZD[0].Replace("[", "").Replace("]", "").Trim(), ZD[1].Replace("'", "").Trim());
                                    }
                                    if (P_cmd.ToLower().IndexOf(" where ") > 0)
                                    {
                                        Regex r_g2 = new Regex(" where " + "(.*?)" + " ���λ ", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                                        string[] temp2 = Regex.Replace(r_g2.Match(P_cmd + " ���λ ").Groups[1].Value.Trim(), " and ", "&", RegexOptions.IgnoreCase).Split('&');
                                        foreach (string OneZDtemp in temp2)
                                        {
                                            string[] TJ = OneZDtemp.Split('=');
                                            where.Add(TJ[0].Replace("[", "").Replace("]", "").Trim(), TJ[1].Replace("'", "").Trim());
                                        }
                                    }
                                    break;
                                case "����":
                                    Regex r_g3 = new Regex("\\(" + "(.*?)" + " ���λ ", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                                    string[] temp3 = Regex.Replace(Regex.Replace("    " + r_g3.Match("    " + P_cmd + " ���λ ").Groups[1].Value, " into ", "", RegexOptions.IgnoreCase).Replace(" ",""), "\\)values\\(", "&", RegexOptions.IgnoreCase).Split('&');
                                    string[] zd_name = temp3[0].Replace("(", "").Replace(")", "").Split(',');
                                    string[] zd_value = temp3[1].Replace("(", "").Replace(")", "").Split(',');
                                    for (int p = 0; p < zd_name.Length; p++)
                                    {
                                        filed.Add(zd_name[p].Replace("'", "").Replace("[", "").Replace("]", "").Trim(), zd_value[p].Replace("'", "").Trim());
                                    }

                                    break;
                                default:
                                    break;
                            }





                            //��ʼ���ô���
                            Assembly serviceAsm = GetCache.InstanceCacheEx.InstanceCache(dllfile);
                            Type typeName = serviceAsm.GetType("SqlRedisForFMDBHelper_" + tablename + "." + "GO");
                            object instance = serviceAsm.CreateInstance("SqlRedisForFMDBHelper_" + tablename + "." + "GO");
                            object rtnObj = typeName.GetMethod("TryUpdateRedis").Invoke(instance, new object[] { typeS, P_cmd, P_ht_in, tablename, filed, where });
                            //����������д����־
                            if (rtnObj == null || rtnObj.ToString() != "ok")
                            {
                                ;
                            }
                        }
                    }


                }
                //===============================================
            }
            catch (Exception exx)
            {
                string trytype = "Redisʧ�ܣ�" + exx.ToString();
                //д����־
            }
        }

        private void StartSaveRedis_al(ArrayList SQLStringList, Hashtable P_ht_in, ArrayList kpkp)
        {
            try
            {
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    StartSaveRedis(SQLStringList[n].ToString(), P_ht_in, Convert.ToInt32(kpkp[n]));
                }
            }
            catch (Exception exx)
            {
                string trytype = "Redisʧ��ʧ��ʧ�ܣ�" + exx.ToString();
                //д����־
            }
        }

    }
}
