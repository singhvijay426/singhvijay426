using ClassLibrary;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebshreeCommonLibrary;


public partial class Admin_UserList : System.Web.UI.Page
{
    static DataSet Exportds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindUser();
        }
    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        gd.PageIndex = e.NewPageIndex;
        bindUser();
    }

    protected void gd_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindUser();
    }

    protected void gd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow grv = gd.Rows[e.RowIndex];
        Label Id = (Label)grv.FindControl("lblId");
        CLUser clUser = new CLUser();
        dlUser obj = new dlUser();
        TransportationPacket packet = new TransportationPacket();
        clUser.Ids = Id.Text;
        packet.MessagePacket = clUser;
        packet = obj.deleteUser(packet);
        if (packet.MessageStatus == EStatus.Success)
        {
            JqueryNotification.NotificationExtensions.ShowSuccessfulNotification(this, "User has been deleted Successfully.", 1500);
        }
        bindUser();
    }


    public void bindUser()
    {
        try
        {
            string visibility = ddlVisibility.SelectedItem.Value;
            CLUser clUser = new CLUser();
            dlUser obj = new dlUser();
            TransportationPacket Packet = new TransportationPacket();
            ///===============================
            if (txtSearch.Text != "")
            {
                clUser.SearchText = txtSearch.Text.Trim();
            }
            else
            {
                clUser.SearchText = "";
            }
            if (visibility == "3")
            {
                clUser.Active = null;
            }
            else if (visibility != "0")
            {
                clUser.Active = false;
            }
            else if (visibility != "1")
            {
                clUser.Active = true;
            }
            //===============================
            Packet.MessagePacket = clUser;
            Packet = obj.getUser(Packet);
            DataSet ds = new DataSet();
            if (Packet.MessageStatus == EStatus.Success)
            {
                ds = (DataSet)Packet.MessageResultset;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gd.DataSource = (DataSet)Packet.MessageResultset;
                    gd.DataBind();
                    Exportds = ds;
                    lblMsg.Text = "";
                }
                else
                {
                    gd.DataSource = null;
                    gd.DataBind();
                }
            }
            else
            {
                gd.DataSource = null;
                gd.DataBind();
                lblMsg.Text = "Data Not Found";
            }
        }
        catch (Exception ex)
        {
            HandleExceptions.ExceptionLogging(ex, "", true);
        }
    }



    /// <summary cref="A">
    /// </summary> Code for search category
    /// <typeparam name="T"></typeparam>

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindUser();
    }

    /// <summary cref="A">
    /// </summary> Code for search category
    /// <typeparam name="T"></typeparam>

    protected void ddlVisibility_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindUser();
    }

    /// <summary>
    /// code for export
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Exportds != null)
            {
                //It represent name of column for which you want to select records
                string[] selectedColumns = new[] { "Name", "Landline No", "Mobile No", "DOB", "Gender"};
                DataTable table = Exportds.Tables[0];
                table.Columns["FirstName"].ColumnName = "Name";
                table.Columns["LandlineNo"].ColumnName = "Landline No";
                table.Columns["MobileNo"].ColumnName = "Mobile No";
                table.Columns["DOB"].ColumnName = "DOB";
                table.Columns["Gender"].ColumnName = "Gender";
                DataTable tableWithOnlySelectedColumns = new DataView(table).ToTable(false, selectedColumns);
                GridViewExportUtil.exportDataTableToExcel(tableWithOnlySelectedColumns, "USER-LIST");
            }
        }
        catch (Exception ex)
        {
            HandleExceptions.ExceptionLogging(ex, "", true);
        }
    }

    /// <summary cref="A">
    /// </summary> Active
    /// <typeparam name="T"></typeparam>

    protected void btn_visible_Click(object sender, EventArgs e)
    {
        try
        {
            //Guid UserId;
            //string UserName;
            //if (HttpContext.Current.User.Identity.Name != "")
            //{
            //    UserName = HttpContext.Current.User.Identity.Name;
            //    MembershipUser user1 = Membership.GetUser(UserName);
            //    UserId = (Guid)user1.ProviderUserKey;
            CLUser clUser = new CLUser();
            dlUser obj = new dlUser();
            TransportationPacket packet = new TransportationPacket();
            Utility All = new Utility();
            clUser.Ids = All.getIds(gd, "lblId");
            clUser.Active = true;
            //clCategory.DeletedBy = UserId;
            packet.MessagePacket = clUser;
            packet = obj.activeinactive(packet);
            if (packet.MessageStatus == EStatus.Success)
            {
                JqueryNotification.NotificationExtensions.ShowSuccessfulNotification(this, "User has been made active Successfully.", 1500);
            }

            //Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
            bindUser();
            //}
        }
        catch (Exception ex)
        {
            HandleExceptions.ExceptionLogging(ex, "", true);
        }
    }

    /// <summary cref="A">
    /// </summary> Inactive
    /// <typeparam name="T"></typeparam>

    protected void btn_hide_Click(object sender, EventArgs e)
    {
        try
        {
            //Guid UserId;
            //string UserName;
            //if (HttpContext.Current.User.Identity.Name != "")
            //{
            //    UserName = HttpContext.Current.User.Identity.Name;
            //    MembershipUser user1 = Membership.GetUser(UserName);
            //    UserId = (Guid)user1.ProviderUserKey;
            CLUser clUser = new CLUser();
            dlUser obj = new dlUser();
            TransportationPacket packet = new TransportationPacket();
            Utility All = new Utility();
            clUser.Ids = All.getIds(gd, "lblId");
            clUser.Active = false;
            //clCategory.DeletedBy = UserId;
            packet.MessagePacket = clUser;
            packet = obj.activeinactive(packet);
            if (packet.MessageStatus == EStatus.Success)
            {
                JqueryNotification.NotificationExtensions.ShowSuccessfulNotification(this, "User has been made Inactive Successfully.", 1500);
            }

            //Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
            bindUser();
            //}
        }
        catch (Exception ex)
        {
            HandleExceptions.ExceptionLogging(ex, "", true);
        }
    }

    /// <summary cref="A">
    /// </summary> Delete
    /// <typeparam name="T"></typeparam>

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {

            CLUser clUser = new CLUser();
            dlUser obj = new dlUser();
            TransportationPacket packet = new TransportationPacket();
            Utility All = new Utility();
            clUser.Ids = All.getIds(gd, "lblId");
            packet.MessagePacket = clUser;
            packet = obj.deleteUser(packet);
            if (packet.MessageStatus == EStatus.Success)
            {
                JqueryNotification.NotificationExtensions.ShowSuccessfulNotification(this, "User has been deleted Successfully.", 1500);
            }

            //Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
            bindUser();
        }
        catch (Exception ex)
        {
            HandleExceptions.ExceptionLogging(ex, "", true);
        }
    }

}
