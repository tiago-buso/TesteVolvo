# TesteVolvo

Alterar a chave: DefaultConnection de appsettings.json para a configuração do SQL Server do seu computador.

"Server=PC-TIAGO;Initial Catalog=TesteVolvo;Trusted_Connection=True;MultipleActiveResultSets=true;"

No meu exemplo, PC-TIAGO é o nome do meu servidor de SQL Server. Initial Catalog é o nome do banco de dados.

No meu exemplo, meu SQL server é autenticado com Windows Authentication, sendo assim, não preciso colocar essas informações na chave de configuração.

Não esquecer também de executar os migrations: 

Para isso:

1 - executar o Visual Studio como como administrador
2 - acessar o Nuget Package Manager (Tools > Nuget Package Manager > Package Manager Console)
3 - no campo Default Project, selecionar o projeto TesteVolvo
4 - executar o comando Update-Database
