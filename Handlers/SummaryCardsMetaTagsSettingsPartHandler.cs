﻿#region Using
using Om.Orchard.SocialMetaTags.Models;
using Orchard.ContentManagement.Handlers;
#endregion

namespace Om.Orchard.SocialMetaTags.Handlers {
    public class SummaryCardsMetaTagsSettingsPartHandler : ContentHandler {
        public SummaryCardsMetaTagsSettingsPartHandler() {
            Filters.Add(new ActivatingFilter<SummaryCardsMetaTagsSettingsPart>(Constants.SiteContentTypeName));

            // initializing selections
            OnInitializing<SummaryCardsMetaTagsSettingsPart>((context, part) => {
                part.CardTitleTagEnabled = true;
                part.CardTitleTagRequired = true;
                part.CardTypeTagEnabled = true;
                part.CardTypeTagRequired = true;
                part.CardDescriptionTagEnabled = true;
                part.CardDescriptionTagRequired = true;
                part.CardCreatorTagEnabled = true;
                part.CardCreatorTagRequired = true;
            });
        }
    }
}
