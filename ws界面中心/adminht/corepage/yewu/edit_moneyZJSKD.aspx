<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_moneyZJSKD.aspx.cs" Inherits="yewu_edit_moneyZJSKD" %>

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
                     $("#searchopenyhbspgogo_M_HID").hide();

                 }
                 else {
                     //增加季度数量选择设置
                     $("#fzzixiangmu").after("<div id='ggjdxx' style='margin-left:30px; color:red; '>快速操作：按照<input type='text' id='ggjdxx_ss' name='ggjdxx_ss'  ></input>季度租金填写金额</div>");
                     $("#ggjdxx_ss").keyup(function () {
                         //如果输入非数字，则替换为'' 
                         this.value = this.value.replace(/[^\d]/g, '');

  
                         var tt_MZ_zj_bcsk = $("#MHjdzj").val() * 10000 * $("#ggjdxx_ss").val() / 10000;
                         var tt_MZ_wy_bcsk = $("#MHjdwyf").val() * 10000 * $("#ggjdxx_ss").val() / 10000;
                         var tt_MZ_dt_bcsk = $("#MHjddtf").val() * 10000 * $("#ggjdxx_ss").val() / 10000;
                         var tt_MZ_q_bcsk = $("#MHjdqtfy").val() * 10000 * $("#ggjdxx_ss").val() / 10000;
                         $("#MZ_zj_bcsk").val(tt_MZ_zj_bcsk.toFixed(2));
                         $("#MZ_wy_bcsk").val(tt_MZ_wy_bcsk.toFixed(2));
                         $("#MZ_dt_bcsk").val(tt_MZ_dt_bcsk.toFixed(2));
                         //$("#MZ_q_bcsk").val(tt_MZ_q_bcsk.toFixed(2));

                     })
                 }
                 //预制合并区域
                 $("#fz0001").after("<div id='stzstr' style='margin-left:50px;'></div>");
                 $("#fenzu0003").after("<div id='xtzstr' style='margin-left:50px;'></div>");

                 //分组线加重
                 $("#fenzu0003").css({ "border-top": "1px solid #0066CC;" });
               
                 

                 window.setInterval(function () {
                     //计算合计值中的数据

                     //合计凭证
                     $("#Mpz").val("租:" + $("#MZ_zj_pzh").val() + "，物:" + $("#MZ_wy_pzh").val() + "，电:" + $("#MZ_dt_pzh").val() + "，其:" + $("#MZ_q_pzh").val() + "");

                     //合计本次收款
                     var MZ_zj_bcsk = $("#MZ_zj_bcsk").val() * 10000 / 10000;
                     var MZ_wy_bcsk = $("#MZ_wy_bcsk").val() * 10000 / 10000;
                     var MZ_dt_bcsk = $("#MZ_dt_bcsk").val() * 10000 / 10000;
                     var MZ_q_bcsk = $("#MZ_q_bcsk").val() * 10000 / 10000;
                     var hjsk = (MZ_zj_bcsk * 10000 / 10000 + MZ_wy_bcsk * 10000 / 10000 + MZ_dt_bcsk * 10000 / 10000 + MZ_q_bcsk * 10000 / 10000);
                     $("#Mje").val(hjsk.toFixed(2));

                     //计算子项欠款
                     var qk_zj = ($("#MZ_zj_xmje").val() * 10000 / 10000 - $("#MZ_zj_yjn").val() * 10000 / 10000 - $("#MZ_zj_bcsk").val() * 10000 / 10000);
                     $("#MZ_zj_qiankuan").val((qk_zj * 10000 / 10000).toFixed(2));
                     var qk_wy = ($("#MZ_wy_xmje").val() * 10000 / 10000 - $("#MZ_wy_yjn").val() * 10000 / 10000 - $("#MZ_wy_bcsk").val() * 10000 / 10000);
                     $("#MZ_wy_qiankuan").val((qk_wy * 10000 / 10000).toFixed(2));
                     var qk_dt = ($("#MZ_dt_xmje").val() * 10000 / 10000 - $("#MZ_dt_yjn").val() * 10000 / 10000 - $("#MZ_dt_bcsk").val() * 10000 / 10000);
                     $("#MZ_dt_qiankuan").val((qk_dt * 10000 / 10000).toFixed(2));
                     var qk_q = ($("#MZ_q_xmje").val() * 10000 / 10000 - $("#MZ_q_yjn").val() * 10000 / 10000 - $("#MZ_q_bcsk").val() * 10000 / 10000);
                     $("#MZ_q_qiankuan").val((qk_q * 10000 / 10000).toFixed(2));

                     //计算合计欠款
                     var MZ_zj_qiankuan = $("#MZ_zj_qiankuan").val() * 10000 / 10000;
                     var MZ_wy_qiankuan = $("#MZ_wy_qiankuan").val() * 10000 / 10000;
                     var MZ_dt_qiankuan = $("#MZ_dt_qiankuan").val() * 10000 / 10000;
                     var MZ_q_qiankuan = $("#MZ_q_qiankuan").val() * 10000 / 10000;
                     var hjqiankuan = (MZ_zj_qiankuan * 10000 / 10000 + MZ_wy_qiankuan * 10000 / 10000 + MZ_dt_qiankuan * 10000 / 10000 + MZ_q_qiankuan * 10000 / 10000);
                     $("#MZ_all_qiankuan").val((hjqiankuan * 10000 / 10000).toFixed(2));



                     //
                     //把上方合计弄成一行显示
                     $("#stzstr").html("租赁期限：" + $("#MHzlqx").val() + ",季度租金：" + $("#MHjdzj").val() + ",季度物业费：" + $("#MHjdwyf").val() + ",季度电梯费：" + $("#MHjddtf").val() + ",季度其他费：" + $("#MHjdqtfy").val() + "");
                     $("#MHzlqx").closest(".form-group").hide();
                     $("#MHjdzj").closest(".form-group").hide();
                     $("#MHjdwyf").closest(".form-group").hide();
                     $("#MHjddtf").closest(".form-group").hide();
                     $("#MHjdqtfy").closest(".form-group").hide();

                     //把下方合计弄成一行显示
                     $("#xtzstr").html("应交费用合计：" + $("#MZ_all_xmje").val() + ",已缴纳合计：" + $("#MZ_all_yjn").val() + ",本次收款合计：" + $("#Mje").val() + ",欠款合计：" + $("#MZ_all_qiankuan").val() + "");
                     $("#MZ_all_xmje").closest(".form-group").hide();
                     $("#MZ_all_yjn").closest(".form-group").hide();
                     $("#Mpz").closest(".form-group").hide();
                     $("#Mje").closest(".form-group").hide();
                     $("#MZ_all_qiankuan").closest(".form-group").hide();
 

                 }, 800);


                 //处理合同选择弹窗
                 var dfx_str_rzhtbh = "#show_searchopenyhbspgogo_M_HID";
                 var oldzhi_rzhtbh = $(dfx_str_rzhtbh).text();
                 var jiancha_rzhtbh = window.setInterval(function () {
                     //带入字段
                     if ($(dfx_str_rzhtbh).text() != oldzhi_rzhtbh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_rzhtbh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[地区")
                                 { $("#Fdiqu").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[坐落")
                                 { $("#Fzuoluo").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[姓名")
                                 { $("#Cxingming").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[身份信息")
                                 { $("#Cshenfenzheng").val($.trim(arr_z[1]).replace("]", "")); }

                                 if (arr_z[0] == "[租赁期限")
                                 { $("#MHzlqx").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[季度租金")
                                 { $("#MHjdzj").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[季度物业费")
                                 { $("#MHjdwyf").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[季度电梯费")
                                 { $("#MHjddtf").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[季度其他费")
                                 { $("#MHjdqtfy").val($.trim(arr_z[1]).replace("]", "")); }

                                 if (arr_z[0] == "[已缴纳租金")
                                 { $("#MZ_zj_yjn").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[已缴纳物业费")
                                 { $("#MZ_wy_yjn").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[已缴纳电梯费")
                                 { $("#MZ_dt_yjn").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[已缴纳其他费")
                                 { $("#MZ_q_yjn").val($.trim(arr_z[1]).replace("]", "")); }
                             }

                             //更换合同后，把其他相关字段计算出来并显示。

                             //计算房租金额
                             var Hzlqx = $("#MHzlqx").val();
                             var Hjdzj = $("#MHjdzj").val() * Hzlqx * 10000 / 10000;
                             var Hjdwyf = $("#MHjdwyf").val() * Hzlqx * 10000 / 10000;
                             var Hjddtf = $("#MHjddtf").val() * Hzlqx * 10000 / 10000;
                             var Hjdqtfy = $("#MHjdqtfy").val() * Hzlqx * 10000 / 10000;
                             var Hje = (Hjdzj * 10000 / 10000 + Hjdwyf * 10000 / 10000 + Hjddtf * 10000 / 10000 + Hjdqtfy * 10000 / 10000);
                             $("#MZ_zj_xmje").val(Hjdzj.toFixed(2));
                             $("#MZ_wy_xmje").val(Hjdwyf.toFixed(2));
                             $("#MZ_dt_xmje").val(Hjddtf.toFixed(2));
                             $("#MZ_q_xmje").val(Hjdqtfy.toFixed(2));
                             $("#MZ_all_xmje").val(Hje.toFixed(2));


                             //计算已缴纳金额，包括合计已缴纳金额
                             var MZ_zj_yjn = $("#MZ_zj_yjn").val() * 10000 / 10000;
                             var MZ_wy_yjn = $("#MZ_wy_yjn").val() * 10000 / 10000;
                             var MZ_dt_yjn = $("#MZ_dt_yjn").val() * 10000 / 10000;
                             var MZ_q_yjn = $("#MZ_q_yjn").val() * 10000 / 10000;
                             var hejijiaona = (MZ_zj_yjn * 10000 / 10000 + MZ_wy_yjn * 10000 / 10000 + MZ_dt_yjn * 10000 / 10000 + MZ_q_yjn * 10000 / 10000);
                             $("#MZ_all_yjn").val(hejijiaona.toFixed(2));

                        

                         }

                         oldzhi_rzhtbh = $(dfx_str_rzhtbh).text();
                     }
                 }, 500);

              
 
        });
        </script>


    
           <!-- 增加一些特殊处理按钮，例如提交，收货 -->
    <script type="text/javascript">
        jQuery(function ($) {

            //调用批量操作的接口
            function begin_ajax(zdyname, xuanzhongzhi, zheshiyige_FID)
            {
                $.ajax({
                    url: '/pucu/gqzidingyi.aspx?zdyname=' + zdyname + '&xuanzhongzhi=' + xuanzhongzhi + '&zheshiyige_FID=' + zheshiyige_FID,
                    type: 'post',
                    data: null,
                    cache: false,
                    dataType: 'html',
                    success: function (data) {
                        //显示调用接口并刷新当前页面
                        bootbox.alert({
                            message: data,
                            callback: function () {
                                var newurl = window.location.href;
                                location.href = newurl;

                            }
                        });


                    },
                    error: function () {
                        bootbox.alert('操作失败，接口调用失败！');
                    }
                });
            }


            

            function add_anniu_spsp()
            {
                //根据现有状态，添加特殊按钮
                if ($("#fifsssss_Mzt").text() == "草稿") {
                    var bjm = "tijiaogo";
                    var bjm_wenben = "提交";
                    var bjm_tubiao = "fa-check blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        
                        begin_ajax("shengxiao", getUrlParam("idforedit"), "160622000083")

                    });

                }
               
                //
                


            }
            
            if (getUrlParam("showinfo") == "1") {
                //数据加载完成才执行，只执行一次
                var jiancha_bdjzwc = window.setInterval(function () {
                    if ($("#editloadinfo").hasClass("hide")) {
                        clearInterval(jiancha_bdjzwc);
                        add_anniu_spsp();
                    }

                }, 1000);
            }
            

        });
        </script>


               <!-- 动态调整界面 -->
    <script type="text/javascript">
        jQuery(function ($) {
            $("#fzzixiangmu").after("<div class='row'><div class='col-sm-3' id='zzz_ys1'></div><div class='col-sm-3'  id='zzz_ys2'></div><div class='col-sm-3'  id='zzz_ys3'></div><div class='col-sm-3'  id='zzz_ys4'></div></div>");
            var ys1_1 = $("#MZ_zj_xmje").closest(".form-group");
            var ys1_2 = $("#MZ_zj_yjn").closest(".form-group");
            var ys1_3 = $("#MZ_zj_pzh").closest(".form-group");
            var ys1_4 = $("#MZ_zj_bcsk").closest(".form-group");
            var ys1_5 = $("#MZ_zj_qiankuan").closest(".form-group");
       
 
            var ys2_1 = $("#MZ_wy_xmje").closest(".form-group");
            var ys2_2 = $("#MZ_wy_yjn").closest(".form-group");
            var ys2_3 = $("#MZ_wy_pzh").closest(".form-group");
            var ys2_4 = $("#MZ_wy_bcsk").closest(".form-group");
            var ys2_5 = $("#MZ_wy_qiankuan").closest(".form-group");

 
            var ys3_1 = $("#MZ_dt_xmje").closest(".form-group");
            var ys3_2 = $("#MZ_dt_yjn").closest(".form-group");
            var ys3_3 = $("#MZ_dt_pzh").closest(".form-group");
            var ys3_4 = $("#MZ_dt_bcsk").closest(".form-group");
            var ys3_5 = $("#MZ_dt_qiankuan").closest(".form-group");

            var ys4_1 = $("#MZ_q_xmje").closest(".form-group");
            var ys4_2 = $("#MZ_q_yjn").closest(".form-group");
            var ys4_3 = $("#MZ_q_pzh").closest(".form-group");
            var ys4_4 = $("#MZ_q_bcsk").closest(".form-group");
            var ys4_5 = $("#MZ_q_qiankuan").closest(".form-group");

            $("#zzz_ys1").append(ys1_1);
            $("#zzz_ys1").append(ys1_2);
            $("#zzz_ys1").append(ys1_3);
            $("#zzz_ys1").append(ys1_4);
            $("#zzz_ys1").append(ys1_5);
            $('label[for^="MZ_zj_"]').attr("class", "col-sm-8  no-padding-right text-left ");
            $('input[id^="MZ_zj_"]').attr("class", "col-xs-12 col-sm-12 ");

            $("#zzz_ys2").append(ys2_1);
            $("#zzz_ys2").append(ys2_2);
            $("#zzz_ys2").append(ys2_3);
            $("#zzz_ys2").append(ys2_4);
            $("#zzz_ys2").append(ys2_5);
            $('label[for^="MZ_wy_"]').attr("class", "col-sm-8  no-padding-right text-left ");
            $('input[id^="MZ_wy_"]').attr("class", "col-xs-12 col-sm-12 ");

            $("#zzz_ys3").append(ys3_1);
            $("#zzz_ys3").append(ys3_2);
            $("#zzz_ys3").append(ys3_3);
            $("#zzz_ys3").append(ys3_4);
            $("#zzz_ys3").append(ys3_5);
            $('label[for^="MZ_dt_"]').attr("class", "col-sm-8  no-padding-right text-left ");
            $('input[id^="MZ_dt_"]').attr("class", "col-xs-12 col-sm-12 ");

            $("#zzz_ys4").append(ys4_1);
            $("#zzz_ys4").append(ys4_2);
            $("#zzz_ys4").append(ys4_3);
            $("#zzz_ys4").append(ys4_4);
            $("#zzz_ys4").append(ys4_5);
            $('label[for^="MZ_q_"]').attr("class", "col-sm-8  no-padding-right text-left ");
            $('input[id^="MZ_q_"]').attr("class", "col-xs-12 col-sm-12 ");

            $("#fzzixiangmu02").hide();
            $("#fenzu003").hide();
            $("#fenzu0003").hide();
            
        });
            </script>
</asp:Content>

