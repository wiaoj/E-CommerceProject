using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business;
using Business.DependencyResolvers.Autofac;
using Business.Helpers;
using Core.Extensions.ExceptionMiddleware;
using Core.Utilities.Messages;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => {
	builder.RegisterModule(new AutofacBusinessModule());
});


//builder.Services.AddDataAccessServices();
builder.Services.AddBusinessServices();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
	policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

var tokenOptions = builder.Configuration.GetSection(TokenOptions.ConfigurationName).Get<TokenOptions>();

//builder.Services.AddAutoMapper(typeof(AutofacBusinessModule));
//builder.Services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
	options.TokenValidationParameters = new TokenValidationParameters {
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidIssuer = tokenOptions.Issuer,
		ValidAudience = tokenOptions.Audience,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
		ClockSkew = TimeSpan.Zero
	};
});

//ServiceTool.ServiceProvider = app.Services.ApplicationServices;



builder.Services.AddControllers()
//    .AddJsonOptions(options => {
//    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
//    options.JsonSerializerOptions.IgnoreNullValues = true;
//})
;


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
	options.SwaggerDoc("v1", new OpenApiInfo {
		Version = SwaggerMessages.Version,
		Title = SwaggerMessages.Title,
		Description = SwaggerMessages.Description,
		TermsOfService = SwaggerMessages.TermsOfService,
		Contact = new OpenApiContact {
			Name = SwaggerMessages.ContactName,
			Email = SwaggerMessages.ContactEMail,
			Url = SwaggerMessages.ContactUrl
		},
		License = new OpenApiLicense {
			Name = SwaggerMessages.LicenceName,
			Url = SwaggerMessages.LicenceUrl
		},
	});
	//WebAAPI.csproj add <GenerateDocumentationFile>true</GenerateDocumentationFile>
	var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

	////options.OperationFilter<AddAuthHeaderOperationFilter>();
	//options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme {
	//    Description = "`Token only!!!` - without `Bearer_` prefix",
	//    Type = SecuritySchemeType.Http,
	//    BearerFormat = "JWT",
	//    In = ParameterLocation.Header,
	//    Scheme = "bearer"
	//});
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI(UIOptions => {
		UIOptions.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
	});
}

app.ConfigureCustomExceptionMiddleware();
//app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());


//app.UseDbOperationClaimCreator();//TODO: Fix it, dont't work

app.UseHttpsRedirection();

app.UseStaticFiles(); //wwwroot a eriþmek için kullanýyoruz

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
