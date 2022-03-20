## Passos necessários para publicar essa aplicaçao no Amazon Web Service (AWS)


## Criar Security Group
Liberar as seguintes regras de entrada (inbound rules):
- SSH
- HTTP


## Criar uma instância RDS MySql



## Criar uma instância EC2 Ubuntu para API
Atribuir Secuciry Group criado anteriormente.


#### Validar conexão com base de dados no RDS
Acessar o EC2 via SSH e: 

Instalar MySql Client:
```
apt-get install mysql-client
```

Verificar conexão com RDS:
```
mysql -u <rds_user> -h <...rds.amazonaws.com> -p<rds_password>
```


#### Instalar Docker no EC2

<https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-20-04-pt>


##### Fazer login na conta DockerHub que contém as imagens necessárias
```
docker login
```
Informar username e password.




#### Executar Container
Necessário ter um arquivo `.env` com as informações de conexão com o banco de dados, seguindo o padrão do arquivo que está neste projeto.
```
docker run -d --name todoapi --restart=always --env-file=./.env -p 80:6001 --log-opt max-size=100m --log-opt max-file=50 vitormoschetta/todoapi:latest
```



## Criar uma instância EC2 Ubuntu para APP

#### Instalar Docker no EC2

<https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-20-04-pt>


#### Fazer login na conta DockerHub que contém as imagens necessárias
```
docker login
```
Informar username e password.

#### Executar Container
```
docker run -d --name todoapp --restart=always -e API_URL_CONNECTION=http://<ip_publico_ec2_api>/api/ -p 80:6002 --log-opt max-size=100m --log-opt max-file=50 vitormoschetta/todoapp:latest
```
