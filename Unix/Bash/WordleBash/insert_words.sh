API_KEY="<Your API Key>"
URL="https://data.mongodb-api.com/app/data-cflgg/endpoint/data/beta"
CLUSTER="Jac"

read -n 5 -p "Enter five-character word to insert: " WORD

curl --location --request POST  $URL'/action/insertOne' \
  --header 'Content-Type: application/json' \
  --header 'Access-Control-Request-Headers: *' \
  --header 'api-key: '$API_KEY \
  --data-raw '{
    "collection":"words",
    "database":"wordle",
    "dataSource":"'$CLUSTER'",
    "document": { "word": "'$WORD'" }
}'