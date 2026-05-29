docker build . -t emersonhzulian/simple-dotnet-api:latest
docker push emersonhzulian/simple-dotnet-api:latest

k port-forward svc/api 80


kubectl patch svc envoy-default-gateway-api-30a1473e -n envoy-gateway-system -p '{"spec":{"type":"NodePort","ports":[{"port":80,"protocol":"TCP","targetPort":8080,"nodePort":32000}]}}'


### Push the image to the cluster:
docker build -t dotnet-api:latest -f apps/dotnet-api/dockerfile apps/dotnet-api
kind load docker-image dotnet-api:latest --name portifolio-cluster