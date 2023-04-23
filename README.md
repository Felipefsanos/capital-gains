# Capital Gains

- [Observações]()
- [Requisitos](#requisitos)
- [Como executar](#como-executar)
- [Como executar os testes](#como-executar-os-testes)

## Observações

- As finalidade das interfaces criadas no projeto foram para facilitar o uso de testes mockados.
- Foi usada a biblioteca [Moq](https://github.com/moq/moq4) para realizar o Mock dos services nos testes de unidade.
- Tentei deixar a arquitetura do projeto bem simples, não achei necessário incluir injeção de depndência pois seria uma complexidade desnecessária, um handler para controlar as operações pensando que possa surgir novas, e foi separada a responsabilidade de compra e venda em dois serviços distintos.

## Requisitos 

- [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
## Como executar
### Docker

Para executar utilizando o docker, basta executar os seguintes comandos:

**Considerando que voc^esteja no diretório onde se encontra o Dockerfile e o .csproj**

`docker build -t capital-gains .`

Onde "capital-gains" pode ser um nome de seu agrado. Esse comando vai construir a imagem docker.

`docker run -i capital-gains`

Para executar o container em modo interativo, para que possa ser informada as linhas das operações. Tanbé é possível passsar um arquivo txt com as linhas:

`docker run -i capital-gains < input.txt`

### Visual Studio

- Abra a solution e execute o projeto com a opção "Execute" ou "Executar"

### Visual Studio Code

Abra a pasta `capital-gains`

Execute o comando `dotnet run --project ./capital-gains/capital-gains.csproj`

## Como executar os testes

Para executar os testes do projeto para usar o comando:

`dotnet test`