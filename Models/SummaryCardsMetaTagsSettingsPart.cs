#region Using
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
#endregion

namespace Om.Orchard.SocialMetaTags.Models {
    [OrchardFeature("Om.Orchard.SocialMetaTags")]
    public class SummaryCardsMetaTagsSettingsPart : ContentPart {
        #region Properties
        public bool CardCreatorTagEnabled { get { return this.Retrieve(x => x.CardCreatorTagEnabled); } set { this.Store(x => x.CardCreatorTagEnabled, value); } }
        public bool CardCreatorTagRequired { get { return this.Retrieve(x => x.CardCreatorTagRequired); } set { this.Store(x => x.CardCreatorTagRequired, value); } }
        public bool CardDescriptionTagEnabled { get { return this.Retrieve(x => x.CardDescriptionTagEnabled); } set { this.Store(x => x.CardDescriptionTagEnabled, value); } }
        public bool CardDescriptionTagRequired { get { return this.Retrieve(x => x.CardDescriptionTagRequired); } set { this.Store(x => x.CardDescriptionTagRequired, value); } }
        public bool CardImageTagEnabled { get { return this.Retrieve(x => x.CardImageTagEnabled); } set { this.Store(x => x.CardImageTagEnabled, value); } }
        public bool CardImageTagRequired { get { return this.Retrieve(x => x.CardImageTagRequired); } set { this.Store(x => x.CardImageTagRequired, value); } }
        public bool CardSiteTagAllowOverwrite { get { return this.Retrieve(x => x.CardSiteTagAllowOverwrite); } set { this.Store(x => x.CardSiteTagAllowOverwrite, value); } }
        public bool CardSiteTagEnabled { get { return this.Retrieve(x => x.CardSiteTagEnabled); } set { this.Store(x => x.CardSiteTagEnabled, value); } }
        public bool CardSiteTagRequired { get { return this.Retrieve(x => x.CardSiteTagRequired); } set { this.Store(x => x.CardSiteTagRequired, value); } }
        public string CardSiteTagValue { get { return this.Retrieve(x => x.CardSiteTagValue); } set { this.Store(x => x.CardSiteTagValue, value); } }
        public bool CardTitleTagEnabled { get { return this.Retrieve(x => x.CardTitleTagEnabled); } set { this.Store(x => x.CardTitleTagEnabled, value); } }
        public bool CardTitleTagRequired { get { return this.Retrieve(x => x.CardTitleTagRequired); } set { this.Store(x => x.CardTitleTagRequired, value); } }
        public bool CardTypeTagEnabled { get { return this.Retrieve(x => x.CardTypeTagEnabled); } set { this.Store(x => x.CardTypeTagEnabled, value); } }
        public bool CardTypeTagRequired { get { return this.Retrieve(x => x.CardTypeTagRequired); } set { this.Store(x => x.CardTypeTagRequired, value); } }
        public bool RenderOutput => CardTypeTagEnabled || CardTitleTagEnabled || CardDescriptionTagEnabled || CardImageTagEnabled || CardCreatorTagEnabled || CardSiteTagEnabled;
        #endregion
    }
}
