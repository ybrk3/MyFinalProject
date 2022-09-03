using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
