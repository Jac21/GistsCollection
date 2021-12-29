# Kubernetes set-up and usage

> kubectl apply -f pod.yaml

> kubectl get pods

> kubectl logs [pod-name]

> kubectl delete -f pod.yaml

# Deployments

> kubectl apply -f bb.yaml

> kubectl get deployments

> kubectl get services

> kubectl delete -f bb.yaml

# Docker Swarm set-up and usage

> docker swarm init

> docker service create --name demo alpine:latest ping 8.8.8.8

> docker service ps demo

> docker servie logs demo

> docker service rm demo