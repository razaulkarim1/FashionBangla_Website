using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FashionBangla.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, StringLength(30)]
        public string CategoryName { get; set; }

        //Navigation
        public virtual IList<Product> Products { get; set; }
    }
    public class Product
    {
        public int ProductId { get; set; }
        [Required, StringLength(50)]
        public string ProductName { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }
        [StringLength(150)]
        public string PictureFile { get; set; }

        //FK
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        //Navigation
        public virtual Category Category { get; set; }
        public virtual IList<OrderItem> OrderItems { get; set; }
    }
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required, StringLength(120)]
        public string UserId { get; set; }
        [StringLength(60)]
        public string CustomerName { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [CreditCard]
        public string CCNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CCExpire { get; set; }
        //Navigation
        public virtual IList<Order> Orders { get; set; }
    }
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ShippedDate { get; set; }
        //FK
        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        //Navigation
        public virtual Customer Customer { get; set; }
        public virtual IList<OrderItem> OrderItems { get; set; }
    }
    public class OrderItem
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
        //Navligation
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
    public class CartItem
    {
        public int CartItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
    public class FashionDbContext : DbContext
    {
        public FashionDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<FashionDbContext>
    {
        protected override void Seed(FashionDbContext context)
        {
            Category c1 = new Category { CategoryName = "Men" };
            Category c2 = new Category { CategoryName = "Women" };
            Category c3 = new Category { CategoryName = "Kids" };
            Category c4 = new Category { CategoryName = "Home Decor" };
            Category c5 = new Category { CategoryName = "Wedding" };
            context.Categories.AddRange(new Category[] { c1, c2, c3, c4, c5 });
            context.Products.AddRange(new Product[] { 
            new Product { ProductName = "Panjabi", UnitPrice=1957, Description="Red slim fit cotton panjabi with yellow, orange and maroon appliqué and black embroidery.", PictureFile="p1.jpg", CategoryId=1},
            new Product { ProductName = "Panjabi", UnitPrice=1520.70, Description="White slim fit cotton panjabi with yellow, orange and maroon appliqué and black embroidery.", PictureFile="p2.jpg", CategoryId=1},
            new Product { ProductName = "Shirt", UnitPrice= 671.43, Description="Deep blue cotton shirt with grey prints and left pocket.", PictureFile="s3.jpg", CategoryId=1},
            new Product { ProductName = "Shirt", UnitPrice= 802.90, Description="Light White cotton shirt with Orange prints and left pocket.", PictureFile="s1.jpg", CategoryId=1},
            new Product { ProductName = "Saree", UnitPrice= 1209.5, Description="White cotton saree with red, blue, green and yellow prints. Aanchal with mirror detailing and tassel trim. Comes with red tie-dyed unstitched blouse piece.", PictureFile="sh1.jpg", CategoryId=2},
            new Product { ProductName = "Saree", UnitPrice= 1020.80, Description="White cotton saree with red, blue, green and yellow prints. Aanchal with mirror detailing and tassel trim. Comes with red tie-dyed unstitched blouse piece.", PictureFile="sh2.jpg", CategoryId=2},
            new Product { ProductName = "Skirt Top Set", UnitPrice= 450.80, Description="White voile top with red embroidery and lace detailing. Comes with multicolour printed cotton skirt.", PictureFile="kd1.jpg", CategoryId=3},
            new Product { ProductName = "Bed Cover Set", UnitPrice= 1500.50, Description="White cotton bed cover with brown, golden and purple embroidery and appliqué over burgundy panel. Comes with matching pillow covers.", PictureFile="b1.jpg", CategoryId=4},
            new Product { ProductName = "Bed Cover Set", UnitPrice= 1500.50, Description="Gray cotton bed cover with brown, golden and purple embroidery and appliqué over burgundy panel. Comes with matching pillow covers.", PictureFile="b2.jpg", CategoryId=4},
            new Product { ProductName = "Silk Banarasi Sherwani", UnitPrice= 14200.50, Description="The exclusive Sherwani has all over hand embroidery Nakshi kantha embroidery in chevron pattern on yarn dyed silk. This is perfect for groom.", PictureFile="w1.jpg", CategoryId=5},
            new Product { ProductName = "Panjabi", UnitPrice=800.80, Description="Red slim fit cotton panjabi with yellow, orange and maroon appliqué and black embroidery.", PictureFile="p3.jpg", CategoryId=1},
            new Product { ProductName = "Cotton Panjabi", UnitPrice=910.50, Description="White slim fit cotton panjabi with yellow, orange and maroon appliqué and black embroidery.", PictureFile="p4.jpg", CategoryId=1},
            new Product { ProductName = "Cotton Panjabi", UnitPrice=1200.30, Description="Red slim fit cotton panjabi with yellow, orange and maroon appliqué and black embroidery.", PictureFile="p5.jpg", CategoryId=1},
            new Product { ProductName = "Cotton Panjabi", UnitPrice=630.50, Description="White slim fit cotton panjabi with yellow, orange and maroon appliqué and black embroidery.", PictureFile="p6.jpg", CategoryId=1},
            new Product { ProductName = "Cotton Single Kameez", UnitPrice= 1020.50, Description="White cotton single kameez with pink and green prints. Zipper opening on chest and slit opening from waist.", PictureFile="sk1.jpg", CategoryId=2},
            new Product { ProductName = "Cotton  Kameez", UnitPrice= 8070.50, Description="Red cotton single kameez with pink and green prints. Zipper opening on chest and slit opening from waist.", PictureFile="sk2.jpg", CategoryId=2},
            new Product { ProductName = "Cotton  Kameez", UnitPrice= 1290.60, Description="White cotton single kameez with pink and green prints. Zipper opening on chest and slit opening from waist.", PictureFile="sk3.jpg", CategoryId=2},
            new Product { ProductName = "Cotton Skirt Top Sett", UnitPrice= 350.80, Description="Red cotton top with multicolour embroidery. Comes with printed white cotton skirt.", PictureFile="kd2.jpg", CategoryId=3},
            new Product { ProductName = "Cotton Skirt Top Sett", UnitPrice= 350.80, Description="Pink cotton top with multicolour embroidery. Comes with printed white cotton skirt.", PictureFile="kd3.jpg", CategoryId=4}
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}