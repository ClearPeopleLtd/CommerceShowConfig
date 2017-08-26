using ClearPeople.UCommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ClearPeople.UCommerce.Website.Sitecore.Admin
{
    public partial class ShowuCommerceConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowConfig();
        }
        private void ShowConfig()
        {
            CustomObjectFactory Of = new CustomObjectFactory();


            Response.Clear(); //Optional: if we've sent anything before
            Response.ContentType = "text/xml"; //Must be 'text/xml'
            Response.ContentEncoding = System.Text.Encoding.UTF8; //We'd like UTF-8

            var doc = Of.GetConfig();
            doc.OwnerDocument.Save(Response.Output);
            
            Response.End();


        }
    }
}