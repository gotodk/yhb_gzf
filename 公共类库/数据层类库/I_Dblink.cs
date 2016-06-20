using System.Collections;
using System.Data;

namespace FMDBHelperClass
{
	/// <summary>
	/// �������ݿ���������Ľӿڣ�Ҳ������ν�ĳ����Ʒ��ɫ
	/// </summary>
	public interface I_Dblink
	{




        /// <summary>
        /// �����ִ�У���֧������
        /// ִ�о�̬��䡣 ���������ݼ��� �᷵����Ӱ��������
        /// ������ִ�о�̬�����и��¡�����������������ڲ�ѯ������
        /// </summary>
        /// <param name="SQL">��Ҫִ�е�sql���</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ�,��Ӱ�������(return_ds��return_float��return_errmsg��return_other)</returns>
		Hashtable RunProc(string SQL);

        /// <summary>
        /// �����ִ�У���֧������
        /// ִ�о�̬��䡣 �᷵�����ݼ��� ��������Ӱ��������
        /// ������ִ�о�̬�����в�ѯ�������������ڲ��롢���²�����
        /// </summary>
        /// <param name="SQL">SQL���</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�(return_ds��return_float��return_errmsg��return_other)</returns>
		Hashtable RunProc(string SQL ,string DTname);


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
        Hashtable RunProc(string SQL, string DTname, string RedisKey, string RedisPZ);












        /// <summary>
        /// �����ִ�У���֧������
        /// ִ�ж�̬��䡣 �᷵�����ݼ��� ��������Ӱ��������
        /// ������ִ�ж�̬�����в�ѯ�������������ڲ��롢���²�����
        /// </summary>
        /// <param name="P_cmd">�����������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="P_ht_in">������ϣ��</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�(return_ds��return_float��return_errmsg��return_other)</returns>
        Hashtable RunParam_SQL(string P_cmd, string DTname, Hashtable P_ht_in);


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
        Hashtable RunParam_SQL(string P_cmd, string DTname, Hashtable P_ht_in, string RedisKey, string RedisPZ);




        /// <summary>
        /// ���ִ�У���֧������
        /// ִ�ж�̬��䡣 ���������ݼ��� �᷵����Ӱ��������
        /// ������ִ�ж�̬�����в��롢���²������������ڲ�ѯ������
        /// </summary>
        /// <param name="P_cmd">�����������</param>
        /// <param name="P_ht_in">������ϣ��</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�(return_ds��return_float��return_errmsg��return_other)</returns>
        Hashtable RunParam_SQL(string P_cmd, Hashtable P_ht_in);
        /// <summary>
        /// �����ִ�У�֧������
        /// ִ�ж�̬��䡣 ���������ݼ��� �᷵����Ӱ��������
        /// ������ִ�ж�̬�����в��롢���²������������ڲ�ѯ������
        /// </summary>
        /// <param name="SQLStringList">����������</param>
        /// <param name="P_ht_in">������ϣ��</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ������᷵�ؽ����</returns>
        Hashtable RunParam_SQL(ArrayList SQLStringList, Hashtable P_ht_in);

        /// <summary>
        /// �����ִ�У�֧������
        /// ִ�о�̬��䡣 ���������ݼ��� �᷵����Ӱ��������
        /// ������ִ�о�̬�����в��롢���²������������ڲ�ѯ������
        /// </summary>
        /// <param name="SQLStringList">����������</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ������᷵�ؽ����</returns>
        Hashtable RunParam_SQL(ArrayList SQLStringList);


		/// <summary>
        /// �޲����洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �᷵�����ݼ��� ��������Ӱ��������
		/// </summary>
		/// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">DataSet����</param>
		/// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�</returns>
        Hashtable RunProc_CMD(string P_cmd, string DTname);

        /// <summary>
        /// �޲����洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �᷵�����ݼ��� ��������Ӱ��������
        /// </summary>
        /// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">���ص����ݼ�DataTable��</param>
        /// <param name="RedisKey">��</param>
        /// <param name="RedisPZ">�����ַ���</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�</returns>
        Hashtable RunProc_CMD(string P_cmd, string DTname, string RedisKey, string RedisPZ);

		/// <summary>
        /// �������洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �����������������������᷵�����ݼ��� ��������Ӱ��������
		/// </summary>
		/// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">DataSet����</param>
		/// <param name="P_ht_in">��ϣ����Ӧ�洢���̴������,keysΪ������ǣ�valuesΪ����ֵ</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�</returns>
        Hashtable RunProc_CMD(string P_cmd, string DTname, Hashtable P_ht_in);


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
        Hashtable RunProc_CMD(string P_cmd, string DTname, Hashtable P_ht_in, string RedisKey, string RedisPZ);

		/// <summary>
        /// �������洢����ִ�У������ڴ洢����������ʵ�֡�
        /// �����������������������᷵�����ݼ��� ��������Ӱ��������
		/// </summary>
		/// <param name="P_cmd">�洢��������</param>
        /// <param name="DTname">DataSet����</param>
		/// <param name="P_ht_in">��ϣ����Ӧ�洢���̴������</param>
		/// <param name="P_ht_out">��ϣ����Ӧ�洢���̴�������,��ַ</param>
        /// <returns>���ؽ�����϶�Ӧ�Ĺ�ϣ��,����ִ���Ƿ�ɹ���ִ�н�������ݼ�</returns>
        Hashtable RunProc_CMD(string P_cmd, string DTname, Hashtable P_ht_in, ref Hashtable P_ht_out);


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
        Hashtable RunProc_CMD(string P_cmd, string DTname, Hashtable P_ht_in, ref Hashtable P_ht_out, string RedisKey, string RedisPZ);

		
	}
}
