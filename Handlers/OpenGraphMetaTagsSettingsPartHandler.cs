#region Using
using Om.Orchard.SocialMetaTags.Models;
using Orchard.ContentManagement.Handlers;
#endregion

namespace Om.Orchard.SocialMetaTags.Handlers {
    public class OpenGraphMetaTagsSettingsPartHandler : ContentHandler {
        public OpenGraphMetaTagsSettingsPartHandler() {
            Filters.Add(new ActivatingFilter<OpenGraphMetaTagsSettingsPart>(Constants.SiteContentTypeName));

            // initializing default selections
            OnInitializing<OpenGraphMetaTagsSettingsPart>((context, part) => {
                part.OgTitleTagEnabled = true;
                part.OgTitleTagRequired = true;
                part.OgTypeTagEnabled = true;
                part.OgTypeTagRequired = true;
                part.OgUrlTagEnabled = true;
                part.OgUrlTagRequired = true;
                part.OgImageTagEnabled = true;
                part.OgImageTagRequired = true;
            });
        }
    }
}
