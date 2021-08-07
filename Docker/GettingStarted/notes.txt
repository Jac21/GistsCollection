--clone

 docker run --name repo alpine/git clone https://github.com/docker/getting-started.git

> docker cp repo:/git/getting-started/ .

-- build

> cd getting-started

> docker build -t docker101tutorial .

-- run

> docker run -d -p 80:80 --name docker-tutorial docker101tutorial

-- share

> docker tag docker101tutorial jac21/docker101tutorial

> docker push jac21/docker101tutorial

-- kube

> minikube start

> kubectl get nodes