using Microsoft.EntityFrameworkCore;
using TEST.Models;

namespace TEST.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                
                if (context != null)
                {
                    context.Database.Migrate();

                    if (!context.Categories.Any())
                    {
                        context.Categories.AddRange(
                            new List<Category>
                            {
                                new Category()
                                {
                                    Name = "Bao Ngu 1",
                                    DisplayOrder = 1
                                },

                                new Category()
                                {
                                    Name = "Bao Ngu 2",
                                    DisplayOrder = 2
                                },

                                new Category()
                                {
                                    Name = "Bao Ngu 3",
                                    DisplayOrder = 3
                                }
                            }
                        );

                    if (!context.CoverTypes.Any())
                    {
                        context.CoverTypes.AddRange(new List<CoverType>
                        {
                            new CoverType() {Name = "HardCover"},
                            new CoverType() {Name = "SoftCover"},
                        });
                        context.SaveChanges();
                    }

                    if (!context.Products.Any())
                    {
                        context.Products.AddRange(new List<Product>
                        {
                            new Product() {
                                Title = "product 1",
                                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard" +
                                " dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                                " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
                                " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop" +
                                " publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                                ISBN = "ISBN",
                                Author = "author 1",
                                Price = 12.99,
                                Price50 = 12,
                                Price100 = 11,
                                ImageUrl = "",
                                Category = context.Categories.Where(i => i.Name == "Bao Ngu 1").FirstOrDefault(),
                                CoverType = context.CoverTypes.Where(i => i.Name == "HardCover").FirstOrDefault(),
                            }
                        });
                        context.SaveChanges();
                    }
                    }
                }
            }
        }
    }
}
