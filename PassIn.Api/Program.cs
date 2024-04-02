using PassIn.Api.Filters;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Repositories.UseCases;
using PassIn.Infrastructure.Repositories.UseCases.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<PassInDbContext>();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IAttendeesRepository, AttendeesRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
