using EpiserverSite1.Models.Properties.Instructions;

namespace EpiserverSite1.Business.Services
{
    public interface IPagesBlocksInstructionsService
    {
        PagesBlocksInstructionsReturnModel GetInstructions(string pageOrBlock);
    }
}
