# maelstrom-broadcast

## RPC 'add' message from Maelstrom -

```json
{
  "type": "add",
  "delta": 123
}
```

## Message reply -

```json
{
  "type": "add_ok"
}
```

## RPC 'read' message from Maelstrom -

```json
{
  "type": "read"
}
```

## Message reply with list of values node has seen -

```json
{
  "type": "read_ok",
  "value": 1234
}
```
