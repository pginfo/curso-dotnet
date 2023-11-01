# Entity framework core

[Referência de providers de banco de dados: ](https://docs.microsoft.com/pt-br/ef/core/providers/)

Entity Framework Core foi escrito do zero e possui melhor performance.

**Criar um projeto a partir da linha de comando**

Criando a solution `CursoEntityFrameworkCore`.

~~~
dotnet new sln -n CursoEntityFrameworkCore
~~~

Criando o projeto com a respectiva versão do framework.

~~~
dotnet new console -n CursoEFCore -o Curso -f net5.0
~~~

- (-n) : nome do projeto.
- (-o) : diretório onde o projeto deverá ser criado.
- (-f) : indica qual framework será utilizado no projeto

Adicionar o projeto a solution criada anteriormente.

~~~
dotnet sln CursoEntityFrameworkCore.sln add Curso\CursoEFCore.csproj
~~~

Instalar o Entity Framework via cli no Visual Studio Code.

~~~
dotnet add Curso\CursoEFCore.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.5
~~~

Instalar o Entity Framework via cli no Visual Studio.

~~~
PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer - Version 3.1.5
~~~

## O que é DbContext

Em poucas palavras podemos definir como uma combinação dos padrões UoW (Unit-of-Work) e Repository, que contém um conjunto de métodos responsáveis por gravar e ler informações do banco de dados. <br>
O DbContext é a classe principal e mais importante que você terá acesso, o objetivo dela é simplificar a interação d sua aplicação com seu banco de dados.

- **OnConfiguring:** é usado para informar qual provider será utilizado e informar a string de conexão, logger, serviços customizados e outros.
- **OnModelCreating:** é usado para configurar todo o modelo de dados que serão posteriormente transformados em tabelas e comandos SQL's.
- **SaveChanges:** método responsável por coletar os dados que sofreram alterações e persistir no banco de dados.

**Referência para strings de conexão aos bancos de dados**
<br>
https://www.connectionstrings.com

## Para usar as migrations

Para gerar é necessário instalar o pacote `Microsoft.EntityFrameworkCore.Design`.

<br>

Para executar os comandos que vão gerar as migrações : `Microsoft.EntityFrameworkCore.Tools`.

<br>

Ou ainda instalar o Dotnet EF CLI : Global Tool Entity Framework Core.

~~~
dotnet tool install --global dotnet-ef --version 3.1.5
~~~

## Criar as Migrações 

A partir do terminal

~~~
dotnet ef migrations add PrimeiraMigracao -p .\CursoEFCore.csproj
~~~

Criar um script SQL a partir da migração, de forma a fornecer esse script a algum DBA.

~~~
dotnet ef migrations script -p .\Curso\CursoEFCore.csproj -o .\Curso\PrimeiraMigracao.SQL
~~~

Executando a migração para refletir as configurações no banco de dados.

~~~
dotnet ef database update -p .\CursoEFCore.csproj -v
~~~

## Gerando scripts SQL Idempotentes

Cria as migrações de forma que não ocorram erros se elas forem usadas repetidas vezes. Gerando validações adicionais. 

~~~
dotnet ef migrations script -p .\Curso\CursoEFCore.csproj -o .\Curso\Idempotente.SQL -i
~~~

## Rollback de migrações

Após incluir a propriedade email na Classe Cliente, executar o seguinte comando.

~~~
dotnet ef migrations add AdicionarEmail -p .\Curso\CursoEFCore.csproj
~~~

Desfazer a migração. Informar o nome da migração que irá refletir no banco de dados.

~~~
dotnet ef database update PrimeiraMigracao -p .\Curso\CursoEFCore.csproj -v
~~~

Excluir a última migração para que as configurações contidas nela não sejam aplicadas ao executar outras migrations.

~~~
dotnet ef migrations remove -p .\Curso\CursoEFCore.csproj -v
~~~

## Migrações Pendentes

Para verificar se há migrações pendentes, de forma que toda a estrutura esteja aplicada ao banco de dados. Usar o arquivo `program.cs` para fazer essa verificação.

~~~
using System;
using System.Linq;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
			/*
			 * Techo de código para verificar migração Pendentes
			 */
			 var existe = db.Database.GetPendingMigrations().Any();
			 
			 if (existe)
			 {
				// Executa alguma coisa ou notificação.
			 }
		
            Console.WriteLine("Hello World!");
        }
    }
}
~~~

## Inserindo registros em massa

Pra essa etapa será instalado um pacote de log, fazendo com que todas as intruções geradas pelo framework sejam exibidas no terminal.

~~~
dotnet add package Microsoft.Extensions.Logging.Console --version 3.1.5
~~~

Após instalado, configurar a classe `ApplicationContext.cs`.

## Carregamento dos registros

- **Carregamento adiantado** o que significa que os dados relacionados são carregdos do banco de dados em uma única consulta.
- **Carregamento explícito** significa que os dados relacionados são explicitamente carregados do banco de dados em um momento posterior.
- **Carregamento lento** significa que os dados relacionados são carregados sob demanda do banco de dados quando a propriedade de navegação é acessada.

