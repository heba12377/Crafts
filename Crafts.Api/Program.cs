using Crafts.BL.Managers.CartItemsManager;
using Crafts.BL.Managers.CartManager;
using Crafts.BL.Managers.CategoryManagers;
using Crafts.BL.Managers.CouponManager;
using Crafts.BL.Managers.ProductManager;
using Crafts.BL.Managers.ReviewManagers;
using Crafts.BL.Managers.Services;
using Crafts.BL.Managers.WishListManager;
using Crafts.DAL.Context;
using Crafts.DAL.Models;
using Crafts.DAL.Models.Enum;
using Crafts.DAL.Repos.CartItemsRepo;
using Crafts.DAL.Repos.CartRepo;
using Crafts.DAL.Repos.CategoryRepo;
using Crafts.DAL.Repos.CouponRepo;
using Crafts.DAL.Repos.IdentityRepo;
using Crafts.DAL.Repos.ProductsRepo;
using Crafts.DAL.Repos.ReviewRepo;
using Crafts.DAL.Repos.WishListRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database
var connectionString = builder.Configuration.GetConnectionString("CraftsDB");
builder.Services.AddDbContext<CraftsContext>(options =>
options.UseSqlServer(connectionString));
#endregion

#region Identity Manager
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.User.RequireUniqueEmail = true;
}
)
    .AddEntityFrameworkStores<CraftsContext>();
#endregion

#region Authentication
//verify token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Cool";
    options.DefaultChallengeScheme = "Cool";
})
    .AddJwtBearer(
    "Cool", options =>
    {
        var secertKeyString = builder.Configuration.GetValue<string>("SecretKey") ?? "";
        var secretKeyInBytes = Encoding.ASCII.GetBytes(secertKeyString);
        var securityKey = new SymmetricSecurityKey(secretKeyInBytes);

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = securityKey,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }

    );
#endregion

#region Authorization
builder.Services.AddAuthorization(Options =>
{
    Options.AddPolicy("AllowAdminsOnly",
        policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));

    Options.AddPolicy("AllowUsersOnly",
        builder => builder.RequireClaim(ClaimTypes.Role, "User"));
});
#endregion

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AllowAdminsOnly", policy => policy.RequireRole(Role.Admin.ToString()));
//});


#region CorsPolicy
var AllowCorsPolicy = "AllowCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowCorsPolicy, builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
#endregion

#region Repos

builder.Services.AddScoped<ICouponRepo, CouponRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
builder.Services.AddScoped<IProductRepo,ProductRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IWishListRepo, WishListRepo>();
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICartItemRepo, CartItemRepo>();




#endregion

#region Managers

builder.Services.AddScoped<ICouponsManager, CouponsManager>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<IReviewManager, ReviewManager>();
builder.Services.AddScoped<IProductsManager,ProductsManager>();
builder.Services.AddScoped<IWishListManager, WishListManager>();
builder.Services.AddScoped<ICartManager, CartManager>();
builder.Services.AddScoped<ICartItemsManager, CartItemsManager>();



#endregion

#region Services

builder.Services.AddScoped<IFileService, FileService>();

#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
