#### Comando para gerar certificado HTTPs:
```
dotnet dev-certs https -ep ./Infra/docker/https/certificate.pfx -p Pass123
```

Passar para a imagem do container por variáveis de ambiente:
``` 
ASPNETCORE_Kestrel__Certificates__Default__Password: ${ASPNETCORE_Kestrel__Certificates__Default__Password}
ASPNETCORE_Kestrel__Certificates__Default__Path: ${ASPNETCORE_Kestrel__Certificates__Default__Path}
```

Criar volume como ponte para o certificado gerado:
```
volumes:
    - ./Infra/docker/https:/https:ro
```

ou na linha de comando:
```
docker run ... -v <certificate_path>:/https:ro
```

#### Mais sobre certificados HTTPs:

<https://docs.microsoft.com/pt-br/aspnet/core/security/docker-compose-https?view=aspnetcore-5.0>

