using System;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EpiserverSite1.Business.Services;

namespace EpiserverSite1.Business.EditorDescriptors
{
    public class PagesBlocksInstructionsEditorDescriptor
    {
        [EditorDescriptorRegistration(
            TargetType = typeof(string),
            UIHint = "InstructionsLink")]
        public class PagesBlocksInstructions : EditorDescriptor
        {
            private readonly IPagesBlocksInstructionsService _instructionsService;

            public PagesBlocksInstructions(IPagesBlocksInstructionsService instructionsService)
            {
                ClientEditingClass = "alloy/Editors/PagesBlocksInstructions";
                _instructionsService = instructionsService;
            }

            public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
            {
                base.ModifyMetadata(metadata, attributes);

                dynamic metadataRuntime = metadata;
                var ownerContent = metadataRuntime.OwnerContent as IContent;

                var pageOrBlock = ownerContent != null ? ownerContent.GetType().BaseType : null;

                if (pageOrBlock != null)
                {
                    var linkUrl = _instructionsService.GetInstructions(pageOrBlock.Name).Link;
                    var linkCaption = _instructionsService.GetInstructions(pageOrBlock.Name).Caption;
                    var instructionsExist = string.IsNullOrEmpty(linkUrl) || string.IsNullOrEmpty(linkCaption) ? false : true;

                    metadata.EditorConfiguration.Add("linkUrl", linkUrl);
                    metadata.EditorConfiguration.Add("linkCaption", linkCaption);
                    metadata.EditorConfiguration.Add("instructionsExist", instructionsExist);
                    metadata.DisplayName = string.Empty;
                }
            }
        }
    }
}