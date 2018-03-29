#region Using
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.UI.Navigation;
#endregion

namespace Om.Orchard.SocialMetaTags {
    [OrchardFeature("Om.Orchard.SocialMetaTags")]
    public class AdminMenu : INavigationProvider {
        public AdminMenu() {
            T = NullLocalizer.Instance;
        }

        #region INavigationProvider Members
        public void GetNavigation(NavigationBuilder builder) {
            builder.Add(T("Social Meta Tags"),
                    "10",
                    menu => menu.Action("Index",
                            "Admin",
                            new {
                                area = "Om.Orchard.SocialMetaTags"
                            })
                        .Add(T("Settings Overview"), "1", item => Describe(item, "Index", "Admin", true))
                        .Add(T("Google Authorship Meta Tags"), "2", item => Describe(item, "Index", "Google", true))
                        .Add(T("Facebook Open Graph Meta Tags"), "3", item => Describe(item, "Index", "Facebook", true))
                        .Add(T("Twitter Summary Cards Meta Tags"), "4", item => Describe(item, "Index", "Twitter", true)));
        }

        public string MenuName { get { return "admin"; } }
        #endregion

        #region Properties
        public Localizer T { get; set; }
        #endregion

        #region Methods
        internal static NavigationItemBuilder Describe(NavigationItemBuilder item, string actionName, string controllerName, bool localNav) {
            item = item.Action(actionName,
                    controllerName,
                    new {
                        area = "Om.Orchard.SocialMetaTags"
                    })
                .Permission(Permissions.ManageSocialMetaTagsSettings)
                .LocalNav(localNav);            
            return item;
        }
        #endregion
    }
}
