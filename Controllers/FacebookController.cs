#region Using
using System.Web.Mvc;
using Om.Orchard.SocialMetaTags.Models;
using Om.Orchard.SocialMetaTags.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Themes;
using Orchard.UI.Admin;
using Orchard.UI.Notify;
#endregion

namespace Om.Orchard.SocialMetaTags.Controllers {
    [OrchardFeature("Om.Orchard.SocialMetaTags")]
    [Themed]
    [Admin]
    public class FacebookController : Controller {
        private readonly IOrchardServices _orchardServices;

        public FacebookController(IOrchardServices orchardServices) {
            _orchardServices = orchardServices;
            T = NullLocalizer.Instance;
        }

        #region Properties
        public Localizer T { get; set; }
        #endregion

        #region Methods
        // GET: /Facebook/
        public ActionResult Index() {
            if (!_orchardServices.Authorizer.Authorize(Permissions.ManageSocialMetaTagsSettings, T(Constants.CannotManageText))) {
                return new HttpUnauthorizedResult();
            }

            var fbIndexViewModel = GetViewModel(_orchardServices.WorkContext.CurrentSite.As<OpenGraphMetaTagsSettingsPart>());
            fbIndexViewModel.CurrentCulture = _orchardServices.WorkContext.CurrentSite.SiteCulture;
            fbIndexViewModel.CurrentSiteName = _orchardServices.WorkContext.CurrentSite.SiteName;
            return View(fbIndexViewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(FacebookIndexViewModel model) {
            if (!_orchardServices.Authorizer.Authorize(Permissions.ManageSocialMetaTagsSettings, T(Constants.CannotManageText))) {
                return new HttpUnauthorizedResult();
            }

            if (model.OgLocaleTagEnabled &&
                model.OgLocaleTagRequired &&
                !model.OgLocaleTagAllowOverwrite &&
                string.IsNullOrWhiteSpace(model.OgLocaleTagValue)) {
                ModelState.AddModelError("_FORM",
                    T("Locale value is required as per your selection.")
                        .Text);
            }

            if (model.OgSiteNameTagEnabled &&
                model.OgSiteNameTagRequired &&
                !model.OgSiteNameTagAllowOverwrite &&
                string.IsNullOrWhiteSpace(model.OgSiteNameTagValue)) {
                ModelState.AddModelError("_FORM",
                    T("Site Name value is required as per your selection.")
                        .Text);
            }

            if (model.FbAdminTagEnabled &&
                model.FbAdminTagRequired &&
                !model.FbAdminTagAllowOverwrite &&
                string.IsNullOrWhiteSpace(model.FbAdminTagValue)) {
                ModelState.AddModelError("_FORM",
                    T("fb admins value is required as per your selection.")
                        .Text);
            }

            if (ModelState.IsValid) {
                if (TryUpdateModel(model)) {
                    SetOgSettingsPart(model);
                    _orchardServices.Notifier.Information(T("Open Graph Meta Tags settings saved successfully."));
                }
                else {
                    _orchardServices.Notifier.Information(T("Could not save Open Graph Meta Tags settings."));
                }
            }
            else {
                _orchardServices.Notifier.Error(T(Constants.ValidationErrorText));
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        private static FacebookIndexViewModel GetViewModel(OpenGraphMetaTagsSettingsPart ogSettingsPart) {
            var fbIndexViewModel = new FacebookIndexViewModel {
                OgTitleTagEnabled = ogSettingsPart.OgTitleTagEnabled,
                OgTitleTagRequired = ogSettingsPart.OgTitleTagRequired,
                OgTypeTagEnabled = ogSettingsPart.OgTypeTagEnabled,
                OgTypeTagRequired = ogSettingsPart.OgTypeTagRequired,
                OgImageTagEnabled = ogSettingsPart.OgImageTagEnabled,
                OgImageTagRequired = ogSettingsPart.OgImageTagRequired,
                OgUrlTagEnabled = ogSettingsPart.OgUrlTagEnabled,
                OgUrlTagRequired = ogSettingsPart.OgUrlTagRequired,
                OgDescriptionTagEnabled = ogSettingsPart.OgDescriptionTagEnabled,
                OgDescriptionTagRequired = ogSettingsPart.OgDescriptionTagRequired,
                OgLocaleTagEnabled = ogSettingsPart.OgLocaleTagEnabled,
                OgLocaleTagRequired = ogSettingsPart.OgLocaleTagRequired,
                OgLocaleTagValue = ogSettingsPart.OgLocaleTagValue,
                OgLocaleTagAllowOverwrite = ogSettingsPart.OgLocaleTagAllowOverwrite,
                OgSiteNameTagEnabled = ogSettingsPart.OgSiteNameTagEnabled,
                OgSiteNameTagRequired = ogSettingsPart.OgSiteNameTagRequired,
                OgSiteNameTagValue = ogSettingsPart.OgSiteNameTagValue,
                OgSiteNameTagAllowOverwrite = ogSettingsPart.OgSiteNameTagAllowOverwrite,
                FbAdminTagEnabled = ogSettingsPart.FbAdminTagEnabled,
                FbAdminTagRequired = ogSettingsPart.FbAdminTagRequired,
                FbAdminTagValue = ogSettingsPart.FbAdminTagValue
            };
            return fbIndexViewModel;
        }

        private void SetOgSettingsPart(FacebookIndexViewModel fbIndexViewModel) {
            var ogSettingsPart = _orchardServices.WorkContext.CurrentSite.As<OpenGraphMetaTagsSettingsPart>();
            ogSettingsPart.OgTitleTagEnabled = fbIndexViewModel.OgTitleTagEnabled;
            ogSettingsPart.OgTitleTagRequired = fbIndexViewModel.OgTitleTagRequired;
            ogSettingsPart.OgTypeTagEnabled = fbIndexViewModel.OgTypeTagEnabled;
            ogSettingsPart.OgTypeTagRequired = fbIndexViewModel.OgTypeTagRequired;
            ogSettingsPart.OgImageTagEnabled = fbIndexViewModel.OgImageTagEnabled;
            ogSettingsPart.OgImageTagRequired = fbIndexViewModel.OgImageTagRequired;
            ogSettingsPart.OgUrlTagEnabled = fbIndexViewModel.OgUrlTagEnabled;
            ogSettingsPart.OgUrlTagRequired = fbIndexViewModel.OgUrlTagRequired;
            ogSettingsPart.OgDescriptionTagEnabled = fbIndexViewModel.OgDescriptionTagEnabled;
            ogSettingsPart.OgDescriptionTagRequired = fbIndexViewModel.OgDescriptionTagRequired;
            ogSettingsPart.OgLocaleTagEnabled = fbIndexViewModel.OgLocaleTagEnabled;
            ogSettingsPart.OgLocaleTagRequired = fbIndexViewModel.OgLocaleTagRequired;
            ogSettingsPart.OgLocaleTagValue = fbIndexViewModel.OgLocaleTagValue;
            ogSettingsPart.OgLocaleTagAllowOverwrite = fbIndexViewModel.OgLocaleTagAllowOverwrite;
            ogSettingsPart.OgSiteNameTagEnabled = fbIndexViewModel.OgSiteNameTagEnabled;
            ogSettingsPart.OgSiteNameTagRequired = fbIndexViewModel.OgSiteNameTagRequired;
            ogSettingsPart.OgSiteNameTagValue = fbIndexViewModel.OgSiteNameTagValue;
            ogSettingsPart.OgSiteNameTagAllowOverwrite = fbIndexViewModel.OgSiteNameTagAllowOverwrite;
            ogSettingsPart.FbAdminTagEnabled = fbIndexViewModel.FbAdminTagEnabled;
            ogSettingsPart.FbAdminTagRequired = fbIndexViewModel.FbAdminTagRequired;
            ogSettingsPart.FbAdminTagValue = fbIndexViewModel.FbAdminTagValue;
        }
        #endregion
    }
}
