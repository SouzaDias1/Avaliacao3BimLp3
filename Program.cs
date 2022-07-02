using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Repositories;
using Microsoft.Data.Sqlite;

var databaseConfig = new DatabaseConfig();
var DatabaseSetup= new DatabaseSetup(databaseConfig);

var productRepository = new ProductRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Product")
{
    if(modelAction == "List")
    {
        if(productRepository.GetAll().Count() != 0){
            foreach(var product in productRepository.GetAll())
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            } 
        }
        else
        {
            Console.WriteLine("Nenhum produto cadastrado");
        }
    }

    if(modelAction == "New")
    {
        int id = Convert.ToInt32(args[2]);
        string name = args[3];
        double price = Convert.ToDouble(args[4]);
        bool active = Convert.ToBoolean(args[5]);

       if (productRepository.existsById(id))
        {
            Console.WriteLine($"Produto com id {id} ja existe");
        }
        else
        {
            Console.WriteLine($"Produto {name} cadastrado com sucesso"); 
            var product = new Product(id, name, price, active);
            productRepository.Save(product);  
        }
    }

    if(modelAction == "Delete")
    {
       int id = Convert.ToInt32(args[2]);
       if (productRepository.existsById(id) == true)
       {
           productRepository.Delete(id);
           Console.WriteLine($"Produto {id} removido com sucesso");
       }
       else
       {
            Console.WriteLine($"Produto {id} não encontrado");
       }
    }

    if(modelAction == "Enable")
    {
        
        int id = Convert.ToInt32(args[2]);
        if (productRepository.existsById(id))
        {
           productRepository.Enable(id);
           Console.WriteLine($"Produto id {id} habilitado com sucesso ");
        }
        else
        {
           Console.WriteLine($"Produto {id} não encontrado");
        }
    }
    
    if(modelAction == "Disable")
    {
        
        int id = Convert.ToInt32(args[2]);
        if (productRepository.existsById(id) == true)
        {
           productRepository.Disable(id);
           Console.WriteLine($"Produto id {id} desabilitado com sucesso ");
        }
        else
        {
            Console.WriteLine($"Produto {id} não encontrado");
        }
    }

    if(modelAction == "PriceBetween")
    {
        var initialPrice = Convert.ToDouble(args[2]);
        var endPrice=Convert.ToDouble(args[3]);

        if (productRepository.GetAllWithPriceBetween(initialPrice, endPrice).Any())
        {
            foreach(var product in productRepository.GetAllWithPriceBetween(initialPrice, endPrice))
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            }
        }
        else
        {
            Console.WriteLine($"Nenhum produto encontrado dentro do intervalo de preço R$ {initialPrice } e R$ {endPrice}");
        }
    }
    
    if(modelAction == "PriceHigherThan")
    {
        double price = Convert.ToDouble(args[2]);

        if(productRepository.GetAllWithPriceHigherThan(price).Any())
        {
            foreach(var product in productRepository.GetAllWithPriceHigherThan(price))
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            }
        }
        else
        {
             Console.WriteLine($"Nenhum produto encontrado com o preço maior que R${price}");
        }
    }


    if(modelAction == "PriceLowerThan")
    {
    double price = Convert.ToDouble(args[2]);  
    
        if(productRepository.GetAllWithPriceLowerThan(price).Any())
        {
            foreach(var product in productRepository.GetAllWithPriceLowerThan(price))
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            }
        }
        else
        {
            Console.WriteLine($"Nenhum produto encontrado com o preço menor que R${price}");
        }
    }

    if(modelAction == "AveragePrice")
    {
        Console.WriteLine("AveragePrice");
        
        if(productRepository.GetAll().Any())
        {
            var mediaPrices= productRepository.GetAveragePrice();
            Console.WriteLine($"A média dos preços é {mediaPrices}");
        }
        else
        {
            Console.WriteLine("Nenhum produto cadastrado");
        }
    }
}