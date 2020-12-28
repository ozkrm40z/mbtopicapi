steps=4
currentStep=1

echo step $currentStep/$steps. Building and publishing api
dotnet publish ./Eventbus.Api/Eventbus.Api.csproj -c Release -o ./publish
((currentStep++))

echo step $currentStep/$steps. Tear down docker compose
docker-compose down
((currentStep++))

echo step $currentStep/$steps. Build api image 
docker build -t eventbusapi:dev .
((currentStep++))

echo step $currentStep/$steps. Compose up
docker-compose up -d