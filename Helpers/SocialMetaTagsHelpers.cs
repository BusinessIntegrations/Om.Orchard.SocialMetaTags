#region Using
using Orchard.UI.Resources;
#endregion

namespace Om.Orchard.SocialMetaTags.Helpers {
    public class SocialMetaTagsHelpers {
        #region Methods
        public static LinkEntry BuildLinkEntry(string rel, string href) {
            var linkEntry = new LinkEntry {
                Rel = rel,
                Href = href
            };
            return linkEntry;
        }

        public static MetaEntry BuildMetaTag(string metaName, string value) {
            return new MetaEntry {
                Name = metaName,
                Content = value
            };
        }

        public static MetaEntry BuildPropertyMetaTag(string metaKey, string propertyType, string value) {
            var metaEntry = new MetaEntry {
                Name = metaKey
            };
            metaEntry.SetAttribute("property", propertyType);
            metaEntry.SetAttribute("content", value);
            return metaEntry;
        }
        #endregion
    }
}
