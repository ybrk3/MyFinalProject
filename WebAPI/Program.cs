using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.Extensions;
using Core.Utilities.IOC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(); //Front'dan erişmek için

//API'ye jwt kullanýlacaðý bildiriliyor

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

//IServiceCollectionExtension;
builder.Services.AddDependencyResolvers(new ICoreModule[] {
    new CoreModule() });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-----------------------------------
//Autofac kullanýlýcaðý bildiriliyor;;
//AutofacServiceProviderFactory <== Autofac.Extensions.DependencyInjection package
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});
//----------------------------------

//Note; Aspect Oriented Programming -- Bir method'un önünde-sonunda-hata verdiðinde çalýþan kod parçacýklarý -> A.K.A. [Loglama]


//Arka planda bir referans oluþturma. IoC kendisi new'liyor
//IProductservice baðýmlýlýðý olduðunda ProductManager'i new'ler/oluþturur. 
//Singleton içerisinde data tutmuyorsak kullanýlýr
//builder.Services.AddSingleton<IProductService,ProductManager>();

////ProductManager new'lenirken IProductDal(Hangi data base baðlanma yöntemi isteniyorsa ona göre) parametresini new'liyor
////O yüzden EntityFramework kullanýlacaðý için EfProductdal AddSingleton için girilir
//builder.Services.AddSingleton<IProductDal, EfProductDal>();
//-------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
 
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
