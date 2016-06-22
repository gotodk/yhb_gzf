<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_rzht.aspx.cs" Inherits="yewu_edit_rzht" %>

<%@ Register Src="~/pucu/wuc_css.ascx" TagPrefix="uc1" TagName="wuc_css" %>
<%@ Register Src="~/pucu/wuc_content.ascx" TagPrefix="uc1" TagName="wuc_content" %>
<%@ Register Src="~/pucu/wuc_script.ascx" TagPrefix="uc1" TagName="wuc_script" %>




<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
    <uc1:wuc_css runat="server" ID="wuc_css" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
    <uc1:wuc_content runat="server" ID="wuc_content"  />
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">

       <!-- 附加的body底部本页专属的自定义js脚本 -->
    <uc1:wuc_script runat="server" ID="wuc_script" />
         <!-- 某些字段，在编辑时禁用，不想用新页面的情况使用 -->
    <script type="text/javascript">
             jQuery(function ($) {
                 if (getUrlParam("fff") == "1") {

                      
                 }
                  


                 var dfx_str_czrbh = "#show_searchopenyhbspgogo_H_CID";
                 var oldzhi_czrbh = $(dfx_str_czrbh).text();
                 var jiancha_czrbh = window.setInterval(function () {
                     //带入字段
                     if ($(dfx_str_czrbh).text() != oldzhi_czrbh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_czrbh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[姓名")
                                 { $("#Cxingming").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[身份信息")
                                 { $("#Cshenfenzheng").val($.trim(arr_z[1]).replace("]", "")); }
                             }
                         }

                         oldzhi_czrbh = $(dfx_str_czrbh).text();
                     }
                 }, 500);


                 var dfx_str_fwbh = "#show_searchopenyhbspgogo_H_FID";
                 var oldzhi_fwbh = $(dfx_str_fwbh).text();
                 var jiancha_fwbh = window.setInterval(function () {
                     //带入字段
                     if ($(dfx_str_fwbh).text() != oldzhi_fwbh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_fwbh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[地区")
                                 { $("#Fdiqu").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[坐落")
                                 { $("#Fzuoluo").val($.trim(arr_z[1]).replace("]", "")); }
                             }
                         }

                         oldzhi_fwbh = $(dfx_str_fwbh).text();
                     }
                 }, 500);


 
        });
        </script>
</asp:Content>

