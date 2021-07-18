using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPictures;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            //if (_productPictureRepository.Exists(x => x.Picture == command.Picture))
            //    return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var path = $"{product.ProductCategory.Slug}//{product.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);

            var productPicture = new ProductPicture(command.ProductId,pictureName,command.PictureAlt,command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var pictureId = _productPictureRepository.GetProductAndCategory(command.Id);

            //if (_productPictureRepository.Exists(x => x.ProductId == command.ProductId 
            //                                          && x.Picture==command.Picture  && x.Id != command.Id))
             //   return operation.Failed(ApplicationMessage.DuplicatedRecord);

            if (pictureId==null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            var path = $"{pictureId.Product.ProductCategory.Slug}//{pictureId.Product.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);


            pictureId.Edit(command.ProductId, pictureName, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductPictureViewModel> SearchModel(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.SearchModel(searchModel);
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            productPicture.Restor();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }
    }
}
