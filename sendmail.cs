using System;
using System.Configuration;
using System.Web.Mail;
using System.IO;
using System.Web;
using WebshreeCommonLibrary;
using ClassLibrary;
using System.Data;
using DAL;

public class SendMail
{
    //
    //Code for setting email id and password 
    //

    //static string fromAddress = "adminmail@fluidwatches.in";// Gmail Address from where you send the mail
    //static string fromPassword = "okmijn215"; //Password of your gmail address
    //static string smtpServer = "mail.fluidwatches.in";

    static string fromAddress = "adminmail@webshree.com";// Gmail Address from where you send the mail
    static string fromPassword = "okmijn#215"; //Password of your gmail address
    static string smtpServer = "mail.webshree.com";

    static string visibleName = "\"fashionkart.uk\"" + "care@fashionkart.uk";

    static string AdminEmail = "vijay.singh@webshree.in";//info@houseofbrands.in
    static string BCCEmail = "";


    //static string AdminEmail = "care@fashionkart.uk";
    //static string BCCEmail = "pujagoyal21@gmail.com";


    static string time;
    //
    //Code for getting current Date Time
    //
    DateTime dt = new DateTime();
    public SendMail()
    {
        dt = DateTime.Now;
        time = dt.ToString("dd,MMM yyyy hh:mm tt");
    }
    //
    //Code for Sending Mail
    //
    public void sendMail(string EmailId, string subject, string myString)
    {
        var toAddress = EmailId;
        int cdoBasic = 1;
        int cdoSendUsingPort = 2;
        System.Web.Mail.MailMessage obj = new System.Web.Mail.MailMessage();
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", fromAddress);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", fromPassword);
        obj.To = toAddress;
        obj.Bcc = BCCEmail;

