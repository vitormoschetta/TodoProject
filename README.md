# TodoProject

## Quick start App
```
docker-compose up -d --build ui.blazor
```

Access `Todo.UI.Blazor`: <http://0.0.0.0:6002/>

Access `Todo.Api`: <http://0.0.0.0:6001/>



<br>



## quick start RabbitMQ

<https://www.rabbitmq.com/getstarted.html>

<https://github.com/vitormoschetta/RabbitMQ>



<br>



## quick start Elastic
```
docker-compose up -d kibana
```

Access Kabana: <http://0.0.0.0:5601/>


#### Enable filebeat to send logs

No `filebeat.yml` setamos o path para leitura de logs de todos os arquivos de logs do Docker:
```
/var/lib/docker/containers/*/*.log
```

Change permissions:
```
sudo chown root ./infra/docker/filebeat/filebeat.yml
sudo chmod go-w ./infra/docker/filebeat/filebeat.yml
```

Container Up:
```
docker-compose up -d filebeat
```

Quando o filebeat está lendo logs do container docker, é possível, no Kibana, filtrar os logos pelo nome do container:
```
container.name : todo.api
```