using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EpiserverSite1.Models.Blocks;
using EpiserverSite1.Models.Pages;
using EpiserverSite1.Models.Properties.Instructions;

namespace EpiserverSite1.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class InstructionsInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var _contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var _contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();

            var startPage = _contentRepository.Get<StartPage>(SiteDefinition.Current.StartPage);
            var writableClone = startPage.CreateWritableClone() as StartPage;

            if (writableClone != null)
            {
                var instructionsPages = writableClone.PagesInstructionsLinks != null
                        ? writableClone.PagesInstructionsLinks
                        : new List<PagesBlocksInstructionsModel>();

                var instructionsBlocks = writableClone.BlocksInstructionsLinks != null
                        ? writableClone.BlocksInstructionsLinks
                        : new List<PagesBlocksInstructionsModel>();

                var allPages = _contentTypeRepository.List().Where(contentType => typeof(SitePageData).IsAssignableFrom(contentType.ModelType));
                var allBlocks = _contentTypeRepository.List().Where(contentType => typeof(SiteBlockData).IsAssignableFrom(contentType.ModelType));

                writableClone.PagesInstructionsLinks = PopulatePagesAndBlocksProperty(allPages, instructionsPages);
                writableClone.BlocksInstructionsLinks = PopulatePagesAndBlocksProperty(allBlocks, instructionsBlocks);
                _contentRepository.Save(writableClone, SaveAction.Publish, AccessLevel.NoAccess);
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        private IList<PagesBlocksInstructionsModel> PopulatePagesAndBlocksProperty(IEnumerable<ContentType> allPagesOrBlocks, IList<PagesBlocksInstructionsModel> instructionsData)
        {
            foreach (var content in allPagesOrBlocks)
            {
                var item = new PagesBlocksInstructionsModel()
                {
                    PageOrBlock = content.Name,
                    InstructionsLink = string.Empty
                };

                if (!instructionsData.Any(instruction => instruction.PageOrBlock == item.PageOrBlock))
                {
                    instructionsData.Add(item);
                }
            }

            return instructionsData;
        }
    }
}