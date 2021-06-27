using System;
using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategorys;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication:IProductCategoryApplication
    {
        
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_categoryRepository.Exists(x=>x.Name==command.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

                var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt,
                command.PictureTitle, command.KeyWords, command.MetaDescription,slug);

            _categoryRepository.Create(productCategory);
            _categoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _categoryRepository.Get(command.Id);
            if (productCategory == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_categoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);


            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name,command.Description,command.Picture,command.PictureAlt,command.PictureTitle,
                command.KeyWords,command.MetaDescription,slug);
            _categoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _categoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _categoryRepository.SearchModel(searchModel);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _categoryRepository.GetProductsCategory();
        }
    }
}
