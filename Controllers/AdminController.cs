#region Using
using System.Web.Mvc;
using Orchard;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Themes;
using Orchard.UI.Admin;
#endregion

namespace Om.Orchard.SocialMetaTags.Controllers {
    [OrchardFeature("Om.Orchard.SocialMetaTags")]
    [Themed]
    [Admin]
    public class AdminController : Controller {
        private readonly IOrchardServices _orchardServices;

        public AdminController(IOrchardServices orchardServices) {
            _orchardServices = orchardServices;
            T = NullLocalizer.Instance;
        }

        #region Properties
        public Localizer T { get; set; }
        #endregion

        #region Methods
        public ActionResult Index() {
            if (!_orchardServices.Authorizer.Authorize(Permissions.ManageSocialMetaTagsSettings, T("Can't manage Social Media Tags Settings"))) {
                return new HttpUnauthorizedResult();
            }
            return View();
        }
        #endregion
    }
}
