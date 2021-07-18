using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                operation.Failed(ApplicationMessage.DuplicatedRecord);


            var slug = command.Slug.Slugify();
            var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            var product = new Product(command.Name, command.Code,command.ShortDescription, command.Description,
                picturePath, command.PictureAlt, command.PictureTitle,
                slug, command.Keywords, command.MetaDescription, command.CategoryId);

            _productRepository.Create(product);
            _productRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct entity)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(entity.Id);

            if (product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            if (_productRepository.Exists(x => x.Name == entity.Name && x.Id != entity.Id))
                return operation.Failed(ApplicationMessage.RecordNotFound);

            var slug = entity.Slug.Slugify();
            var path = $"{product.ProductCategory.Slug}/{slug}";
            var picturePath = _fileUploader.Upload(entity.Picture, path);

            product.Edit(entity.Name,entity.Code,entity.ShortDescription,
                entity.Description,picturePath,entity.PictureAlt,entity.PictureTitle,
                slug, entity.Keywords,entity.MetaDescription, entity.Id);

            _productRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> SearchModel(ProductSearchModel searchModel)
        {
          return  _productRepository.Search(searchModel);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}
