using System.Collections.Generic;
using EPiServer;
using EPiServer.Web;
using EpiserverSite1.Models.Pages;
using EpiserverSite1.Models.Properties.Instructions;

namespace EpiserverSite1.Business.Services
{
    public class PagesBlocksInstructionsService : IPagesBlocksInstructionsService
    {
        private readonly IContentLoader _contentLoader;

        public PagesBlocksInstructionsService(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public PagesBlocksInstructionsReturnModel GetInstructions(string pageOrBlock)
        {
            var startPage = _contentLoader.Get<StartPage>(SiteDefinition.Current.StartPage);

            var instructionsCaption = startPage.InstructionsLinkCaption;

            var pagesInstructions = startPage.PagesInstructionsLinks;
            var blocksInstructions = startPage.BlocksInstructionsLinks;

            return new PagesBlocksInstructionsReturnModel()
            {
                Caption = !string.IsNullOrEmpty(instructionsCaption) ? instructionsCaption : string.Empty,
                Link = FindLink(pageOrBlock, pagesInstructions, blocksInstructions)
            };
        }

        private string FindLink(string blockOrPageName, IList<PagesBlocksInstructionsModel> pageInstructions, IList<PagesBlocksInstructionsModel> blockInstructions)
        {
            string instructionsLink = string.Empty;

            foreach (var page in pageInstructions)
            {
                if (blockOrPageName == page.PageOrBlock)
                {
                    instructionsLink = page.InstructionsLink;
                    break;
                }
                else
                {
                    foreach (var block in blockInstructions)
                    {
                        if (blockOrPageName == block.PageOrBlock)
                        {
                            instructionsLink = block.InstructionsLink;
                            break;
                        }
                    }
                }
            }

            return instructionsLink;
        }
    }
}