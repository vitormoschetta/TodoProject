#!/bin/bash

#kind create cluster --name todo-project

kubectl apply -f db/

kubectl apply -f api/

kubectl apply -f app/

kubectl apply -f external/

sleep 20

kubectl port-forward service/app-service 6002:6002