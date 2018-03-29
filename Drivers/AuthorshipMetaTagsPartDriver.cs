#region Using
using Om.Orchard.SocialMetaTags.Helpers;
using Om.Orchard.SocialMetaTags.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.UI.Resources;
#endregion

namespace Om.Orchard.SocialMetaTags.Drivers {
    public class AuthorshipMetaTagsPartDriver : ContentPartDriver<AuthorshipMetaTagsPart> {
        private readonly IWorkContextAccessor _wca;

        public AuthorshipMetaTagsPartDriver(IWorkContextAccessor workContextAccessor) {
            _wca = workContextAccessor;
            T = NullLocalizer.Instance;
        }

        #region Properties
        public Localizer T { get; set; }
        protected override string Prefix => "socialmetatags";
        #endregion

        #region Methods
        protected override DriverResult Display(AuthorshipMetaTagsPart part, string displayType, dynamic shapeHelper) {
            if (displayType != "Detail") {
                return null;
            }
            var resourceManager = _wca.GetContext()
                .Resolve<IResourceManager>();
            var googleTagsSettings = _wca.GetContext()
                .CurrentSite.As<AuthorshipMetaTagsSettingsPart>();
            if (googleTagsSettings.RenderOutput) {
                if (googleTagsSettings.AuthorRelTagEnabled &&
                    !string.IsNullOrWhiteSpace(part.GpAuthorProfileUrl)) {
                    resourceManager.RegisterLink(SocialMetaTagsHelpers.BuildLinkEntry("author", part.GpAuthorProfileUrl));
                }
                if (googleTagsSettings.PublisherRelTagEnabled &&
                    !string.IsNullOrWhiteSpace(part.GpPublisherProfileUrl)) {
                    resourceManager.RegisterLink(SocialMetaTagsHelpers.BuildLinkEntry("publisher", part.GpPublisherProfileUrl));
                }
            }
            return null;
        }

        //Get
        protected override DriverResult Editor(AuthorshipMetaTagsPart part, dynamic shapeHelper) {
            part.GoogleTagsSettings = _wca.GetContext()
                .CurrentSite.As<AuthorshipMetaTagsSettingsPart>();
            return ContentShape("Parts_AuthorshipMetaTags_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/AuthorshipMetaTags", Model: part, Prefix: Prefix));
        }

        //Post
        protected override DriverResult Editor(AuthorshipMetaTagsPart part, IUpdateModel updater, dynamic shapeHelper) {
            if (updater.TryUpdateModel(part, Prefix, null, null)) {
                part.GoogleTagsSettings = _wca.GetContext()
                    .CurrentSite.As<AuthorshipMetaTagsSettingsPart>();
                if (part.GoogleTagsSettings.AuthorRelTagEnabled &&
                    part.GoogleTagsSettings.AuthorRelTagRequired &&
                    string.IsNullOrWhiteSpace(part.GpAuthorProfileUrl)) {
                    updater.AddModelError("_FORM", T("Google+ Profile Url is required"));
                }
                if (part.GoogleTagsSettings.PublisherRelTagEnabled &&
                    part.GoogleTagsSettings.PublisherRelTagRequired &&
                    string.IsNullOrWhiteSpace(part.GpPublisherProfileUrl)) {
                    updater.AddModelError("_FORM", T("Google+ Business page Url is required"));
                }
            }
            return Editor(part, shapeHelper);
        }
        #endregion
    }
}
