# External Services

Representam serviços fora de nosso domínio. 

Em exemplo anterior (branch `v1.1.0`) tínhamos uma alta acoplação com uma `External.API`. O sucesso de nossas operações dependiam da disponibilidade desse serviço externo. Não faz sentido manter esse tipo de dependência à um serviço que não faz parte de nosso domínio.

O que fizemos foi adicionar um serviço de fila (RabbitMQ), e agora esse serviço externo, representado pelo `External.Consumer` consome os eventos que estamos enviando para a fila do RabbitMQ.


### Executar

Trata-se de um Console Application, basta entrar no diretório e executar:

```
dotnet run
``` 

A medida que nossa aplicação TodoApp realiza as operações de domínio, eventos são gerados e podemos ver esses eventos no console deste consumidor.