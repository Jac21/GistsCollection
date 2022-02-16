API_KEY="<Your API Key>"
URL="https://data.mongodb-api.com/app/data-cflgg/endpoint/data/beta"
CLUSTER="Jac"

curl --location --request POST  $URL'/action/insertMany' \
  --header 'Content-Type: application/json' \
  --header 'Access-Control-Request-Headers: *' \
  --header 'api-key: '$API_KEY \
  --data-raw '{
    "collection":"words",
    "database":"wordle",
    "dataSource":"'$CLUSTER'",
    "documents": '$(curl -s https://raw.githubusercontent.com/mongodb-developer/bash-wordle/main/words.json)'
}'