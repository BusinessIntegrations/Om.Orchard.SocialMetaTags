#region Using
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
#endregion

namespace Om.Orchard.SocialMetaTags.Models {
    [OrchardFeature("Om.Orchard.SocialMetaTags")]
    public class SummaryCardsMetaTagsPart : ContentPart {
        #region Properties
        public string CardCreator
        {
            get { return this.Retrieve(x => x.CardCreator, string.Empty, true); }
            set { this.Store(x => x.CardCreator, value, true); }
        }

        public string CardDescription
        {
            get { return this.Retrieve(x => x.CardDescription, string.Empty, true); }
            set { this.Store(x => x.CardDescription, value, true); }
        }

        public string CardImage
        {
            get { return this.Retrieve(x => x.CardImage, string.Empty, true); }
            set { this.Store(x => x.CardImage, value, true); }
        }

        public string CardSite
        {
            get { return this.Retrieve(x => x.CardSite, string.Empty, true); }
            set { this.Store(x => x.CardSite, value, true); }
        }

        public string CardTitle
        {
            get { return this.Retrieve(x => x.CardTitle, string.Empty, true); }
            set { this.Store(x => x.CardTitle, value, true); }
        }

        public string CardType
        {
            get { return this.Retrieve(x => x.CardType, string.Empty, true); }
            set { this.Store(x => x.CardType, value, true); }
        }

        public SummaryCardsMetaTagsSettingsPart SummaryCardsTagsSettings { get; set; }
        #endregion
    }
}
