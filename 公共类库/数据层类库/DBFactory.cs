namespace FMDBHelperClass
{
	/// <summary>
	/// �����������Դ��������,Ҳ���Ǿ��幤����ɫ
	/// </summary>
	public class DBFactory : I_DBFactory
	{
 
        /// <summary>
        /// ��ȡ���ݿ�������ʵ��,����sql���ݿ�
        /// </summary>
        public I_Dblink DbLinkSqlMain(string ConnConfig)
        {
            return new Dblink_Sql_Main(ConnConfig); 
        }
	}
}
