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
                    context.Database.EnsureCreated();
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
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
