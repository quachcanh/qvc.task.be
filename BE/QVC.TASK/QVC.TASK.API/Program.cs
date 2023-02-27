using QVC.TASK.BL;
using QVC.TASK.BL.CompanyBL;
using QVC.TASK.DL;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IRegisterBL), typeof(RegisterBL));
builder.Services.AddScoped(typeof(IRegisterDL), typeof(RegisterDL));
builder.Services.AddScoped(typeof(ILoginBL), typeof(LoginBL));
builder.Services.AddScoped(typeof(ILoginDL), typeof(LoginDL));
builder.Services.AddScoped(typeof(IDepartmentBL), typeof(DepartmentBL));
builder.Services.AddScoped(typeof(IDepartmentDL), typeof(DepartmentDL));
builder.Services.AddScoped(typeof(IProjectBL), typeof(ProjectBL));
builder.Services.AddScoped(typeof(IProjectDL), typeof(ProjectDL));
builder.Services.AddScoped(typeof(IEmployeeBL), typeof(EmployeeBL));
builder.Services.AddScoped(typeof(IEmployeeDL), typeof(EmployeeDL));
builder.Services.AddScoped(typeof(IJobBL), typeof(JobBL));
builder.Services.AddScoped(typeof(IJobDL), typeof(JobDL));
builder.Services.AddScoped(typeof(ICompanyBL), typeof(CompanyBL));
builder.Services.AddScoped(typeof(ICompanyDL), typeof(CompanyDL));
builder.Services.AddScoped(typeof(IAssignBL), typeof(AssignBL));
builder.Services.AddScoped(typeof(IAssignDL), typeof(AssignDL));
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