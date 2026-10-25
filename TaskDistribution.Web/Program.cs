using Microsoft.EntityFrameworkCore;
using TaskDistribution.Application.Abstractions;
using TaskDistribution.Application.Features.OperationType;
using TaskDistribution.Application.Features.Personnel;
using TaskDistribution.Application.Features.TaskDistribution;
using TaskDistribution.Application.Features.WorkTask;
using TaskDistribution.Infrastructure.Persistence;
using TaskDistribution.Infrastructure.Repositories;
using TaskDistribution.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection")));

//Personnel
builder.Services.AddScoped<IPersonnelRepository,PersonnelRepository>();
builder.Services.AddScoped<PersonnelService>();

//Operation
builder.Services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
builder.Services.AddScoped<OperationTypeSeed>();
builder.Services.AddScoped<OperationTypeService>();

//Worktask
builder.Services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
builder.Services.AddScoped<WorkTaskService>();

//TaskDistribution
builder.Services.AddScoped<TaskDistributionService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var operationTypeSeed = scope.ServiceProvider
		.GetRequiredService<OperationTypeSeed>();

	await operationTypeSeed.SeedAsync();
}

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
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
