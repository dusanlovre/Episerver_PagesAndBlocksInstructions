using EPiServer.Core;
using EPiServer.PlugIn;
using System.ComponentModel.DataAnnotations;

namespace EpiserverSite1.Models.Properties.Instructions
{
    [PropertyDefinitionTypePlugIn]
    public class PagesBlocksInstructionsPropertyDefinition : PropertyList<PagesBlocksInstructionsModel> { }

    public class PagesBlocksInstructionsModel
    {
        [Display(Name = "Page or block")]
        public string PageOrBlock { get; set; }

        [Display(Name = "Link to instructions")]
        public string InstructionsLink { get; set; }
    }
}