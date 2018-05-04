#region Using
using Om.Orchard.SocialMetaTags.Settings;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
#endregion

namespace Om.Orchard.SocialMetaTags.Models {
    [OrchardFeature("Om.Orchard.SocialMetaTags")]
    public class OpenGraphMetaTagsPart : ContentPart {
        #region Properties
        public OpenGraphMetaTagsPartSettings DefaultSettings { get; set; }

        public string FbAdmins
        {
            get { return this.Retrieve(x => x.FbAdmins, string.Empty, true); }
            set { this.Store(x => x.FbAdmins, value, true); }
        }

        public string OgDescription
        {
            get { return this.Retrieve(x => x.OgDescription, string.Empty, true); }
            set { this.Store(x => x.OgDescription, value, true); }
        }

        public string OgImage { get { return this.Retrieve(x => x.OgImage, string.Empty, true); } set { this.Store(x => x.OgImage, value, true); } }

        public string OgLocale
        {
            get { return this.Retrieve(x => x.OgLocale, string.Empty, true); }
            set { this.Store(x => x.OgLocale, value, true); }
        }

        public string OgSiteName
        {
            get { return this.Retrieve(x => x.OgSiteName, string.Empty, true); }
            set { this.Store(x => x.OgSiteName, value, true); }
        }

        public string OgTitle { get { return this.Retrieve(x => x.OgTitle, string.Empty, true); } set { this.Store(x => x.OgTitle, value, true); } }
        public string OgType { get { return this.Retrieve(x => x.OgType, string.Empty, true); } set { this.Store(x => x.OgType, value, true); } }
        public string OgUrl { get { return this.Retrieve(x => x.OgUrl, string.Empty, true); } set { this.Store(x => x.OgUrl, value, true); } }
        public OpenGraphMetaTagsSettingsPart OpenGraphTagsSettings { get; set; }
        #endregion
    }
}
