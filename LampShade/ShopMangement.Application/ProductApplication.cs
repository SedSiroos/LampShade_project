using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var product = new Product(command.Name, command.Code,command.UnitPrice, command.ShortDescription, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle,
                slug, command.Keywords, command.MetaDescription, command.CategoryId);

            _productRepository.Create(product);
            _productRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct entity)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(entity.Id);

            if (product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            if (_productRepository.Exists(x => x.Name == entity.Name && x.Id != entity.Id))
                return operation.Failed(ApplicationMessage.RecordNotFound);

            var slug = entity.Slug.Slugify();
            product.Edit(entity.Name,entity.Code,entity.Picture,entity.ShortDescription,
                entity.ShortDescription,entity.Picture,entity.PictureAlt,entity.PictureTitle,
                slug, entity.Keywords,entity.MetaDescription, entity.Id);

            _productRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult IsInStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);

            if (product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            product.IsStock();
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult NotInStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);

            if (product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            product.NotIsInStock();
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

        public List<ProductViewModel> GetProducs()
        {
            return _productRepository.GetProducts();
        }
    }
}
