using WebRazorPage.Data;
using WebRazorPage.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Thêm dbcontext vào dependency injection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Seed dữ liệu
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SeedData(context);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

// Phương thức SeedData
void SeedData(AppDbContext context)
{
    // Kiểm tra nếu không có dữ liệu trong bảng Categories
    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category { CategoryName = "None" },
            new Category { CategoryName = "Apple" },
            new Category { CategoryName = "Android" },
            new Category { CategoryName = "Samsung" }
        );
        context.SaveChanges();
    }
}