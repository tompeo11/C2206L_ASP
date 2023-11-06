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

                    if (!context.VaccinesTypes.Any())
                    {
                        context.VaccinesTypes.AddRange(new List<VaccineType>
                        {
                            new VaccineType() {Id = 1, Name = "Inactivated", CreateAt = DateTime.Now},
                            new VaccineType() {Id = 2, Name = "Live-attenuated", CreateAt = DateTime.Now},
                            new VaccineType() {Id = 3, Name = "Messenger RNA (mRNA)", CreateAt = DateTime.Now},
                            new VaccineType() {Id = 4, Name = "Subunit, recombinant, polysaccharide, and conjugate", CreateAt = DateTime.Now},
                        });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
