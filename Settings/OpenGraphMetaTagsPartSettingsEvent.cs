#region Using
using System.Collections.Generic;
using Om.Orchard.SocialMetaTags.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using Orchard.Localization;
#endregion

namespace Om.Orchard.SocialMetaTags.Settings {
    public class OpenGraphMetaTagsPartSettingsEvent : ContentDefinitionEditorEventsBase {
        private readonly IWorkContextAccessor _workContextAccessor;

        public OpenGraphMetaTagsPartSettingsEvent(IWorkContextAccessor workContextAccessor) {
            _workContextAccessor = workContextAccessor;
            T = NullLocalizer.Instance;
        }

        #region Properties
        public Localizer T { get; set; }
        #endregion

        #region Methods
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != Constants.OpenGraphMetaTagsPartName) {
                yield break;
            }
            var settings = definition.Settings.GetModel<OpenGraphMetaTagsPartSettings>();
            settings.OpenGraphTagsSettings = _workContextAccessor.GetContext()
                .CurrentSite.As<OpenGraphMetaTagsSettingsPart>();
            yield return DefinitionTemplate(settings);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != Constants.OpenGraphMetaTagsPartName) {
                yield break;
            }
            var settings = new OpenGraphMetaTagsPartSettings {
                OpenGraphTagsSettings = _workContextAccessor.GetContext()
                    .CurrentSite.As<OpenGraphMetaTagsSettingsPart>()
            };
            if (updateModel.TryUpdateModel(settings, Constants.OpenGraphSettings.Name, null, null)) {
                builder.WithSetting(Constants.OpenGraphSettings.DefaultTitleName, settings.OgDefaultTitle);
                builder.WithSetting(Constants.OpenGraphSettings.DefaultDescriptionName, settings.OgDefaultDescription);
                builder.WithSetting(Constants.OpenGraphSettings.DefaultImageName, settings.OgDefaultImage);
                builder.WithSetting(Constants.OpenGraphSettings.DefaultTypeName, settings.OgDefaultType);
                builder.WithSetting(Constants.OpenGraphSettings.DefaultUrlName, settings.OgDefaultUrl);
            }
            yield return DefinitionTemplate(settings);
        }
        #endregion
    }
}
