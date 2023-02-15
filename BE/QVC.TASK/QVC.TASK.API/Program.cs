using QVC.TASK.BL;
using QVC.TASK.DL;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IRegisterBL), typeof(RegisterBL));
builder.Services.AddScoped(typeof(IRegisterDL), typeof(RegisterDL));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Viết hoa chữ cái đầu trong key json
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

//Tắt validate mặc định
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    options.SuppressModelStateInvalidFilter = true
);

// Sửa lỗi CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Lấy chuỗi kết nối database
DatabaseContext.ConnectionDBInfoString = builder.Configuration.GetConnectionString("DBInfo");
DatabaseContext.ConnectionDBDomainString = builder.Configuration.GetConnectionString("DBDomain");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();