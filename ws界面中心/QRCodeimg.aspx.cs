﻿using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing.Imaging;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using System.IO;
using System.Text;

public partial class QRCodeimg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["rqstr"] == null || Request["tmlx"] == null)
        {
            return;
        }

        //二维码
        if (Request["tmlx"].ToString() == "2")
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;

            qrCodeEncoder.QRCodeScale = 4;

            qrCodeEncoder.QRCodeVersion = 8;

            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            //String data = "Hello 二维码！";
            String data = Request["rqstr"].ToString();
            //Response.Write(data);

            System.Drawing.Bitmap image = qrCodeEncoder.Encode(data);

            System.IO.MemoryStream MStream = new System.IO.MemoryStream();

            image.Save(MStream, System.Drawing.Imaging.ImageFormat.Png);

            Response.ClearContent();

            Response.ContentType = "image/Png";

            Response.BinaryWrite(MStream.ToArray());
        }
        //一维码条码 128码
        if (Request["tmlx"].ToString() == "1")
        { 
            string num = Request["rqstr"].ToString();
            //string num = "KM20110715002";
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Drawing.Image myimg = BarCodeHelper.MakeBarcodeImage(num, 1, true);
            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
            Response.End();

        }


    }
}