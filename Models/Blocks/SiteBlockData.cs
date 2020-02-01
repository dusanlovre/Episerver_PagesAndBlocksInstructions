using EPiServer.DataAbstraction;
using System.ComponentModel.DataAnnotations;

namespace EpiserverSite1.Models.Blocks
{
    /// <summary>
    /// Base class for all block types on the site
    /// </summary>
    public abstract class SiteBlockData : EPiServer.Core.BlockData
    {
        [Display(
         GroupName = SystemTabNames.PageHeader,
         Order = 20)]
        [UIHint("InstructionsLink")]
        public virtual string InstructionsLink { get; set; }
    }
}
