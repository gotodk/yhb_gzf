namespace FMDBHelperClass
{
	/// <summary>
	/// ���幤�����ýӿڣ�Ҳ������ν�ĳ��󹤳���ɫ
	/// </summary>
	public interface I_DBFactory
	{

        /// <summary>
        /// ��ȡ���ݿ�������ʵ��,����sql
        /// </summary>
        /// <param name="ConnConfig">���ݿ�����key</param>
        /// <returns></returns>
        I_Dblink DbLinkSqlMain(string ConnConfig);
	}
}
