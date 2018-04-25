using System;
using System.Threading.Tasks;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using CMSCore.Shared.Abstractions;
using Orleans;

namespace CMSCore.Content.GrainInterfaces
{
    public interface IContentGrain : IGrainWithGuidKey
    {
        #region Create

        Task<IOperationResult> Create(CreateOperation<Page> model);
        Task<IOperationResult> Create(CreateOperation<BlogPost> model);

        #endregion

        #region Update

        Task<IOperationResult> Update(UpdateOperation<Page> model);
        Task<IOperationResult> Update(UpdateOperation<Blog> model);
        Task<IOperationResult> Update(UpdateOperation<BlogPost> model);
        Task<IOperationResult> Update(UpdateOperation<StaticContent> model);

        #endregion

        #region Delete

        Task<IOperationResult> Delete(DeleteOperation<Page> model);
        Task<IOperationResult> Delete(DeleteOperation<Blog> model);
        Task<IOperationResult> Delete(DeleteOperation<BlogPost> model);
        Task<IOperationResult> Delete(DeleteOperation<StaticContent> model);

        #endregion
    }
}