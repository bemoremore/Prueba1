using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Prueba1_Real.src.Models;
namespace Prueba1_Real.src.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDBContext>();
                context.Database.EnsureCreated();
                var existingRuts = new HashSet<string>();

                if (!context.Users.Any())
                {
                    var userFaker = new Faker<User>()
                        .RuleFor(u => u.Rut, f => GenerateUniqueRandomRut(existingRuts))
                        .RuleFor(u => u.Nombre, f => f.Name.FullName())
                        .RuleFor(u => u.correo, f => f.Internet.Email())
                        .RuleFor(u => u.genero, f => f.PickRandom(new[] { "masculino", "femenino", "otro", "prefiero no decirlo" }))
                        .RuleFor(u => u.fechaNacimiento, f => f.Date.Past().ToString("dd-MM-yyyy"));
                    
                    var users = userFaker.Generate(10);
                    context.Users.AddRange(users);
                    context.SaveChanges();
                }

                
            }
        }

        private static string GenerateUniqueRandomRut(HashSet<string> existingRuts)
        {
            string rut;
            do
            {
                rut = GenerateRandomRut();
            } while (existingRuts.Contains(rut));
            existingRuts.Add(rut);
            return rut;
        }


        private static string GenerateRandomRut() 
        {
            Random random = new();
            int rutNumber = random.Next(1, 99999999);
            int verificador = CalcularVerificador(rutNumber);
            string verificadorStr= verificador.ToString();
            if (verificador == 10) {
                verificadorStr = "K";
            }
            return $"{rutNumber}-{verificadorStr}";
            
        }

        private static int CalcularVerificador(int rutNumber) 
        {
            int[] coefficients = [2, 3, 4, 5, 6, 7];
            int sum = 0;
            int index = 0;

            while (rutNumber != 0) {
                sum += rutNumber % 10 * coefficients[index];
                rutNumber = rutNumber / 10;
                index = (index + 1) % 6;
            }
            int verificador = 11 - (sum % 11);
            return verificador == 11 ? 0 : verificador;
        }
        public static void SeedData(AppDBContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;
            }
            context.Users.AddRange(
                new Models.User
                {
                    Rut = "12345678-9",
                    Nombre = "Juan Perez",
                    correo = "JuanPerez@gmail.com",
                    genero = "masculino",
                    fechaNacimiento = "01-01-2000"
                },
                new Models.User
                {
                    Rut = "98765432-1",
                    Nombre = "Maria Lopez",
                    correo = "MariaLopez@gmail.com",
                    genero = "femenino",
                    fechaNacimiento = "01-01-2000"
                },
                new Models.User
                {
                    Rut = "12345678-9",
                    Nombre = "Pedro Perez",
                    correo = "PedroPerez@gmail.com",
                    genero = "masculino",
                    fechaNacimiento = "01-01-2000"
                },
                new Models.User
                {
                    Rut = "98765432-1",
                    Nombre = "Ana Lopez",
                    correo = "AnaLopez@hotmail.com",
                    genero = "femenino",
                    fechaNacimiento = "01-01-1998"
                },
                new Models.User
                {
                    Rut = "12345678-9",
                    Nombre = "Jose Perez",
                    correo = "JPerez@gmail.com",
                    genero = "otro",
                    fechaNacimiento = "01-01-2004"
                },
                new Models.User
                {
                    Rut = "98765432-1",
                    Nombre = "Luis Lopez",
                    correo = "modoslololo@gmail.com",
                    genero = "prefiero no decirlo",
                    fechaNacimiento = "01-08-2001"
                },
                new Models.User
                {
                    Rut = "12345678-9",
                    Nombre = "Benjamin Llanos",
                    correo = "benjaLlano@hotmail.com",
                    genero = "masculino",
                    fechaNacimiento = "01-01-2004"
                },
                new Models.User
                {
                    Rut = "98765432-1",
                    Nombre = "Catalina Lopez",
                    correo = "Cata@gmail.com",
                    genero = "femenino",
                    fechaNacimiento = "01-01-2008"
                },
                new Models.User
                {
                    Rut = "12345678-9",
                    Nombre = "Javiera Contador",
                    correo = "javiContador@gmail.com",
                    genero = "femenino",
                    fechaNacimiento = "01-01-1980"
                },
                new Models.User
                {
                    Rut = "98765432-1",
                    Nombre = "Jose Peña",
                    correo = "jpeña@gmail.com",
                    genero = "masculino",
                    fechaNacimiento = "01-01-2000"
                }
            );
    }
    }
}