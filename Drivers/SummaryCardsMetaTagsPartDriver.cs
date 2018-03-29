﻿#region Using
using Om.Orchard.SocialMetaTags.Helpers;
using Om.Orchard.SocialMetaTags.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.UI.Resources;
#endregion

namespace Om.Orchard.SocialMetaTags.Drivers {
    public class SummaryCardsMetaTagsPartDriver : ContentPartDriver<SummaryCardsMetaTagsPart> {
        private readonly IWorkContextAccessor _wca;

        public SummaryCardsMetaTagsPartDriver(IWorkContextAccessor wca) {
            _wca = wca;
            T = NullLocalizer.Instance;
        }

        #region Properties
        public Localizer T { get; set; }
        protected override string Prefix => "summarycardmetatags";
        #endregion

        #region Methods
        protected override DriverResult Display(SummaryCardsMetaTagsPart part, string displayType, dynamic shapeHelper) {
            if (displayType != "Detail") {
                return null;
            }
            var resourceManager = _wca.GetContext()
                .Resolve<IResourceManager>();
            var summaryCardsTagsSettings = _wca.GetContext()
                .CurrentSite.As<SummaryCardsMetaTagsSettingsPart>();
            if (summaryCardsTagsSettings.RenderOutput) {
                if (summaryCardsTagsSettings.CardTypeTagEnabled &&
                    part.CardType != "select") {
                    resourceManager.SetMeta(SocialMetaTagsHelpers.BuildMetaTag("twitter:card", part.CardType));
                }
                if (summaryCardsTagsSettings.CardTitleTagEnabled &&
                    !string.IsNullOrWhiteSpace(part.CardTitle)) {
                    resourceManager.SetMeta(SocialMetaTagsHelpers.BuildMetaTag("twitter:title", part.CardTitle));
                }
                if (summaryCardsTagsSettings.CardDescriptionTagEnabled &&
                    !string.IsNullOrWhiteSpace(part.CardDescription)) {
                    resourceManager.SetMeta(SocialMetaTagsHelpers.BuildMetaTag("twitter:description", part.CardDescription));
                }
                if (summaryCardsTagsSettings.CardImageTagEnabled &&
                    !string.IsNullOrWhiteSpace(part.CardImage)) {
                    resourceManager.SetMeta(SocialMetaTagsHelpers.BuildMetaTag("twitter:image", part.CardImage));
                }
                if (summaryCardsTagsSettings.CardSiteTagEnabled &&
                    !string.IsNullOrWhiteSpace(part.CardSite)) {
                    resourceManager.SetMeta(SocialMetaTagsHelpers.BuildMetaTag("twitter:site", part.CardSite));
                }
                if (summaryCardsTagsSettings.CardCreatorTagEnabled &&
                    !string.IsNullOrWhiteSpace(part.CardCreator)) {
                    resourceManager.SetMeta(SocialMetaTagsHelpers.BuildMetaTag("twitter:creator", part.CardCreator));
                }
            }
            return null;
        }

        protected override DriverResult Editor(SummaryCardsMetaTagsPart part, dynamic shapeHelper) {
            part.SummaryCardsTagsSettings = _wca.GetContext()
                .CurrentSite.As<SummaryCardsMetaTagsSettingsPart>();
            return ContentShape("Parts_SummaryCardsMetaTags_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/SummaryCardsMetaTags", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(SummaryCardsMetaTagsPart part, IUpdateModel updater, dynamic shapeHelper) {
            if (updater.TryUpdateModel(part, Prefix, null, null)) {
                //Validation as per selections
                if (part.SummaryCardsTagsSettings.CardTypeTagEnabled &&
                    part.SummaryCardsTagsSettings.CardTypeTagRequired &&
                    part.CardType == "select") {
                    updater.AddModelError("_FORM", T("Twitter Card Type is required"));
                }
                if (part.SummaryCardsTagsSettings.CardTitleTagEnabled &&
                    part.SummaryCardsTagsSettings.CardTitleTagRequired &&
                    string.IsNullOrWhiteSpace(part.CardTitle)) {
                    updater.AddModelError("_FORM", T("Twitter Card Title field is required"));
                }
                if (part.SummaryCardsTagsSettings.CardDescriptionTagEnabled &&
                    part.SummaryCardsTagsSettings.CardDescriptionTagRequired &&
                    string.IsNullOrWhiteSpace(part.CardDescription)) {
                    updater.AddModelError("_FORM", T("Twitter Card Description field is required"));
                }
                if (part.SummaryCardsTagsSettings.CardImageTagEnabled &&
                    part.SummaryCardsTagsSettings.CardImageTagRequired &&
                    string.IsNullOrWhiteSpace(part.CardImage)) {
                    updater.AddModelError("_FORM", T("Twitter Card Image Url field is required"));
                }
                if (part.SummaryCardsTagsSettings.CardCreatorTagEnabled &&
                    part.SummaryCardsTagsSettings.CardCreatorTagRequired &&
                    string.IsNullOrWhiteSpace(part.CardCreator)) {
                    updater.AddModelError("_FORM", T("Twitter Card Creator field is required"));
                }
                if (part.SummaryCardsTagsSettings.CardSiteTagEnabled &&
                    part.SummaryCardsTagsSettings.CardSiteTagRequired &&
                    string.IsNullOrWhiteSpace(part.CardSite)) {
                    updater.AddModelError("_FORM", T("Twitter Card Site field is required"));
                }
            }
            return Editor(part, shapeHelper);
        }
        #endregion
    }
}
