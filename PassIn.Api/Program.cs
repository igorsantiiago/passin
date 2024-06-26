using PassIn.Api.Filters;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Repositories.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddDbContext<PassInDbContext>();
builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IAttendeesRepository, AttendeesRepository>();
builder.Services.AddScoped<ICheckInRepository, CheckInRepository>();

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
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
