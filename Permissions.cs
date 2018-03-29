﻿#region Using
using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;
#endregion

namespace Om.Orchard.SocialMetaTags {
    public class Permissions : IPermissionProvider {
        public static readonly Permission ManageSocialMetaTagsSettings = new Permission {
            Description = "Managing Social Meta Tags Settings",
            Name = "ManageSocialMetaTagsSettings"
        };

        #region IPermissionProvider Members
        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {ManageSocialMetaTagsSettings}
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] {ManageSocialMetaTagsSettings}
                },
                new PermissionStereotype {
                    Name = "Moderator"
                },
                new PermissionStereotype {
                    Name = "Author"
                },
                new PermissionStereotype {
                    Name = "Contributor"
                }
            };
        }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {ManageSocialMetaTagsSettings};
        }

        public virtual Feature Feature { get; set; }
        #endregion
    }
}
