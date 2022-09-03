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
//Autofac kullan�l�ca�� bildiriliyor;;
//AutofacServiceProviderFactory <== Autofac.Extensions.DependencyInjection package
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});
//----------------------------------

//Note; Aspect Oriented Programming -- Bir method'un �n�nde-sonunda-hata verdi�inde �al��an kod par�ac�klar� -> A.K.A. [Loglama]


//Arka planda bir referans olu�turma. IoC kendisi new'liyor
//IProductservice ba��ml�l��� oldu�unda ProductManager'i new'ler/olu�turur. 
//Singleton i�erisinde data tutmuyorsak kullan�l�r
//builder.Services.AddSingleton<IProductService,ProductManager>();

////ProductManager new'lenirken IProductDal(Hangi data base ba�lanma y�ntemi isteniyorsa ona g�re) parametresini new'liyor
////O y�zden EntityFramework kullan�laca�� i�in EfProductdal AddSingleton i�in girilir
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
