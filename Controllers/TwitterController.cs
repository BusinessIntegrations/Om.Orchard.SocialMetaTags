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
    public class TwitterController : Controller {
        private readonly IOrchardServices _services;

        public TwitterController(IOrchardServices services) {
            _services = services;
            T = NullLocalizer.Instance;
        }

        #region Properties
        public Localizer T { get; set; }
        #endregion

        #region Methods
        // GET: /Twitter/
        public ActionResult Index() {
            if (!_services.Authorizer.Authorize(Permissions.ManageSocialMetaTagsSettings, T(Constants.CannotManageText))) {
                return new HttpUnauthorizedResult();
            }

            var twitterIndexViewModel = GetViewModel(_services.WorkContext.CurrentSite.As<SummaryCardsMetaTagsSettingsPart>());
            return View(twitterIndexViewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(TwitterIndexViewModel model) {
            if (!_services.Authorizer.Authorize(Permissions.ManageSocialMetaTagsSettings, T(Constants.CannotManageText))) {
                return new HttpUnauthorizedResult();
            }

            if (model.CardSiteTagEnabled &&
                model.CardSiteTagRequired &&
                !model.CardSiteTagAllowOverwrite &&
                string.IsNullOrWhiteSpace(model.CardSiteTagValue)) {
                ModelState.AddModelError("_FORM",
                    T("Site Twitter Handle is required as per your selection.")
                        .Text);
            }

            if (ModelState.IsValid) {
                if (TryUpdateModel(model)) {
                    SetTwitterCardSettings(model);
                    _services.Notifier.Information(T("Twitter Summary Card Meta Tags settings saved successfully."));
                }
                else {
                    _services.Notifier.Information(T("Could not save Twitter Summary Card Meta Tags settings."));
                }
            }
            else {
                _services.Notifier.Error(T(Constants.ValidationErrorText));
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        private static TwitterIndexViewModel GetViewModel(SummaryCardsMetaTagsSettingsPart scSettingsPart) {
            var twitterIndexViewModel = new TwitterIndexViewModel {
                CardTypeTagEnabled = scSettingsPart.CardTypeTagEnabled,
                CardTypeTagRequired = scSettingsPart.CardTypeTagRequired,
                CardTitleTagEnabled = scSettingsPart.CardTitleTagEnabled,
                CardTitleTagRequired = scSettingsPart.CardTitleTagRequired,
                CardDescriptionTagEnabled = scSettingsPart.CardDescriptionTagEnabled,
                CardDescriptionTagRequired = scSettingsPart.CardDescriptionTagRequired,
                CardImageTagEnabled = scSettingsPart.CardImageTagEnabled,
                CardImageTagRequired = scSettingsPart.CardImageTagRequired,
                CardCreatorTagEnabled = scSettingsPart.CardCreatorTagEnabled,
                CardCreatorTagRequired = scSettingsPart.CardCreatorTagRequired,
                CardSiteTagEnabled = scSettingsPart.CardSiteTagEnabled,
                CardSiteTagRequired = scSettingsPart.CardSiteTagRequired,
                CardSiteTagAllowOverwrite = scSettingsPart.CardSiteTagAllowOverwrite,
                CardSiteTagValue = scSettingsPart.CardSiteTagValue
            };
            return twitterIndexViewModel;
        }

        private void SetTwitterCardSettings(TwitterIndexViewModel twitterIndexViewModel) {
            var scMetaTagsSettingsPart = _services.WorkContext.CurrentSite.As<SummaryCardsMetaTagsSettingsPart>();
            scMetaTagsSettingsPart.CardTypeTagEnabled = twitterIndexViewModel.CardTypeTagEnabled;
            scMetaTagsSettingsPart.CardTypeTagRequired = twitterIndexViewModel.CardTypeTagRequired;
            scMetaTagsSettingsPart.CardTitleTagEnabled = twitterIndexViewModel.CardTitleTagEnabled;
            scMetaTagsSettingsPart.CardTitleTagRequired = twitterIndexViewModel.CardTitleTagRequired;
            scMetaTagsSettingsPart.CardDescriptionTagEnabled = twitterIndexViewModel.CardDescriptionTagEnabled;
            scMetaTagsSettingsPart.CardDescriptionTagRequired = twitterIndexViewModel.CardDescriptionTagRequired;
            scMetaTagsSettingsPart.CardImageTagEnabled = twitterIndexViewModel.CardImageTagEnabled;
            scMetaTagsSettingsPart.CardImageTagRequired = twitterIndexViewModel.CardImageTagRequired;
            scMetaTagsSettingsPart.CardCreatorTagEnabled = twitterIndexViewModel.CardCreatorTagEnabled;
            scMetaTagsSettingsPart.CardCreatorTagRequired = twitterIndexViewModel.CardCreatorTagRequired;
            scMetaTagsSettingsPart.CardSiteTagEnabled = twitterIndexViewModel.CardSiteTagEnabled;
            scMetaTagsSettingsPart.CardSiteTagRequired = twitterIndexViewModel.CardSiteTagRequired;
            scMetaTagsSettingsPart.CardSiteTagAllowOverwrite = twitterIndexViewModel.CardSiteTagAllowOverwrite;
            scMetaTagsSettingsPart.CardSiteTagValue = twitterIndexViewModel.CardSiteTagValue;
        }
        #endregion
    }
}
