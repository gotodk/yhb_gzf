<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_fordemohome.ascx.cs" Inherits="adminht_corepage_WUC_fordemohome" %>


<!-- 自定义预警弹窗 -->
<div id="dialog-message-yujing" class="hide">

    <div class="row">
        <div class="col-sm-12">

            <span class="red">若存在需要预警的信息，本弹窗仅在每月的首个周一全天出现！</span>

        </div>
        <!-- /.col -->
    </div>
    <div class="row">
        <hr />
        <div class="col-sm-12">

            <span>发现以下预警信息：</span><div style="width:0px;overflow:hidden"><input  /></div>

            <div id="xianshiquyu_yujing">
            <ul class="list-unstyled spaced2">

                <li class="bigger-110 ">
                    <a id="lla_s1" href="#">
                    <i class="ace-icon fa fa-exclamation-triangle"></i>
                    将在<span class="red" id="llb_s1">0</span>月份，租户资格到期的人数为：<span  id="llc_s1" class="red">0</span>人。
                        </a>
                </li>
                <li class="bigger-110">
                    <a id="lla_s2" href="#">
                    <i class="ace-icon fa fa-exclamation-triangle"></i>
                    将在<span id="llb_s2" class="red">0</span>月份，房租到期的人数为：<span id="llc_s2" class="red">0</span>人。
                        </a>
                </li>
                <li class="bigger-110">
                    <a  id="lla_s3" href="#">
                    <i class="ace-icon fa fa-exclamation-triangle"></i>
                    将在<span id="llb_s3" class="red">0</span>月份，入住合同到期的人数为：<span  id="llc_s3" class="red">0</span>人。
                        </a>
                </li>
                <li class="bigger-110">
                    <a id="lla_s4"  href="#">
                    <i class="ace-icon fa fa-exclamation-triangle"></i>
                    将在<span id="llb_s4"  class="red">0</span>月份，车位费到期的人数为：<span id="llc_s4"  class="red">0</span>人。
                    </a>
                </li>
            </ul>
            </div>

        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->


</div>
<!-- #dialog-message -->


                                <div class="row">

                                    <div class="col-sm-12">
										<div class="widget-box transparent">
											<div class="widget-header widget-header-flat">
												<h4 class="widget-title lighter">
													<i class="ace-icon fa fa-signal"></i>
													我的工作台
												</h4>

												<div class="widget-toolbar">
													<a href="#" data-action="collapse">
														<i class="ace-icon fa fa-chevron-up"></i>
													</a>
												</div>
											</div>

											<div class="widget-body">
												<div class="widget-main padding-4">
													 


                                                    <div class="space-12"></div>

                                                    <div class="row">
									                  
                                                        <%
                                                            if (dtgzt != null)
                                                            {
                                                                for (int i = 0; i < dtgzt.Rows.Count; i++)
                                                                {
                                                                    if (i < 4)
                                                                    {
                                                                    %>
                                                           <div class="col-sm-3">
									  <div class="well">
											<h4 class="green smaller lighter"><a class="red" href="<%=dtgzt.Rows[i]["Mdizhi"].ToString() %>"><%=dtgzt.Rows[i]["Mbiaoti"].ToString() %></a><a href="/adminht/corepage/bas/edit_mygzt.aspx?idforedit=gzt_<%=UserSession.唯一键%>_<%=i.ToString() %>&fff=1"><i id="gzt_<%=UserSession.唯一键%>_<%=i.ToString() %>" class="ace-icon fa fa-pencil-square-o align-top bigger-125 pull-right inline" style="cursor:pointer"></i></a></h4>
											<%=dtgzt.Rows[i]["Mbeiwanglu"].ToString() %>
										</div>
									</div>

                                                        <%
                                                                    }
                                                                }
                                                            }
                                                             %>
                                                               
								</div> <div class="row">
									                  
                                               <%
                                                            if (dtgzt != null)
                                                            {
                                                                for (int i = 0; i < dtgzt.Rows.Count; i++)
                                                                {
                                                                    if (i > 3)
                                                                    {
                                                                    %>
                                                           <div class="col-sm-3">
									  <div class="well">
											<h4 class="green smaller lighter"><a class="orange" href="<%=dtgzt.Rows[i]["Mdizhi"].ToString() %>"><%=dtgzt.Rows[i]["Mbiaoti"].ToString() %></a><a href="/adminht/corepage/bas/edit_mygzt.aspx?idforedit=gzt_<%=UserSession.唯一键%>_<%=i.ToString() %>&fff=1"><i id="gzt_<%=UserSession.唯一键%>_<%=i.ToString() %>" class="ace-icon fa fa-pencil-square-o align-top bigger-125 pull-right inline" style="cursor:pointer"></i></a></h4>
											<%=dtgzt.Rows[i]["Mbeiwanglu"].ToString() %>
										</div>
									</div>

                                                        <%
                                                                    }
                                                                }
                                                            }
                                                             %>                
								</div>




												</div><!-- /.widget-main -->
											</div><!-- /.widget-body -->
										</div><!-- /.widget-box -->
									</div><!-- /.col -->
								</div><!-- /.row -->
<!-- /.row -->






