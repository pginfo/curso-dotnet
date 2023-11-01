# Curso ASP.NET

### Global JSON
Arquivo configura a versão do sdk que o projeto deve ser compilado. Deve ser colocado na pasta raiz.

~~~json
{
	"projects": ["src"],
	"sdk": {
		"version": "^6.0.201"
	}
}
~~~

### .NET STANDARD

É similar a uma interface onde podemos verificar se determinado recurso existe em uma versão específica. A última versão é a 2.1.

**Criar novo projeto web**

~~~shell
$ dotnet new web -n NomeDoProjeto
~~~

**Remover o certificado de desenvolvimento da máquina**

~~~shell
$ dotnet dev-certs https --clean
~~~

**Instalação do certificado no servidor**

~~~shell
$ dotnet dev-certs https
~~~

**Checar o status do certificado**

Neste ponto o certificado foi instalado mas não é confiável.

~~~shell
$ dotnet dev-certs https --check --trust
The following certificates were found, but none of them is trusted: 1 certificate
    1) C056C462C64D1001C5DF5F770FCF415982A9AE17 - CN=localhost - Valid from 2023-10-18 16:59:29Z to 2024-10-17 16:59:29Z - IsHttpsDevelopmentCertificate: true - IsExportable: true
~~~

**Instalação do certificado e aceitando a relação de confiança.**

~~~shell
$ dotnet dev-certs https --trust
Trusting the HTTPS development certificate was requested. A confirmation prompt will be displayed if the certificate was not previously trusted. Click yes on the prompt to trust the certificate.
A valid HTTPS certificate is already present.
~~~

**Checando novamente o status do certificado**

~~~shell
$ dotnet dev-certs https --check --trust
A trusted certificate was found: C056C462C64D1001C5DF5F770FCF415982A9AE17 - CN=localhost - Valid from 2023-10-18 16:59:29Z to 2024-10-17 16:59:29Z - IsHttpsDevelopmentCertificate: true - IsExportable: true
~~~

**Trabalhar com banco de dados**

Precisa instalar o pacote `Microsoft.EntityFrameworkCore`.

**Pipeline ASP.NET**

~~~
-> ExceptionHandler 
-> HSTS (verifica se existe protocolo ssl, se sim, direciona para https)
-> HttpsRedirection
-> Static Files (envolve os arquivos .css e .js quando necessário)
-> Routing (direciona para o endpoint solicitado)
-> CORS	(verifica se o cliente pode acessar o endpoint)
-> Authentication
-> Authorization
-> Custom Middlewares
-> Endpoint
~~~

**Program.cs**
<br>
É a classe responsável por configurar todas as responsabilidades e comportamentos da aplicação.

**WebApplicationBuilder**
<br>
Classe que possui a responsabilidade de construir uma instância de aplicação ASP.NET, com todas as configurações definidas.

~~~
var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => 
	options.UseMysqlServer(cs))
	
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(
	options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
	
builder.Services.AddControllersWithViews();

var app = builder.Build();
~~~

**WebApplication**
<br>
A instância que representa a aplicação e todas as configurações dos middlewares em relação aos seus comportamentos durante um request.

~~~
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Routing();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "defaut", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
~~~

**Criar estruta MVC via cli**
<br>
~~~
dotnet new mvc -n DemoMVC --auth Individual
~~~

