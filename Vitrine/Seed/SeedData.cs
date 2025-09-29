using Vitrine.Data;
using Vitrine.Models;
using System.Text.Json;

namespace Vitrine.Seed
{
    public static class SeedData
    {
        public static void Initialize(VitrineDbContext context)
        {
            // Se já existir algum produto, não faz nada
            if (context.Products.Any())
            {
                return;
            }

            var products = new List<Product>
            {
                new Product { Name = "Smartphone Galaxy S23", Price = 4999, Category = "Celulares", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/galaxy_s23.jpg" }), Description = "Smartphone Samsung topo de linha" },
                new Product { Name = "iPhone 15", Price = 6999, Category = "Celulares", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/iphone_15.jpg" }), Description = "Novo iPhone 15 da Apple" },
                new Product { Name = "Notebook Dell XPS 15", Price = 10999, Category = "Notebooks", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/dell_xps15.jpg" }), Description = "Notebook Dell premium para produtividade" },
                new Product { Name = "Notebook MacBook Pro 16\"", Price = 18999, Category = "Notebooks", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/macbook_pro16.jpg" }), Description = "MacBook Pro com M2 Max" },
                new Product { Name = "Monitor LG 27\" 4K", Price = 2399, Category = "Monitores", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/lg_27_4k.jpg" }) },
                new Product { Name = "Monitor Samsung Odyssey 32\"", Price = 3299, Category = "Monitores", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/odyssey_32.jpg" }) },
                new Product { Name = "Headset HyperX Cloud II", Price = 499, Category = "Periféricos", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/hyperx_cloudII.jpg" }) },
                new Product { Name = "Teclado Mecânico Logitech G915", Price = 1299, Category = "Periféricos", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/logitech_g915.jpg" }) },
                new Product { Name = "Mouse Razer DeathAdder V3", Price = 399, Category = "Periféricos", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/razer_deathadder.jpg" }) },
                new Product { Name = "SSD Samsung 1TB NVMe", Price = 749, Category = "Armazenamento", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/samsung_1tb.jpg" }) },
                new Product { Name = "SSD WD Black 2TB NVMe", Price = 1499, Category = "Armazenamento", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/wd_black_2tb.jpg" }) },
                new Product { Name = "Placa de Vídeo RTX 4090", Price = 15999, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/rtx4090.jpg" }) },
                new Product { Name = "Placa de Vídeo RX 7900 XT", Price = 12499, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/rx7900xt.jpg" }) },
                new Product { Name = "Processador Intel i9-14900K", Price = 4399, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/i9_14900k.jpg" }) },
                new Product { Name = "Processador AMD Ryzen 9 7950X", Price = 3899, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/ryzen_9_7950x.jpg" }) },
                new Product { Name = "Placa-Mãe ASUS ROG Strix Z790", Price = 2599, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/asus_rog_z790.jpg" }) },
                new Product { Name = "Placa-Mãe MSI MPG X670E", Price = 2199, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/msi_mpg_x670e.jpg" }) },
                new Product { Name = "Memória RAM Corsair 32GB DDR5", Price = 899, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/corsair_32gb.jpg" }) },
                new Product { Name = "Memória RAM G.Skill Trident Z 32GB", Price = 949, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/gskill_tridentz.jpg" }) },
                new Product { Name = "Fonte Corsair 850W 80+ Gold", Price = 799, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/corsair_850w.jpg" }) },
                new Product { Name = "Gabinete NZXT H710", Price = 1299, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/nzxt_h710.jpg" }) },
                new Product { Name = "Webcam Logitech C920", Price = 599, Category = "Periféricos", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/logitech_c920.jpg" }) },
                new Product { Name = "Microfone Blue Yeti", Price = 999, Category = "Periféricos", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/blue_yeti.jpg" }) },
                new Product { Name = "Cadeira Gamer DXRacer", Price = 1999, Category = "Periféricos", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/dxracer.jpg" }) },
                new Product { Name = "Docking Station USB-C", Price = 699, Category = "Periféricos", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/docking_station.jpg" }) },
                new Product { Name = "Roteador Asus RT-AX88U", Price = 899, Category = "Redes", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/asus_rt_ax88u.jpg" }) },
                new Product { Name = "Switch Cisco SG350", Price = 1799, Category = "Redes", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/cisco_sg350.jpg" }) },
                new Product { Name = "Placa de Captura Elgato HD60", Price = 899, Category = "Hardware", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/elgato_hd60.jpg" }) },
                new Product { Name = "Cabo HDMI 2m", Price = 49, Category = "Periféricos", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/hdmi_2m.jpg" }) },
                new Product { Name = "Hub USB 3.0 4 Portas", Price = 119, Category = "Periféricos", ImagesJson = JsonSerializer.Serialize(new[] { "https://example.com/images/usb_hub_4p.jpg" }) }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
