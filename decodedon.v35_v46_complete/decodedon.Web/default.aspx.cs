using decodedon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace decodedon.Web
{
    public partial class _default : System.Web.UI.Page
    {
        TootService _toots = new TootService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            LocalTimeline.DataSource = _toots.Load(false);
            LocalTimeline.DataBind();
            FederatedTimeline.DataSource = _toots.Load(true);
            FederatedTimeline.DataBind();
        }

        protected void TootButton_Click(object sender, EventArgs e)
        {
            var toot = new Toot
            {
                Name = Page.User.Identity.Name,
                Text = TootText.Text,
                CreateAt = DateTime.Now
            };

            if (string.IsNullOrEmpty(toot.Text))
                return;

            _toots.Add(toot);
            TootText.Text = null;

            // post-redirect-get !
            Response.Redirect(Page.AppRelativeVirtualPath);
        }
    }
}