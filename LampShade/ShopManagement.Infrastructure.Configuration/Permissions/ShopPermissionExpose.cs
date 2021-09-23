using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.Configuration.Permissions
{
    public class ShopPermissionExpose :IPermissionExpose
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>()
                    {
                        new PermissionDto(ShopPermission.ListProduct, "لیست محصولات"),
                        new PermissionDto(ShopPermission.SearchProduct, "سرچ کردن در محصولات"),
                        new PermissionDto(ShopPermission.CreateProduct, "ایجاد محصول جدید"),
                        new PermissionDto(ShopPermission.EditProduct, "ویرایش محصول")
                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>()
                    {
                        new PermissionDto(ShopPermission.ListProductCategories, "لیست دسته بندی محصولات"),
                        new PermissionDto(ShopPermission.SearchProductCategories, "سرچ در دسته بندی محصولات"),
                        new PermissionDto(ShopPermission.CreateProductCategory, "ساخت دسته بندی محصول جدید"),
                        new PermissionDto(ShopPermission.EditProductCategory, "ویرایش دسته بندی محصول "),
                    }
                }
            };
        }
    }
}