        obj.From = visibleName;
        obj.Subject = subject;
        obj.Body = myString;
        obj.BodyFormat = MailFormat.Html;
        try
        {
            SmtpMail.SmtpServer = smtpServer;
            SmtpMail.Send(obj);
        }
        catch (Exception ex) { }
    }

    public void sendMailToAdmin(string EmailId, string subject, string myString)
    {
        var toAddress = EmailId;
        int cdoBasic = 1;
        int cdoSendUsingPort = 2;
        System.Web.Mail.MailMessage obj = new System.Web.Mail.MailMessage();
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", fromAddress);
        obj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", fromPassword);
        obj.To = toAddress;
        obj.Bcc = "";

        obj.From = visibleName;
        obj.Subject = subject;
        obj.Body = myString;
        obj.BodyFormat = MailFormat.Html;
        try
        {
            SmtpMail.SmtpServer = smtpServer;
            SmtpMail.Send(obj);
        }
        catch (Exception ex) { }
    }

    //
    //=======================================(1)Code for Sending Welcome Mail
    //
    public void sendWelcomeMsg(string Email, string FullName, string Password, string UserId)
    {
        //StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/welcome.htm"));
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/NewWelcome.html"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$user$$", FullName);
        myString = myString.Replace("$$email$$", Email);
        myString = myString.Replace("$$pass$$", Password);
        myString = myString.Replace("$$ReferralCode$$", UserId);
        reader.Dispose();
        try
        {
            sendMail(Email, "Welcome to FashionKart.uk(India's First Pound Store)! We are delighted to have you.", myString);
            sendMailToAdmin(AdminEmail, "Welcome to FashionKart.uk(India's First Pound Store)! We are delighted to have you.", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }


    //
    //=======================================(1)Code for Sending ContactUs Mail
    //
    public void SendContactUsMail(string Name, string EmailId, string MobileNo, string Message)
    {
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/contact_us.htm"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$user$$", Name);
        myString = myString.Replace("$$email$$", EmailId);
        myString = myString.Replace("$$MobileNo$$", MobileNo);
        myString = myString.Replace("$$Message$$", Message);
        reader.Dispose();
        try
        {
            sendMail(EmailId, "Thank you for contacting us, we will get back to you shortly.", myString);
            sendMailToAdmin(AdminEmail, "Thank you for contacting us, we will get back to you shortly.", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }


    //
    //===========================================(2)Code for Send forget password Mail
    //
    public void sendforgetpass(string Name, string url, string emailid)
    {
        string varifylinkButton = "<a href=" + url + " style='text-align: center; color:#ffffff; text-decoration: none; background: #ed1c24; border-bottom: 3px solid #98080e; padding: 10px 50px; border-radius: 2px;'><span>Reset Your Password</span></a>";
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/forget_password.htm"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$user$$", Name);
        myString = myString.Replace("$$email$$", emailid);
        myString = myString.Replace("$$link$$", url);
        myString = myString.Replace("$$varifylinkButton$$", varifylinkButton);
        reader.Dispose();
        try
        {
            sendMail(emailid, "Fashion kart Password Assistance.", myString);
            sendMailToAdmin(AdminEmail, "Fashion kart Password Assistance.", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    //
    //===========================================(3)Code for send Changed Password mail
    //
    public void sendChangedPassword(string Name, string Email)
    {
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/change_password.htm"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$user$$", Name);
        myString = myString.Replace("$$email$$", Email);
        reader.Dispose();
        try
        {
            sendMail(Email, "Your fashionkart.uk account password has been changed.", myString);
            sendMailToAdmin(AdminEmail, "Your fashionkart.uk account password has been changed.", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    public void GiftMail(string Name, string Email)
    {
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/gift.htm"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$user$$", Name);
        reader.Dispose();
        try
        {
            sendMail(Email, "Gift Coupons-fashionkart.uk.", myString);
            sendMailToAdmin(AdminEmail, "Gift Coupons-fashionkart.uk.", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    //
    //===========================================(3)Code for send review mail
    //
    public void sendReviewMail(string Name, string Email, string ProductName, string ImagePath, string ProductId, string Category)
    {
        string Image = "<img src=http://www.fashionkart.uk/Developer/ProductImages/" + ImagePath + " align=middle height=100px width=100px>";
        string REVIEW = "<a style='width:200px;margin:0px auto;background:#ed1c24;text-align:center;border:1px solid #FF020B;padding:8px 0;text-decoration:none;display:block;color:#fff;font-size:13px' align='center' href=http://www.fashionkart.uk/User/Review.aspx?pid=" + ProductId + " target='_blank'> <span style='color:#ffffff;font-size:13px;background-color:#ed1c24;'>YOUR REVIEW</span> </a>";
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/review.htm"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$user$$", Name);
        myString = myString.Replace("$$email$$", Email);
        myString = myString.Replace("$$ProductName$$", ProductName);
        myString = myString.Replace("$$Images$$", Image);
        myString = myString.Replace("$$YOURREVIEW$$", REVIEW);
        myString = myString.Replace("$$Category$$", Category);
        reader.Dispose();
        try
        {
            sendMail(Email, "Kindly Submit your Review for your purchase at fashionkart.uk", myString);
            sendMailToAdmin(AdminEmail, "Kindly Submit your Review for your purchase at fashionkart.uk", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    //
    //===========================================(4)Code for send Product Purches mail
    //

    public void sendProductPurchesMail(string Name, string PaymentMode, string EmailId, DataSet ds1)
    {
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/product_purches.htm"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        string OrderedProductDetails = "";
        string MobileNo = "";
        string PurchasingInformation = "";
        string ORDERID_COD = "";
        string Address = "";
        decimal ShippingAmount = 0;
        decimal NetOrderAmount = 0;
        decimal TNetOrderAmount = 0;
        decimal TNOA = 0;
        decimal CODCharge = 0;
        bool IsCombinOrder = false;
        String CombinOrderId = "";
        String OrderId = "";
        foreach (DataRow dr1 in ds1.Tables[0].Rows)
        {
            Address = dr1["StreetAddress"].ToString() + "," + dr1["State"].ToString() + "," + dr1["City"].ToString() + "," + dr1["Landmark"].ToString() + "," + dr1["PinCode"].ToString() + "," + dr1["MobileNo"].ToString();
            ShippingAmount += Convert.ToDecimal(dr1["ShippingCharge"].ToString());
            //NetOrderAmount += Convert.ToDecimal(dr1["TotalAmount"]) + Convert.ToDecimal(dr1["ShippingCharge"]);
            CODCharge = Convert.ToDecimal(dr1["CODCharge"].ToString());
            NetOrderAmount += Convert.ToDecimal(dr1["TotalAmount"].ToString());
            TNetOrderAmount += Convert.ToDecimal(dr1["ShippingCharge"].ToString()) + Convert.ToDecimal(dr1["TotalAmount"].ToString());
            IsCombinOrder = Convert.ToBoolean(dr1["IsCombinOrder"].ToString());
            OrderId = dr1["OrderId"].ToString();
            MobileNo = dr1["MobileNo"].ToString();
            CombinOrderId = dr1["CombinOrderId"].ToString();
            OrderedProductDetails += "<div style='overflow: hidden;  margin-top: 10px;'>" +
                "<span style=' display: block; width: 370px; float: left; '>" +
                "<img src=http://www.fashionkart.uk/" + GetImage(dr1["ProductId"].ToString()) + "  style='max-width: 54px; max-height: 63px; float: left; padding-right: 10px;'>" +
                    "<span style='padding-left: 10px; display: block;'>" + dr1["ProductName"].ToString() + "</span>" +
                        "</span>" +
                            "<span style='display: block; width: 57px; float: left; border: 1px solid #bcbcbc; margin: auto; text-align: center; background: #ffffff;'>" + Convert.ToInt32(dr1["Quantity"].ToString()) + "</span>" +
                            "<span style='display: block; width: 152px; float: left; text-align: right;'>Rs. " + dr1["TotalAmount"].ToString() + " </span>" +
                            "</div>";
        }

        TNOA= Convert.ToDecimal(CODCharge) + Convert.ToDecimal(TNetOrderAmount);

        PurchasingInformation += "<table style='width: 100%; border-collapse: collapse; display:block;  line-height:30px; '>" +
                                "<tr>" +
                                    "<td style='width: 155px; vertical-align: text-top;'>Delivery Address</td>" +
                                    "<td style='width: 23px; vertical-align: text-top;'>:</td>" +
                                    "<td style='width: 400px; vertical-align: text-top;'>" + Address + "</td>" +
                                "</tr>" +

                                "<tr>" +
                                    "<td style='width: 155px; vertical-align: text-top; ' >Handling Amount</td>" +
                                    "<td style='width: 23px; vertical-align: text-top;'>:</td>" +
                                    "<td style='width: 400px; vertical-align: text-top;'>Rs. " + ShippingAmount + "</td>" +
                                "</tr>" +

                                "<tr>" +
                                    "<td style='width: 155px; vertical-align: text-top; ' >COD Amount</td>" +
                                    "<td style='width: 23px; vertical-align: text-top;'>:</td>" +
                                    "<td style='width: 400px; vertical-align: text-top;'>Rs. " + CODCharge + "</td>" +
                                "</tr>" +

                                "<tr>" +
                                    "<td style='width: 155px; vertical-align: text-top;'>Net Item Amount </td>" +
                                    "<td style='width: 23px; vertical-align: text-top;'>:</td>" +
                                    "<td style='width: 400px; vertical-align: text-top;'>Rs. " + NetOrderAmount + "</td>" +
                                "</tr>" +
                                 "<tr>" +
                                    "<td style='width: 155px; vertical-align: text-top;'>Total Order Amount </td>" +
                                    "<td style='width: 23px; vertical-align: text-top;'>:</td>" +
                                    "<td style='width: 400px; vertical-align: text-top;'>Rs. " + TNOA + "</td>" +
                                "</tr>" +

                            "</table>";
        if (IsCombinOrder == true)
        {
            ORDERID_COD += "Your Combined order no -" + CombinOrderId;
        }
        else
        {
            ORDERID_COD += "Your order no -" + OrderId;
        }

        

        myString = myString.Replace("$$ORDERID_COD$$", ORDERID_COD);
        myString = myString.Replace("$$OrderedProductDetails$$", OrderedProductDetails);
        myString = myString.Replace("$$PurchasingInformation$$", PurchasingInformation);
        myString = myString.Replace("$$PaymentMode$$", PaymentMode);
        myString = myString.Replace("$$user$$", Name);
        myString = myString.Replace("$$email$$", EmailId);
        reader.Dispose();
        try
        {
            sendMail(EmailId, "Thank you for your Purchase - fashionkart.uk", myString);
            sendMailToAdmin(AdminEmail, "Thank you for your Purchase - fashionkart.uk", myString);

            DateTime now = DateTime.Now;
            string dt = now.ToString("yyyy-MM-dd");
            string OrderDt= now.ToString("dd MMM yyyy");
            string tm = now.AddSeconds(20).ToString("HH:mm:ss");
            OTP.SendMessage3("Hi " + Name + ", your Order " + CombinOrderId + " has been received by Fashionkart.uk on " + OrderDt + ". We will update delivery status soon. Thank you for shopping with us.", MobileNo, dt, tm);
            

        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    public static string GetImage(string ProductId)
    {
        string img = "";
        TransportationPacket Packet = new TransportationPacket();
        CLProductImage clPI = new CLProductImage();
        dlProductImages obj = new dlProductImages();
        clPI.ProductId = ProductId;
        clPI.TOP = 1;
        clPI.Active = true;
        Packet.MessagePacket = clPI;
        Packet = obj.GetProductImage(Packet);
        DataSet ds = new DataSet();
        if (Packet.MessageStatus == EStatus.Success)
        {
            ds = (DataSet)Packet.MessageResultset;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    img = "/Developer/ProductImages/" + dr["ContentUrl"].ToString();
                }
            }
        }
        return img;
    }

    //
    //===========================================(3)Code for send review mail
    //
    public void sendShipmentMail(string Name, string EmailId, DataSet ds1)
    {
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/shipment.htm"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        string OrderedProductDetails = "";
        string PurchasingInformation = "";
        string ORDERID_COD = "";
        string Address = "";
        decimal ShippingAmount = 0;
        decimal NetOrderAmount = 0;
        bool IsCombinOrder = false;
        String CombinOrderId = "";
        String OrderId = "";
        int Count = 0;
        string MobileNo = "";
        string LandMarks = "";
        string City = "";
        string PinCode = "";
        string State = "";
        foreach (DataRow dr1 in ds1.Tables[0].Rows)
        {
            Count = Count + 1;
            Address = dr1["StreetAddress"].ToString();
            MobileNo = dr1["MobileNo"].ToString();
            LandMarks = dr1["Landmark"].ToString();
            City = dr1["City"].ToString();
            State = dr1["State"].ToString();
            PinCode = dr1["PinCode"].ToString();
            ShippingAmount += Convert.ToDecimal(dr1["ShippingCharge"].ToString());
            // NetOrderAmount += Convert.ToDecimal(dr1["TotalAmount"]) + Convert.ToDecimal(dr1["ShippingCharge"]);
            NetOrderAmount += Convert.ToDecimal(dr1["TotalAmount"]);
            IsCombinOrder = Convert.ToBoolean(dr1["IsCombinOrder"].ToString());
            OrderId = dr1["OrderId"].ToString();
            CombinOrderId = dr1["CombinOrderId"].ToString();
            OrderedProductDetails += "<tr>" +
                                        "<td valign='top' align='center' width='350' style='background-color: #fcf7f7'>" +
                                            "<table border='0' cellspacing='0' cellpadding='0' width='100%'>" +
                                                "<tbody>" +
                                                    "<tr>" +
                                                        "<td width='40%' style='padding-left: 20px; text-align: center' valign='middle' align='center'><a style='text-decoration: none' href='/product-info.aspx' target='_blank'>" +
                                                            "<img src=http://www.fashionkart.uk/" + GetImage(dr1["ProductId"].ToString()) + "  style='max-width: 54px; max-height: 63px; float: left; padding-right: 10px;'>" +
                                                        "</a></td>" +
                                                        "<td width='60%' align='center' valign='top' style='padding: 12px 15px 0 10px; margin: 0; vertical-align: top; min-width: 100px'>" +
                                                           " <p style='padding: 0; margin: 0'><a style='text-decoration: none; color: #565656' href='/product-info.aspx' target='_blank'><span style='color: #565656; font-size: 13px'>" + dr1["ProductName"].ToString() + "</span></a> </p>" +//HOB More than 20 Fluid Watch
                                                        "</td>" +
                                                   " </tr>" +
                                               " </tbody>" +
                                            "</table>" +
                                       " </td>" +
                                       " <td valign='top' align='center' width='250' style='background-color: #fcf7f7'>" +
                                       "     <table border='0' cellspacing='0' cellpadding='0' width='100%'>" +
                                        "        <tbody>" +
                                         "           <tr>" +
                                          "              <td width='33%' valign='top' align='center' style='padding: 12px 10px 0 10px; margin: 0; text-align: center'>" +
                                           "                 <p style='white-space: nowrap; padding: 0; margin: 0; color: #848484; font-size: 12px'>Item Price</p>" +
                                            "                <p style='white-space: nowrap; margin: 0; padding: 7px 0 0 0; color: #565656; font-size: 13px'>Rs. " + Convert.ToDecimal(dr1["Price"].ToString()) + " </p>" +
                                             "           </td>" +
                                              "          <td width='33%' valign='top' align='center' style='padding: 12px 10px 0 10px; margin: 0; text-align: center'>" +
                                               "             <p style='padding: 0; margin: 0; color: #848484; font-size: 12px'>Qty</p>" +
                                                "            <p style='padding: 7px 0 0 0; margin: 0; color: #565656; font-size: 13px'>" + Convert.ToInt32(dr1["Quantity"].ToString()) + "</p>" +
                                                 "       </td>" +
                                                  "      <td width='33%' valign='top' align='center' style='padding: 12px 20px 0 10px; margin: 0; text-align: center'>" +
                                                   "         <p style='white-space: nowrap; padding: 0; margin: 0; color: #848484; font-size: 12px'>Subtotal</p>" +
                                                    "        <p style='white-space: nowrap; padding: 7px 0 0 0; margin: 0; color: #565656; font-size: 13px'>Rs. " + dr1["TotalAmount"] + " </p>" +
                                                     "   </td>" +
                                                    "</tr>" +
                                       "         </tbody>" +
                                      "      </table>" +
                                     "   </td>" +
                                    "</tr>";
        }
        PurchasingInformation += " <tr>" +
                                                  "                      <td valign='top' align='left' style='padding: 20px 20px 0 20px; margin: 0'>" +
                                                   "                         <p style='margin: 0; padding: 0; color: #565656; font-size: 13px'>THE SHIPMENT WAS SENT TO:</p>" +
                                                    "                        <p style='padding: 0; margin: 15px 0 10px 0; font-size: 18px; color: #333333'>" +
                                                     "                           " + Name + " &nbsp;&nbsp; " + MobileNo + " " +
                                                      "                      </p>" +
                                                       "                     <p style='line-height: 18px; padding: 0; margin: 0; color: #565656; font-size: 13px'>" +
                                                        "                        " + Address + "<br>" +
                                                         "                       " + LandMarks + "<br>" +
                                                          "                      " + City + " - " + PinCode + "<br>" +
                                                          "                      " + State + "" +
                                                            "                </p>" +
                                                             "           </td>" +
                                                              "      </tr>";
        if (IsCombinOrder == true)
        {
            ORDERID_COD += "HOB has shipped " + Count + " item in your combined order no -" + CombinOrderId;
        }
        else
        {
            ORDERID_COD += "HOB has shipped " + Count + " item in your order no -" + OrderId;
        }
        string TotalShippingAmount = "(+) Delivery: Rs." + ShippingAmount;
        string TotalNetOrderAmount = NetOrderAmount.ToString();
        myString = myString.Replace("$$ORDERID_COD$$", ORDERID_COD);
        myString = myString.Replace("$$OrderedProductDetails$$", OrderedProductDetails);
        myString = myString.Replace("$$PurchasingInformation$$", PurchasingInformation);
        myString = myString.Replace("$$ShippingAmount$$", TotalShippingAmount);
        myString = myString.Replace("$$TotalNetOrderAmount$$", TotalNetOrderAmount);
        myString = myString.Replace("$$user$$", Name);
        myString = myString.Replace("$$email$$", EmailId);
        reader.Dispose();
        try
        {
            sendMail(EmailId, "Your fashionkart.uk order (" + ORDERID_COD + ") has been dispatched!", myString);
            sendMailToAdmin(AdminEmail, "Your fashionkart.uk order (" + ORDERID_COD + ") has been dispatched!", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }


    public void SendReferralMail(string UserName, string EmailId, string ReferralName, string ReferralCode)
    {
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/ReferralProgram.html"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$UserName$$", UserName);
        myString = myString.Replace("$$email$$", EmailId);
        myString = myString.Replace("$$ReferralName$$", ReferralName);
        myString = myString.Replace("$$ReferralCode$$", ReferralCode);
        reader.Dispose();
        try
        {
            sendMail(EmailId, "Invitation for fashionkart.uk(India's First Pound Store) by " + ReferralName + ".", myString);
            sendMailToAdmin(AdminEmail, "Invitation for fashionkart.uk(India's First Pound Store) by " + ReferralName + ".", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }


    public void sendmailwalletMoneyInReferralAccount(string Email, string FullName, string AcceptedReferralUserNames)
    {
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/sendmailwalletMoneyInReferralAccount.html"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$user$$", FullName);
        myString = myString.Replace("$$AcceptedReferralUserNamess$$", AcceptedReferralUserNames);
        myString = myString.Replace("$$email$$", Email);
        reader.Dispose();
        try
        {
            sendMail(Email, "Invitation accepted for fashionkart.uk(India's First Pound Store) by your Friend " + AcceptedReferralUserNames + ".", myString);
            sendMailToAdmin(AdminEmail, "Invitation accepted for fashionkart.uk(India's First Pound Store) by your Friend " + AcceptedReferralUserNames + ".", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }


    public void sendGuessMailerForLess(string Email, string FullName)
    {
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/GuessMailerForLess.html"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$user$$", FullName);
        myString = myString.Replace("$$email$$", Email);
        reader.Dispose();
        try
        {
            sendMail(Email, "Guess Price On FashionKart.", myString);
            sendMailToAdmin(AdminEmail, "Guess Price On FashionKart.", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }


    public void sendGuessMailerForMore(string Email, string FullName)
    {
        StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Emailer/sendGuessMailerForMore.html"));
        string readFile = reader.ReadToEnd();
        string myString = "";
        myString = readFile;
        myString = myString.Replace("$$user$$", FullName);
        myString = myString.Replace("$$email$$", Email);
        reader.Dispose();
        try
        {
            sendMail(Email, "Guess Price On FashionKart.", myString);
            sendMailToAdmin(AdminEmail, "Guess Price On FashionKart.", myString);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }




}





