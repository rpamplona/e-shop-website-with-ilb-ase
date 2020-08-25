/*   
 *   * Copyright (c) Microsoft Corporation. All rights reserved. Licensed under the MIT license.  
 *   * See LICENSE in the project root for license information.  
 */

using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // Only run this if using a real database
                // context.Database.Migrate();

                if (!catalogContext.CatalogBrands.Any())
                {
                    catalogContext.CatalogBrands.AddRange(
                        GetPreconfiguredCatalogBrands());

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogTypes.Any())
                {
                    catalogContext.CatalogTypes.AddRange(
                        GetPreconfiguredCatalogTypes());

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogItems.Any())
                {
                    catalogContext.CatalogItems.AddRange(
                        GetPreconfiguredItems());

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CatalogContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(catalogContext, loggerFactory, retryForAvailability);
                }
            }
        }

        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand() { Brand = "Azure"},
                new CatalogBrand() { Brand = "Visual Studio" },
                new CatalogBrand() { Brand = "AWS" },
                new CatalogBrand() { Brand = "Other" }
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType() { Type = "Mug"},
                new CatalogType() { Type = "Wearables" },
                new CatalogType() { Type = "Office Supplies" },
                new CatalogType() { Type = "Bag" },
                new CatalogType() { Type = "USB Memory Stick" }
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem() { CatalogTypeId=3,CatalogBrandId=3, Description = "Pen", Name = "Pen", Price = 19.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png" },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = "Bring the Energy", Name = "Bring the Energy", Price= 8.50M, PictureUri = "http://catalogbaseurltobereplaced/images/products/2.png" },
                new CatalogItem() { CatalogTypeId=1,CatalogBrandId=1, Description = "Purple Tumber", Name = "Purple Tumbler", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/3.png" },
                new CatalogItem() { CatalogTypeId=3,CatalogBrandId=3, Description = "Planner", Name = "Planner", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/4.png" },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = "Little Disruptor", Name = "Little Disruptor", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/5.png" },
                new CatalogItem() { CatalogTypeId=5,CatalogBrandId=4, Description = "USB FlashDisk", Name = "USB FlashDisk", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/6.png" },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = "Socks", Name = "Socks", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/7.png"  },
                new CatalogItem() { CatalogTypeId=1,CatalogBrandId=1, Description = "White Tumbler", Name = "White Tumbler", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/8.png" },
                new CatalogItem() { CatalogTypeId=4,CatalogBrandId=4, Description = "Tote bag", Name = "Tote bag", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/9.png" },
                new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = "Accenture T-shirt", Name = "Accenture T-shirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/10.png" },
                new CatalogItem() { CatalogTypeId=1,CatalogBrandId=1, Description = "Glass Tumbler", Name = "Glass Tumbler", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/11.png" },
                new CatalogItem() { CatalogTypeId=3,CatalogBrandId=3, Description = "UV Sanitizer", Name = "UV Sanitizer", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/12.png" }
            };
        }
    }
}
