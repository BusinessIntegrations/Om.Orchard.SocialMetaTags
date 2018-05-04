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
                "2.0.1",
                menu => menu.Permission(Permissions.ManageSocialMetaTagsSettings)
                    .LinkToFirstChild(true)
                    .Add(T("Settings Overview"), "1", item => Describe(item, Constants.IndexActionName, Constants.AdminControllerName))
                    .Add(T("Google Authorship Meta Tags"), "2", item => Describe(item, Constants.IndexActionName, Constants.GoogleControllerName))
                    .Add(T("Facebook Open Graph Meta Tags"),
                        "3",
                        item => Describe(item, Constants.IndexActionName, Constants.FacebookControllerName))
                    .Add(T("Twitter Summary Cards Meta Tags"),
                        "4",
                        item => Describe(item, Constants.IndexActionName, Constants.TwitterControllerName)),
                new[] {Constants.BiMenuSection});
        }

        public string MenuName => "admin";
        #endregion

        #region Properties
        public Localizer T { get; set; }
        #endregion

        #region Methods
        internal static NavigationItemBuilder Describe(NavigationItemBuilder item, string actionName, string controllerName) {
            var values = new {
                area = Constants.AreaName
            };
            item = item.Action(actionName, controllerName, values)
                .Permission(Permissions.ManageSocialMetaTagsSettings)
                .LocalNav();
            return item;
        }
        #endregion
    }
}
