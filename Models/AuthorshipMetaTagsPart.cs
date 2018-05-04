#region Using
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
#endregion

namespace Om.Orchard.SocialMetaTags.Models {
    [OrchardFeature("Om.Orchard.SocialMetaTags")]
    public class AuthorshipMetaTagsPart : ContentPart {
        #region Properties
        public AuthorshipMetaTagsSettingsPart GoogleTagsSettings { get; set; }

        public string GpAuthorProfileUrl
        {
            get { return this.Retrieve(x => x.GpAuthorProfileUrl, string.Empty, true); }
            set { this.Store(x => x.GpAuthorProfileUrl, value, true); }
        }

        public string GpPublisherProfileUrl
        {
            get { return this.Retrieve(x => x.GpPublisherProfileUrl, string.Empty, true); }
            set { this.Store(x => x.GpPublisherProfileUrl, value, true); }
        }
        #endregion
    }
}
