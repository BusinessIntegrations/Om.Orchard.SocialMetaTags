﻿#region Using
using Om.Orchard.SocialMetaTags.Models;
using Orchard.ContentManagement.Handlers;
#endregion

namespace Om.Orchard.SocialMetaTags.Handlers {
    public class AuthorshipMetaTagsSettingsPartHandler : ContentHandler {
        public AuthorshipMetaTagsSettingsPartHandler() {
            Filters.Add(new ActivatingFilter<AuthorshipMetaTagsSettingsPart>(Constants.SiteContentTypeName));

            // initializing default selections
            OnInitializing<AuthorshipMetaTagsSettingsPart>((context, part) => {
                part.AuthorRelTagEnabled = true;
                part.AuthorRelTagEnabled = true;
                part.PublisherRelTagEnabled = false;
                part.PublisherRelTagAllowOverWrite = false;
            });
        }
    }
}
