# K8s

### Quick Start

Init cluster:
```
sh cluster-init.sh
```

expose `todo.ui.blazor` port:
```
kubectl port-forward service/app-service 6002:6002 
```

expose `todo.rabbitmq` port:
```
kubectl port-forward service/rabbitmq-service 5672:5672
```


<br>


### Cluster
Criar:
```
kind create cluster --name todo-project
```

Excluir:
```
kind delete cluster --name todo-project
```

### Services, Replicasets, Deployments, Pods, etc..
Criar:
```
kubectl apply -f <file>.yaml
```
ou, executar todos .yaml de um mesmo diretório:
```
kubectl apply -f path/
```

Excluir:
```
kubectl delete -f <file>.yaml 
```

Consultar:
```
kubectl get services
kubectl get deployments
kubectl get pods
kubectl get configmaps
kubectl get nodes
```


### Mapear porta de um Service do Cluster para o HOST
```
kubectl port-forward service/app-service 6002:6002
kubectl port-forward service/kibana-service 5601:5601
```
Acessar: 

<http://localhost:6002/>
<http://localhost:5601/>






### Aplicacao UI para gerenciar Kubernetes:

<https://k8slens.dev/>


### Kubernets Lite para Producao:

<https://k3s.io/>