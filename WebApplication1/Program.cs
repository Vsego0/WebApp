using WebApplication1.Controllers.UtilityControllers;
using WebApplication1.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// �������� ������ ����������� �� ����� ������������
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.Add(new ServiceDescriptor(typeof(PlantContext), new PlantContext(connection)));
builder.Services.Add(new ServiceDescriptor(typeof(QuestionContext), new QuestionContext(connection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Plant}/{action=Index}/{id?}");

QuestionStack.Init();

QuestionList.Init(QuestionList.Count());
QuestionList.FillList();

SearchStringBuilder.Init();

PlantList.Init();

app.Run();