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
    public class GoogleController : Controller {
        private readonly IOrchardServices _services;

        public GoogleController(IOrchardServices services) {
            _services = services;
            T = NullLocalizer.Instance;
        }

        #region Properties
        public Localizer T { get; set; }
        #endregion

        #region Methods
        public ActionResult Index() {
            if (!_services.Authorizer.Authorize(Permissions.ManageSocialMetaTagsSettings, T(Constants.CannotManageText))) {
                return new HttpUnauthorizedResult();
            }

            var googleTagsSettings = _services.WorkContext.CurrentSite.As<AuthorshipMetaTagsSettingsPart>();
            var model = new GoogleIndexViewModel {
                AuthorRelTagEnabled = googleTagsSettings.AuthorRelTagEnabled,
                AuthorRelTagRequired = googleTagsSettings.AuthorRelTagRequired,
                PublisherRelTagEnabled = googleTagsSettings.PublisherRelTagEnabled,
                PublisherRelTagRequired = googleTagsSettings.PublisherRelTagRequired,
                PublisherRelTagAllowOverWrite = googleTagsSettings.PublisherRelTagAllowOverWrite,
                PublisherRelTagPageUrl = googleTagsSettings.PublisherRelTagPageUrl
            };
            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(GoogleIndexViewModel model) {
            if (!_services.Authorizer.Authorize(Permissions.ManageSocialMetaTagsSettings, T(Constants.CannotManageText))) {
                return new HttpUnauthorizedResult();
            }

            if (model.PublisherRelTagRequired &&
                string.IsNullOrWhiteSpace(model.PublisherRelTagPageUrl) &&
                !model.PublisherRelTagAllowOverWrite) {
                ModelState.AddModelError("_FORM",
                    T("Publisher Url is required as user can not overwrite.")
                        .Text);
            }

            if (!string.IsNullOrWhiteSpace(model.PublisherRelTagPageUrl)) {
                if (!model.PublisherRelTagPageUrl.StartsWith("http")) {
                    ModelState.AddModelError("_FORM",
                        T("Url must be in valid format")
                            .Text);
                }
            }

            if (ModelState.IsValid) {
                if (TryUpdateModel(model)) {
                    var googleTagsSettings = _services.WorkContext.CurrentSite.As<AuthorshipMetaTagsSettingsPart>();
                    googleTagsSettings.AuthorRelTagEnabled = model.AuthorRelTagEnabled;
                    googleTagsSettings.AuthorRelTagRequired = model.AuthorRelTagRequired;
                    googleTagsSettings.PublisherRelTagEnabled = model.PublisherRelTagEnabled;
                    googleTagsSettings.PublisherRelTagRequired = model.PublisherRelTagRequired;
                    googleTagsSettings.PublisherRelTagAllowOverWrite = model.PublisherRelTagAllowOverWrite;
                    googleTagsSettings.PublisherRelTagPageUrl = model.PublisherRelTagPageUrl;
                    _services.Notifier.Information(T("Google Search Authorship Meta Tags settings saved successfully."));
                }
                else {
                    _services.Notifier.Information(T("Could not save Google Search Authorship Meta Tags settings"));
                }
            }
            else {
                _services.Notifier.Error(T(Constants.ValidationErrorText));
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
