#region Using
using Om.Orchard.SocialMetaTags.Models;
#endregion

namespace Om.Orchard.SocialMetaTags.Settings {
    public class OpenGraphMetaTagsPartSettings {
        #region Properties
        public string OgDefaultDescription { get; set; }
        public string OgDefaultImage { get; set; }
        public string OgDefaultTitle { get; set; }
        public string OgDefaultType { get; set; }
        public string OgDefaultUrl { get; set; }
        public OpenGraphMetaTagsSettingsPart OpenGraphTagsSettings { get; set; }
        #endregion
    }
}
