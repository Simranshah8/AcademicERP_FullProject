using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Setup
{
    public class Themes : ResponeValues
    {
        public bool NoNavbarBorder { get; set; }
        public bool BodySmallText { get; set; }
        public bool NavbarSmallText { get; set; }
        public bool SidebarNavSmallText { get; set; }
        public bool FooterSmallText { get; set; }
        public bool SidebarNavFlatStyle { get; set; }
        public bool SidebarNavLegacyStyle { get; set; }
        public bool SidebarNavCompact { get; set; }
        public bool SidebarNavChildIndent { get; set; }
        public bool SidebarNavChildHideOnCollapse { get; set; }
        public bool MainSidebarDisable { get; set; }
        public bool BrandSmallText { get; set; }
        public string NavbarVariants { get; set; }
        public string AccentColorVariants { get; set; }
        public string DarkSidebarVariants { get; set; }
        public string LightSidebarVariants { get; set; }
        public string BrandLogoVariants { get; set; }
    }
}
