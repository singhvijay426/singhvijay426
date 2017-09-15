<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="ClassLibrary" %>
<%@ Import Namespace="DAL" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        WebshreeCommonLibrary.HandleExceptions.FilePath = AppDomain.CurrentDomain.BaseDirectory + "/Developer/ExceptionDetails.txt";
        //http://www.asp.net/mvc/overview/older-versions-1/controllers-and-routing/creating-custom-routes-cs
        RegisterRoutes(System.Web.Routing.RouteTable.Routes);
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        if (!Request.Url.Host.StartsWith("www") && !Request.Url.IsLoopback)
        {
            UriBuilder builder = new UriBuilder(Request.Url);
            builder.Host = "www." + Request.Url.Host;
            Response.StatusCode = 301;
            Response.AddHeader("Location", builder.ToString());
            Response.End();
        }
    }

    public static void RegisterRoutes(RouteCollection routeCollection)
    {
        routeCollection.MapPageRoute("common1", "Home", "~/index.aspx");
        routeCollection.MapPageRoute("UserRoute1", "Admin/ProductImage/{ProductId}", "~/Admin/ProductImage.aspx");
        routeCollection.MapPageRoute("UserRoute2", "{ProductUniqueName}/{ProductId}/{SKUCode}/{ProductUniqueCode}", "~/product-info.aspx");
        routeCollection.MapPageRoute("UserRoute3", "cart", "~/cart.aspx");
        routeCollection.MapPageRoute("UserRoute4", "wishlist", "~/User/wishlist.aspx");
        routeCollection.MapPageRoute("UserRoute5", "orders", "~/user/order-show.aspx");
        routeCollection.MapPageRoute("UserRoute7", "product", "~/product.aspx");
        routeCollection.MapPageRoute("UserRoute8", "my_account", "~/User/my-account.aspx");
        routeCollection.MapPageRoute("UserRoute9", "order_details", "~/User/order.aspx");
        routeCollection.MapPageRoute("UserRoute10", "change_password", "~/User/change-password.aspx");
        routeCollection.MapPageRoute("UserRoute11", "addresses", "~/User/address.aspx");
        routeCollection.MapPageRoute("UserRoute12", "contact_us", "~/contactus.aspx");
        routeCollection.MapPageRoute("UserRoute13", "cancellation_policy", "~/cancellation.aspx");
        routeCollection.MapPageRoute("UserRoute14", "faq", "~/faq.aspx");
        routeCollection.MapPageRoute("UserRoute15", "return_policy", "~/return_policy.aspx");
        routeCollection.MapPageRoute("UserRoute16", "privacy_policy", "~/privacy-policy.aspx");
        routeCollection.MapPageRoute("UserRoute17", "swiss_design", "~/swiss_design.aspx");
        routeCollection.MapPageRoute("UserRoute18", "fluid", "~/fluid.aspx");
        routeCollection.MapPageRoute("UserRoute19", "fantasy_world", "~/fantasy_world.aspx");
        routeCollection.MapPageRoute("UserRoute20", "mango_people", "~/mango_people.aspx");
        routeCollection.MapPageRoute("UserRoute21", "about_us", "~/about-us.aspx");
        routeCollection.MapPageRoute("UserRoute22", "style_feed", "~/style_feed.aspx");
        routeCollection.MapPageRoute("UserRoute23", "attar", "~/attar_al_carter.aspx");
        routeCollection.MapPageRoute("UserRoute24", "babes", "~/babes.aspx");
        routeCollection.MapPageRoute("UserRoute25", "Pay/{CombinedOrderID}", "~/Pay.aspx");
        routeCollection.MapPageRoute("UserRoute26", "wallet", "~/user/Wallet.aspx");
        routeCollection.MapPageRoute("UserRoute27", "sitemap", "~/sitemap.aspx");     
        routeCollection.MapPageRoute("UserRoute28", "PoundStore", "~/PoundsStore.aspx");
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Session["ProfileComplete"] = "0";
        Session["srch"] = "0";
        Session["securelogin"] = "0";
        Session["OTP"] = "0";
        Session["UserId"] ="0";
        Session["Redirect"] = "0";
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null)
        {
            if (Request.IsAuthenticated == true)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Context.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                string[] roles = new string[1] { ticket.UserData };
                FormsIdentity id = new FormsIdentity(ticket);
                Context.User = new System.Security.Principal.GenericPrincipal(id, roles);
            }
        }

    }
       
</script>
