#!/bin/bash


echo "************************************* Creating Cluster ************************************"
kind create cluster --name todo-project



echo "**************************** Creating Elasticsearch and Kibana ****************************"
kubectl apply -f elasticsearch/

sleep 5

kubectl apply -f kibana/



echo "************************************ Creating Application **********************************"

kubectl apply -f db/

sleep 5

kubectl apply -f api/

kubectl apply -f app/

kubectl apply -f external/
