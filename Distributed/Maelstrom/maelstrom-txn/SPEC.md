# maelstrom-kafka

## "txn" message from Maelstrom -

```json
{
  "type": "txn",
  "msg_id": 3,
  "txn": [
    ["r", 1, null],
    ["w", 1, 6],
    ["w", 2, 9]
  ]
}
```

## "txn_ok" message reply -

```json
{
  "type": "txn_ok",
  "msg_id": 1,
  "in_reply_to": 3,
  "txn": [
    ["r", 1, 3],
    ["w", 1, 6],
    ["w", 2, 9]
  ]
}
```
